using DmmmPortal.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DmmmPortal.Models.Entities;
using System.Threading.Tasks;
using DmmmPortal.Models;
using System.Data.Entity;
using DmmmPortal.Models.Dtos;
using System.Drawing;

namespace DmmmPortal.Areas.DataService.Services
{
    public class PostService : IPostService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task New(Post models, IEnumerable<HttpPostedFileBase> upload)
        {

            models.PostedBy = "Admin";
            models.IsPublished = true;
            models.DatePosted = DateTime.UtcNow.AddHours(1);
            db.Posts.Add(models);
            await db.SaveChangesAsync();
            //byte[] upload;
            var checkimg = upload.Count(a => a != null && a.ContentLength > 0);
            {
                if (upload.Count() >= 1)
                {


                    foreach (var upload1 in upload)
                    {
                        if (upload1 != null && upload1.ContentLength > 0)
                        {

                            ImageModel model = new ImageModel();
                            // Find its length and convert it to byte array
                            int ContentLength = upload1.ContentLength;

                            // Create Byte Array
                            byte[] bytImg = new byte[ContentLength];

                            // Read Uploaded file in Byte Array
                            upload1.InputStream.Read(bytImg, 0, ContentLength);
                           
                            model.ImageContent = bytImg;
                            
                            model.ContentType = upload1.ContentType;
                            model.FileName = upload1.FileName;

                            model.PostId = models.Id;
                            db.ImageModel.Add(model);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        public async Task<Post> Get(int? id)
        {
            var post = await db.Posts.Include(x => x.ImageModels).FirstOrDefaultAsync(x=>x.Id == id);
            return post;
        }

        public async Task Edit(Post models, IEnumerable<HttpPostedFileBase> upload)
        {
            var checkimg = upload.Count(a => a != null && a.ContentLength > 0);
            {
                if (upload.Count() != 0)
                {


                    foreach (var upload1 in upload)
                    {
                        if (upload1 != null && upload1.ContentLength > 0)
                        {
                            var photo = db.ImageModel.Where(x => x.PostId == models.Id);
                            int imga = photo.Count();
                            if (photo != null)
                            {
                                foreach (var img in photo)
                                {
                                    db.ImageModel.Remove(img);
                                }

                                await db.SaveChangesAsync();
                            }

                            // Find its length and convert it to byte array
                            int ContentLength = upload1.ContentLength;

                            // Create Byte Array
                            byte[] bytImg = new byte[ContentLength];

                            // Read Uploaded file in Byte Array
                            upload1.InputStream.Read(bytImg, 0, ContentLength);
                            ImageModel model = new ImageModel();
                            model.ImageContent = bytImg;
                           
                            model.ContentType = upload1.ContentType;
                            model.FileName = upload1.FileName;

                            model.PostId = models.Id;
                            db.ImageModel.Add(model);
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
            //the more photo section

        }

        public async Task Delete(int? id)
        {
            Post models = await db.Posts.FindAsync(id);
            var imgdel = db.ImageModel.Where(x => x.PostId == id);
            foreach (var i in imgdel)
            {
                var img = db.ImageModel.Find(i.Id);
                db.ImageModel.Remove(img);
               
            }
            db.SaveChanges();
            db.Posts.Remove(models);
            db.SaveChanges();
        }

        public async Task<Post> Details(int? id)
        {
            var post = db.Posts.Include(x=>x.ImageModels).FirstOrDefaultAsync(x => x.Id == id);
            return await post;
        }



        public async Task<List<Post>> Posts(string searchString, string currentFilter, int? page)
        {
            var items = from item in db.Posts.Include(x => x.ImageModels).OrderByDescending(x=>x.DatePosted)
            select item;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper())
                    || p.Content.ToUpper().Contains(searchString.ToUpper())
                   
                    );
            }
            return await items.ToListAsync();
           
        }


        public async Task PublishUnpublish(int id)
        {
            Post models = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (models.IsPublished == false)
            {
                models.IsPublished = true;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            else
            {
                models.IsPublished = false;
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }


    }
}
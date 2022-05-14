using DmmmPortal.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DmmmPortal.Models.Entities;
using System.Threading.Tasks;

using DmmmPortal.Models;
using System.Data.Entity;

namespace DmmmPortal.Areas.Data.Services
{
    public class HallOfFameService : IHallOfFameService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task Create(Models.Entities.HallOfFame model, HttpPostedFileBase upload)
        {
            
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                model.Image = bytImg;
                

            }
            model.DateCreated = DateTime.UtcNow.AddHours(1);
            db.HallOfFames.Add(model);
            await db.SaveChangesAsync();
           
        }

        public async Task Delete(int? id)
        {
            var fame = await db.HallOfFames.FirstOrDefaultAsync(x => x.Id == id);
            if (fame != null)
            {
                db.HallOfFames.Remove(fame);
                await db.SaveChangesAsync();
            }

        }

        public async Task Edit(Models.Entities.HallOfFame models, HttpPostedFileBase upload)
        {

                if (upload != null && upload.ContentLength > 0)
                {


                    // Find its length and convert it to byte array
                    int ContentLength = upload.ContentLength;

                    // Create Byte Array
                    byte[] bytImg = new byte[ContentLength];

                    // Read Uploaded file in Byte Array
                    upload.InputStream.Read(bytImg, 0, ContentLength);

                    models.Image = bytImg;
                   
                }
                db.Entry(models).State = EntityState.Modified;
                await db.SaveChangesAsync();
            
        }

        public async Task<Models.Entities.HallOfFame> Get(int? id)
        {
            var fame = await db.HallOfFames.FirstOrDefaultAsync(x => x.Id == id);
            return fame;
        }

        public async Task<List<Models.Entities.HallOfFame>> List()
        {
            var fame = await db.HallOfFames.OrderBy(x=>x.SortOrder).ToListAsync();
            return fame;
        }
    }
}
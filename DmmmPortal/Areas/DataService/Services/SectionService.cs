using DmmmPortal.Areas.DataService.IServices;
using DmmmPortal.Models;
using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace DmmmPortal.Areas.DataService.Services
{
    public class SectionService : ISectionService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task Create(Section model)
        {
            model.DateCreated = DateTime.UtcNow.AddHours(1);
            db.Sections.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task CreateSubSection(Subsection model, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                model.SchoolLogo = bytImg;
               

            }
            model.DateCreated = DateTime.UtcNow.AddHours(1);
            db.Subsections.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Sections.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.Sections.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteSubSection(int? id)
        {
            var item = await db.Subsections.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.Subsections.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Edit(Section models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task EditSubSection(Subsection models, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {


                // Find its length and convert it to byte array
                int ContentLength = upload.ContentLength;

                // Create Byte Array
                byte[] bytImg = new byte[ContentLength];

                // Read Uploaded file in Byte Array
                upload.InputStream.Read(bytImg, 0, ContentLength);

                models.SchoolLogo = bytImg;


            }
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Section> Get(int? id)
        {
            var item = await db.Sections.Include(x => x.Subsections).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<Subsection> GetSubSection(int? id)
        {
            var item = await db.Subsections.Include(x => x.Section).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<Section>> List()
        {
            var item = await db.Sections.Include(x => x.Subsections).ToListAsync();
            return item;
        }

        public async Task<List<Subsection>> ListSubSection()
        {
            var item = await db.Subsections.Include(x => x.Section).ToListAsync();
            return item;
        }

        public async Task<List<Subsection>> ListSubSectionBySectionId(int id)
        {
            var item = await db.Subsections.Include(x => x.Section).Where(x=>x.SectionId == id).ToListAsync();
            return item;
        }
    }
}
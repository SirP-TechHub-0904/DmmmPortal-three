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
    public class CategoryService : ICategoryService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task Create(Category model)
        {
            model.DateCreated = DateTime.UtcNow.AddHours(1);
            db.Categories.Add(model);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var item = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                db.Categories.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task Edit(Category models)
        {
            db.Entry(models).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<Category> Get(int? id)
        {
            var item = await db.Categories.Include(x=>x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<List<Category>> List()
        {
            var item = await db.Categories.Include(x => x.Posts).ToListAsync();
            return item;
        }
    }
}
using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmmmPortal.Areas.DataService.IServices
{
    interface ICategoryService
    {
        Task Create(Category model);
        Task<Category> Get(int? id);

        Task Edit(Category models);
        Task Delete(int? id);
        Task<List<Category>> List();
    }
}

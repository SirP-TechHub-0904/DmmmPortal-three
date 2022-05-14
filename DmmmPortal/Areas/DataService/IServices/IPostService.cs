using DmmmPortal.Models.Dtos;
using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DmmmPortal.Areas.Data.IServices
{
    interface IPostService
    {
        Task New(Post models, IEnumerable<HttpPostedFileBase> upload);
       
        Task<Post> Get(int? id);
        
        Task Edit(Post models, IEnumerable<HttpPostedFileBase> upload);
       
        Task Delete(int? id);
      
        Task<Post> Details(int? id);
       
        Task<List<Post>> Posts(string searchString, string currentFilter, int? page);
        
        Task PublishUnpublish(int id);
       

            }
}

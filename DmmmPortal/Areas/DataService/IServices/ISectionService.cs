using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DmmmPortal.Areas.DataService.IServices
{
    interface ISectionService
    {
        Task Create(Section model);
        Task<Section> Get(int? id);

        Task Edit(Section models);
        Task Delete(int? id);
        Task<List<Section>> List();


        ///
        Task CreateSubSection(Subsection model, HttpPostedFileBase upload);
        Task<Subsection> GetSubSection(int? id);

        Task EditSubSection(Subsection models, HttpPostedFileBase upload);
        Task DeleteSubSection(int? id);
        Task<List<Subsection>> ListSubSection();
        Task<List<Subsection>> ListSubSectionBySectionId(int id);
    }
}

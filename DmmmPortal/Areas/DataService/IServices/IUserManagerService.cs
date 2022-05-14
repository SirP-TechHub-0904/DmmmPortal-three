using DmmmPortal.Models;
using DmmmPortal.Models.Dtos;
using DmmmPortal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmmmPortal.Areas.Data.IServices
{
    interface IUserManagerService
    {
        Task<string> NewStaff(RegisterViewModel model);
        Task<StaffProfile> GetStaff(int? id);
       
       // Task Edit(NewStaffDto models);
        Task Delete(int? id);
        Task<ApplicationUser> GetUserByUserId(string id);
        Task<ApplicationUser> GetUserByUserEmail(string email);
        Task<string> NewStudent(RegisterViewModel model);
        Task<StudentProfile> GetStudent(int? id);

        Task<List<StudentProfile>> ListStudent(string searchString, string currentFilter, int? page);
        Task<List<StaffProfile>> ListStaff(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> AllUsers(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Users();

        Task<bool> IsUsersinRole(string userid, string role);
        Task<bool> UpdateUser(ApplicationUser model);
        Task AddUserToRole(string userId, string rolename);
        Task RemoveUserToRole(string userId, string rolename);

        Task<List<ApplicationUser>> UserAll();
        Task<List<ApplicationUser>> Active(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Expelled(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Withdrawn(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Archived(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> Suspeneded(string searchString, string currentFilter, int? page);




    }
}

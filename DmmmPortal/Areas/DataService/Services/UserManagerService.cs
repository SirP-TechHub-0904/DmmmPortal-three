using DmmmPortal.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using DmmmPortal.Models.Dtos;
using System.Threading.Tasks;
using DmmmPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DmmmPortal.Models.Entities;

namespace DmmmPortal.Areas.Data.Services
{

    public class UserManagerService : IUserManagerService
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public UserManagerService()
        {
        }

        public UserManagerService(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        public Task Delete(int? id)
        {
            throw new NotImplementedException();
        }

        //public Task Edit(NewStaffDto models)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<StaffProfile> GetStaff(int? id)
        {
            var staff = await db.StaffProfiles.Include(x=>x.user).FirstOrDefaultAsync(x => x.Id == id);
            if(staff != null)
            {
                return staff;
            }
            return null;
        }

        public async Task<StudentProfile> GetStudent(int? id)
        {
            var student = await db.StudentProfiles.Include(x => x.user).FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                return student;
            }
            return null;
        }

        public async Task<ApplicationUser> GetUserByUserId(string id)
        {
            var student = await UserManager.FindByIdAsync(id);
            if (student != null)
            {
                return student;
            }
            return null;
        }



        public async Task<string> NewStaff(RegisterViewModel model)
        {
            var setting =  db.Settings.OrderByDescending(x => x.Id).First();
            var officer = HttpContext.Current.User.Identity.GetUserName();
            if(officer == "SuperAdmin")
            {
                officer = "Admin";
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Surname = model.Surname,
                Email = model.Email,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                DateOfBirth = model.DateOfBirth,
                Religion = model.Religion,
                DateRegistered = DateTime.UtcNow.AddHours(1),
                Phone = model.Phone,
                ContactAddress = model.ContactAddress,
                City = model.City,
                Status = EntityStatus.Active,
                StateOfOrigin = model.StateOfOrigin,
                Nationality = model.Nationality,
                RegisteredBy = officer 
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Staff");
                StaffProfile staff = new StaffProfile();
                staff.UserId = user.Id;
                staff.DateOfAppointment = DateTime.UtcNow;
                db.StaffProfiles.Add(staff);
                await db.SaveChangesAsync();

                var staffReg = await db.StaffProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                staffReg.StaffRegistrationId = setting.SchoolInitials + "/STAFF/00" + staffReg.Id;
                db.Entry(staffReg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(", ", errors);
            
            return message;
        }
           
    

        public async Task<string> NewStudent(RegisterViewModel model)
        {
            var setting = db.Settings.OrderByDescending(x => x.Id).First();
            var officer = HttpContext.Current.User.Identity.GetUserName();
            if (officer == "SuperAdmin")
            {
                officer = "Admin";
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Surname = model.Surname,
                FirstName = model.FirstName,
                OtherName = model.OtherName,
                DateOfBirth = model.DateOfBirth,
                Religion = model.Religion,
                Phone = model.Phone,
                DateRegistered = DateTime.UtcNow.AddHours(1),
                ContactAddress = model.ContactAddress,
                City = model.City,
                Status = EntityStatus.Active,
                StateOfOrigin = model.StateOfOrigin,
                Nationality = model.Nationality,
               RegisteredBy = officer 
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Student");
               
                StudentProfile student = new StudentProfile();
                student.UserId = user.Id;
                
                db.StudentProfiles.Add(student);
                await db.SaveChangesAsync();

                var studentReg = await db.StudentProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
                studentReg.StudentRegNumber = setting.SchoolInitials + "/000" + studentReg.Id;
                db.Entry(studentReg).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return "true";
            }
            
            string error = string.Join(" ", result.Errors);
            return error;
        }

        public async Task<List<StudentProfile>> ListStudent(string searchString, string currentFilter, int? page)
        {
            var list =  db.StudentProfiles.Include(x=>x.user);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        list = list.Where(s => s.user.Surname.ToUpper().Contains(item.ToUpper()) || s.user.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(item.ToUpper()) || s.StudentRegNumber.ToUpper().Contains(item.ToUpper()) || s.user.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    list = list.Where(s => s.user.Surname.ToUpper().Contains(searchString.ToUpper()) || s.user.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.StudentRegNumber.ToUpper().Contains(searchString.ToUpper()) || s.user.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }
            return await list.ToListAsync();
        }

        public async Task<List<StaffProfile>> ListStaff(string searchString, string currentFilter, int? page)
        {
            var list = db.StaffProfiles.Include(x => x.user);
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        list = list.Where(s => s.user.Surname.ToUpper().Contains(item.ToUpper()) || s.user.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(item.ToUpper()) || s.StaffRegistrationId.ToUpper().Contains(item.ToUpper()) || s.user.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    list = list.Where(s => s.user.Surname.ToUpper().Contains(searchString.ToUpper()) || s.user.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.user.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.StaffRegistrationId.ToUpper().Contains(searchString.ToUpper()) || s.user.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }
            return await list.ToListAsync();
        }

        public async Task<List<ApplicationUser>> UserAll()
        {

            var users = UserManager.Users.Where(x => x.UserName != "SuperAdmin").OrderBy(x=>x.UserName);
            return await users.ToListAsync();
        }


            public async Task<List<ApplicationUser>> AllUsers(string searchString, string currentFilter, int? page)
        {

            var users = UserManager.Users.Where(x=>x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }
           
            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<bool> IsUsersinRole(string userid, string role)
        {
            var users = await _userManager.IsInRoleAsync(userid, role);
            return users;
        }
        ///
        public int CountString(string searchString)
        {
            int result = 0;

            searchString = searchString.Trim();

            if (searchString == "")
                return 0;

            while (searchString.Contains("  "))
                searchString = searchString.Replace("  ", " ");

            foreach (string y in searchString.Split(' '))

                result++;


            return result;
        }


        public async Task<List<ApplicationUser>> Users()
        {
            return (await UserManager.Users.ToListAsync());
        }

        public async Task AddUserToRole(string userId, string rolename)
        {
            await UserManager.AddToRoleAsync(userId, rolename);
        }
        public async Task RemoveUserToRole(string userId, string rolename)
        {
            await UserManager.RemoveFromRoleAsync(userId, rolename);
        }

        public async Task<bool> UpdateUser(ApplicationUser model)
        {
            
           
            //IdentityResult check = await UserManager.UpdateAsync(model);
            // if (check.Succeeded)
            db.Entry(model).State = EntityState.Modified;
            await db.SaveChangesAsync();
                return true;
           
        }

        public async Task<List<ApplicationUser>> Active(string searchString, string currentFilter, int? page)
        {
            var users = UserManager.Users.Where(x => x.Status == EntityStatus.Active && x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<List<ApplicationUser>> Expelled(string searchString, string currentFilter, int? page)
        {
            var users = UserManager.Users.Where(x => x.Status == EntityStatus.Expelled && x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<List<ApplicationUser>> Withdrawn(string searchString, string currentFilter, int? page)
        {
            var users = UserManager.Users.Where(x => x.Status == EntityStatus.Withdrawn && x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<List<ApplicationUser>> Archived(string searchString, string currentFilter, int? page)
        {
            var users = UserManager.Users.Where(x => x.Status == EntityStatus.Archived && x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<List<ApplicationUser>> Suspeneded(string searchString, string currentFilter, int? page)
        {
            var users = UserManager.Users.Where(x => x.Status == EntityStatus.Suspeneded && x.UserName != "SuperAdmin");
            if (!String.IsNullOrEmpty(searchString))
            {
                if (CountString(searchString) > 1)
                {
                    string[] searchStringCollection = searchString.Split(' ');

                    foreach (var item in searchStringCollection)
                    {
                        users = users.Where(s => s.Surname.ToUpper().Contains(item.ToUpper()) || s.FirstName.ToUpper().Contains(item.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(item.ToUpper()) || s.UserName.ToUpper().Contains(item.ToUpper()));
                    }
                }
                else
                {
                    users = users.Where(s => s.Surname.ToUpper().Contains(searchString.ToUpper()) || s.FirstName.ToUpper().Contains(searchString.ToUpper())
                                                               || s.OtherName.ToUpper().Contains(searchString.ToUpper()) || s.UserName.ToUpper().Contains(searchString.ToUpper()));
                }

            }

            return await users.OrderBy(x => x.UserName).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByUserEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DmmmPortal.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DmmmPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string Surname { get; set; }
        [Display(Name = "First Name")]

        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }
        public string Religion { get; set; }

        public string Gender { get; set; }
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Local Government Area")]
        public string LocalGov { get; set; }

        [Display(Name = "State Of Origin")]
        public string StateOfOrigin { get; set; }

        public string Nationality { get; set; }
        public DateTime? DateRegistered { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public UserStatus userStatus { get; set; }
        public int ImageId { get; set; }

        public string Disability { get; set; }
        public string RegistrationId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


     
        public DbSet<States> States { get; set; }
        public DbSet<LocalGovs> LocalGovs { get; set; }
        public DbSet<ImageModel> ImageModel { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HallOfFame> HallOfFames { get; set; }
        public DbSet<ImageSlider> ImageSlider { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Subsection> Subsections { get; set; }
        public DbSet<ScratchCardInfo> ScratchCardInfos { get; set; }
        public DbSet<SchoolPortalData> SchoolPortalDatas { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
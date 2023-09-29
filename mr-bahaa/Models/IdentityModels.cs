using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace mr_bahaa.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "اسم المدرسه")]
        public string School { get; set; }

        [Display(Name = "صفك الدراسي ")]
        public string Yourclass { get; set; }
        [Display(Name = "ميعاد الحصه ")]
        public string Appointment { get; set; }


        [Required]
        [Display(Name = "رقم تليفونك الشخصي ")]
        public string secphonenumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<photo> photos { get; set; }
        public DbSet<Student> students { get; set; }

        public DbSet<lecture> lectures { get; set; }
        public DbSet<photolec> photolecs { get; set; }
        public DbSet<exam> exams { get; set; }
        public DbSet<questions> questions { get; set; }
        public DbSet<img> Imgs { get; set; }
        public DbSet<quese_score> quese_Scores { get; set; }
        public DbSet<qcontrol> qcontrols { get; set; }
        public DbSet<studentquestions> Studentquestions { get; set; }
        public DbSet<studnetquestionphoto> studnetquestionphotos { get; set; }
        public DbSet<replayphotos> replayphotos { get; set; }
        public DbSet<lecturecontrolview> lecturecontrolviews { get; set; }
        public DbSet<examviewcontrol> examviewcontrols { get; set; }
        public DbSet<Absence> absences { get; set; }
        public DbSet<examtime> examtimes { get; set; }
        public DbSet<homework> homeworks { get; set; }
        public DbSet<imghomework> imghomeworks { get; set; }
  
        public DbSet<examscore> examescore { get; set; }
        public DbSet<forgetpass> forgetpass { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<teacherimg> teacherimgs { get; set; }
        public DbSet<subjectcoure> subjectcoures { get; set; }
        public DbSet<subjectcoureimg> subjectcoureimg { get; set; }
        public DbSet<coursescategories> coursescategories { get; set; }
        public DbSet<Joins> Joins { get; set; }
        public DbSet<lecturelock> lecturelock { get; set; }
        public DbSet<studentip> studentips { get; set; }
        public DbSet<onelecture> onelectures { get; set; }
        public DbSet<codes> codes { get; set; }
        public DbSet<lecturehomework> lecturehomework { get; set; }














        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        object placeHolderVariable;
    }
}
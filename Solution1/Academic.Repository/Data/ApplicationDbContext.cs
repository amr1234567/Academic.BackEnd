using Academic.Core.Base;
using Academic.Core.Entities;
using Academic.Core.Entities.ManyToManyEntities;
using Academic.Core.Identitiy;
using Academic.Repository.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository.Data
{
    public  class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AdminConfig());
            //modelBuilder.ApplyConfiguration(new UserConfig());
            //modelBuilder.ApplyConfiguration(new InstructorConfig());
            //modelBuilder.ApplyConfiguration(new ModuleConfig());
            //modelBuilder.ApplyConfiguration(new ModuleSectionConfig());
            //modelBuilder.ApplyConfiguration(new MultiChoiceQuestionConfig());
            //modelBuilder.ApplyConfiguration(new PathConfig());
            //modelBuilder.ApplyConfiguration(new PathTaskConfig());
            //modelBuilder.ApplyConfiguration(new QuizConfig());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InstructorConfig).Assembly); // Corrected here
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Instructor> Instructors { get; set; } // Corrected to Instructors
        public DbSet<Module> Modules { get; set; } // Corrected to Modules
        public DbSet<ModuleSection> ModuleSections { get; set; } // Corrected to ModuleSections
        public DbSet<MultiChoiceQuestion> MultiChoiceQuestions { get; set; } // Corrected to MultiChoiceQuestions
        public DbSet<EducationalPath> Paths { get; set; } // Corrected to Paths
        public DbSet<User> Users { get; set; } // Corrected to Users
        public DbSet<PathTask> PathTasks { get; set; } // Corrected to PathTasks
        public DbSet<Quiz> Quizzes { get; set; } // Corrected to Quizzes
        public DbSet<UserQuestionAnswer> userQuestionAnswers { get; set; }

        public DbSet<ModuleSectionUsers> ModuleSectionUsers { get; set; } 
        public DbSet<ModuleUsers> ModuleUsers { get; set; } 
        public DbSet<PathTaskUsers> PathTaskUsers { get; set; } 
        public DbSet<PathUsers> PathUsers { get; set; }

    }
}

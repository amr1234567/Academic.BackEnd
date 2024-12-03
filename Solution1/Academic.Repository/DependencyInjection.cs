using Academic.Repository.Data;
using Academic.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academic.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayerServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(connectionString);
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });


            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
            services.AddScoped<IModuleSectionsRepository, ModuleSectionsRepository>();
            services.AddScoped<IAdminRepository, IAdminRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IPathRepository, PathRepository>();

            return services;
        }
    }
}

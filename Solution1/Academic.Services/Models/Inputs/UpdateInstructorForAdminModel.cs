using Academic.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Models.Inputs
{
    public class UpdateInstructorForAdminModel
    {
        public int Id { get; internal set; }

        public string Title { get; set; }

        public string JobType { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }
    }
}
using Academic.Core.Enums;

namespace Academic.Services.Models.Outputs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
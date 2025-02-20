using Sahibinden.Entities.Enums;

namespace Sahibinden.AdminPanel.Models.User
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public DateTime RecordDate { get; set; }
    }
}

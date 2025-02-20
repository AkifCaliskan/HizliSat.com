namespace Sahibinden.AdminPanel.Models.User
{
    public class UserLoginDetailModel
    {
        public int userId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public short Type { get; set; }
    }
}

using OrganizeYou.DAL.Entities;

namespace OrganizeYou.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }
    }
}


namespace Lift_application.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Users(string email, string password, Role role)
        {
           Email = email;
            Password = password;
            Role = role;
        }

    }
    public class Role
    {
        public string Name { get; set; }
        public Role(string name) => Name = name;
    }
}


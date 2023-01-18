using Lift_application.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Lift_application.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<Lift_applicationUser> Members { get; set; }
        public IEnumerable<Lift_applicationUser> NonMembers { get; set; }
    }
}

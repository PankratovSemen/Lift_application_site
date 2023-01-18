using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lift_application.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Lift_applicationUser class
[Table("AspNetUsers")]
public class Lift_applicationUser : IdentityUser
{
   
    [PersonalData]
    [Column (TypeName="nvarchar(100)")]
    public string FristName { set; get; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { set; get; }
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string MiddleName { set; get; }
    public List<IdentityUserRole<string>> Roles { get; set; }

}


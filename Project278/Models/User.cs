using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project278.Models
{
    public class User : IdentityUser
    {
        public string? AvatarUrl { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; } = "Unkown";
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}

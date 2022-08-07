using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project278.Data;
using Project278.Models;

namespace Project278.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly AppDbContext _db;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ProfileController(AppDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string id)
        {
            User user = _db.Users.Find(id);
            return View(user);
        }
    }
}

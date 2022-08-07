using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project278.Data;
using Project278.Models;

namespace Project278.Controllers
{
    public class LabelController : Controller
    {
        private readonly AppDbContext _db;

        public LabelController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()    
        {
            return View(_db.Labels);
        }

        [HttpGet]
        public IActionResult Details(int id) {
            Label? label = _db.Labels.Find(id);
            return View(label);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using modul_133_AronBA.Models;
using System.Diagnostics;
using modul_133_AronBA.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace modul_133_AronBA.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, DbGainzbourgContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            
            ViewBag.Gruppenkurs = await _dbcontext.TblGruppenkurs.Where(x => x.TrainerId == int.Parse(userid)).Take(5).ToListAsync();
            ViewBag.TblMitgliedsDeleted = await _dbcontext.TblMitglieds.Include(m => m.Trainer).Where(t => t.Trainer.Deleted == true).ToListAsync();
            ViewBag.Mitglieder = await _dbcontext.TblMitglieds.Where(x => x.Trainerid == int.Parse(userid)).Take(5).ToListAsync();

            ViewBag.MitgliederCount =  _dbcontext.TblMitglieds.Count(x => x.Trainerid == int.Parse(userid));
            ViewBag.GruppenkursCount = _dbcontext.TblGruppenkurs.Count(x => x.TrainerId == int.Parse(userid));
            ViewBag.TblMitgliedsDeletedCount = _dbcontext.TblMitglieds.Include(m => m.Trainer).Count(t => t.Trainer.Deleted == true);
            return View();
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
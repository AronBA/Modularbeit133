using Microsoft.AspNetCore.Mvc;
using modul_133_AronBA.Models;
using System.Diagnostics;
using modul_133_AronBA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace modul_133_AronBA.Controllers
{
    [Authorize(Roles = "True")]
    public class TrashController : Controller
    {
        private readonly ILogger<TrainerController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public TrashController(ILogger<TrainerController> logger, DbGainzbourgContext _dbcontext)
        {
            _logger = logger;
            this._dbcontext = _dbcontext;

        }


        public async Task<IActionResult> Index()
        {
            ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == true).ToListAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Restore(int id)
        {
            var Trainer = await _dbcontext.TblTrainers.FindAsync(id);
            Console.WriteLine("asdasdasd1");
            if (Trainer == null)
            {
                Console.WriteLine("asdasdasd2");
                return NotFound();
            }
            Console.WriteLine("asdasdasd3");
            Trainer.Deleted = false;

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
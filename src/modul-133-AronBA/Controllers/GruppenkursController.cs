using Microsoft.AspNetCore.Mvc;
using modul_133_AronBA.Models;
using System.Diagnostics;
using modul_133_AronBA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace modul_133_AronBA.Controllers
{
    [Authorize]
    public class GruppenkursController : Controller
    {
        private readonly ILogger<GruppenkursController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public GruppenkursController(ILogger<GruppenkursController> logger, DbGainzbourgContext _dbcontext)
        {
            _logger = logger;
            this._dbcontext = _dbcontext;

        }


        public async Task<IActionResult> Index()
        {

            ViewBag.Gruppenkurs = await _dbcontext.TblGruppenkurs.Include(t => t.Trainer).ToListAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var gruppenkurs = await _dbcontext.TblGruppenkurs.FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();
            return View(gruppenkurs);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TblGruppenkur addGruppenkursRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();
                return View();
            }
            TempData["message"] = "Gruppenkurs wurde erfolgreich hinzugefügt";
            await _dbcontext.TblGruppenkurs.AddAsync(addGruppenkursRequest);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TblGruppenkur deleteGruppenkursRequest)
        {

            var gruppenkurs = await _dbcontext.TblGruppenkurs.FindAsync(deleteGruppenkursRequest.Id);

            if (gruppenkurs != null)
            {
                _dbcontext.TblGruppenkurs.Remove(gruppenkurs);
                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Gruppenkurs wurde erfolgreich gelöscht";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Ein unerwartet Fehler ist aufgetreten";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblGruppenkur editGruppenkursRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();

                return View();
            }
            var gruppenkurs = await _dbcontext.TblGruppenkurs.FindAsync(editGruppenkursRequest.Id);

            if (gruppenkurs != null)
            {
                gruppenkurs.Bezeichnung = editGruppenkursRequest.Bezeichnung;
                gruppenkurs.Beginn = editGruppenkursRequest.Beginn;
                gruppenkurs.Ende = editGruppenkursRequest.Ende;
                gruppenkurs.Beschreibung = editGruppenkursRequest.Beschreibung;
                gruppenkurs.TrainerId = editGruppenkursRequest.TrainerId;
             

                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Gruppenkurs wurde erfolgreich bearbeitet";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Ein unerwartet Fehler ist aufgetreten";
            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
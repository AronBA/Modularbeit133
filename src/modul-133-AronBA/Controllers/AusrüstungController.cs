using Microsoft.AspNetCore.Mvc;
using modul_133_AronBA.Models;
using System.Diagnostics;
using modul_133_AronBA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace modul_133_AronBA.Controllers
{
    [Authorize]
    public class AusrüstungController : Controller
    {
        private readonly ILogger<AusrüstungController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public AusrüstungController(ILogger<AusrüstungController> logger, DbGainzbourgContext _dbcontext)
        {
            _logger = logger;
            this._dbcontext = _dbcontext;

        }

     
        public async Task<IActionResult>  Index()
        {
            ViewBag.Ausruestung = await _dbcontext.TblAusruestungs.ToListAsync();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
         
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ausruestung = await _dbcontext.TblAusruestungs.FirstOrDefaultAsync(x => x.ArtNr == id);

            return View(ausruestung);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TblAusruestung addAusruestungRequest)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            TempData["message"] = "Ausrüstung wurde erfolgreich hinzugefügt";
            await _dbcontext.TblAusruestungs.AddAsync(addAusruestungRequest);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TblAusruestung deleteAusruestungRequest)
        {

            var ausruestung = await _dbcontext.TblAusruestungs.FindAsync(deleteAusruestungRequest.ArtNr);

            if (ausruestung != null) { 
            
                _dbcontext.TblAusruestungs.Remove(ausruestung);
                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Ausrüstung wurde erfolgreich gelöscht";
                return RedirectToAction("Index");

            }
            TempData["error"] = "Ein unerwartet Fehler ist aufgetreten";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblAusruestung editAusruestungRequest)
        {

            if (!ModelState.IsValid)
            {

                return View();
            }
            var ausruestung = await _dbcontext.TblAusruestungs.FindAsync(editAusruestungRequest.ArtNr);

            if (ausruestung != null)
            {
                ausruestung.ArtNr = editAusruestungRequest.ArtNr;
                ausruestung.Bezeichnung = editAusruestungRequest.Bezeichnung;
                ausruestung.GewichtKg = editAusruestungRequest.GewichtKg;
                ausruestung.Anzahl = editAusruestungRequest.Anzahl;
             
                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Ausrüstung wurde erfolgreich bearbeitet";
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
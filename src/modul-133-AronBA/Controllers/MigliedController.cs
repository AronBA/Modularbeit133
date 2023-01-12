using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modul_133_AronBA.Data;
using modul_133_AronBA.Models;
using System.Diagnostics;

namespace modul_133_AronBA.Controllers
{
    [Authorize]
    public class MitgliedController : Controller
    {
        private readonly ILogger<MitgliedController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public MitgliedController(ILogger<MitgliedController> logger, DbGainzbourgContext _dbcontext)
        {
            _logger = logger;
            this._dbcontext = _dbcontext;
            
        }

     
        public async Task<IActionResult> Index(string searchString, int FilterOption, int page = 1)
        {
            var take = 8;
            var skip = (page - 1) * take;
          

            ViewBag.Page = page;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (FilterOption)
                {
                    case 1:
                        ViewBag.Mitglied = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Vorname.Contains(searchString)).Skip(skip).Take(take).ToListAsync();
                        ViewBag.LastCount = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Vorname.Contains(searchString)).CountAsync();
                        break;

                        case 2:
                        ViewBag.Mitglied = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Nachname.Contains(searchString)).Skip(skip).Take(take).ToListAsync();
                        ViewBag.Count = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Nachname.Contains(searchString)).CountAsync();

                        break;

                    case 3:
                        ViewBag.Mitglied = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Mail.Contains(searchString)).Skip(skip).Take(take).ToListAsync();
                        ViewBag.Count = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Mail.Contains(searchString)).CountAsync();

                        break;

                    default:
                        ViewBag.Mitglied = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Vorname.Contains(searchString) || s.Mail.Contains(searchString) || s.Nachname.Contains(searchString)).Skip(skip).Take(take).ToListAsync();
                        ViewBag.Count = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Where(s => s.Vorname.Contains(searchString) || s.Mail.Contains(searchString) || s.Nachname.Contains(searchString)).CountAsync();

                        break;
                }
            }
            else
            {
                ViewBag.Mitglied = await _dbcontext.TblMitglieds.Include(t => t.Trainer).Skip(skip).Take(take).ToListAsync();
                ViewBag.Count = await _dbcontext.TblMitglieds.Include(t => t.Trainer).CountAsync();

            }
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
           
            var mitglied = await _dbcontext.TblMitglieds.Include(m => m.Trainer).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t=>t.Deleted == false).ToListAsync();
            return View(mitglied);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TblMitglied addMitgliedRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();

                return View();
            }
            TempData["message"] = "Mitglied wurde erfolgreich hinzugefügt";
            await _dbcontext.TblMitglieds.AddAsync(addMitgliedRequest);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TblMitglied deleteMitgliedRequest)
        {

            var mitglied = await _dbcontext.TblMitglieds.FindAsync(deleteMitgliedRequest.Id);

            if (mitglied != null)
            {
                _dbcontext.TblMitglieds.Remove(mitglied);
                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Mitglied wurde erfolgreich gelöscht";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Ein unerwartet Fehler ist aufgetreten";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblMitglied editMitgliedRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Trainer = await _dbcontext.TblTrainers.Where(t => t.Deleted == false).ToListAsync();

                return View();
            }
            var mitglied = await _dbcontext.TblMitglieds.FindAsync(editMitgliedRequest.Id);

            if (mitglied != null)
            {
                mitglied.Vorname = editMitgliedRequest.Vorname;
                mitglied.Nachname = editMitgliedRequest.Nachname;
                mitglied.Gebursdatum = editMitgliedRequest.Gebursdatum;
                mitglied.Mail = editMitgliedRequest.Mail;
                mitglied.Ahv = editMitgliedRequest.Ahv;
                mitglied.Trainerid = editMitgliedRequest.Trainerid;
                mitglied.MitgliedschaftAnfang = editMitgliedRequest.MitgliedschaftAnfang;
                mitglied.MitgliedschaftEnde = editMitgliedRequest.MitgliedschaftEnde;

                await _dbcontext.SaveChangesAsync();
                TempData["message"] = "Mitglied wurde erfolgreich bearbeitet";
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
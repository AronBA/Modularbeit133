using Microsoft.AspNetCore.Mvc;
using modul_133_AronBA.Models;
using System.Diagnostics;
using modul_133_AronBA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace modul_133_AronBA.Controllers
{
    [Authorize(Roles = "True")]
    public class TrainerController : Controller
    {
        private readonly ILogger<TrainerController> _logger;
        private readonly DbGainzbourgContext _dbcontext;

        public TrainerController(ILogger<TrainerController> logger, DbGainzbourgContext _dbcontext)
        {
            _logger = logger;
            this._dbcontext = _dbcontext;

        }

        
        public async Task<IActionResult> Index()
        {
            
            
            ViewBag.Trainer = await _dbcontext.TblTrainers.Where(x => x.Deleted == false).ToListAsync();
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
            var trainer = await _dbcontext.TblTrainers.FirstOrDefaultAsync(x => x.Id == id);

            return View(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TblTrainer addTrainerRequest)
        {

           var trainer = new TblTrainer();

            if(!ModelState.IsValid){

                return View();
            }
            TempData["message"] = "Mitglied wurde erfolgreich hinzugefügt";
            trainer.Vorname = addTrainerRequest.Vorname;
            trainer.Nachname = addTrainerRequest.Nachname;
            trainer.Passwort = LoginController.Encrypt(addTrainerRequest.Passwort);
            trainer.Benutzername = addTrainerRequest.Benutzername;
            await _dbcontext.TblTrainers.AddAsync(trainer);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TblTrainer deleteTrainerRequest)
        {
  
            var trainer = await _dbcontext.TblTrainers.FindAsync(deleteTrainerRequest.Id);
   
            if (trainer != null)
            {
                TempData["message"] = "Mitglied wurde erfolgreich gelöscht";
                _dbcontext.TblTrainers.Remove(trainer);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            TempData["error"] = "Ein unerwartet Fehler ist aufgetreten";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblTrainer editTrainerRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var trainer = await _dbcontext.TblTrainers.FindAsync(editTrainerRequest.Id);

            if (trainer != null)
            {
                trainer.Benutzername = editTrainerRequest.Benutzername;
                trainer.Vorname = editTrainerRequest.Vorname;
                trainer.Nachname = editTrainerRequest.Nachname;
               
         
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
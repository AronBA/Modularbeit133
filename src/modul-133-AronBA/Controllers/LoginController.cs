using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using modul_133_AronBA.Data;
using modul_133_AronBA.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace modul_133_AronBA.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DbGainzbourgContext _dbcontext;
        

        public LoginController(ILogger<LoginController> logger, DbGainzbourgContext _dbcontext) {

            _logger = logger;
            this._dbcontext = _dbcontext;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(TblTrainer trainer)
        {
           
             
            List<TblTrainer> user = await _dbcontext.TblTrainers.Where(t => trainer.Benutzername == t.Benutzername && Encrypt(trainer.Passwort) == t.Passwort && t.Deleted == false).ToListAsync();


           
            if (!user.Any())
            {

                TempData["Message"] = "Passwort oder Benutzername sind falsch";
                return RedirectToAction("Login");
                
                
            }
      

            var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user[0].Headcoach.ToString()),
            new(ClaimTypes.Name, user[0].Benutzername),
            new(ClaimTypes.Surname, user[0].Vorname),
            new(ClaimTypes.GivenName, user[0].Nachname),
            new(ClaimTypes.NameIdentifier, user[0].Id.ToString()),

            


        };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
           
           

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));





            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync();
            TempData["Message"] = "Sie wurden erfolgreich abgemeldet";
            return RedirectToAction("Login");
        }


        public static string Encrypt(string passwort)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(passwort));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;

        }
    }
}

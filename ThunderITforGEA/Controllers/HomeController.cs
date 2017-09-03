using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ThunderITforGEA.Models;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ThunderITforGEA.Controllers
{
    //paneladmina2 - dodaj edytuj usun (wszystkie grupy)
    //restart hasla = wyslij uzytkownikowi nowe haslo
    //zablokuj odblokuj uzytkownika
    //bilingi
    //edycja uprawinien

    //logi - kto, jaka tresc, jaki serviceguard
    //logi wedlug serviceguarda - kto i ile skorzystal
    //na koncu miesiaca np. 100 smsow x 5gr 
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {         
            Entities daneDoModelu = new Entities();          
            ViewBag.User_Id = User.Identity.GetUserId(); //wszystkie widoki będą znać ID zalogowanego użytkownika
            string jezyk = daneDoModelu.AspNetUsers.Find(User.Identity.GetUserId()).jezyk.Trim();
            if (User.IsInRole("admin")) 
            {                           
                if (jezyk == "0") //polski
                {
                    var SprzetUzytkownika = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                    return View("Admin", SprzetUzytkownika.ToList());
                }
                if(jezyk=="1")
                {
                    var SprzetUzytkownika = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                    return View("Admin-en", SprzetUzytkownika.ToList());
                }
            }
            if (User.IsInRole("user"))
            {
                if (jezyk == "0") //polski
                {
                    var id = User.Identity.GetUserId();
                    var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers).Where(u => u.AspNetUsers.Id == id);
                    return View("Uzytkownik", dane.ToList());
                }
                if(jezyk=="1")
                {
                    var id = User.Identity.GetUserId();
                    var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers).Where(u => u.AspNetUsers.Id == id);
                    return View("Uzytkownik-en", dane.ToList());
                }
            }
            if (User.IsInRole("gea"))
            {
                if (jezyk == "0")
                {
                    var id = User.Identity.GetUserId();
                    var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                    return View("GEA", dane.ToList());
                }
             if(jezyk=="1")
             {
                 var id = User.Identity.GetUserId();
                 var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                 return View("GEA-en", dane.ToList());
             }
            }                 
                return View(); 
        }
    }
}
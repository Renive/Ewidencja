using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ThunderITforGEA.Models;

namespace ThunderITforGEA.Controllers
{
    public class SprzetUzytkownikasController : Controller
    {
        private Entities db = new Entities();
        // GET: SprzetUzytkownikas
        public ActionResult Index(string sortOrder, string searchString)
        {
            Entities daneDoModelu = new Entities();           
            if (!String.IsNullOrEmpty(searchString))
            {
               var wynikiWyszukiwania = daneDoModelu.SprzetUzytkownika.Where(s => s.AspNetUsers.nazwisko.Contains(searchString)
                                       || s.AspNetUsers.imie.Contains(searchString)).Include(u=>u.AspNetUsers);
                if(User.IsInRole("admin"))
                    return View("admin",wynikiWyszukiwania.ToList());
                if (User.IsInRole("gea"))
                    return View("GEA", wynikiWyszukiwania.ToList());
            }
            if(!String.IsNullOrEmpty(sortOrder) && searchString==null)
            {
                var dane = from s in daneDoModelu.SprzetUzytkownika.Include(u=>u.AspNetUsers)
                               select s;
                switch (sortOrder)
                {
                    case "1":
                        dane = dane.OrderByDescending(s => s.AspNetUsers.nazwisko);                       
                        break;
                    case "2":
                        dane = dane.OrderBy(s => s.AspNetUsers.segment);
                        break;
                    case "3":
                        dane = dane.OrderByDescending(s => s.AspNetUsers.segment2);
                        break;
                    case "4":
                        dane = dane.OrderByDescending(s => s.rodzajSprzetu);
                        break;
                }
                 if (User.IsInRole("admin"))
                            return View("Admin", dane.ToList());
                            if (User.IsInRole("user"))
                                 return View("Uzytkownik", dane.ToList());
                                if (User.IsInRole("gea"))
                                    return View("GEA", dane.ToList());
            }
         
            ViewBag.User_Id = User.Identity.GetUserId(); //wszystkie widoki będą znać ID zalogowanego użytkownika

            if (User.IsInRole("admin"))
            {
                var SprzetUzytkownika = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                return View("Admin", SprzetUzytkownika.ToList());
            }
            if (User.IsInRole("user"))
            {
                var id = User.Identity.GetUserId();
                var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers).Where(u => u.AspNetUsers.Id == id);
                return View("Uzytkownik", dane.ToList());
            }
            if (User.IsInRole("gea"))
            {
                var id = User.Identity.GetUserId();
                var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
                return View("GEA", dane.ToList());
            }
            else
                return View(); 
        }
       
        public ActionResult Index2(string search)
        {           
            Entities daneDoModelu = new Entities();
            var wynikiWyszukiwania = daneDoModelu.SprzetUzytkownika.Where(s => s.AspNetUsers.segment.Contains(search)
                                     || s.AspNetUsers.segment2.Contains(search)).Include(u => u.AspNetUsers);
            if (User.IsInRole("admin"))
                return View("admin", wynikiWyszukiwania.ToList());
            if (User.IsInRole("gea"))
                return View("GEA", wynikiWyszukiwania.ToList());
            return View();
        }
        // GET: SprzetUzytkownikas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprzetUzytkownika sprzetUzytkownika = db.SprzetUzytkownika.Find(id);
            if (sprzetUzytkownika == null)
            {
                return HttpNotFound();
            }
            return View(sprzetUzytkownika);
        }

        // GET: SprzetUzytkownikas/Create
        public ActionResult Create()
        {
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash");
            return View();
        }
        public ActionResult Create_en()
        {
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash");
            return View("Create-en");
        }

        // POST: SprzetUzytkownikas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_ua,rodzajSprzetu,marka,nrPUK,nr_tel,procentZuzycia,model,serviceTag,serialNumber,numerPin,nrTuszu,id_user,pojemnosc,rodzaj,przyjecie,zwrot,moc,numerRejestracyjny,numerVin,opony,ubezpieczenie,przeglad,przebieg")] SprzetUzytkownika sprzetUzytkownika)
        {
            if (ModelState.IsValid)
            {
                sprzetUzytkownika.Id_ua = Guid.NewGuid().ToString();
                sprzetUzytkownika.id_user = User.Identity.GetUserId();
                db.SprzetUzytkownika.Add(sprzetUzytkownika);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", sprzetUzytkownika.id_user);
            return View(sprzetUzytkownika);
        }
   
        // GET: SprzetUzytkownikas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprzetUzytkownika sprzetUzytkownika = db.SprzetUzytkownika.Find(id);
            if (sprzetUzytkownika == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", sprzetUzytkownika.id_user);
            return View(sprzetUzytkownika);
        }

        // POST: SprzetUzytkownikas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ua,rodzajSprzetu,marka,nrPUK,nr_tel,procentZuzycia,model,serviceTag,serialNumber,numerPin,nrTuszu,id_user,pojemnosc,rodzaj,przyjecie,zwrot,moc,numerRejestracyjny,numerVin,opony,ubezpieczenie,przeglad,przebieg")] SprzetUzytkownika sprzetUzytkownika)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprzetUzytkownika).State = EntityState.Modified;
                db.Entry(sprzetUzytkownika).Entity.id_user = User.Identity.GetUserId();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", sprzetUzytkownika.id_user);
            return View(sprzetUzytkownika);
        }
        public ActionResult oznaczDoZwrotu(string id,string powod)
        {
        
            SprzetUzytkownika sprzetUzytkownika = db.SprzetUzytkownika.Find(id);
            sprzetUzytkownika.zwracany = "tak";
            sprzetUzytkownika.zwrot = powod;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: SprzetUzytkownikas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SprzetUzytkownika sprzetUzytkownika = db.SprzetUzytkownika.Find(id);
            if (sprzetUzytkownika == null)
            {
                return HttpNotFound();
            }
            return View(sprzetUzytkownika);
        }

        // POST: SprzetUzytkownikas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id, string powod)
        {
            SprzetUzytkownika sprzetUzytkownika = db.SprzetUzytkownika.Find(id);
            if (User.IsInRole("user"))
            {
                oznaczDoZwrotu(id, powod);
                return RedirectToAction("Index");
            }
            Historia kopiowanywpis = new Historia();
           // sprzetUzytkownika.i//TODO
            kopiowanywpis.Id_h = sprzetUzytkownika.Id_ua;
            kopiowanywpis.AspNetUsers = sprzetUzytkownika.AspNetUsers;
            kopiowanywpis.marka = sprzetUzytkownika.marka;
            kopiowanywpis.moc = sprzetUzytkownika.moc;
            kopiowanywpis.model = sprzetUzytkownika.model;
            kopiowanywpis.nr_tel = sprzetUzytkownika.nr_tel;
            kopiowanywpis.nrPUK = sprzetUzytkownika.nrPUK;
            kopiowanywpis.nrTuszu = sprzetUzytkownika.nrTuszu;
            kopiowanywpis.numerPin = sprzetUzytkownika.numerPin;
            kopiowanywpis.numerRejestracyjny = sprzetUzytkownika.numerRejestracyjny;
            kopiowanywpis.numerVin = sprzetUzytkownika.numerVin;
            kopiowanywpis.opony = sprzetUzytkownika.opony;
            kopiowanywpis.pojemnosc = sprzetUzytkownika.pojemnosc;
            kopiowanywpis.powodZwrotu = powod;
            kopiowanywpis.procentZuzycia = sprzetUzytkownika.procentZuzycia;
            kopiowanywpis.przebieg = sprzetUzytkownika.przebieg;
            kopiowanywpis.przeglad = sprzetUzytkownika.przeglad;
            kopiowanywpis.przyjecie = sprzetUzytkownika.przyjecie;
            kopiowanywpis.rodzaj = sprzetUzytkownika.rodzaj;
            kopiowanywpis.rodzajSprzetu = sprzetUzytkownika.rodzajSprzetu;
            kopiowanywpis.serialNumber = sprzetUzytkownika.serialNumber;
            kopiowanywpis.serviceTag = sprzetUzytkownika.serviceTag;
            kopiowanywpis.ubezpieczenie = sprzetUzytkownika.ubezpieczenie;
            kopiowanywpis.zwrot = sprzetUzytkownika.zwrot;
            db.Historia.Add(kopiowanywpis);
            db.SprzetUzytkownika.Remove(sprzetUzytkownika);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThunderITforGEA.Models;

namespace ThunderITforGEA.Controllers
{
    public class HistoriaController : Controller
    {
        private Entities db = new Entities();

        // GET: Historia
        public ActionResult Index()
        {
            var historia = db.Historia.Include(h => h.AspNetUsers);
            return View(historia.ToList());
        }

        // GET: Historia/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historia historia = db.Historia.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            return View(historia);
        }

        // GET: Historia/Create
        public ActionResult Create()
        {
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash");
            return View();
        }

        // POST: Historia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_h,rodzajSprzetu,marka,nrPUK,nr_tel,procentZuzycia,model,serviceTag,serialNumber,numerPin,nrTuszu,id_user,pojemnosc,rodzaj,przyjecie,zwrot,moc,numerRejestracyjny,numerVin,opony,ubezpieczenie,przeglad,przebieg,powodZwrotu,torba,zasilacz,mysz,pamiec_usb,modem_iplus,bluetooth,wifi,naped_optyczny,sluchawki,dysk_przenosny,drukarka_przenosna,nagrywarka_przenosna,samochod_sprawny,zestaw_glosnomowiacy,radio,apteczka,trojkat,gasnica,opony_zimowe,opony_letnie,kolo_zapasowe,ksiazka_serwisowa,karta_pojazdu,dowod_rejestracyjny,karta_paliwowa")] Historia historia)
        {
            if (ModelState.IsValid)
            {
                db.Historia.Add(historia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", historia.id_user);
            return View(historia);
        }

        // GET: Historia/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historia historia = db.Historia.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", historia.id_user);
            return View(historia);
        }

        // POST: Historia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_h,rodzajSprzetu,marka,nrPUK,nr_tel,procentZuzycia,model,serviceTag,serialNumber,numerPin,nrTuszu,id_user,pojemnosc,rodzaj,przyjecie,zwrot,moc,numerRejestracyjny,numerVin,opony,ubezpieczenie,przeglad,przebieg,powodZwrotu,torba,zasilacz,mysz,pamiec_usb,modem_iplus,bluetooth,wifi,naped_optyczny,sluchawki,dysk_przenosny,drukarka_przenosna,nagrywarka_przenosna,samochod_sprawny,zestaw_glosnomowiacy,radio,apteczka,trojkat,gasnica,opony_zimowe,opony_letnie,kolo_zapasowe,ksiazka_serwisowa,karta_pojazdu,dowod_rejestracyjny,karta_paliwowa")] Historia historia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_user = new SelectList(db.AspNetUsers, "Id", "PasswordHash", historia.id_user);
            return View(historia);
        }

        // GET: Historia/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Historia historia = db.Historia.Find(id);
            if (historia == null)
            {
                return HttpNotFound();
            }
            return View(historia);
        }

        // POST: Historia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Historia historia = db.Historia.Find(id);
            db.Historia.Remove(historia);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaLoja.Models;

namespace SistemaLoja.Controllers
{
    public class OrdemAPIController : Controller
    {
        private SistemaLojaContext db = new SistemaLojaContext();

        // GET: OrdemAPI
        public ActionResult Index()
        {
            var ordem = db.Ordem.Include(o => o.Customizar);
            return View(ordem.ToList());
        }

        // GET: OrdemAPI/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = db.Ordem.Find(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // GET: OrdemAPI/Create
        public ActionResult Create()
        {
            ViewBag.CustomizarId = new SelectList(db.Customizars, "CustomizarId", "Nome");
            return View();
        }

        // POST: OrdemAPI/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrdemId,OrdemData,CustomizarId,OrdemStatus")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                db.Ordem.Add(ordem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomizarId = new SelectList(db.Customizars, "CustomizarId", "Nome", ordem.CustomizarId);
            return View(ordem);
        }

        // GET: OrdemAPI/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = db.Ordem.Find(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomizarId = new SelectList(db.Customizars, "CustomizarId", "Nome", ordem.CustomizarId);
            return View(ordem);
        }

        // POST: OrdemAPI/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrdemId,OrdemData,CustomizarId,OrdemStatus")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomizarId = new SelectList(db.Customizars, "CustomizarId", "Nome", ordem.CustomizarId);
            return View(ordem);
        }

        // GET: OrdemAPI/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordem ordem = db.Ordem.Find(id);
            if (ordem == null)
            {
                return HttpNotFound();
            }
            return View(ordem);
        }

        // POST: OrdemAPI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordem ordem = db.Ordem.Find(id);
            db.Ordem.Remove(ordem);
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

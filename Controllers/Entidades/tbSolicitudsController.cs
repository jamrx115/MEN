using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConvalidacionEducacionSuperiorDatos;

namespace ConvalidacionEducacionSuperior.Controllers.Entidades
{
    public class tbSolicitudsController : Controller
    {
        private bdConvalidacionesEntities db = new bdConvalidacionesEntities();

        // GET: tbSolicituds
        public ActionResult Index()
        {
            var tbSolicitud = db.tbSolicitud.Include(t => t.tbEstado);
            return View(tbSolicitud.ToList());
        }

        // GET: tbSolicituds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitud);
        }

        // GET: tbSolicituds/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.tbEstado, "EstadoId", "Estado");
            return View();
        }

        // POST: tbSolicituds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SolicitudId,Fecha,Tipo,Estado,Usuario,notificacionElectronica,variasInstituciones,beca,convenio,preConvalidado,valor")] tbSolicitud tbSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.tbSolicitud.Add(tbSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.tbEstado, "EstadoId", "Estado", tbSolicitud.Estado);
            return View(tbSolicitud);
        }

        // GET: tbSolicituds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.tbEstado, "EstadoId", "Estado", tbSolicitud.Estado);
            return View(tbSolicitud);
        }

        // POST: tbSolicituds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SolicitudId,Fecha,Tipo,Estado,Usuario,notificacionElectronica,variasInstituciones,beca,convenio,preConvalidado,valor")] tbSolicitud tbSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.tbEstado, "EstadoId", "Estado", tbSolicitud.Estado);
            return View(tbSolicitud);
        }

        // GET: tbSolicituds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            if (tbSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tbSolicitud);
        }

        // POST: tbSolicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbSolicitud tbSolicitud = db.tbSolicitud.Find(id);
            db.tbSolicitud.Remove(tbSolicitud);
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

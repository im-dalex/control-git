using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace FinalProject.Controllers
{
    public class VacacionesController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Vacaciones
        public ActionResult Index()
        {
            var vacaciones = db.vacaciones.Include(v => v.Empleados);
            return View(vacaciones.ToList());
        }

        // GET: Vacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            return View(vacaciones);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = new SelectList(db.empleado.Where(s => s.Estatus == true), "ID", "codigoEmpleado");
            return View();
        }

        // POST: Vacaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Empleado,Desde,Hasta,correspondiente,Comentarios")] Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                db.vacaciones.Add(vacaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", vacaciones.Empleado);
            return View(vacaciones);
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", vacaciones.Empleado);
            return View(vacaciones);
        }

        // POST: Vacaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Empleado,Desde,Hasta,correspondiente,Comentarios")] Vacaciones vacaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado = new SelectList(db.empleado, "ID", "codigoEmpleado", vacaciones.Empleado);
            return View(vacaciones);
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vacaciones vacaciones = db.vacaciones.Find(id);
            if (vacaciones == null)
            {
                return HttpNotFound();
            }
            return View(vacaciones);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacaciones vacaciones = db.vacaciones.Find(id);
            db.vacaciones.Remove(vacaciones);
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

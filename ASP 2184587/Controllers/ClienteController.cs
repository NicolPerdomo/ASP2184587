using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            {
                using (var db = new inventarioEntities())
                {
                    return View(db.cliente.ToList());
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(cliente cliente)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var db = new inventarioEntities())
                {
                    db.cliente.Add(cliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            using (var db = new inventarioEntities())
            {
                var findUser = db.cliente.Find(id);
                return View(findUser);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente FindUser = db.cliente.Where(a => a.id == id).FirstOrDefault();
                    return View(FindUser);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error" + ex);
                return View();

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente EditUser)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    cliente cliente = db.cliente.Find(EditUser.id);
                    cliente.nombre = EditUser.nombre;
                    cliente.documento = EditUser.documento;
                    cliente.email = EditUser.email;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findUser = db.cliente.Find(id);
                    db.cliente.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error" + ex);
                return View();
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
//IMPORTANDO LOS MODELOS DE LA BASE DE DATOS 
using ASP_2184587.Models;

namespace ASP_2184587.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            using(var db= new inventarioEntities())
            {
                return View(db.usuario.ToList());

            }
         
        }

        public ActionResult Create()
        {
            return View();
        } 

        //los decradores son caraeristicas que se le pueden dar
        //a los atributos o a los metodos

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            //para capturar errores
            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario.password = UsuarioController.HashSHA1(usuario.password);
                    db.usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }
               

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }


        //encriptar contraseñas
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public ActionResult Details (int id)
        {
            using (var db= new inventarioEntities())
            {
                var findUser = db.usuario.Find(id);
                return View(findUser);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }

                         
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
                
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario editUser)
        {
            try
            {
                using  (var db= new inventarioEntities())
                {
                    usuario user = db.usuario.Find(editUser.id);

                    user.nombre = editUser.nombre;
                    user.apellido = editUser.apellido;
                    user.email = editUser.email;
                    user.fecha_nacimiento = editUser.fecha_nacimiento;
                    user.password = editUser.password;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();

            }
        }

        public ActionResult Delete (int id)
        {
            try
            {
                using (var db = new inventarioEntities())
                {
                    var findUser = db.usuario.Find(id);
                    db.usuario.Remove(findUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error" + ex);
                return View();
            }

        }

    }

}

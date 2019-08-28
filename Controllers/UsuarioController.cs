using ConvalidacionEducacionSuperior.Models;
using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConvalidacionEducacionSuperior.Controllers
{
    public class UsuarioController : Controller
    {
        private bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            UsuarioModel modelo = new UsuarioModel();
            modelo.PaisDisplay = "display:inline";
            modelo.PaisDisplay_II = "display:none";
            combos("", "", "COLOMBIA", "AMAZONAS", "");
            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(UsuarioModel modelo)
        {
            if (ModelState.IsValid)
            {
                //Invoca servicio para regustrar usuario

                LoginModel loginModel = new LoginModel();
                loginModel.usuario = modelo.login;
                loginModel.Password = modelo.Password;
                loginModel.tipoDoc = modelo.tipoDoc;

                loginModel.datosUsuario = modelo;

                //Inicia Sesion
                Session["usuarioValido"] = loginModel;

                return RedirectToAction("BandejaConvalidante", "Inicio");
            }

            if (modelo.pais.ToUpper().Equals("COLOMBIA"))
            {
                modelo.PaisDisplay = "display:inline";
                modelo.PaisDisplay_II = "display:none";
            }
            else
            {
                modelo.PaisDisplay = "display:none";
                modelo.PaisDisplay_II = "display:inline";
            }

            combos(modelo.genero, modelo.tipoDoc, modelo.pais, modelo.departamento, modelo.ciudad);
            return View(modelo);
        }

        //[CaptchaValidator]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult CreateComment(Int32 id, bool captchaValid)
        //{
        //    if (!captchaValid)
        //    {
        //        ModelState.AddModelError("_FORM", "You did not type the verification word correctly. Please try again.");
        //    }
        //    else
        //    {
        //        // If we got this far, something failed, redisplay form
        //        return View();
        //    }
        //    return View();
        //}

        public ActionResult Edita()
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            UsuarioModel modelo = usuario.datosUsuario;

            if (modelo.pais.ToUpper().Equals("COLOMBIA"))
            {
                modelo.PaisDisplay = "display:inline";
                modelo.PaisDisplay_II = "display:none";
            }
            else
            {
                modelo.PaisDisplay = "display:none";
                modelo.PaisDisplay_II = "display:inline";
            }

            combos(modelo.genero, modelo.tipoDoc, modelo.pais, modelo.departamento,modelo.ciudad);
            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edita(UsuarioModel modelo)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("Login", "Inicio");
            }

            if (ModelState.IsValid)
            {
                //Invoca servicio para Actualizar datos
                return RedirectToAction("BandejaConvalidante", "Inicio");
            }

            if (modelo.pais.ToUpper().Equals("COLOMBIA"))
            {
                modelo.PaisDisplay = "display:inline";
                modelo.PaisDisplay_II = "display:none";
            }
            else
            {
                modelo.PaisDisplay = "display:none";
                modelo.PaisDisplay_II = "display:inline";
            }

            combos(modelo.genero, modelo.tipoDoc, modelo.pais, modelo.departamento, modelo.ciudad);
            return View(modelo);
        }

        public ActionResult CambiaContrasena()
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("Login","Inicio");
            }
            CambioContrasena modelo = new CambioContrasena();
            modelo.user = usuario.usuario;
            modelo.validPassword = usuario.Password;
            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CambiaContrasena(CambioContrasena modelo)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("Login", "Inicio");
            }
            
            if (ModelState.IsValid)
            {
                //Invoca servicio para cambiar la contraseña
                return RedirectToAction("BandejaConvalidante", "Inicio");
            }

            return View(modelo);
        }

        private void combos(string sex, string tipoDoc, string paisRes, string depto, string ciudadRes)
        {
            List<SelectListItem> cmbSexo = new SelectList(db.tbSexoBiologico, "sexoBiologico", "sexoBiologico", sex).ToList();
            ViewBag.sexo = cmbSexo;

            List<SelectListItem> cmbTipoDoc = new SelectList(db.tbTipoDocumento, "TipoDocumento", "TipoDocumento", tipoDoc).ToList();
            ViewBag.tipoDoc = cmbTipoDoc;

            List<SelectListItem> cmbPais = new SelectList(db.tbPais.OrderBy(p => p.pais).ToList(), "pais", "pais", paisRes).ToList();
            ViewBag.Pais = cmbPais;

            List<SelectListItem> cmbDepartamento = new SelectList(db.tbDepartamento.OrderBy(d => d.departamento).ToList(), "departamento", "departamento", depto).ToList();
            ViewBag.Departamento = cmbDepartamento;

            List<tbCiudad> ciudades = db.tbCiudad.Where(c => c.tbDepartamento.departamento == depto).OrderBy(c => c.ciudad).ToList();
            List<SelectListItem> cmbCiudad = new SelectList(ciudades, "ciudad", "ciudad", ciudadRes).ToList();
            ViewBag.Ciudad = cmbCiudad;
        }
    }
}
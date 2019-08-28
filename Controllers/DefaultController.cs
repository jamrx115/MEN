using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConvalidacionEducacionSuperior.Controllers
{
    public class DefaultController : Controller
    {
        private bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult AutoComplete(string prefix)
        //{
        //    var institutos = (from inst in db.tbNombreInstitucion
        //                      where inst.nombreInstitucion.Contains(prefix)
        //                      select new
        //                      {
        //                          label = inst.nombreInstitucion,
        //                          val = inst.nombreInstitucionId
        //                      }).ToList();

        //    return Json(institutos);
        //}

        [HttpPost]
        public JsonResult AutoComplete()
        {
            var institutos = (from inst in db.INS_INSTITUCION_EDUCATIVA
                              select new
                              {
                                  label = inst.NOMBRE_INSTITUCION,
                                  val = inst.ID_INSTITUCION
                              }).Distinct().ToList();

            return Json(institutos);
        }

        [HttpPost]
        public JsonResult AutoCompleteNombrePrograma()
        {
            var programas = (from prog in db.PRO_PROGRAMA_EXTRANJERO
                              select new
                              {
                                  label = prog.TITULO_EXTRANJERO,
                                  val = prog.ID_PROGRAMA_EXTRANJERO
                              }).OrderBy(p => p.label).Distinct().ToList();

            return Json(programas);
        }

        [HttpPost]
        public ActionResult Index(string CustomerName, string CustomerId)
        {
            ViewBag.Message = "CustomerName: " + CustomerName + " CustomerId: " + CustomerId;
            return View();
        }
    }
}
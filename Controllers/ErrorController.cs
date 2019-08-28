﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConvalidacionEducacionSuperior.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0, string msg = "")
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Error Inesperado";
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }
            ViewBag.Error = msg;
            return View("~/views/error/_ErrorPage.cshtml");
        }
    }
}
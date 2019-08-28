using ConvalidacionEducacionSuperior.Models;
using ConvalidacionEducacionSuperior.Reportes.Constancia;
using ConvalidacionEducacionSuperiorDatos;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
//using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;

namespace ConvalidacionEducacionSuperior.Controllers
{
    public class InicioController : Controller
    {
        private bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        public static List<Institucion> lstInstitucionesStatic;
        public static int destinoFII;
        
        public ActionResult LoginSession()
        {
            Session["usuarioValido"] = null;

            return View();
        }

        // GET: Login 
        public ActionResult Login()
        {
            //return RedirectToAction("LoginSession");
            Session["usuarioValido"] = null;
            comboTipoDocumento();

            //valorMonetario valor = new valorMonetario();
            //valor.moneda = "$";
            //valor.valorTransaccion = 120000;

            //ParametrosEntrada param = new ParametrosEntrada
            //{
            //    codRefPago = "Id_Solicitud",
            //    codServicio = "1001",
            //    codBanco = 10,
            //    codPlataforma = 123,
            //    valorMonetario = valor,
            //    FechaTransaccion = DateTime.Now,
            //    FechaContabilizacion = DateTime.Now.AddDays(3),
            //    codEntidadFinanciera = "COL",
            //    comentarios = "",
            //    codMedioPago = "DEBITO"
            //};

            //string json = JsonConvert.SerializeObject(param);
            //byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            //HttpWebRequest request;
            //request = WebRequest.Create("https://localhost:44356/api/serviciopago") as HttpWebRequest;
            //request.Timeout = 10 * 1000;
            //request.Method = "POST";
            //request.ContentLength = data.Length;
            //request.ContentType = "application/json; charset=utf-8";

            //Stream postStream = request.GetRequestStream();
            //postStream.Write(data, 0, data.Length);

            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //StreamReader reader = new StreamReader(response.GetResponseStream());
            //string body = reader.ReadToEnd();

            //string respData = Json(body).Data.ToString();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                System.Web.HttpContext.Current.Session["email"] = "lpradac@hotmail.com";
                System.Web.HttpContext.Current.Session["token"] = "vvvv";

                return RedirectToAction("BandejaConvalidante");
            }
            comboTipoDocumento();
            return View(loginModel);
        }

        public void comboTipoDocumento()
        {
            List<SelectListItem> cmbTipoDoc = new List<SelectListItem>();
            cmbTipoDoc.Insert(0, (new SelectListItem { Text = "Tipo de Documento", Value = "0" }));
            cmbTipoDoc.Insert(1, (new SelectListItem { Text = "Cédula de Ciudadanía", Value = "CC" }));
            cmbTipoDoc.Insert(2, (new SelectListItem { Text = "Cédula de Extranjería", Value = "CE" }));
            cmbTipoDoc.Insert(3, (new SelectListItem { Text = "Pasaporte", Value = "PA" }));
            ViewBag.tipoDoc = cmbTipoDoc;
        }

        public ActionResult BandejaConvalidante()
        {
            try
            {
                //LLama servicio de autenticación

                //var sesi = Request["email"];
                var sesi = System.Web.HttpContext.Current.Session["email"];
                var toke = System.Web.HttpContext.Current.Session["token"];
                LoginModel usuario = new LoginModel();

                bool vaidaSes = true;

                if (sesi == null)
                {
                    usuario = (LoginModel)Session["usuarioValido"];
                    if (usuario == null)
                    {
                        //return RedirectToAction("LoginSession");
                        sesi = Request["email"];
                        if (sesi == null)
                        {
                            //Response.Redirect(String.Format("~/Error/Index/?error={0}&msg={1}", 404, "NO se ha logeado"));
                            return RedirectToAction("LoginSession");
                        }
                    }
                    else
                    {
                        vaidaSes = false;
                    }
                }

                if (vaidaSes)
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };

                    AutenticacionLogin.ConsultaWSWebRegistryClient usuarioAutenticado = new AutenticacionLogin.ConsultaWSWebRegistryClient();
                    var usu = usuarioAutenticado.getWebRegistry(sesi.ToString());

                    //LLena datos Usuario
                    LoginModel loginModel = new LoginModel();
                    loginModel.usuario = usu.correoElectronico;
                    loginModel.tipoDoc = usu.tipoIdentificacion;

                    loginModel.datosUsuario = new UsuarioModel();

                    loginModel.datosUsuario.login = usu.correoElectronico;
                    //loginModel.datosUsuario.Password = psw;

                    loginModel.datosUsuario.primerNombre = usu.primerNombre;
                    loginModel.datosUsuario.segundoNombre = usu.segundoNombre;
                    loginModel.datosUsuario.primerApellido = usu.primerApellido;
                    loginModel.datosUsuario.segundoApellido = usu.segundoApellido;
                    loginModel.datosUsuario.numeroDocumento = usu.numeroIdentificacion;
                    loginModel.datosUsuario.tipoDoc = usu.tipoIdentificacion;
                    loginModel.datosUsuario.email = usu.correoElectronico;
                    loginModel.datosUsuario.ciudadExpedicion = usu.codigoCiudadExpedicion;
                    loginModel.datosUsuario.ciudad = usu.descripcionCiudadExpedicion;
                    int codDeptoExp = 0;
                    if (!string.IsNullOrEmpty(usu.codigoCiudadExpedicion))
                        codDeptoExp = (int)db.tbCiudad.Find(int.Parse(usu.codigoCiudadExpedicion)).departamentoId;

                    tbDepartamento deptoExp = db.tbDepartamento.Find(codDeptoExp);

                    if (deptoExp != null)
                        loginModel.datosUsuario.departamento = deptoExp.departamento;

                    //OJO FALTA EN EL SERVICIO ESTA INFO
                    loginModel.datosUsuario.genero = "MASCULINO";
                    loginModel.datosUsuario.generoId = 1;
                    loginModel.datosUsuario.direccion = "Cra 77 3a - 44";
                    loginModel.datosUsuario.pais = "COLOMBIA";
                    loginModel.datosUsuario.celular = "3106782510";
                    loginModel.datosUsuario.nacionalidad = "Colombiano";

                    //Inicia Sesion
                    Session["usuarioValido"] = loginModel;
                    usuario = loginModel;

                    //ServicioBozagiConsulta.QueryFormSOASoapClient consultaCLient = new ServicioBozagiConsulta.QueryFormSOASoapClient();
                    //var servCOnsulta = consultaCLient.
                }

                //LoginModel usuario = new LoginModel();
                List<tbSolicitud> lstSol = db.tbSolicitud.Where(s => s.Usuario == usuario.usuario).Where(s => s.Estado < 4).ToList();

                List<tbSolicitud> lstSolTramite = db.tbSolicitud.Where(s => s.Usuario == usuario.usuario).Where(s => s.Estado > 3).ToList();
                //var tt = lstSolTramite[0].tbInstitucion.FirstOrDefault().InstitutoText
                usuario.lstSolicitudes = new List<tbSolicitud>();
                usuario.lstSolicitudesTramite = new List<tbSolicitud>();

                usuario.lstSolicitudes.AddRange(lstSol);
                usuario.lstSolicitudesTramite.AddRange(lstSolTramite);
                usuario.solicitudActual = null;

                return View(usuario);
            }
            catch (Exception ex)
            {
                HttpException httpException = ex as HttpException;

                int error1 = httpException != null ? httpException.GetHttpCode() : 0;
                string mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return RedirectToAction("Index", "Error", new { error = error1, msg = mensaje });
            }
            
        }

        public ActionResult listaSolicitudes()
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            return View(usuario);
        }

        public string eliminaSolicitud(int solicidutId)
        {
            try
            {
                tbDatosContacto autor = db.tbDatosContacto.Where(a => a.Solicitud == solicidutId).FirstOrDefault();
                if(autor != null)
                    db.tbDatosContacto.Remove(autor);

                tbPrograma prog = db.tbPrograma.Where(a => a.Solicitud == solicidutId).FirstOrDefault();
                if (prog != null)
                    db.tbPrograma.Remove(prog);

                List<tbInstitucion> inst = db.tbInstitucion.Where(a => a.Solicitud == solicidutId).ToList();
                if (inst.Count > 0)
                    db.tbInstitucion.RemoveRange(inst);

                List<tbPregrado> preg = db.tbPregrado.Where(a => a.Solicitud == solicidutId).ToList();
                if (preg.Count > 0)
                    db.tbPregrado.RemoveRange(preg);

                List<tbDocumentos> doctos = db.tbDocumentos.Where(a => a.solicitudId == solicidutId).ToList();
                if (doctos.Count > 0)
                    db.tbDocumentos.RemoveRange(doctos);

                tbSolicitud solicitud = db.tbSolicitud.Find(solicidutId);
                db.tbSolicitud.Remove(solicitud);
                db.SaveChanges();

                LoginModel usuario = (LoginModel)Session["usuarioValido"];
                List<tbSolicitud> lstSol = db.tbSolicitud.Where(s => s.Usuario == usuario.usuario).ToList();
                usuario.lstSolicitudes = new List<tbSolicitud>();

                usuario.lstSolicitudes.AddRange(lstSol);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }
        }

        public ActionResult FaseI(string Tipo)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if(usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            usuario.tipoValidacion = Tipo;

            if(usuario.solicitudActual == null)
            {
                tbSolicitud newSol = new tbSolicitud();
                newSol.notificacionElectronica = true;
                newSol.nacional = true;
                newSol.notificaTercero = false;
                newSol.nacional = true;

                newSol.beca = false;
                newSol.preConvalidado = false;
                newSol.variasInstituciones = false;
                newSol.convenio = false;

                newSol.Tipo = usuario.tipoValidacion;
                newSol.Usuario = usuario.usuario;
                newSol.Fecha = DateTime.Now;
                newSol.valor = 999999;
                newSol.Estado = 1;//Nueva

                //db.tbSolicitud.Add(newSol);
                //db.SaveChanges();
                usuario.solicitudActual = newSol;
                usuario.lstSolicitudes.Add(newSol);
                
            }

            return View(usuario.solicitudActual);
        }

        public ActionResult FaseII(int? id)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            Paso2Model modelo = new Paso2Model();

            if (id == 0)
            {
                usuario.solicitudActual = null;
                modelo.solicitud = new tbSolicitud();
                modelo.notificaElectrinica = true;
                modelo.notificaTercero = false;
                modelo.displayNotifica = "display:none";
                modelo.nacional = false;
                modelo.institucionesExtrangeras = false;
                modelo.convenio = false;
                modelo.beca = false;
                modelo.preGradoValidado = false;
            }
            else
            {
                tbSolicitud soli = db.tbSolicitud.Where(s => s.SolicitudId == id).FirstOrDefault();
                modelo.solicitud = soli;
                usuario.solicitudActual = soli;
                usuario.tipoValidacion = soli.Tipo;

                modelo.beca = (bool)soli.beca;
                modelo.convenio = (bool)soli.convenio;
                modelo.institucionesExtrangeras = (bool)soli.variasInstituciones;
                modelo.notificaElectrinica = (bool)soli.notificacionElectronica;
                modelo.notificaTercero = (bool)soli.notificaTercero;
                modelo.preGradoValidado = (bool)soli.preConvalidado;
                modelo.nacional = (bool)soli.nacional;

                if (modelo.notificaElectrinica)
                    modelo.displayNotifica = "display:none";
                else
                    modelo.displayNotifica = "display:inline";
            }

            if (usuario.tipoValidacion.ToUpper().Equals("POSGRADO"))
            {
                modelo.estilo = "display:inline";                
                modelo.esPregrado = false;
                if (modelo.nacional)
                {
                    modelo.estilo_1 = "display:none";
                }
                else
                {
                    modelo.estilo_1 = "display:inline";
                }
            }
            else
            {
                modelo.estilo = "display:none";
                modelo.estilo_1 = "display:none";
                modelo.esPregrado = true;
            }               

            modelo.convalidacion = usuario.tipoValidacion;
            modelo.valor = 999999;
            modelo.valorString = modelo.valor.ToString("#,#,#.00#");

            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult FaseII(Paso2Model modelo, string btnAceptaII, string notificaElectrinica)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            try
            {
                tbSolicitud tbSol = guardaSolicitudFaseII(modelo,ref usuario);
                if (btnAceptaII == "atrasF1")
                    return RedirectToAction("FaseI", routeValues: new { Tipo = usuario.tipoValidacion });
                else if(btnAceptaII == "atrasBandeja")
                    return RedirectToAction("BandejaConvalidante");
                else
                    return RedirectToAction("FaseIII", routeValues: new { solicitudId = tbSol.SolicitudId });
            }
            catch(Exception ex)
            {
                HttpException httpException = ex as HttpException;

                int error1 = httpException != null ? httpException.GetHttpCode() : 0;
                string mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return RedirectToAction("Index", "Error", new { error = error1, msg = mensaje });
            }
        }

        public tbSolicitud guardaSolicitudFaseII(Paso2Model modelo, ref LoginModel usuario)
        {
            tbSolicitud tbSol = new tbSolicitud();
            tbSol.Tipo = usuario.tipoValidacion;
            tbSol.Usuario = usuario.usuario;
            tbSol.Fecha = DateTime.Now;
            tbSol.valor = 999999;
            tbSol.Estado = 1;//Nueva

            tbSol.beca = modelo.beca;
            tbSol.notificacionElectronica = modelo.notificaElectrinica;
            tbSol.notificaTercero = modelo.notificaTercero;
            tbSol.preConvalidado = modelo.preGradoValidado;
            tbSol.nacional = modelo.nacional;
            tbSol.variasInstituciones = modelo.institucionesExtrangeras;
            tbSol.convenio = modelo.convenio;
            tbSol.acreditacionCalidad = true;

            modelo.solicitud = tbSol;

            if (usuario.solicitudActual == null)
                db.tbSolicitud.Add(tbSol);
            else
            {
                tbSol.SolicitudId = usuario.solicitudActual.SolicitudId;
                tbSol.Estado = usuario.solicitudActual.Estado;
                tbSol.Fecha = usuario.solicitudActual.Fecha;
                db.Entry(tbSol).State = EntityState.Modified;
            }

            db.SaveChanges();
            usuario.lstSolicitudes.Add(tbSol);
            usuario.solicitudActual = tbSol;

            return tbSol;
        }
        private Paso3Model llenaSolicitud(int? solicitudId, LoginModel usuario)
        {
            Paso3Model modelo = new Paso3Model();

            modelo.PrimerNombre = usuario.datosUsuario.primerNombre;
            modelo.PrimerApellido = usuario.datosUsuario.primerApellido;
            modelo.SegundoNombre = usuario.datosUsuario.segundoNombre;
            modelo.SegundoApellido = usuario.datosUsuario.segundoApellido;
            modelo.TipoDocumento = usuario.datosUsuario.codigotipoDoc;
            modelo.NombreTipoDocumento = usuario.datosUsuario.nombretipoDoc;
            modelo.NumeroDocumento = usuario.datosUsuario.numeroDocumento;
            modelo.Nacionalidad = usuario.datosUsuario.nacionalidad;
            modelo.GeneroId = usuario.datosUsuario.generoId;
            modelo.Genero = usuario.datosUsuario.genero;
            modelo.CiudadExpedicionCC = usuario.datosUsuario.ciudad;
            modelo.DeptoExpedicion = usuario.datosUsuario.departamento;
            modelo.CiudadExpedicionCCCod = int.Parse(usuario.datosUsuario.ciudadExpedicion);

            tbSolicitud sol = db.tbSolicitud.Find(solicitudId);
            modelo.solicitudActual = sol;
            usuario.solicitudActual = sol;
            modelo.Observaciones = sol.Observaciones;
            modelo.acreditacionCalidad = (bool)sol.acreditacionCalidad;

            usuario.tipoValidacion = sol.Tipo;

            if (sol.Tipo.ToUpper().Equals("POSGRADO"))
                modelo.estilo = "display:inline";
            else
                modelo.estilo = "display:none";

            if ((bool)sol.notificacionElectronica)
            {
                modelo.displayNotificaElectronica = "display:none";
            }
            else
            {
                modelo.displayNotificaElectronica = "display:inline";
                if ((bool)sol.notificaTercero)
                {
                    modelo.displayAutorizado = "display:inline;width:100%";
                    modelo.displayPropio = "display:none";
                }
                else
                {
                    modelo.displayAutorizado = "display:none";
                    modelo.displayPropio = "display:inline";
                }
            }

            tbDatosContacto tbAut = db.tbDatosContacto.Where(a => a.Solicitud == sol.SolicitudId).FirstOrDefault();

            if (tbAut != null)
            {
                if (modelo.TipoDocumento.ToUpper().Equals("CE"))
                {
                    modelo.CiudadExpedicion = tbAut.ciudadExpedicion;
                    modelo.PaisExpedicion = tbAut.paisExpedicion;
                }

                //if (modelo.TipoDocumento.ToUpper().Equals("CC"))
                //{
                //    modelo.CiudadExpedicionCC = (int)tbAut.ciudadExpedicionCC;
                //    modelo.DeptoExpedicion = (int)tbAut.dptoExpedicionCC;
                //}

                if ((bool)modelo.solicitudActual.notificacionElectronica)
                {
                    //Notificacion electrónica
                    modelo.Direccion = tbAut.direccionResidencia;
                    modelo.CodigoPostal = tbAut.codigoPostalResidencia;
                    modelo.Pais = tbAut.paisResidencia == null ? 0 : int.Parse(tbAut.paisResidencia);
                    if (modelo.Pais == 170)
                    {
                        modelo.Ciudad = tbAut.ciudadResidencia;
                        modelo.Departamento = tbAut.departamentoResidencia;
                    }
                    else
                    {
                        modelo.Ciudad = tbAut.ciudadResidencia;
                        modelo.Ciudad_II = tbAut.ciudadResidencia;
                    }

                    modelo.Email = tbAut.email;
                    modelo.ConfirmaEmail = tbAut.email;
                    modelo.Email2 = tbAut.emailOpcional;
                    modelo.ConfirmaEmail2 = tbAut.emailOpcional;
                    if(!string.IsNullOrEmpty(tbAut.telefonoPropio))
                        modelo.TelefonoFijo = int.Parse(tbAut.telefonoPropio);
                    if (!string.IsNullOrEmpty(tbAut.celularPropio))
                        modelo.Celular = long.Parse(tbAut.celularPropio);

                    modelo._autorizado = new Autorizado();
                }
                else
                {
                    //Autorizado
                    Autorizado aut = new Autorizado();
                    modelo._autorizado = aut.convertTbAutorizado(tbAut);
                    modelo.CiudadNotificacion = tbAut.CiudadAutorizado;
                    modelo.DepartamentoNotificacion = "-1";
                    modelo._autorizado.Pais = "COLOMBIA";
                    modelo.Ciudad = "-1";
                    modelo.Departamento = "-1";
                }
            }
            else
            {
                modelo._autorizado = new Autorizado();
                modelo._autorizado.Pais = "COLOMBIA";               
            }

            Institucion inst = new Institucion();
            tbInstitucion tbInst = db.tbInstitucion.Where(i => i.Solicitud == sol.SolicitudId).FirstOrDefault();
            modelo._institucion = tbInst == null ? new Institucion() : inst.convertTbInstituto(tbInst);

            string paisInst = "0"; // db.tbPais.FirstOrDefault().pais;
            List<Institucion> lstInstituciones = inst.convertListTbInstituto(db.tbInstitucion.Where(i => i.Solicitud == sol.SolicitudId).ToList());
            if (lstInstituciones.Count == 0)
            {
                inst.Pais = paisInst;
                inst.Instituto = "0";
                inst.Ciudad = "-1";
                lstInstituciones.Add(inst);
            }

            modelo._institucionList = lstInstituciones;

            modelo.PaisInstituto = tbInst == null ? paisInst : String.IsNullOrEmpty(tbInst.Pais) ? paisInst : tbInst.Pais;

            tbPrograma tbProg = db.tbPrograma.Where(p => p.Solicitud == sol.SolicitudId).FirstOrDefault();
            if (tbProg == null)
            {
                modelo._programa = new Programa();
                modelo._programa.FechaTitulo = DateTime.Now;
                modelo._programa.TipoMaestria = string.Empty;
                modelo._programa.CampoAmplioConocimiento = 0;// string.Empty;
                if (sol.Tipo.ToUpper().Equals("POSGRADO"))
                    modelo._programa.TipoEducacionSuperior = "2";
                else
                    modelo._programa.TipoEducacionSuperior = "6";
                modelo.segundaEspecialidadSalud = true;
                modelo.segundaEspecialidadSaludNal = false;
                modelo.EspecialidadMedicaSalud = true;
                modelo.displayNombrePrograma = "display:inline";
                modelo._programa.TituloObtenido = "N/A";
                modelo.displayValidacionInstituto = "display:inline";
            }
            else
            {
                Programa prog = new Programa();
                modelo._programa = prog.convertTbPrograma(tbProg);
                modelo.segundaEspecialidadSalud = modelo._programa.SegundaEspecialidad;
                modelo.segundaEspecialidadSaludNal = modelo._programa.SegundaEspecialidadNal;
                modelo.EspecialidadMedicaSalud = modelo._programa.EspecialidadMedicaSalud;
                if (string.IsNullOrEmpty(modelo._programa.NombrePrograma) || modelo._programa.NombrePrograma == "0")
                {
                    modelo.displayNombrePrograma = "display:inline";
                    modelo._programa.TituloObtenido = "N/A";
                }                   
                else
                    modelo.displayNombrePrograma = "display:none";
                //modelo._programa_TituloObtenido = modelo._programa.TituloObtenido;
            }

            if (modelo.Pais == 0)
            {
                modelo.PaisDisplay = "display:inline";
                modelo.PaisDisplay_II = "display:none";
            }
            else
            {
                if (modelo.Pais == 170)
                {
                    modelo.PaisDisplay = "display:inline";
                    modelo.PaisDisplay_II = "display:none";
                }
                else
                {
                    modelo.PaisDisplay = "display:none";
                    modelo.PaisDisplay_II = "display:inline";
                }
            }

            //Obtenemos codigo salud
            string campoSaludId = WebConfigurationManager.AppSettings["campoSaludId"].ToString();
            modelo.areaSaludParam = int.Parse(campoSaludId);

            if (modelo._programa.CampoAmplioConocimiento == modelo.areaSaludParam)
            {
                modelo.displayValidacionInstituto = "display:none";
            }
            else
            {
                if (modelo.solicitudActual.Tipo.ToUpper().Equals("PREGRADO"))
                {
                    if (modelo._programa.CampoAmplioConocimiento == 41 || modelo._programa.CampoAmplioConocimiento == 42 || modelo._programa.CampoAmplioConocimiento == 11)
                    {
                        modelo.displayValidacionInstituto = "display:none";
                    }
                    else
                    {
                        modelo.displayValidacionInstituto = "display:inline";
                    }
                }
                else
                {
                    modelo.displayValidacionInstituto = "display:inline";
                }
            }

            if (modelo._programa.CampoAmplioConocimiento == modelo.areaSaludParam && modelo.solicitudActual.Tipo.ToUpper().Equals("POSGRADO"))
                modelo.areaSalud = "display:inline";
            else
                modelo.areaSalud = "display:none";

            if (modelo._programa.TipoEducacionSuperior.Equals("2"))
                modelo.esMaestria = "display:inline";
            else
                modelo.esMaestria = "display:none";

            if (modelo.segundaEspecialidadSalud)
                modelo.SegundaEspecialidadSaludDisplay = "display:inline";
            else
                modelo.SegundaEspecialidadSaludDisplay = "display:none";

            if (modelo._programa.TipoEducacionSuperior.Equals("1") || 
                (modelo._programa.TipoEducacionSuperior.Equals("2") && modelo._programa.TipoMaestria.Equals("1")))
            {
                modelo.EspecialidadMedicaSaludDisplay = "display:inline";
            }
            else
            {
                modelo.EspecialidadMedicaSaludDisplay = "display:none";
            }               

            if (modelo.segundaEspecialidadSaludNal)
            {
                modelo.SegundaEspecialidadSaludDisplaySI = "display:inline";
                modelo.SegundaEspecialidadSaludDisplayNO = "display:none";
            }
            else
            {
                modelo.SegundaEspecialidadSaludDisplaySI = "display:none";
                modelo.SegundaEspecialidadSaludDisplayNO = "display:inline";
            }

            tbPregrado tbPre = db.tbPregrado.Where(p => p.Solicitud == sol.SolicitudId && p.esSegundaEspecialidad == false).FirstOrDefault();
            if (tbPre == null)
            {
                modelo._infoPregrado = new InfoPregrado();
                modelo._infoPregrado.FechaTitulo = DateTime.Now;
                modelo._infoPregrado.FechaResolucion = DateTime.Now;
                modelo.displayInstitucionPre = "display:inline";
                modelo.DptoInstitutoPre = -1;
                modelo.CiudadInstitutoPre = -1;
                modelo.NombreInstitucionOtorgaPre = "160";
            }
            else
            {
                InfoPregrado infoPre = new InfoPregrado();
                modelo._infoPregrado = infoPre.convertTbPregrado(tbPre, (bool)modelo.solicitudActual.nacional);
                if ((bool)modelo.solicitudActual.nacional)
                {
                    modelo.DptoInstitutoPre = modelo._infoPregrado.DptoInstitutoPre;
                    modelo.CiudadInstitutoPre = modelo._infoPregrado.CiudadInstitutoPre;
                }                
                if (modelo._infoPregrado.Instituto == "0")
                    modelo.displayInstitucionPre = "display:inline";
                else
                    modelo.displayInstitucionPre = "display:none";
            }

            tbPregrado tbSegunda = db.tbPregrado.Where(p => p.Solicitud == sol.SolicitudId && p.esSegundaEspecialidad == true).FirstOrDefault();
            if (tbSegunda == null)
            {
                modelo._segundaEspecialidad = new InfoPregrado();
                modelo._segundaEspecialidad.FechaTitulo = DateTime.Now;
                modelo._segundaEspecialidad.FechaResolucion = DateTime.Now;
                modelo.displayInstitucionSalud = "display:inline";
                modelo.DptoInstitutoPreSeg = -1;
                modelo.CiudadInstitutoPreSeg = -1;
                modelo._segundaEspecialidad.Instituto = "0";
            }
            else
            {
                InfoPregrado infoSegunda = new InfoPregrado();
                modelo._segundaEspecialidad = infoSegunda.convertTbPregrado(tbSegunda, modelo.segundaEspecialidadSaludNal);
                if (modelo.segundaEspecialidadSaludNal)
                {
                    modelo.DptoInstitutoPreSeg = modelo._segundaEspecialidad.DptoInstitutoPre;
                    modelo.CiudadInstitutoPreSeg = modelo._segundaEspecialidad.CiudadInstitutoPre; ;
                }

                if (modelo._segundaEspecialidad.Instituto == "0")
                    modelo.displayInstitucionSalud = "display:inline";
                else
                    modelo.displayInstitucionSalud = "display:none";
            }

            return modelo;
        }
        
        public ActionResult FaseIII(int? solicitudId)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            Paso3Model modelo = new Paso3Model();

            try
            {
                modelo = llenaSolicitud(solicitudId, usuario);
            }
            catch(Exception ex)
            {
                HttpException httpException = ex as HttpException;

                int error1 = httpException != null ? httpException.GetHttpCode() : 0;
                string mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                return RedirectToAction("Index", "Error", new { error = error1, msg = mensaje });
            }
            

            bool esPost = modelo.solicitudActual.Tipo == "POSGRADO" ? true : false;
            string pais = modelo.Pais == 0 ? "170" : modelo.Pais.ToString();
            string dpto = string.IsNullOrEmpty(modelo.Departamento) ? string.IsNullOrEmpty(modelo.DepartamentoNotificacion)? "-1" : modelo.DepartamentoNotificacion : modelo.Departamento;
            string ciudad = string.IsNullOrEmpty(modelo.Ciudad) ? "-1" : modelo.Ciudad;
            string dptoNoti = modelo._autorizado == null ? "-1" : string.IsNullOrEmpty(modelo._autorizado.Departamento) ? "-1" : modelo._autorizado.Departamento;
            
            combosP2(esPost, pais, int.Parse(dpto), ciudad, int.Parse(dptoNoti), modelo.PaisInstituto, 
                modelo._infoPregrado.Instituto, modelo._infoPregrado.Entidad, modelo._programa.CampoAmplioConocimiento,
                modelo._programa.ModalidadPrograma, modelo._programa.TipoDuracion, modelo._programa.TipoEducacionSuperior,
                modelo._programa.TipoMaestria, modelo._programa.NombrePrograma, modelo._institucion.Instituto, modelo._segundaEspecialidad.Instituto,
                modelo._autorizado.TipoDocumento, modelo._segundaEspecialidad.Entidad, 
                modelo.DptoInstitutoPre, modelo.CiudadInstitutoPre, modelo._infoPregrado.NombreProgramaOtorgaPre,
                modelo.DptoInstitutoPreSeg, modelo.CiudadInstitutoPreSeg, modelo._segundaEspecialidad.NombreProgramaOtorgaPre);

            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult FaseIII(Paso3Model modelo, string btnAceptaIII,
            HttpPostedFileBase txtFileSegunda, HttpPostedFileBase txtFileResolucionSegunda, HttpPostedFileBase txtFilePre)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            if (btnAceptaIII.Equals("siguiente"))
            {
                bool modeloValidado = true;
                try
                {
                    //Actualiza Solicitud                
                    tbSolicitud solActu = db.tbSolicitud.Find(usuario.solicitudActual.SolicitudId);
                    solActu.Observaciones = modelo.Observaciones;
                    solActu.acreditacionCalidad = modelo.acreditacionCalidad;

                    db.Entry(solActu).State = EntityState.Modified;
                    db.SaveChanges();

                    modelo.solicitudActual = solActu;
                    usuario.solicitudActual = solActu;

                    //Guarda Registro de solicitud  
                    //-- Datos Adicionales

                    if (validaDatosPersonalesModeloP3(modelo))
                        registraDatosAdicionales(modelo);
                    else
                        modeloValidado = false;

                    //-- programa
                    if (lstInstitucionesStatic != null)
                        if (lstInstitucionesStatic.Count > 0)
                            modelo._institucionList = lstInstitucionesStatic;

                    if (validaprogramaModeloP3(modelo))
                    {
                        registraPrograma(modelo, txtFileSegunda, txtFileResolucionSegunda);
                    }
                    else
                        modeloValidado = false;

                    //-- pregrado
                    if (solActu.Tipo.ToUpper().Equals("POSGRADO"))
                    {
                        if (validapreGradoModeloP3(modelo))
                            registraPregrado(modelo, txtFilePre);
                        else
                            modeloValidado = false;
                    }

                    if (modeloValidado)
                    {
                        solActu.Estado = 2;
                        db.Entry(solActu).State = EntityState.Modified;
                        db.SaveChanges();
                        usuario.solicitudActual = solActu;                       

                        return RedirectToAction("FaseIV", routeValues: new { solicitudId = solActu.SolicitudId });
                    }
                    else
                    {
                        refrescaModeloP3(ref modelo, usuario);
                        ModelState.AddModelError("", "Señor solicitante, por favor completar los campos obligatorios marcados con **");
                        return View(modelo);
                    }
                }
                catch (Exception ex)
                {
                    HttpException httpException = ex as HttpException;

                    int error1 = httpException != null ? httpException.GetHttpCode() : 0;
                    string mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    return RedirectToAction("Index", "Error", new { error = error1, msg = mensaje });
                }
            }
            else 
            {
                //Actualiza Solicitud                
                tbSolicitud solActu = db.tbSolicitud.Find(usuario.solicitudActual.SolicitudId);
                solActu.Observaciones = modelo.Observaciones;
                solActu.acreditacionCalidad = modelo.acreditacionCalidad;

                db.Entry(solActu).State = EntityState.Modified;
                db.SaveChanges();

                modelo.solicitudActual = solActu;
                usuario.solicitudActual = solActu;

                registraDatosAdicionales(modelo);                

                //-- programa
                if (lstInstitucionesStatic != null)
                    if (lstInstitucionesStatic.Count > 0)
                        modelo._institucionList = lstInstitucionesStatic;

                registraPrograma(modelo, txtFileSegunda, txtFileResolucionSegunda);

                //-- pregrado
                if (solActu.Tipo.ToUpper().Equals("POSGRADO"))
                    registraPregrado(modelo, txtFilePre);

                if (btnAceptaIII.Equals("atras"))
                    return RedirectToAction("FaseII", routeValues: new { id = usuario.solicitudActual.SolicitudId });
                else
                    return RedirectToAction("BandejaConvalidante");
            }            
        }

        public void llenaListaInstituciones(string[] paises, string[] intitutos, string[] institucionesText, string[] estados, string[] ciudades, string[] facultades, string[] ciudadesText)
        {
            lstInstitucionesStatic = new List<Institucion>();
            tbInstitucion tbInstLast = db.tbInstitucion.OrderByDescending(i => i.InstitucionId).FirstOrDefault();
            int maxId = (tbInstLast == null ? 0 : tbInstLast.InstitucionId) + 1;
            for(int i = 0; i< paises.Length; i++)
            {
                Institucion inst = new Institucion();
                inst.Pais = paises[i];
                inst.Instituto = intitutos[i];
                inst.InstitutoText = institucionesText[i];
                inst.Estado = estados[i];
                inst.Ciudad = ciudades[i];
                inst.CiudadText = ciudadesText[i];
                inst.Facultad = facultades[i];
                //inst.Instructivo = rutas[i];
                inst.institucionId = maxId++;

                lstInstitucionesStatic.Add(inst);
            }
        }

        public ActionResult FaseIV(int? solicitudId)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }
            Paso4Model modelo = new Paso4Model();

            modelo = llenaLaseIV((int)solicitudId);

            return View(modelo);
        }

        public Paso4Model llenaLaseIV(int solicitudId)
        {
            Paso4Model modelo = new Paso4Model();
            tbSolicitud solActu = db.tbSolicitud.Find(solicitudId);
            modelo.solicitudActual = solActu;

            List<tbDocumentos> lstDoctos = db.tbDocumentos.Where(d => d.solicitudId == solicitudId).ToList();
            foreach (tbDocumentos doctos in lstDoctos)
            {
                switch (doctos.tipoDoc)
                {
                    case 0:
                        modelo.diplomaPosgrado = doctos.nombre;
                        modelo.diplomaPosgradoTraducido = doctos.nombreTraduccion;
                        break;
                    case 1:
                        modelo.tituloPregrado = doctos.nombre;
                        modelo.tituloPregradoTraducido = doctos.nombreTraduccion;
                        break;
                    case 2:
                        modelo.certificadoAsignaturas = doctos.nombre;
                        modelo.certificadoAsignaturasTraducido = doctos.nombreTraduccion;
                        break;
                    case 3:
                        modelo.documentoIdentidad = doctos.nombre;
                        modelo.documentoIdentidadTraducido = doctos.nombreTraduccion;
                        break;
                    case 4:
                        modelo.certificadoProgramapre = doctos.nombre;
                        modelo.certificadoProgramapreTraducido = doctos.nombreTraduccion;
                        break;
                    case 5:
                        modelo.formatoInvestigacion = doctos.nombre;
                        modelo.formatoInvestigacionTraducido = doctos.nombreTraduccion;
                        break;
                    case 6:
                        modelo.actaSustentacion = doctos.nombre;
                        modelo.actaSustentacionTraducido = doctos.nombreTraduccion;
                        break;
                    case 7:
                        modelo.tesisMaestrias = doctos.nombre;
                        modelo.tesisMaestriasTraducido = doctos.nombreTraduccion;
                        break;
                    case 8:
                        modelo.tituloEspecialidad = doctos.nombre;
                        modelo.tituloEspecialidadTraducido = doctos.nombreTraduccion;
                        break;
                    case 9:
                        modelo.recordProcedimientos = doctos.nombre;
                        modelo.recordProcedimientosTraducido = doctos.nombreTraduccion;
                        break;
                    case 10:
                        modelo.certificadoActividadesAcademicas = doctos.nombre;
                        modelo.certificadoActividadesAcademicasTraducido = doctos.nombreTraduccion;
                        break;
                    case 11:
                        modelo.certificadoCalificacionesPos = doctos.nombre;
                        modelo.certificadoCalificacionesPosTraducido = doctos.nombreTraduccion;
                        break;
                    case 15:
                        modelo.certificadoPrograma = doctos.nombre;
                        modelo.certificadoProgramaTraducido = doctos.nombreTraduccion;
                        break;
                    case 16:
                        modelo.requisitoEspecialSalud = doctos.nombre;
                        modelo.requisitoEspecialSaludTraducido = doctos.nombreTraduccion;
                        break;
                    case 17:
                        modelo.requisitoEspecialContaduria = doctos.nombre;
                        modelo.requisitoEspecialContaduriaTraducido = doctos.nombreTraduccion;
                        break;
                    case 18:
                        modelo.requisitoEspecialDerecho = doctos.nombre;
                        modelo.requisitoEspecialDerechoTraducido = doctos.nombreTraduccion;
                        break;
                    case 19:
                        modelo.requisitoEspecialEducacion = doctos.nombre;
                        modelo.requisitoEspecialEducacionTraducido = doctos.nombreTraduccion;
                        break;
                    case 20:
                        modelo.convalidacionPregrado = doctos.nombre;
                        modelo.convalidacionPregradoTraducido = doctos.nombreTraduccion;
                        break;
                    case 21:
                        modelo.convalidacionEspecialidadBase = doctos.nombre;
                        modelo.convalidacionEspecialidadBaseTraducido = doctos.nombreTraduccion;
                        break;
                    case 22:
                        modelo.documentoAdicional1 = doctos.nombre;
                        break;
                    case 23:
                        modelo.documentoAdicional2 = doctos.nombre;
                        break;
                    case 24:
                        modelo.documentoAdicional3 = doctos.nombre;
                        break;
                    case 25:
                        modelo.documentoAdicional4 = doctos.nombre;
                        break;
                    case 26:
                        modelo.documentoAdicional5 = doctos.nombre;
                        break;
                }
            }

            return modelo;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult FaseIV(Paso4Model modelo,
            HttpPostedFileBase fileDocumentoIdentidadPos, HttpPostedFileBase fileDocumentoIdentidadPostraducido,
            HttpPostedFileBase filediplomaPosgrado, HttpPostedFileBase filediplomaPosgradotraducido,
            HttpPostedFileBase fileCertificadoAsignaturas, HttpPostedFileBase fileCertificadoAsignaturastraducido,
            //pregrado
            HttpPostedFileBase fileCertificadoPrograma, HttpPostedFileBase fileCertificadoProgramatraducido,
            HttpPostedFileBase fileRequisitoEspecialSalud, HttpPostedFileBase fileRequisitoEspecialSaludtraducido,
            HttpPostedFileBase fileRequisitoEspecialDerecho, HttpPostedFileBase fileRequisitoEspecialDerechoTraducido,
            HttpPostedFileBase fileRequisitoEspecialContaduria, HttpPostedFileBase fileRequisitoEspecialContaduriatraducido,
            HttpPostedFileBase fileRequisitoEspecialEducacion, HttpPostedFileBase fileRequisitoEspecialEducaciontraducido,
            //posgrado
            HttpPostedFileBase fileTituloPregrado, HttpPostedFileBase fileTituloPregradotraducido,
            HttpPostedFileBase fileFormatoInvestigacion, HttpPostedFileBase fileFormatoInvestigaciontraducido,
            HttpPostedFileBase fileActaSustentacion, HttpPostedFileBase fileActaSustentaciontraducido,
            HttpPostedFileBase fileTesisMaestrias, HttpPostedFileBase fileTesisMaestriastraducido,
            HttpPostedFileBase fileTituloEspecialidad, HttpPostedFileBase fileTituloEspecialidadtraducido,
            HttpPostedFileBase fileRecordProcedimientos, HttpPostedFileBase fileRecordProcedimientostraducido,
            HttpPostedFileBase fileCertificadoActividadesAcademicas, HttpPostedFileBase fileCertificadoActividadesAcademicastraducido,
            HttpPostedFileBase fileConvalidacionPregrado, HttpPostedFileBase fileConvalidacionPregradotraducido,
            HttpPostedFileBase fileConvalidacionEspecialidadBase, HttpPostedFileBase fileConvalidacionEspecialidadBasetraducido,
            //Adicionales
            HttpPostedFileBase fileDocumentoAdicional1, HttpPostedFileBase fileDocumentoAdicional2,
            HttpPostedFileBase fileDocumentoAdicional3, HttpPostedFileBase fileDocumentoAdicional4,
            HttpPostedFileBase fileDocumentoAdicional5)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            if (validaFase4(modelo))
            {
                try
                {
                    //Elimina registro de documentos anteriores
                    List<tbDocumentos> lstDoctos = db.tbDocumentos.Where(d => d.solicitudId == modelo.solicitudActual.SolicitudId).ToList();
                    if (lstDoctos.Count > 0)
                        db.tbDocumentos.RemoveRange(lstDoctos);

                    // Comunes
                    guardaDocumentos(modelo, fileDocumentoIdentidadPos, fileDocumentoIdentidadPostraducido, (int)nombreDocumentos.documentoIdentidad, "001");
                    guardaDocumentos(modelo, filediplomaPosgrado, filediplomaPosgradotraducido, (int)nombreDocumentos.diplomaPosgrado, "002");
                    guardaDocumentos(modelo, fileCertificadoAsignaturas, fileCertificadoAsignaturastraducido, (int)nombreDocumentos.certificadoAsignaturas, "019");
                    guardaDocumentos(modelo, fileCertificadoPrograma, fileCertificadoProgramatraducido, (int)nombreDocumentos.certificadoPrograma, "005");

                    if (usuario.solicitudActual.Tipo.ToUpper().Equals("PREGRADO"))
                    {
                        //Pregrado                        
                        guardaDocumentos(modelo, fileRequisitoEspecialSalud, fileRequisitoEspecialSaludtraducido, (int)nombreDocumentos.requisitoEspecialSalud, "013");
                        guardaDocumentos(modelo, fileRequisitoEspecialDerecho, fileRequisitoEspecialDerechoTraducido, (int)nombreDocumentos.requisitoEspecialDerecho, "014");
                        guardaDocumentos(modelo, fileRequisitoEspecialContaduria, fileRequisitoEspecialContaduriatraducido, (int)nombreDocumentos.requisitoEspecialContaduria, "015");
                        guardaDocumentos(modelo, fileRequisitoEspecialEducacion, fileRequisitoEspecialEducaciontraducido, (int)nombreDocumentos.requisitoEspecialEducacion, "016");
                    }
                    else
                    {
                        //posgrado
                        guardaDocumentos(modelo, fileTituloPregrado, fileTituloPregradotraducido, (int)nombreDocumentos.tituloPregrado, "004");
                        guardaDocumentos(modelo, fileFormatoInvestigacion, fileFormatoInvestigaciontraducido, (int)nombreDocumentos.formatoInvestigacion, "006");
                        guardaDocumentos(modelo, fileActaSustentacion, fileActaSustentaciontraducido, (int)nombreDocumentos.actaSustentacion, "007");
                        guardaDocumentos(modelo, fileTesisMaestrias, fileTesisMaestriastraducido, (int)nombreDocumentos.tesisMaestrias, "008");
                        guardaDocumentos(modelo, fileTituloEspecialidad, fileTituloEspecialidadtraducido, (int)nombreDocumentos.tituloEspecialidad, "009");
                        guardaDocumentos(modelo, fileRecordProcedimientos, fileRecordProcedimientostraducido, (int)nombreDocumentos.recordProcedimientos, "010");
                        guardaDocumentos(modelo, fileCertificadoActividadesAcademicas, fileCertificadoActividadesAcademicastraducido, (int)nombreDocumentos.certificadoActividadesAcademicas, "011");
                        guardaDocumentos(modelo, fileConvalidacionPregrado, fileConvalidacionPregradotraducido, (int)nombreDocumentos.convalidacionPregrado, "017");
                        guardaDocumentos(modelo, fileConvalidacionEspecialidadBase, fileConvalidacionEspecialidadBasetraducido, (int)nombreDocumentos.convalidacionEspecialidadBase, "018");
                    }

                    //Adicionales
                    guardaDocumentos(modelo, fileDocumentoAdicional1, null, (int)nombreDocumentos.documentoAdicional1, "020");
                    guardaDocumentos(modelo, fileDocumentoAdicional2, null, (int)nombreDocumentos.documentoAdicional2, "021");
                    guardaDocumentos(modelo, fileDocumentoAdicional3, null, (int)nombreDocumentos.documentoAdicional3, "022");
                    guardaDocumentos(modelo, fileDocumentoAdicional4, null, (int)nombreDocumentos.documentoAdicional4, "023");
                    guardaDocumentos(modelo, fileDocumentoAdicional5, null, (int)nombreDocumentos.documentoAdicional5, "024");

                    db.SaveChanges();

                    ////Solicitud                
                    //tbSolicitud solActu = db.tbSolicitud.Find(usuario.solicitudActual.SolicitudId);

                    return RedirectToAction("Resumen", routeValues: new { solicitudId = modelo.solicitudActual.SolicitudId });
                }
                catch (Exception ex)
                {
                    HttpException httpException = ex as HttpException;

                    int error1 = httpException != null ? httpException.GetHttpCode() : 0;
                    string mensaje = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    return RedirectToAction("Index", "Error", new { error = error1, msg = mensaje });
                }
            }

            tbSolicitud solActu1 = db.tbSolicitud.Find(modelo.solicitudActual.SolicitudId);
            modelo.solicitudActual = solActu1;
            ModelState.AddModelError("", "Señor solicitate existen campos obligatorios sin completar, por favor revise el formulario y complete la información faltante.");
            return View(modelo);
        }

        public bool cargaArchivo4(string fileUpload)
        {
            bool resp = true;

            return resp;
        }

        public void guardaDocumentos(Paso4Model modelo, HttpPostedFileBase archivoAguardar, HttpPostedFileBase archivoTraducidoAguardar, int tipoDoc, string codigoDoc)
        {
            try
            {
                string rutaOriginal = string.Empty;
                string rutaTraduccion = string.Empty;
                string nombreDocto = string.Empty;
                string prefijo = modelo.solicitudActual.SolicitudId.ToString() + "_" + tipoDoc.ToString() + "_";
                bool tieneArchivo = true;

                switch (codigoDoc)
                {
                    case "001":
                        rutaOriginal = modelo.documentoIdentidad;
                        rutaTraduccion = modelo.documentoIdentidadTraducido;
                        nombreDocto = nombreDocumentos.documentoIdentidad.ToString();
                        break;
                    case "002":
                        rutaOriginal = modelo.diplomaPosgrado;
                        rutaTraduccion = modelo.diplomaPosgradoTraducido;
                        nombreDocto = nombreDocumentos.diplomaPosgrado.ToString();
                        break;
                    case "004":
                        rutaOriginal = modelo.tituloPregrado;
                        rutaTraduccion = modelo.tituloPregradoTraducido;
                        nombreDocto = nombreDocumentos.tituloPregrado.ToString();
                        break;
                    case "005":
                        rutaOriginal = modelo.certificadoPrograma;
                        rutaTraduccion = modelo.certificadoProgramaTraducido;
                        nombreDocto = nombreDocumentos.certificadoPrograma.ToString();
                        break;
                    case "006":
                        rutaOriginal = modelo.formatoInvestigacion;
                        rutaTraduccion = modelo.formatoInvestigacionTraducido;
                        nombreDocto = nombreDocumentos.formatoInvestigacion.ToString();
                        break;
                    case "007":
                        rutaOriginal = modelo.actaSustentacion;
                        rutaTraduccion = modelo.actaSustentacionTraducido;
                        nombreDocto = nombreDocumentos.actaSustentacion.ToString();
                        break;
                    case "008":
                        rutaOriginal = modelo.tesisMaestrias;
                        rutaTraduccion = modelo.tesisMaestriasTraducido;
                        nombreDocto = nombreDocumentos.tesisMaestrias.ToString();
                        break;
                    case "009":
                        rutaOriginal = modelo.tituloEspecialidad;
                        rutaTraduccion = modelo.tituloEspecialidadTraducido;
                        nombreDocto = nombreDocumentos.tituloEspecialidad.ToString();
                        break;
                    case "010":
                        rutaOriginal = modelo.recordProcedimientos;
                        rutaTraduccion = modelo.recordProcedimientosTraducido;
                        nombreDocto = nombreDocumentos.recordProcedimientos.ToString();
                        break;
                    case "011":
                        rutaOriginal = modelo.certificadoActividadesAcademicas;
                        rutaTraduccion = modelo.certificadoActividadesAcademicasTraducido;
                        nombreDocto = nombreDocumentos.certificadoActividadesAcademicas.ToString();
                        break;
                    case "013":
                        rutaOriginal = modelo.requisitoEspecialSalud;
                        rutaTraduccion = modelo.requisitoEspecialSaludTraducido;
                        nombreDocto = nombreDocumentos.requisitoEspecialSalud.ToString();
                        break;
                    case "014":
                        rutaOriginal = modelo.requisitoEspecialDerecho;
                        rutaTraduccion = modelo.requisitoEspecialDerechoTraducido;
                        nombreDocto = nombreDocumentos.requisitoEspecialDerecho.ToString();
                        break;
                    case "015":
                        rutaOriginal = modelo.requisitoEspecialContaduria;
                        rutaTraduccion = modelo.requisitoEspecialContaduriaTraducido;
                        nombreDocto = nombreDocumentos.requisitoEspecialContaduria.ToString();
                        break;
                    case "016":
                        rutaOriginal = modelo.requisitoEspecialEducacion;
                        rutaTraduccion = modelo.requisitoEspecialEducacionTraducido;
                        nombreDocto = nombreDocumentos.requisitoEspecialEducacion.ToString();
                        break;
                    case "017":
                        rutaOriginal = modelo.convalidacionPregrado;
                        rutaTraduccion = modelo.convalidacionPregradoTraducido;
                        nombreDocto = nombreDocumentos.convalidacionPregrado.ToString();
                        break;
                    case "018":
                        rutaOriginal = modelo.convalidacionEspecialidadBase;
                        rutaTraduccion = modelo.convalidacionEspecialidadBaseTraducido;
                        nombreDocto = nombreDocumentos.convalidacionEspecialidadBase.ToString();
                        break;
                    case "019":
                        rutaOriginal = modelo.certificadoAsignaturas;
                        rutaTraduccion = modelo.certificadoAsignaturasTraducido;
                        nombreDocto = nombreDocumentos.certificadoAsignaturas.ToString();
                        break;
                    case "020":
                        rutaOriginal = modelo.documentoAdicional1;
                        rutaTraduccion = "";
                        nombreDocto = nombreDocumentos.documentoAdicional1.ToString();
                        break;
                    case "021":
                        rutaOriginal = modelo.documentoAdicional2;
                        rutaTraduccion = "";
                        nombreDocto = nombreDocumentos.documentoAdicional2.ToString();
                        break;
                    case "022":
                        rutaOriginal = modelo.documentoAdicional3;
                        rutaTraduccion = "";
                        nombreDocto = nombreDocumentos.documentoAdicional3.ToString();
                        break;
                    case "023":
                        rutaOriginal = modelo.documentoAdicional4;
                        rutaTraduccion = "";
                        nombreDocto = nombreDocumentos.documentoAdicional4.ToString();
                        break;
                    case "024":
                        rutaOriginal = modelo.documentoAdicional5;
                        rutaTraduccion = "";
                        nombreDocto = nombreDocumentos.documentoAdicional5.ToString();
                        break;
                }

                if (string.IsNullOrEmpty(rutaOriginal) && string.IsNullOrEmpty(rutaTraduccion))
                    tieneArchivo = false;

                if (tieneArchivo)
                {
                    rutaOriginal = string.IsNullOrEmpty(rutaOriginal) ? string.Empty : rutaOriginal.Replace(prefijo, "");
                    rutaTraduccion = string.IsNullOrEmpty(rutaTraduccion) ? string.Empty : rutaTraduccion.Replace(prefijo, "");

                    rutaOriginal = string.IsNullOrEmpty(rutaOriginal) ? string.Empty : prefijo + rutaOriginal;
                    rutaTraduccion = string.IsNullOrEmpty(rutaTraduccion) ? string.Empty : prefijo + rutaTraduccion;

                    string pathOri = string.Empty;
                    string pathTra = string.Empty;

                    if (archivoAguardar != null && !string.IsNullOrEmpty(rutaOriginal))
                    {
                        string path = Server.MapPath("~/Archivos/Documentos/Originales/");
                        archivoAguardar.SaveAs(path + rutaOriginal);
                        
                    }

                    if (archivoTraducidoAguardar != null && !string.IsNullOrEmpty(rutaTraduccion))
                    {
                        string path = Server.MapPath("~/Archivos/Documentos/Traducidos/");
                        archivoTraducidoAguardar.SaveAs(path + rutaTraduccion);
                        
                    }

                    pathOri = string.IsNullOrEmpty(rutaOriginal) ? "" : "http://192.168.32.199:9090/Archivos/Documentos/Originales/" + rutaOriginal;
                    pathTra = string.IsNullOrEmpty(rutaTraduccion) ? "" : "http://192.168.32.199:9090/Archivos/Documentos/Traducidos/" + rutaTraduccion;

                    tbDocumentos doc = new tbDocumentos();
                    doc.solicitudId = modelo.solicitudActual.SolicitudId;
                    doc.tipoDoc = tipoDoc;
                    doc.nombre = rutaOriginal;
                    doc.nombreTraduccion = rutaTraduccion;
                    doc.nombreDoc = nombreDocto;
                    doc.codigoDoc = codigoDoc;
                    doc.rutaFisica = pathOri;
                    doc.sharepoint = pathTra;

                    db.tbDocumentos.Add(doc);
                }
                    
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }
        public ActionResult Resumen(int? solicitudId)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            Paso3Model modelo = new Paso3Model();
            modelo = llenaSolicitud(solicitudId, usuario);
            Documentos docs = new Documentos();
            modelo.lstDocumentos = docs.convertTbDocumento(db.tbDocumentos.Where(d => d.solicitudId == solicitudId).ToList());
            return View(modelo);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Resumen(Paso3Model _modelo, string btnResumen)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            int solicitudId = _modelo.solicitudActual.SolicitudId;
            if (btnResumen.Equals("pago"))
            {
                Paso3Model modelo = new Paso3Model();
                modelo = llenaSolicitud(solicitudId, usuario);
                Documentos docs = new Documentos();
                modelo.lstDocumentos = docs.convertTbDocumento(db.tbDocumentos.Where(d => d.solicitudId == solicitudId).ToList());                

                tbSolicitud solAct = db.tbSolicitud.Find(solicitudId);
                //GESTOR DOCUMENTAL
                ConsumeServicioSGD servSGD = new ConsumeServicioSGD(modelo);
                string respGestor = servSGD.RadicaDocumento();

                if (!respGestor.ToUpper().Contains("ERROR"))
                {
                    solAct.Radicado = respGestor; 

                    modelo.solicitudActual = solAct;

                    ConsumeServicioBizagi serv = new ConsumeServicioBizagi(modelo);
                    string resp = serv.creaSolicitud();

                    if (string.IsNullOrEmpty(resp))
                    {
                        solAct.Estado = 5;

                        db.Entry(solAct).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Respuesta", routeValues: new { solicitudId = solicitudId, radicado = solAct.Radicado, tipo = 0 });
                    }
                    else
                    {
                        return RedirectToAction("Respuesta", routeValues: new { solicitudId = solicitudId, radicado = resp, tipo = 1 });
                    }
                }
                else
                {
                    return RedirectToAction("Respuesta", routeValues: new { solicitudId = solicitudId, radicado = respGestor, tipo = 1 });
                }
            }
            else
            {
                tbSolicitud solAct = db.tbSolicitud.Find(solicitudId);
                solAct.Estado = 3;

                db.Entry(solAct).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Respuesta", routeValues: new { solicitudId = solicitudId, radicado = "Pediente", tipo = 2 });
            }
        }

        public string llamaPaginaPago(string solId)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            try
            {
                string urlPagos = WebConfigurationManager.AppSettings["urlPagos"].ToString();
                string urlRespuestaPagos = WebConfigurationManager.AppSettings["urlRespuestaPagos"].ToString() + solId;
                string codServ = usuario.solicitudActual.Tipo.ToUpper().Equals("POSGRADO") ? "1002" : "1001";
                var product = new PagoRequest
                {
                    codAplicacion = "001",
                    codServicio = codServ, // pregrado = 1001; posgrado = 1002
                    referenciaPago = solId, // se asigna por nosotros. Identificacion unica del pago
                    tipoDocumento = "CC",
                    noDocumento = "12345", //12345, 123456, 987654321. Documento del Ususario
                    nombre1 = usuario.datosUsuario.primerNombre,
                    nombre2 = usuario.datosUsuario.segundoNombre,
                    Apellido1 = usuario.datosUsuario.primerApellido,
                    Apellido2 = usuario.datosUsuario.segundoApellido,
                    email = usuario.datosUsuario.email,
                    urlRedirectOri = urlRespuestaPagos
                };

                string json = JsonConvert.SerializeObject(product);
                byte[] data = UTF8Encoding.UTF8.GetBytes(json);


                HttpWebRequest request;
                request = WebRequest.Create(urlPagos) as HttpWebRequest;
                request.Timeout = 10 * 1000;
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/json; charset=utf-8";

                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string body = reader.ReadToEnd();

                string respData = Json(body).Data.ToString();
                PagoResponse req = JsonConvert.DeserializeObject<PagoResponse>(respData);
                return req.URLRedirect;
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ActionResult Respuesta(int? solicitudId, string radicado, int? tipo)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            if (tipo == 1)
            {
                ViewBag.Respuesta = "Error en el servicio.";
                ViewBag.Radicado = radicado;
            }
            else
            {
                if (tipo == 2)
                {
                    ViewBag.Respuesta = "solicitud de convalidación Pendiente de Pago.";
                    ViewBag.Radicado = "Tiene 30 días a partir de la fecha de radicación de la solicitud para efectuar el pago.";
                }
                else
                {
                    ViewBag.Respuesta = "solicitud de convalidación en trámite.";
                    ViewBag.Radicado = "Número de Radicado: " + radicado;
                }                
            }
            
            ViewBag.Solicitud = solicitudId;
            return View();
        }

        private void refrescaModeloP3(ref Paso3Model modelo, LoginModel usuario)
        {
            //autorizado
            if(modelo._autorizado == null)
                modelo._autorizado = new Autorizado();

            modelo._autorizado.TipoDocumentoAutorizadoCod = modelo.tipoDoc;
            modelo._autorizado.Pais = "COLOMBIA";
            modelo._autorizado.Departamento = modelo.DepartamentoNotificacion;
            modelo._autorizado.Ciudad = modelo.CiudadNotificacion;


            //programa
            modelo._programa.CampoAmplioConocimiento = (int)modelo.CampoAmplioConocimiento;
            modelo._programa.TipoEducacionSuperior = modelo.TipoEducacionSuperior;
            modelo._programa.ModalidadPrograma = modelo.ModalidadPrograma;

            if (modelo.solicitudActual.Tipo.ToUpper().Equals("POSGRADO"))
                modelo.estilo = "display:inline";
            else
                modelo.estilo = "display:none";

            if (modelo.TipoEducacionSuperior.Equals("2"))
                modelo._programa.TipoMaestria = modelo.TipoMaestria;
            else
                modelo._programa.TipoMaestria = string.Empty;

            modelo._programa.SegundaEspecialidad = modelo.segundaEspecialidadSalud; 
            modelo._programa.EspecialidadMedicaSalud = modelo.EspecialidadMedicaSalud;

            modelo._programa.NombrePrograma = modelo.NombrePrograma;

            if (modelo._infoPregrado is null)
                modelo._infoPregrado = new InfoPregrado();

            modelo._infoPregrado.Instituto = modelo.NombreInstitucionOtorgaPre;
            modelo._infoPregrado.NombreProgramaOtorgaPre = modelo.NombreProgramaOtorgaPre;
            modelo._infoPregrado.Entidad = modelo._infoPregrado_Entidad;

            modelo._segundaEspecialidad.Entidad = modelo._segundaEsp_Entidad;
            modelo._segundaEspecialidad.NombreProgramaOtorgaPre = modelo.NombreProgramaOtorgaPreSeg;
            //modelo._programa.TituloObtenido = modelo._programa.TituloObtenido;

            //Opciones de display

            if ((bool)modelo.solicitudActual.notificacionElectronica)
            {
                modelo.displayNotificaElectronica = "display:none";
            }
            else
            {
                modelo.displayNotificaElectronica = "display:inline";
                if ((bool)modelo.solicitudActual.notificaTercero)
                {
                    modelo.displayAutorizado = "display:inline";
                    modelo.displayPropio = "display:none";
                }
                else
                {
                    modelo.displayAutorizado = "display:none";
                    modelo.displayPropio = "display:inline";
                }
            }

            if(modelo.Pais == 0)
            {
                modelo.PaisDisplay = "display:inline";
                modelo.PaisDisplay_II = "display:none";
            }
            else
            {
                if (modelo.Pais == 170)
                {
                    modelo.PaisDisplay = "display:inline";
                    modelo.PaisDisplay_II = "display:none";
                }
                else
                {
                    modelo.PaisDisplay = "display:none";
                    modelo.PaisDisplay_II = "display:inline";
                }
            }

            //Obtenemos codigo salud
            string campoSaludId = WebConfigurationManager.AppSettings["campoSaludId"].ToString();
            modelo.areaSaludParam = int.Parse(campoSaludId);

            if (modelo._programa.CampoAmplioConocimiento == modelo.areaSaludParam)
            {
                modelo.displayValidacionInstituto = "display:none";
            }
            else
            {
                if (modelo.solicitudActual.Tipo.ToUpper().Equals("PREGRADO"))
                {
                    if (modelo._programa.CampoAmplioConocimiento == 41 || modelo._programa.CampoAmplioConocimiento == 42 || modelo._programa.CampoAmplioConocimiento == 11)
                    {
                        modelo.displayValidacionInstituto = "display:none";
                    }
                    else
                    {
                        modelo.displayValidacionInstituto = "display:inline";
                    }
                }
                else
                {
                    modelo.displayValidacionInstituto = "display:inline";
                }
            }

            if (modelo._programa.CampoAmplioConocimiento == modelo.areaSaludParam && modelo.solicitudActual.Tipo.ToUpper().Equals("POSGRADO"))
                modelo.areaSalud = "display:inline";
            else
                modelo.areaSalud = "display:none";

            if (modelo._programa.TipoEducacionSuperior.Equals("2"))
                modelo.esMaestria = "display:inline";
            else
                modelo.esMaestria = "display:none";

            if (modelo._programa.NombrePrograma == "0")
                modelo.displayNombrePrograma = "display:inline";
            else
                modelo.displayNombrePrograma = "display:none";

            if (modelo._segundaEspecialidad.Instituto == "0")
                modelo.displayInstitucionSalud = "display:inline";
            else
                modelo.displayInstitucionSalud = "display:none";

            if (modelo._infoPregrado.Instituto == "0")
                modelo.displayInstitucionPre = "display:inline";
            else
                modelo.displayInstitucionPre = "display:none";

            if (modelo.segundaEspecialidadSalud)
                modelo.SegundaEspecialidadSaludDisplay = "display:inline";
            else
                modelo.SegundaEspecialidadSaludDisplay = "display:none";

            if (modelo.TipoEducacionSuperior.Equals("1") || (modelo.TipoEducacionSuperior.Equals("2")  && modelo.TipoMaestria.Equals("1")))
                modelo.EspecialidadMedicaSaludDisplay = "display:inline";
            else
                modelo.EspecialidadMedicaSaludDisplay = "display:none";

            if (modelo.segundaEspecialidadSaludNal)
            {
                modelo.SegundaEspecialidadSaludDisplaySI = "display:inline";
                modelo.SegundaEspecialidadSaludDisplayNO = "display:none";
            }
            else
            {
                modelo.SegundaEspecialidadSaludDisplaySI = "display:none";
                modelo.SegundaEspecialidadSaludDisplayNO = "display:inline";
            }

            if (lstInstitucionesStatic != null)
                if (lstInstitucionesStatic.Count > 0)
                    modelo._institucionList = lstInstitucionesStatic;

            bool esPost = modelo.solicitudActual.Tipo == "POSGRADO" ? true : false;
            int dpto = string.IsNullOrEmpty(modelo.Departamento) ? 0 : int.Parse(modelo.Departamento);
            int dptoAutor = string.IsNullOrEmpty(modelo._autorizado.Departamento) ? 0 : int.Parse(modelo._autorizado.Departamento);
            combosP2(esPost,modelo.Pais.ToString(), dpto, modelo.Ciudad, dptoAutor, modelo.NombreInstitucion, //ojo
                modelo._infoPregrado.Instituto, modelo._infoPregrado.Entidad, modelo.CampoAmplioConocimiento, 
                modelo._programa.ModalidadPrograma, modelo._programa.TipoDuracion, modelo._programa.TipoEducacionSuperior,
                modelo._programa.TipoMaestria, modelo._programa.NombrePrograma, modelo.PaisInstituto, //ojo 
                modelo._segundaEspecialidad.Instituto, modelo._autorizado.TipoDocumento, modelo._segundaEspecialidad.Entidad,
                modelo.DptoInstitutoPre, modelo.CiudadInstitutoPre, modelo._infoPregrado.NombreProgramaOtorgaPre,
                modelo.DptoInstitutoPreSeg, modelo.CiudadInstitutoPreSeg, modelo._segundaEspecialidad.NombreProgramaOtorgaPre);
        }

        public string guardaSolicitud(int solicidutId)
        {
            string resp = "sii";
            return resp;
        }

        public string buscaInstructivo(string pais)
        {
            string resp = "";
            try
            {
                tbValidaInstitucion rutaValida = db.tbValidaInstitucion.Where(v => v.Pais.ToUpper().Equals(pais.ToUpper())).FirstOrDefault();
                if (rutaValida == null)
                    resp = "Señor solicitante, el país origen del título a convalidar posiblemente no cuenta con un sistema de acreditación o reconocimiento de alta calidad.";
                else
                    resp = rutaValida.linkVerificacion;
            }catch(Exception ex)
            {
                resp = string.IsNullOrEmpty(ex.InnerException.Message) ? ex.Message : ex.InnerException.Message;
                resp = "Señor solicitante, se ha presentado el siguiente error: " + resp; 
            }
            

            return resp;
        }

        #region Registro en base de datos

        public void registraDatosAdicionales(Paso3Model modelo)
        {
            try
            {
                tbDatosContacto datosAdicionalesPrevios = db.tbDatosContacto.Where(d => d.Solicitud == modelo.solicitudActual.SolicitudId).FirstOrDefault();
                if (datosAdicionalesPrevios != null)
                    db.tbDatosContacto.Remove(datosAdicionalesPrevios);

                tbDatosContacto datosAdicionales = new tbDatosContacto();
                datosAdicionales.Solicitud = modelo.solicitudActual.SolicitudId;
                if (modelo.TipoDocumento.ToUpper().Equals("CE"))
                {
                    datosAdicionales.ciudadExpedicion = modelo.CiudadExpedicion;
                    datosAdicionales.paisExpedicion = modelo.PaisExpedicion;
                }

                //if (!modelo.TipoDocumento.ToUpper().Equals("CC"))
                //{
                //    datosAdicionales.ciudadExpedicionCC = modelo.CiudadExpedicionCC;
                //    datosAdicionales.dptoExpedicionCC = modelo.DeptoExpedicion;
                //}

                if ((bool)modelo.solicitudActual.notificacionElectronica)
                {
                    datosAdicionales.direccionResidencia = modelo.Direccion;
                    datosAdicionales.codigoPostalResidencia = modelo.CodigoPostal;
                    datosAdicionales.paisResidencia = modelo.Pais.ToString();
                    datosAdicionales.departamentoResidencia = modelo.Departamento;

                    if (modelo.Pais == 170)
                        datosAdicionales.ciudadResidencia = modelo.Ciudad;
                    else
                        datosAdicionales.ciudadResidencia = modelo.Ciudad_II;

                    datosAdicionales.email = modelo.Email;
                    datosAdicionales.emailOpcional = modelo.Email2;
                    datosAdicionales.celularPropio = modelo.Celular.ToString();
                    datosAdicionales.telefonoPropio = modelo.TelefonoFijo.ToString();
                }
                else
                {
                    if ((bool)modelo.solicitudActual.notificaTercero)
                    {
                        //datosAdicionales.NombreCompletoAutorizado = modelo._autorizado.NombreCompleto;
                        datosAdicionales.TipoDocumentoAutorizadoCod = modelo.tipoDoc;// db.tbTipoDocumento.Where(t => t.TipoDocumento.Equals(modelo.tipoDoc)).FirstOrDefault().itpoDocumentoId;
                        datosAdicionales.NumeroDocumentoAutorizado = modelo._autorizado.NumeroDocumento;
                        datosAdicionales.nombre1Autorizado = modelo._autorizado.Nombre1Aut;
                        datosAdicionales.nombre2Autorizado = modelo._autorizado.Nombre2Aut;
                        datosAdicionales.apellido1Autorizado = modelo._autorizado.apellido1Aut;
                        datosAdicionales.apellido2Autorizado = modelo._autorizado.apellido2Aut;
                    }

                    datosAdicionales.DireccionAutorizado = modelo._autorizado.Direccion;
                    datosAdicionales.CodigoPostalAutorizado = modelo._autorizado.CodigoPostal;
                    datosAdicionales.PaisAutorizado = "COLOMBIA"; // modelo._autorizado.Pais;
                    datosAdicionales.DepartamentoAutorizado = modelo.DepartamentoNotificacion;
                    datosAdicionales.CiudadAutorizado = modelo.CiudadNotificacion;
                    //if (modelo._autorizado.TelefonoFijo == null)
                    //    modelo._autorizado.TelefonoFijo = 0;
                    datosAdicionales.TelefonoAutorizado = modelo._autorizado.TelefonoFijo.ToString();
                    //if (modelo._autorizado.Celular == null)
                    //    modelo._autorizado.Celular = 0;
                    datosAdicionales.CelularAutorizado = modelo._autorizado.Celular.ToString();
                }
                
                db.tbDatosContacto.Add(datosAdicionales);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registraPrograma(Paso3Model modelo, HttpPostedFileBase txtFileSegunda, HttpPostedFileBase txtFileResolucionSegunda)
        {
            try
            {
                //Datos de Institucion
                List<tbInstitucion> institucionActual = db.tbInstitucion.Where(i => i.Solicitud == modelo.solicitudActual.SolicitudId).ToList();
                if (institucionActual != null)
                    db.tbInstitucion.RemoveRange(institucionActual);

                Institucion inst = new Institucion();

                foreach(Institucion instructivoInsti in modelo._institucionList)
                {
                    instructivoInsti.Instructivo = buscaInstructivo(instructivoInsti.Pais);
                }

                List<tbInstitucion> institucionesNew = inst.convertListInstituto(modelo._institucionList);
                institucionesNew.ToList().ForEach(x => x.Solicitud = modelo.solicitudActual.SolicitudId);

                db.tbInstitucion.AddRange(institucionesNew);

                //Datos de Programa
                tbPrograma programaActual = db.tbPrograma.Where(i => i.Solicitud == modelo.solicitudActual.SolicitudId).FirstOrDefault();
                if (programaActual != null)
                    db.tbPrograma.Remove(programaActual);

                modelo._programa.TipoEducacionSuperior = modelo.TipoEducacionSuperior;
                modelo._programa.TipoMaestria = modelo.TipoMaestria;
                modelo._programa.TipoDuracion = modelo.TipoDuracion;
                modelo._programa.ModalidadPrograma = modelo.ModalidadPrograma;
                modelo._programa.CampoAmplioConocimiento = modelo.CampoAmplioConocimiento;
                modelo._programa.SegundaEspecialidad = modelo.segundaEspecialidadSalud;
                modelo._programa.SegundaEspecialidadNal = modelo.segundaEspecialidadSaludNal;
                if(modelo.NombrePrograma.Equals("0"))
                    modelo._programa.TituloObtenido = modelo._programa.TituloObtenidoManual;
                else
                    modelo._programa.TituloObtenido = modelo._programa.TituloObtenido;
                modelo._programa.EspecialidadMedicaSalud = modelo.EspecialidadMedicaSalud;

                //Obtenemos codigo salud
                string campoSaludId = WebConfigurationManager.AppSettings["campoSaludId"].ToString();
                int campoSaludIdInt = int.Parse(campoSaludId);

                tbPregrado pregradoActual = db.tbPregrado.Where(p => p.Solicitud == modelo.solicitudActual.SolicitudId && p.esSegundaEspecialidad == true).FirstOrDefault();
                if (pregradoActual != null)
                    db.tbPregrado.Remove(pregradoActual);

                if (modelo.CampoAmplioConocimiento == campoSaludIdInt)
                {
                    if (modelo.segundaEspecialidadSalud)
                    {
                        InfoPregrado preg = new InfoPregrado();
                        modelo._segundaEspecialidad.Entidad = modelo._segundaEsp_Entidad;
                        modelo._segundaEspecialidad.NombreProgramaOtorgaPre = modelo.NombreProgramaOtorgaPreSeg;
                        tbPregrado pregrado = preg.convertPregrado(modelo._segundaEspecialidad, modelo.segundaEspecialidadSaludNal);
                        pregrado.Solicitud = modelo.solicitudActual.SolicitudId;
                        pregrado.esSegundaEspecialidad = true;

                        if (modelo.segundaEspecialidadSaludNal)
                        {
                            pregrado.ciudad = modelo.CiudadInstitutoPreSeg;
                            pregrado.departamento = modelo.DptoInstitutoPreSeg;
                            if (txtFileSegunda != null)
                            {
                                pregrado.RutaCopiaTitulo = modelo.solicitudActual.SolicitudId.ToString() + "_" + pregrado.RutaCopiaTitulo;
                                //Guarda archivo copia de titulo
                                string path = Server.MapPath("~/Archivos/Pregrado/SegundaEspNal/");
                                txtFileSegunda.SaveAs(path + pregrado.RutaCopiaTitulo);
                            }
                        }
                        else
                        {
                            if (txtFileResolucionSegunda != null)
                            {
                                pregrado.RutaCopiaResolucion = modelo.solicitudActual.SolicitudId.ToString() + "_" + pregrado.RutaCopiaResolucion;
                                //Guarda archivo copia de titulo
                                string path = Server.MapPath("~/Archivos/Pregrado/SegundaEspInernal/");
                                txtFileResolucionSegunda.SaveAs(path + pregrado.RutaCopiaResolucion);
                            }
                        }

                        db.tbPregrado.Add(pregrado);
                    }
                }

                Programa pro = new Programa();
                tbPrograma progNew = pro.convertPrograma(modelo._programa);
                progNew.Solicitud = modelo.solicitudActual.SolicitudId;

                db.tbPrograma.Add(progNew);

                db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void registraPregrado(Paso3Model modelo, HttpPostedFileBase txtFilePre)
        {
            tbPregrado pregradoActual = db.tbPregrado.Where(p => p.Solicitud == modelo.solicitudActual.SolicitudId  && p.esSegundaEspecialidad == false).FirstOrDefault();
            if (pregradoActual != null)
                db.tbPregrado.Remove(pregradoActual);

            InfoPregrado preg = new InfoPregrado();
            modelo._infoPregrado.Entidad = modelo._infoPregrado_Entidad;
            modelo._infoPregrado.CiudadInstitutoPre = modelo.CiudadInstitutoPre;
            modelo._infoPregrado.DptoInstitutoPre = modelo.DptoInstitutoPre;
            modelo._infoPregrado.NombreProgramaOtorgaPre = modelo.NombreProgramaOtorgaPre;
            tbPregrado pregrado = preg.convertPregrado(modelo._infoPregrado, (bool)modelo.solicitudActual.nacional);
            pregrado.Solicitud = modelo.solicitudActual.SolicitudId;
            pregrado.esSegundaEspecialidad = false;

            if (txtFilePre != null)
            {
                pregrado.RutaCopiaTitulo = modelo.solicitudActual.SolicitudId.ToString() + "_" + pregrado.RutaCopiaTitulo;
                //Guarda archivo copia de titulo
                string path = Server.MapPath("~/Archivos/Pregrado/ValidaTitulo/");
                txtFilePre.SaveAs(path + pregrado.RutaCopiaTitulo);
            }

            db.tbPregrado.Add(pregrado);

            db.SaveChanges();
        }

        #endregion

        #region Validaciones

        public bool validaFase4(Paso4Model modelo)
        {

            tbSolicitud solActu1 = db.tbSolicitud.Find(modelo.solicitudActual.SolicitudId);
            modelo.solicitudActual = solActu1;
            //modelo.certificadoAsignaturas
            bool resp = true;
            //Documentos compartidos
            if (!ModelState.IsValidField("documentoIdentidad") || !ModelState.IsValidField("diplomaPosgrado") 
                || !ModelState.IsValidField("certificadoAsignaturas") || !ModelState.IsValidField("diplomaPosgradoTraducido")
                || !ModelState.IsValidField("certificadoAsignaturasTraducido"))
                return false;

            if (modelo.solicitudActual.Tipo.ToUpper().Equals("PREGRADO"))
            {
                if (modelo.displayProgramaAcademico)
                    if (!ModelState.IsValidField("certificadoPrograma") || !ModelState.IsValidField("certificadoProgramaTraducido"))
                        return false;

                if (modelo.displaySalud)
                    if (!ModelState.IsValidField("requisitoEspecialSalud") || !ModelState.IsValidField("requisitoEspecialSaludTraducido"))
                        return false;

                if (modelo.displayDerecho)
                    if (!ModelState.IsValidField("requisitoEspecialDerecho"))
                        return false;

                if (modelo.displayContaduria)
                    if (!ModelState.IsValidField("requisitoEspecialContaduria"))
                        return false;

                if (modelo.displayEducacion)
                    if (!ModelState.IsValidField("requisitoEspecialEducacion"))
                        return false;
            }
            else
            {
                if (!(bool)modelo.solicitudActual.nacional)
                    if (!ModelState.IsValidField("convalidacionPregrado"))
                        return false;

                if((bool)modelo.solicitudActual.nacional)
                    if (!ModelState.IsValidField("tituloPregrado"))
                        return false;

                if (modelo.displayProgramaAcademicoPosgrado)
                    if (!ModelState.IsValidField("certificadoPrograma") || !ModelState.IsValidField("certificadoProgramaTraducido"))
                        return false;

                if (modelo.displayProductosInvestigacion)
                    if (!ModelState.IsValidField("formatoInvestigacion"))
                        return false;

                if (modelo.displayTrabajoInvestigacion)
                    if (!ModelState.IsValidField("actaSustentacion") || !ModelState.IsValidField("actaSustentacionTraducido"))
                        return false;

                if (modelo.displayTesis)
                    if (!ModelState.IsValidField("tesisMaestrias") || !ModelState.IsValidField("tesisMaestriasTraducido"))
                        return false;

                if (modelo.displayEspecialidadBaseNal)
                    if (!ModelState.IsValidField("tituloEspecialidad"))
                        return false;

                if (modelo.displayRecorProcedimientos)
                    if (!ModelState.IsValidField("recordProcedimientos") || !ModelState.IsValidField("recordProcedimientosTraducido")
                        || !ModelState.IsValidField("certificadoActividadesAcademicas") || !ModelState.IsValidField("certificadoActividadesAcademicasTraducido"))
                        return false;

                if (modelo.displaySaludSegundaEsp)
                    if (!ModelState.IsValidField("convalidacionEspecialidadBase"))
                        return false;
            }

            return resp;
        }

        private bool validaDatosPersonalesModeloP3(Paso3Model modelo)
        {
            //Valida Info Adicional Usuario 
            if (modelo.TipoDocumento.ToUpper().Equals("CE"))
            {
                if (!ModelState.IsValidField("CiudadExpedicion") || !ModelState.IsValidField("PaisExpedicion"))
                    return false;
            }

            if ((bool)modelo.solicitudActual.notificacionElectronica)
            {
                //Valida Notificacion electronica
                if (!ModelState.IsValidField("Direccion") || !ModelState.IsValidField("Email") || !ModelState.IsValidField("ConfirmaEmail")
                    || !ModelState.IsValidField("Email2") || !ModelState.IsValidField("ConfirmaEmail2") || !ModelState.IsValidField("Celular") || !ModelState.IsValidField("Celular"))
                    return false;

                if (modelo.Pais != 170 )
                    if (!ModelState.IsValidField("Ciudad_II"))
                        return false;
            }
            else
            {
                //Valida Info Autorizado
                if ((bool)modelo.solicitudActual.notificaTercero)
                    if ( !ModelState.IsValidField("_autorizado.NumeroDocumento")
                        || !ModelState.IsValidField("_autorizado.Nombre1Aut") || !ModelState.IsValidField("_autorizado.apellido1Aut"))
                        return false;

                if (!ModelState.IsValidField("_autorizado.Direccion") || !ModelState.IsValidField("_autorizado.Pais") || !ModelState.IsValidField("_autorizado.Celular") || !ModelState.IsValidField("_autorizado.TelefonoFijo"))
                    return false;
            }

            return true;
        }

        private bool validaprogramaModeloP3(Paso3Model modelo)
        {
            //Valida Institución
            for (int i = 0; i< modelo._institucionList.Count; i++)
            {
                if (!ModelState.IsValidField("_institucionList[" + i.ToString() + "].Pais") || !ModelState.IsValidField("_institucionList[" + i.ToString() + "].Instituto") || !ModelState.IsValidField("_institucionList[" + i.ToString() + "].InstitutoText") || !ModelState.IsValidField("_institucionList[" + i.ToString() + "].Ciudad") || !ModelState.IsValidField("_institucionList[" + i.ToString() + "].CiudadText"))
                    return false;

                if (string.IsNullOrEmpty(modelo._institucionList[i].Pais) || string.IsNullOrEmpty(modelo._institucionList[i].Instituto) || string.IsNullOrEmpty(modelo._institucionList[i].InstitutoText) || string.IsNullOrEmpty(modelo._institucionList[i].Ciudad) || string.IsNullOrEmpty(modelo._institucionList[i].CiudadText))
                    return false;
            }

            //Valida Programa !ModelState.IsValidField("_programa.NombrePrograma") ||
            if (!ModelState.IsValidField("_programa.Duracion")
                    || !ModelState.IsValidField("_programa.FechaTitulo") || !ModelState.IsValidField("_programa.DenominacionTitulo"))
                return false;

            if(modelo.NombrePrograma.Equals("0") || string.IsNullOrEmpty(modelo.NombrePrograma))
                if(!ModelState.IsValidField("_programa.NombreProgramaText") || !ModelState.IsValidField("_programa.TituloObtenidoManual"))
                    return false;

            //Obtenemos codigo salud
            string campoSaludId = WebConfigurationManager.AppSettings["campoSaludId"].ToString();
            int campoSaludIdInt = int.Parse(campoSaludId);

            if (modelo.CampoAmplioConocimiento == campoSaludIdInt && modelo.solicitudActual.Tipo.Equals("POSGRADO"))
            {
                if (modelo.segundaEspecialidadSalud)
                {
                    if (modelo.segundaEspecialidadSaludNal)
                    {
                        if (!ModelState.IsValidField("_segundaEspecialidad.Instituto") || !ModelState.IsValidField("_segundaEspecialidad.InstitutoText") 
                            || !ModelState.IsValidField("_segundaEspecialidad.Titulo") || !ModelState.IsValidField("_segundaEspecialidad.FechaTitulo")
                            || !ModelState.IsValidField("DptoInstitutoPreSeg") || !ModelState.IsValidField("CiudadInstitutoPreSeg"))
                            return false;//|| !ModelState.IsValidField("_segundaEspecialidad.RutaCopiaTitulo")
                    }
                    else
                    {
                        if (!ModelState.IsValidField("_segundaEspecialidad.NroResolucion") || !ModelState.IsValidField("_segundaEspecialidad.FechaResolucion"))
                            return false;
                    }
                }
            }

            return true;
        }

        private bool validapreGradoModeloP3(Paso3Model modelo)
        {
            //Valida Pregrado obtenido, cuando se convalida un posGrado
            if (modelo.solicitudActual.Tipo.ToUpper().Equals("POSGRADO"))
            {
                if ((bool)modelo.solicitudActual.nacional)
                {
                    if (!ModelState.IsValidField("_infoPregrado.Instituto") || !ModelState.IsValidField("_infoPregrado.InstitutoText") 
                        || !ModelState.IsValidField("_infoPregrado.Titulo") || !ModelState.IsValidField("_infoPregrado.FechaTitulo")
                        || !ModelState.IsValidField("DptoInstitutoPre") || !ModelState.IsValidField("CiudadInstitutoPre"))
                        return false;//|| !ModelState.IsValidField("_infoPregrado.RutaCopiaTitulo")
                }
                else
                {
                    if (!ModelState.IsValidField("_infoPregrado.NroResolucion") || !ModelState.IsValidField("_infoPregrado.FechaResolucion"))
                        return false;
                }
            }

            return true;
        }

        private bool validaModeloP3(Paso3Model modelo, HttpPostedFileBase txtFilePre, HttpPostedFileBase txtFileresolucion, HttpPostedFileBase txtFileSegunda, HttpPostedFileBase txtFileresolucionSegunda)
        {
            bool respuesta = validaDatosPersonalesModeloP3(modelo);

            if (respuesta)
                respuesta = validaprogramaModeloP3(modelo);

            if (respuesta)
                respuesta = validapreGradoModeloP3(modelo);

            return respuesta;
        }

        #endregion
        
        #region Metodos Adicionales
        private void combosP2(bool esPostGrado, string paisRes, int depto, string ciudadRes, 
            int deptoNoti, string paisInstituto, string nombreInst, string Entidad, 
            int campoConocimiento, string modalidad, string TipoDuracion, string TipoEducacionSuperior,
            string TipoMaestria, string NombrePrograma, string NombreInstitucion, string NombreInstitucionSegundaEsp,
            string tipoDoc, string SegundaEsp_Entidad, int deptoInstitutoPre, int ciudadInstPre, int programaPre,
            int deptoInstitutoPreSeg, int ciudadInstPreSeg, int programaPreSeg)
        {
            List<SelectListItem> cmbNombrePrograma = new SelectList(db.PRO_PROGRAMA_EXTRANJERO.OrderBy(p => p.TITULO_EXTRANJERO).ToList(), "ID_PROGRAMA_EXTRANJERO", "DENOMINACION", NombrePrograma).ToList();
            cmbNombrePrograma.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));
            ViewBag.NombrePrograma = cmbNombrePrograma;

            List<SelectListItem> cmbNombreProgramaTitulo = new SelectList(db.PRO_PROGRAMA_EXTRANJERO.OrderBy(p => p.TITULO_EXTRANJERO).ToList(), "ID_PROGRAMA_EXTRANJERO", "TITULO_EXTRANJERO", NombrePrograma).ToList();
            cmbNombreProgramaTitulo.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));
            ViewBag.NombreProgramaTitulo = cmbNombreProgramaTitulo;

            List<SelectListItem> cmbNombreInstitucion = new SelectList(db.INS_INSTITUCION_EXTRANJERA.OrderBy(p => p.NOMBRE_EN_ESPANOL).ToList(), "ID_INSTITUCION", "NOMBRE_EN_ESPANOL", NombreInstitucion).ToList();
            cmbNombreInstitucion.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));
            ViewBag.NombreInstitucion = cmbNombreInstitucion;

            List<INS_INSTITUCION_EDUCATIVA> lstINS_INSTITUCION_EDUCATIVA = db.INS_INSTITUCION_EDUCATIVA.Where(i => i.ID_MUNICIPIO == ciudadInstPre.ToString()).ToList();
            List<SelectListItem> cmbNombreInstitucionOtorgaPre = new SelectList(lstINS_INSTITUCION_EDUCATIVA.OrderBy(p => p.NOMBRE_INSTITUCION).ToList(), "ID_INSTITUCION", "NOMBRE_INSTITUCION", nombreInst).ToList();
            cmbNombreInstitucionOtorgaPre.Insert(0, (new SelectListItem { Text = "SELECCIONE INSTITUCION", Value = "0" }));
            ViewBag.NombreInstitucionOtorgaPre = cmbNombreInstitucionOtorgaPre;

            List<PRO_PROGRAMA_ACADEMICO> lstPRO_PROGRAMA_ACADEMICO = listaProgramaXInstituto(nombreInst);

            List < SelectListItem > cmbNombreProgramaOtorgaPre = new SelectList(lstPRO_PROGRAMA_ACADEMICO.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "DENOMINACION", programaPre).ToList();
            cmbNombreProgramaOtorgaPre.Insert(0, (new SelectListItem { Text = "SELECCIONE PROGRAMA", Value = "0" })); 
            ViewBag.NombreProgramaOtorgaPre = cmbNombreProgramaOtorgaPre;
            
            List<SelectListItem> cmbNombreProgramaTituloPre = new SelectList(lstPRO_PROGRAMA_ACADEMICO.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "TITULO", programaPre).ToList();
            cmbNombreProgramaOtorgaPre.Insert(0, (new SelectListItem { Text = "SELECCIONE PROGRAMA", Value = "0" })); 
            ViewBag.NombreProgramaTituloPre = cmbNombreProgramaTituloPre;    
            
            List<INS_INSTITUCION_EDUCATIVA> listaInstitutoXCiudad = db.INS_INSTITUCION_EDUCATIVA.Where(c => c.ID_MUNICIPIO.Equals(ciudadInstPreSeg.ToString())).OrderBy(c => c.NOMBRE_INSTITUCION).ToList();
            List<SelectListItem> cmbNombreInstitucionSegundaEsp = new SelectList(listaInstitutoXCiudad, "ID_INSTITUCION", "NOMBRE_INSTITUCION", NombreInstitucionSegundaEsp).ToList();
            cmbNombreInstitucionSegundaEsp.Insert(0, (new SelectListItem { Text = "SELECCIONE INSTITUCION", Value = "0" }));
            ViewBag.NombreInstitucionSegundaEsp = cmbNombreInstitucionSegundaEsp;

            List<PRO_PROGRAMA_ACADEMICO> lstPRO_PROGRAMA_ACADEMICO_II = listaProgramaXInstituto(NombreInstitucionSegundaEsp);

            List<SelectListItem> cmbNombreProgramaOtorgaPreSeg = new SelectList(lstPRO_PROGRAMA_ACADEMICO_II.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "DENOMINACION", programaPreSeg).ToList();
            cmbNombreProgramaOtorgaPreSeg.Insert(0, (new SelectListItem { Text = "SELECCIONE PROGRAMA", Value = "0" })); 
            ViewBag.NombreProgramaOtorgaPreSeg = cmbNombreProgramaOtorgaPreSeg;

            List<SelectListItem> cmbNombreProgramaTituloPreSeg = new SelectList(lstPRO_PROGRAMA_ACADEMICO_II.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "TITULO", programaPreSeg).ToList();
            cmbNombreProgramaOtorgaPre.Insert(0, (new SelectListItem { Text = "SELECCIONE PROGRAMA", Value = "0" }));
            ViewBag.NombreProgramaTituloPreSeg = cmbNombreProgramaTituloPreSeg;

            List<SelectListItem> cmbPais = new SelectList(db.tbPais.OrderBy(p => p.pais).ToList(), "paisId", "pais", paisRes).ToList();
            cmbPais.Insert(0, (new SelectListItem { Text = "Seleccione País", Value = "0" }));
            ViewBag.Pais = cmbPais;

            List<SelectListItem> cmbDepartamento = new SelectList(db.tbDepartamento.OrderBy(d => d.departamento).ToList(), "departamentoId", "departamento", depto).ToList();
            cmbDepartamento.Insert(0, (new SelectListItem { Text = "Seleccione Departamento", Value = "-1" }));
            ViewBag.Departamento = cmbDepartamento;

            List<tbCiudad> ciudades = db.tbCiudad.Where(c => c.tbDepartamento.departamentoId == depto).OrderBy(c => c.ciudad).ToList();
            List<SelectListItem> cmbCiudad = new SelectList(ciudades, "ciudadId", "ciudad", ciudadRes).ToList();
            cmbCiudad.Insert(0, (new SelectListItem { Text = "Seleccione Ciudad", Value = "-1" }));
            ViewBag.Ciudad = cmbCiudad;

            List<SelectListItem> cmbDepartamentoNotificacion = new SelectList(db.tbDepartamento.OrderBy(d => d.departamento).ToList(), "departamentoId", "departamento", deptoNoti).ToList();
            cmbDepartamentoNotificacion.Insert(0, (new SelectListItem { Text = "Seleccione Departamento", Value = "-1" }));
            ViewBag.DepartamentoNotificacion = cmbDepartamentoNotificacion;

            List<tbCiudad> ciudadesNotificacion = db.tbCiudad.Where(c => c.tbDepartamento.departamentoId == deptoNoti).OrderBy(c => c.ciudad).ToList();
            List<SelectListItem> cmbCiudadNotificacion = new SelectList(ciudadesNotificacion, "ciudadId", "ciudad", ciudadRes).ToList();
            cmbCiudadNotificacion.Insert(0, (new SelectListItem { Text = "Seleccione Ciudad", Value = "-1" }));
            ViewBag.CiudadNotificacion = cmbCiudadNotificacion;

            List<SelectListItem> cmbPaisInstituto = new SelectList(db.tbPais.Where(p => p.pais != "COLOMBIA").OrderBy(p => p.pais).ToList(), "pais", "pais", paisInstituto).ToList();
            cmbPaisInstituto.Insert(0, (new SelectListItem { Text = "Seleccione País", Value = "0" }));
            ViewBag.PaisInstituto = cmbPaisInstituto;

            List<SelectListItem> cmbDptoInstitutoPre = new SelectList(db.tbDepartamento.OrderBy(d => d.departamento).ToList(), "departamentoId", "departamento", deptoInstitutoPre).ToList();
            cmbDptoInstitutoPre.Insert(0, (new SelectListItem { Text = "SELECCIONE DEPARTAMENTO", Value = "-1" }));
            ViewBag.DptoInstitutoPre = cmbDptoInstitutoPre;

            List<tbCiudad> ciudadInstitutoPre = db.tbCiudad.Where(c => c.tbDepartamento.departamentoId == deptoInstitutoPre).OrderBy(c => c.ciudad).ToList();
            List<SelectListItem> cmbCiudadInstitutoPre = new SelectList(ciudadInstitutoPre, "ciudadId", "ciudad", ciudadInstPre).ToList();
            cmbCiudadInstitutoPre.Insert(0, (new SelectListItem { Text = "SELECCIONE CIUDAD", Value = "-1" }));
            ViewBag.CiudadInstitutoPre = cmbCiudadInstitutoPre;

            List<SelectListItem> cmbDptoInstitutoPreSeg = new SelectList(db.tbDepartamento.OrderBy(d => d.departamento).ToList(), "departamentoId", "departamento", deptoInstitutoPreSeg).ToList();
            cmbDptoInstitutoPreSeg.Insert(0, (new SelectListItem { Text = "SELECCIONE DEPARTAMENTO", Value = "-1" }));
            ViewBag.DptoInstitutoPreSeg = cmbDptoInstitutoPreSeg;

            List<tbCiudad> ciudadInstitutoPreSeg = db.tbCiudad.Where(c => c.tbDepartamento.departamentoId == deptoInstitutoPreSeg).OrderBy(c => c.ciudad).ToList();
            List<SelectListItem> cmbCiudadInstitutoPreSeg = new SelectList(ciudadInstitutoPreSeg, "ciudadId", "ciudad", ciudadInstPreSeg).ToList();
            cmbCiudadInstitutoPreSeg.Insert(0, (new SelectListItem { Text = "SELECCIONE CIUDAD", Value = "-1" }));
            ViewBag.CiudadInstitutoPreSeg = cmbCiudadInstitutoPreSeg;

            List<SelectListItem> cmbTipoMaestria = new SelectList(db.tbEnfasis, "enfasisId", "enfasis", TipoMaestria).ToList();
            ViewBag.TipoMaestria = cmbTipoMaestria;

            List<SelectListItem> cmbTipoDoc = new SelectList(db.tbTipoDocumento, "TipoDocumentoCod", "TipoDocumento", tipoDoc).ToList();
            ViewBag.tipoDoc = cmbTipoDoc;           

            List<tbTipoEducacionSuperior> lstTipoEdu = db.tbTipoEducacionSuperior.Where(t => t.espostgrado == esPostGrado).ToList();

            List<SelectListItem> cmbTipoEducacionSuperior = new SelectList(lstTipoEdu, "tipoEducacionSupId", "TipoEducacionSuperior", TipoEducacionSuperior).ToList();
            ViewBag.TipoEducacionSuperior = cmbTipoEducacionSuperior;            

            List<SelectListItem> cmbTipoDuracion = new SelectList(db.tbTipoDuracion, "TipoDuracionId", "TipoDuracion", TipoDuracion).ToList();
            ViewBag.TipoDuracion = cmbTipoDuracion;

            List<SelectListItem> cmbModalidadPrograma = new SelectList(db.tbModalidadPrograma, "ModalidadProgramaId", "ModalidadPrograma", modalidad).ToList();
            ViewBag.ModalidadPrograma = cmbModalidadPrograma;

            List<SelectListItem> cmbCampoAmplioCono = new SelectList(db.tbCampoAmplioConocimiento.OrderBy(c => c.CampoAmplioConocimiento).ToList(), "CampoAmplioConocimientoId", "CampoAmplioConocimiento", campoConocimiento).ToList();
            ViewBag.CampoAmplioConocimiento = cmbCampoAmplioCono;

            List<SelectListItem> cmbEntidad= new SelectList(db.tbEntidad, "EntidadId", "Entidad", Entidad).ToList();
            ViewBag._infoPregrado_Entidad = cmbEntidad;

            List<SelectListItem> cmbEntidadSegunda = new SelectList(db.tbEntidad, "EntidadId", "Entidad", SegundaEsp_Entidad).ToList();
            ViewBag._segundaEsp_Entidad = cmbEntidadSegunda;
        }

        public List<PRO_PROGRAMA_ACADEMICO> listaProgramaXInstituto(string nombreInst)
        {
            int ID_INSTITUCION = int.Parse(string.IsNullOrEmpty(nombreInst) ? "0" : nombreInst);
            return (from pro in db.PRO_PROGRAMA_ACADEMICO
                    join pin in db.PRO_PROGRAMAS_INSTITUCION on pro.ID_PROGRAMA equals pin.ID_PROGRAMA
                    join ins in db.INS_INSTITUCION_EDUCATIVA on pin.ID_INSTITUCION equals ins.ID_INSTITUCION
                    where ins.ID_INSTITUCION == ID_INSTITUCION && pro.ID_NIVEL_DE_FORMACION > 3
                    select pro).ToList();
        }

        public JsonResult ActualizacomboInstitutoXciudad(string ciudad)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<INS_INSTITUCION_EXTRANJERA> listaCiudadExt = db.INS_INSTITUCION_EXTRANJERA.Where(d => d.ID_CIUDAD_EXTRANJERA.Equals(ciudad)).OrderBy(d => d.NOMBRE_EN_ESPANOL).ToList();
            cmbActualiza = new SelectList(listaCiudadExt, "ID_INSTITUCION", "NOMBRE_EN_ESPANOL").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));

            return Json(cmbActualiza);
        }

        public JsonResult ActualizacomboCiudadExtrangera(string pais)
        {
            string idPais = db.tbPais.Where(p => p.pais.ToUpper().Equals(pais.ToUpper())).FirstOrDefault().paisId.ToString();
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<REF_MUNICIPIO_EXTRANJERO> listaCiudadExt = db.REF_MUNICIPIO_EXTRANJERO.Where(d => d.ID_PAIS.Equals(idPais)).OrderBy(d => d.CIUDAD_EXTRANJERA).ToList();
            cmbActualiza = new SelectList(listaCiudadExt, "ID_CIUDAD_EXT", "CIUDAD_EXTRANJERA").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "OTRA", Value = "0" }));

            return Json(cmbActualiza);
        }

        public JsonResult ActualizacomboDepartamento(string pais)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<tbDepartamento> listaDpto = db.tbDepartamento.Where(d => d.tbPais.pais.Equals(pais)).OrderBy(d => d.departamento).ToList();
            cmbActualiza = new SelectList(listaDpto, "departamentoId", "departamento").ToList();

            return Json(cmbActualiza);
        }

        public JsonResult ActualizacomboCiudad(string dpto)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<tbCiudad> listaCiudad = db.tbCiudad.Where(c => c.tbDepartamento.departamento.Equals(dpto)).OrderBy(c => c.ciudad).ToList();
            cmbActualiza = new SelectList(listaCiudad, "ciudadId", "ciudad").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "SELECCIONE CIUDAD", Value = "0" }));

            return Json(cmbActualiza);
        }
        public JsonResult institucionXciudad(string ciudad)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<INS_INSTITUCION_EDUCATIVA> listaCiudad = db.INS_INSTITUCION_EDUCATIVA.Where(c => c.ID_MUNICIPIO.Equals(ciudad)).OrderBy(c => c.NOMBRE_INSTITUCION).ToList();
            cmbActualiza = new SelectList(listaCiudad, "ID_INSTITUCION", "NOMBRE_INSTITUCION").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "SELECCIONE INSTITUCION", Value = "0" }));

            return Json(cmbActualiza);
        }
        public JsonResult programaXinstitutoExtr(int instituto)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            List<PRO_PROGRAMA_EXTRANJERO> listaPrograma = db.PRO_PROGRAMA_EXTRANJERO.Where(c => c.ID_INSTITUCION_EXTRANJERA.Equals(instituto)).ToList();
            if (listaPrograma.Count > 0)
                listaPrograma = listaPrograma.OrderBy(p => p.TITULO_EXTRANJERO).ToList();
            cmbActualiza = new SelectList(listaPrograma, "ID_PROGRAMA_EXTRANJERO", "DENOMINACION").ToList();
            //cmbActualiza.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));

            return Json(cmbActualiza);
        }

        public JsonResult programaXinstitucion(string institucion)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            int ID_INSTITUCION = int.Parse(string.IsNullOrEmpty(institucion) ? "0" : institucion);
            List<PRO_PROGRAMA_ACADEMICO> lstPRO_PROGRAMA_ACADEMICO = (from pro in db.PRO_PROGRAMA_ACADEMICO
                                                                      join pin in db.PRO_PROGRAMAS_INSTITUCION on pro.ID_PROGRAMA equals pin.ID_PROGRAMA
                                                                      join ins in db.INS_INSTITUCION_EDUCATIVA on pin.ID_INSTITUCION equals ins.ID_INSTITUCION
                                                                      where ins.ID_INSTITUCION == ID_INSTITUCION && pro.ID_NIVEL_DE_FORMACION > 3
                                                                      select pro).ToList();
            cmbActualiza = new SelectList(lstPRO_PROGRAMA_ACADEMICO.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "DENOMINACION").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "SELECCIONE PROGRAMA", Value = "0" }));

            return Json(cmbActualiza);
        }

        public JsonResult programaTituloXinstitucion(string institucion)
        {
            List<SelectListItem> cmbActualiza = new List<SelectListItem>();
            int ID_INSTITUCION = int.Parse(string.IsNullOrEmpty(institucion) ? "0" : institucion);
            List<PRO_PROGRAMA_ACADEMICO> lstPRO_PROGRAMA_ACADEMICO = (from pro in db.PRO_PROGRAMA_ACADEMICO
                                                                      join pin in db.PRO_PROGRAMAS_INSTITUCION on pro.ID_PROGRAMA equals pin.ID_PROGRAMA
                                                                      join ins in db.INS_INSTITUCION_EDUCATIVA on pin.ID_INSTITUCION equals ins.ID_INSTITUCION
                                                                      where ins.ID_INSTITUCION == ID_INSTITUCION && pro.ID_NIVEL_DE_FORMACION > 3
                                                                      select pro).ToList();
            cmbActualiza = new SelectList(lstPRO_PROGRAMA_ACADEMICO.OrderBy(p => p.DENOMINACION).ToList(), "ID_PROGRAMA", "TITULO").ToList();
            cmbActualiza.Insert(0, (new SelectListItem { Text = "SELECCIONE TITULO", Value = "0" }));

            return Json(cmbActualiza);
        }


        #endregion Metodos Adicionales

        #region Reportes

        public ActionResult ConstanciaConvalidacion(string tipoReporte, int solicitudId)
        {
            LoginModel usuario = (LoginModel)Session["usuarioValido"];
            if (usuario == null)
            {
                return RedirectToAction("LoginSession");
            }

            try
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("../Reportes/Constancia"), "ConstanciaReport.rdlc");

                if (System.IO.File.Exists(path))
                    lr.ReportPath = path;
                else
                    return View("Index");

                Paso3Model modelo = new Paso3Model();
                modelo = llenaSolicitud(solicitudId, usuario);

                Constancia ds = new Constancia();

                //Titulos
                DataTable dt0 = ds.dtConstancia;
                DataRow row0 = dt0.NewRow();

                int td = int.Parse(usuario.datosUsuario.tipoDoc);
                int ce = int.Parse(usuario.datosUsuario.ciudadExpedicion);
                string tipDoc = db.tbTipoDocumento.Where(d => d.itpoDocumentoId == td).FirstOrDefault().TipoDocumento;
                string ciuExp = db.tbCiudad.Where(c => c.ciudadId == ce).FirstOrDefault().ciudad;

                //row0["solicitudId"] = solicitudId;
                //row0["radicado"] = modelo.solicitudActual.Radicado;
                //row0["cor_derecho"] = "COR - 2019 - 0003236";
                //row0["nombreCompleto"] = usuario.datosUsuario.primerNombre + " " + usuario.datosUsuario.segundoNombre + usuario.datosUsuario.primerApellido + " " + usuario.datosUsuario.segundoApellido;
                //row0["tipoDocumento"] = tipDoc;
                //row0["nroDocumento"] = usuario.datosUsuario.numeroDocumento;
                //row0["ciudadExpedicion"] = ciuExp;
                //row0["titulo"] = modelo._programa.NombrePrograma.Equals("0") ? modelo._programa.TituloObtenidoManual : modelo._programa.TituloObtenido;
                //row0["institucion"] = modelo._institucionList[0].InstitutoText;
                //row0["paisTitulo"] = modelo._institucionList[0].Pais;
                //row0["fechaRadicado"] = DateTime.Now.ToLongDateString();// modelo.solicitudActual.Fecha.ToLongDateString();
                //row0["folder"] = "0002554";
                //row0["fchaActual"] = DateTime.Now.ToLongDateString();
                //row0["ley"] = solicitudId;
                //row0["anioLey"] = solicitudId;
                //row0["decreto"] = solicitudId;
                //row0["anioDecreto"] = solicitudId;
                //row0["tipoConvalidacion"] = modelo.solicitudActual.Tipo.ToUpper();

                string nombreCompleto = usuario.datosUsuario.primerNombre + " " + usuario.datosUsuario.segundoNombre + usuario.datosUsuario.primerApellido + " " + usuario.datosUsuario.segundoApellido;
                string titulo = modelo._programa.NombrePrograma.Equals("0") ? modelo._programa.TituloObtenidoManual : modelo._programa.TituloObtenido;
                string texto1 =  WebConfigurationManager.AppSettings["constanciaTexto1"].ToString();
                texto1 = texto1.Replace("[NOMBRECOMPLETO]", nombreCompleto);
                texto1 = texto1.Replace("[TIPODOCUMENTO]", tipDoc);
                texto1 = texto1.Replace("[DOCUMENTO]", usuario.datosUsuario.numeroDocumento);
                texto1 = texto1.Replace("[CIUDADEXPEDICION]", ciuExp);
                texto1 = texto1.Replace("[TIPOCONVALIDACION]", modelo.solicitudActual.Tipo);
                texto1 = texto1.Replace("[TITULO]", titulo.ToUpper());
                texto1 = texto1.Replace("[INSTITUTO]", modelo._institucionList[0].InstitutoText.ToUpper());
                texto1 = texto1.Replace("[PAISINSTITUTO]", modelo._institucionList[0].Pais.ToUpper());
                string texto2 =  WebConfigurationManager.AppSettings["constanciaTexto2"].ToString();
                texto2 = texto2.Replace("[RADICADO]", modelo.solicitudActual.Radicado);
                texto2 = texto2.Replace("[FECHARADICACION]", modelo.solicitudActual.Fecha.ToLongDateString());
                texto2 = texto2.Replace("[FOLDER]", "0002554");
                string texto3 =  WebConfigurationManager.AppSettings["constanciaTexto3"].ToString();
                texto3 = texto3.Replace("[FECHAACTUAL]", DateTime.Now.ToLongDateString());

                row0["cor_derecho"] = "COR - 2019 - 0003236";
                row0["texto1"] = texto1;
                row0["texto2"] = texto2;
                row0["texto3"] = texto3;

                dt0.Rows.Add(row0);
                ReportDataSource rds0 = new ReportDataSource("DataSet1", dt0);
                lr.DataSources.Add(rds0);
                
                string reportType = tipoReporte;
                string mimeType;
                string encoding;
                string fileNameExtension;

                string deviceInfo =
                    "<DeviceInfo>" +
                    "  <OutputFormat>" + tipoReporte + "</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.5in</MarginTop>" +
                    "  <MarginLeft>0.0in</MarginLeft>" +
                    "  <MarginRight>0.0in</MarginRight>" +
                    "  <MarginBottom>0.5in</MarginBottom>" +
                    "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
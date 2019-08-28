using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace ConvalidacionEducacionSuperior.Models
{
    public class ConsumeServicioBizagi
    {
        public Paso3Model modelo;

        public ConsumeServicioBizagi(Paso3Model _modelo)
        {
            modelo = _modelo;
        }

        public string creaSolicitud()
        {
            try
            {
                ServicioBizagi.WorkflowEngineSOASoapClient bizagiClient = new ServicioBizagi.WorkflowEngineSOASoapClient();

                //string ruta = HttpContext.Current.Server.MapPath("../Archivos") + "\\ModeloXml";
                //XElement root = XElement.Load(ruta);
                //XmlDocument xD = new XmlDocument();
                //xD.LoadXml(root.ToString());
                //XmlNode xN = xD.FirstChild;

                XmlNode nodoRequest = construyeXml();

                XmlNode ti = bizagiClient.createCases(nodoRequest);

                XmlNodeList nodeList = ti.ChildNodes[0].ChildNodes;
                string error = string.Empty;
                string msg = nodeList[1].Name;
                if (msg.ToUpper().Equals("PROCESSERROR"))
                {
                    try
                    {
                        XmlNodeList lst = nodeList[1].ChildNodes;
                        XmlNodeList lstErrores = lst[1].ChildNodes;
                        XmlNodeList lstErr = lstErrores[0].ChildNodes;
                        XmlNode lstErr1 = lstErr[3];
                        error = lstErr1.ChildNodes[0].Value;
                    }
                    catch(Exception ex)
                    {
                        error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                    }
                    
                }
                return error;
            }
            catch(Exception ex)
            {
                return "ERROR: " + ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message;
            }
        }

        public XmlNode construyeXml()
        {
            XmlDocument doc = new XmlDocument();

            //Nodo princial
            XmlElement BizAgiWSParam = doc.CreateElement(string.Empty, "BizAgiWSParam", string.Empty);

            //Nodo Domain
            XmlElement domain = doc.CreateElement(string.Empty, "domain", string.Empty);
            XmlText text1 = doc.CreateTextNode("domain");
            domain.AppendChild(text1);
            BizAgiWSParam.AppendChild(domain);
            //Insert Nodo Domain

            //Nodo userName
            XmlElement userName = doc.CreateElement(string.Empty, "userName", string.Empty);
            XmlText text2 = doc.CreateTextNode("Convalidante");
            userName.AppendChild(text2);
            BizAgiWSParam.AppendChild(userName);
            //Insert Nodo userName

            //Nodo Cases
            XmlElement Cases = doc.CreateElement(string.Empty, "Cases", string.Empty);
            //Nodo Case
            XmlElement Case = doc.CreateElement(string.Empty, "Case", string.Empty);

            //Nodo Process
            XmlElement Process = doc.CreateElement(string.Empty, "Process", string.Empty);
            XmlText text3 = doc.CreateTextNode("Convalidaciones");
            Process.AppendChild(text3);
            Case.AppendChild(Process);
            //Insert Nodo Process

            //Nodo Entities
            XmlElement Entities = doc.CreateElement(string.Empty, "Entities", string.Empty);

            //Nodo CON_T_MConvalidaciones
            XmlElement CON_T_MConvalidaciones = doc.CreateElement(string.Empty, "CON_T_MConvalidaciones", string.Empty);

            //Nodo CON_T_MSolicitud
            XmlElement CON_T_MSolicitud = doc.CreateElement(string.Empty, "CON_T_MSolicitud", string.Empty);

            // Nodo sNumerodeRadicado 
            XmlElement sNumerodeRadicado = doc.CreateElement(string.Empty, "sNumerodeRadicado", string.Empty);
            XmlText text5 = doc.CreateTextNode(modelo.solicitudActual.Radicado);
            sNumerodeRadicado.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(sNumerodeRadicado);
            //Insert Nodo sNumerodeRadicado 

            //Nodo dFechadeRadicacion
            XmlElement dFechadeRadicacion = doc.CreateElement(string.Empty, "dFechadeRadicacion", string.Empty);
            text5 = doc.CreateTextNode(modelo.solicitudActual.Fecha.ToString("o"));
            dFechadeRadicacion.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(dFechadeRadicacion);
            //Insert Nodo dFechadeRadicacion

            // Nodo sObservacionesGenerales 
            XmlElement sObservacionesGenerales = doc.CreateElement(string.Empty, "sObservacionesGenerales", string.Empty);
            text5 = doc.CreateTextNode(modelo.Observaciones);
            sObservacionesGenerales.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(sObservacionesGenerales);
            //Insert Nodo sObservacionesGenerales 

            // Nodo bNotificacionElectronica 
            XmlElement bNotificacionElectronica = doc.CreateElement(string.Empty, "bNotificacionElectronica", string.Empty);
            text5 = doc.CreateTextNode(modelo.solicitudActual.notificacionElectronica.ToString());
            bNotificacionElectronica.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(bNotificacionElectronica);
            //Insert Nodo bNotificacionElectronica 

            // Nodo bAutorizarTercero 
            XmlElement bAutorizarTercero = doc.CreateElement(string.Empty, "bAutorizarTercero", string.Empty);
            text5 = doc.CreateTextNode(modelo.solicitudActual.notificaTercero.ToString());
            bAutorizarTercero.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(bAutorizarTercero);
            //Insert Nodo bAutorizarTercero 

            // Nodo bAcreditacionIESoPro 
            XmlElement bAcreditacionIESoPro = doc.CreateElement(string.Empty, "bAcreditacionIESoPro", string.Empty);
            text5 = doc.CreateTextNode(modelo.solicitudActual.acreditacionCalidad.ToString()); 
            bAcreditacionIESoPro.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(bAcreditacionIESoPro);
            //Insert Nodo bAcreditacionIESoPro 

            // Nodo bPregradoNacional 
            XmlElement bPregradoNacional = doc.CreateElement(string.Empty, "bPregradoNacional", string.Empty);
            text5 = doc.CreateTextNode(modelo.solicitudActual.nacional.ToString());
            bPregradoNacional.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(bPregradoNacional);
            //Insert Nodo bPregradoNacional 

            // Nodo bSubespecialidad 
            XmlElement bSubespecialidad = doc.CreateElement(string.Empty, "bSubespecialidad", string.Empty);
            text5 = doc.CreateTextNode(modelo.segundaEspecialidadSalud.ToString());
            bSubespecialidad.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(bSubespecialidad);
            //Insert Nodo bSubespecialidad 

            //Nodo bEspBaseNacional
            XmlElement bEspBaseNacional = doc.CreateElement(string.Empty, "bEspBaseNacional", string.Empty);
            XmlText text4 = doc.CreateTextNode(modelo.segundaEspecialidadSaludNal.ToString());
            bEspBaseNacional.AppendChild(text4);
            CON_T_MSolicitud.AppendChild(bEspBaseNacional);
            //Insert Nodo bEspBaseNacional

            //Nodo bTituloEmitidoDosoMasIES
            XmlElement bTituloEmitidoDosoMasIES = doc.CreateElement(string.Empty, "bTituloEmitidoDosoMasIES", string.Empty);
            text4 = doc.CreateTextNode(modelo.solicitudActual.variasInstituciones.ToString());
            //text4 = doc.CreateTextNode("1");
            bTituloEmitidoDosoMasIES.AppendChild(text4);
            CON_T_MSolicitud.AppendChild(bTituloEmitidoDosoMasIES);
            //Insert Nodo bTituloEmitidoDosoMasIES

            //Nodo bRealizoEstudiosConBecas
            XmlElement bRealizoEstudiosConBecas = doc.CreateElement(string.Empty, "bRealizoEstudiosConBecas", string.Empty);
            text4 = doc.CreateTextNode(modelo.solicitudActual.beca.ToString());
            //text4 = doc.CreateTextNode("1");
            bRealizoEstudiosConBecas.AppendChild(text4);
            CON_T_MSolicitud.AppendChild(bRealizoEstudiosConBecas);
            //Insert Nodo bRealizoEstudiosConBecas

            //Nodo bProgramaConvenio
            XmlElement bProgramaConvenio = doc.CreateElement(string.Empty, "bProgramaConvenio", string.Empty);
            text4 = doc.CreateTextNode(modelo.solicitudActual.convenio.ToString());
            bProgramaConvenio.AppendChild(text4);
            CON_T_MSolicitud.AppendChild(bProgramaConvenio);
            //Insert Nodo bRealizoEstudiosConBecas

            //Nodo sPosibleDenominacion
            XmlElement sPosibleDenominacion = doc.CreateElement(string.Empty, "sPosibleDenominacion", string.Empty);
            text5 = doc.CreateTextNode(modelo._programa.DenominacionTitulo);
            sPosibleDenominacion.AppendChild(text5);
            CON_T_MSolicitud.AppendChild(sPosibleDenominacion);
            //Insert Nodo sPosibleDenominacion

            //Nodo idCON_T_MConvalidante
            XmlElement idCON_T_MConvalidante = doc.CreateElement(string.Empty, "idCON_T_MConvalidante", string.Empty);

            //Nodo sNombre1 Solicitante
            XmlElement sNombre1 = doc.CreateElement(string.Empty, "sNombre1", string.Empty);
            text5 = doc.CreateTextNode(modelo.PrimerNombre);
            sNombre1.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sNombre1);
            //Insert Nodo sNombre1 Solicitante

            // Nodo sNombre2 Solicitante
            XmlElement sNombre2 = doc.CreateElement(string.Empty, "sNombre2", string.Empty);
            text5 = doc.CreateTextNode(modelo.SegundoNombre);
            sNombre2.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sNombre2);
            //Insert Nodo sNombre2  Solicitante

            //Nodo sApellido1 Solicitante
            XmlElement sApellido1 = doc.CreateElement(string.Empty, "sApellido1", string.Empty);
            text5 = doc.CreateTextNode(modelo.PrimerApellido);
            sApellido1.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sApellido1);
            //Insert Nodo sApellido1 Solicitante

            // Nodo sApellido2  Solicitante
            XmlElement sApellido2 = doc.CreateElement(string.Empty, "sApellido2", string.Empty);
            text5 = doc.CreateTextNode(modelo.SegundoApellido);
            sApellido2.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sApellido2);
            //Insert Nodo sApellido2A  Solicitante

            //Nodo idREF_T_PSexo_Biologico Solicitante
            string sex = modelo.GeneroId == 1 ? "M" : "F";
            XmlElement idREF_T_PSexo_Biologico = doc.CreateElement(string.Empty, "idREF_T_PSexo_Biologico", string.Empty);
            XmlAttribute atr = doc.CreateAttribute("businessKey");
            atr.Value = "Id_Sexo_Biologico='" + sex + "'";
            idREF_T_PSexo_Biologico.Attributes.Append(atr);
            idCON_T_MConvalidante.AppendChild(idREF_T_PSexo_Biologico);
            //Insert Nodo idREF_T_PSexo_Biologico Solicitante

            // Nodo sNacionalidad  Solicitante
            XmlElement sNacionalidad = doc.CreateElement(string.Empty, "sNacionalidad", string.Empty);
            text5 = doc.CreateTextNode(modelo.Nacionalidad);
            sNacionalidad.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sNacionalidad);
            //Insert Nodo sNacionalidad  Solicitante

            //Nodo idREF_T_PTipoDocumento Solicitante
            XmlElement idREF_T_PTipoDocumento = doc.CreateElement(string.Empty, "idREF_T_PTipoDocumento", string.Empty);
            atr = doc.CreateAttribute("businessKey");
            atr.Value = "Id_Tipo_Documento='" + modelo.TipoDocumento + "'";
            idREF_T_PTipoDocumento.Attributes.Append(atr);
            idCON_T_MConvalidante.AppendChild(idREF_T_PTipoDocumento);
            //Insert Nodo idREF_T_PTipoDocumento Solicitante

            //Nodo sNumeroDocumento Solicitante
            XmlElement sNumeroDocumento = doc.CreateElement(string.Empty, "sNumeroDocumento", string.Empty);
            text5 = doc.CreateTextNode(modelo.NumeroDocumento);
            sNumeroDocumento.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sNumeroDocumento);
            //Insert Nodo sNumeroDocumento Solicitante

            if (modelo.TipoDocumento.Equals("CC"))
            {
                //Nodo idREF_T_PPais Expedicion doc Solicitante
                XmlElement idREF_T_PPais = doc.CreateElement(string.Empty, "idREF_T_PPais", string.Empty);
                string campoVacio = modelo.Pais == 0 ? "170" : modelo.Pais.ToString();
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_Pais='170'";
                idREF_T_PPais.Attributes.Append(atr);
                idCON_T_MConvalidante.AppendChild(idREF_T_PPais);
                //Insert Nodo idREF_T_PPais Expedicion doc Solicitante

                //Nodo idREF_T_PMunicipio Expedicion doc Solicitante
                XmlElement idREF_T_PMunicipio1 = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "sId_Municipio='" + modelo.CiudadExpedicionCCCod.ToString() + "'";
                idREF_T_PMunicipio1.Attributes.Append(atr);
                idCON_T_MConvalidante.AppendChild(idREF_T_PMunicipio1);
                //Insert Nodo idREF_T_PMunicipio Expedicion doc Solicitante
            }
            else if (modelo.TipoDocumento.Equals("CE"))
            {
                //Nodo idREF_T_PPais Expedicion doc Solicitante
                XmlElement idREF_T_PPais = doc.CreateElement(string.Empty, "idREF_T_PPais", string.Empty);
                string campoVacio = modelo.Pais == 0 ? "170" : modelo.Pais.ToString();
                atr = doc.CreateAttribute("businessKey");
                //atr.Value = "Id_Pais='" +  modelo.Pais + "'"; //OJO AGREGAR PAIS DE EXPEDICION DOC EXTRANGERO
                atr.Value = "Id_Pais='170'";
                idREF_T_PPais.Attributes.Append(atr);
                idCON_T_MConvalidante.AppendChild(idREF_T_PPais);
                //Insert Nodo idREF_T_PPais Expedicion doc Solicitante

                //Nodo idREF_T_PMunicipio Expedicion doc Solicitante
                if (!string.IsNullOrEmpty(modelo.CiudadExpedicion))
                {
                    XmlElement idREF_T_PMunicipio1 = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    atr.Value = "sId_Municipio='" + modelo.CiudadExpedicion + "'";
                    idREF_T_PMunicipio1.Attributes.Append(atr);
                    idCON_T_MConvalidante.AppendChild(idREF_T_PMunicipio1);
                }
                //Insert Nodo idREF_T_PMunicipio Expedicion doc Solicitante
            }

            //Nodo idCON_T_MDireccionesRes
            XmlElement idCON_T_MDireccionesRes = doc.CreateElement(string.Empty, "idCON_T_MDireccionesRes", string.Empty);

            //Nodo idREF_T_PPais Solicitante
            if(modelo.Pais > 0)
            {
                XmlElement idREF_T_PPais1 = doc.CreateElement(string.Empty, "idREF_T_PPais", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_Pais='" + modelo.Pais + "'";
                idREF_T_PPais1.Attributes.Append(atr);
                idCON_T_MDireccionesRes.AppendChild(idREF_T_PPais1);
            }
            //Insert Nodo idREF_T_PPais Solicitante

            //Nodo idREF_T_PMunicipio Solicitante
            if (!string.IsNullOrEmpty(modelo.Ciudad))
            {
                string campoSaludId = WebConfigurationManager.AppSettings["paisColombia"].ToString();
                int campoSaludIdInt = int.Parse(campoSaludId);
                if (modelo.Pais == campoSaludIdInt)
                {
                    int ciu = int.Parse(modelo.Ciudad);
                    if (ciu > 0)
                    {
                        XmlElement idREF_T_PMunicipio = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                        atr = doc.CreateAttribute("businessKey");
                        atr.Value = "sId_Municipio=" + ciu;
                        idREF_T_PMunicipio.Attributes.Append(atr);
                        idCON_T_MDireccionesRes.AppendChild(idREF_T_PMunicipio);
                    }
                }                          
            }            
            //Insert Nodo idREF_T_PMunicipio Solicitante

            //Nodo sCiudad_Extranjera Solicitante
            XmlElement sCiudad_Extranjera = doc.CreateElement(string.Empty, "sCiudad_Extranjera", string.Empty);
            text5 = doc.CreateTextNode(modelo.Ciudad_II);
            sCiudad_Extranjera.AppendChild(text5);
            idCON_T_MDireccionesRes.AppendChild(sCiudad_Extranjera);
            //Insert Nodo sCiudad_Extranjera Solicitante

            //Nodo sCodigoPostal Residencia
            XmlElement sCodigoPostal = doc.CreateElement(string.Empty, "sCodigoPostal", string.Empty);
            text5 = doc.CreateTextNode(modelo.CodigoPostal);
            sCodigoPostal.AppendChild(text5);
            idCON_T_MDireccionesRes.AppendChild(sCodigoPostal);
            //Insert Nodo sCodigoPostal Residencia

            //Nodo sDireccion Residencia Solicitante
            XmlElement sDireccion1 = doc.CreateElement(string.Empty, "sDireccion", string.Empty);
            text5 = doc.CreateTextNode(modelo.Direccion);
            sDireccion1.AppendChild(text5);
            idCON_T_MDireccionesRes.AppendChild(sDireccion1);
            //Insert Nodo sDireccion Residencia  Solicitante          

            //Insert Nodo idCON_T_MDireccionesRes
            idCON_T_MConvalidante.AppendChild(idCON_T_MDireccionesRes);

            //Nodo sCorreo  Solicitante
            XmlElement sCorreo = doc.CreateElement(string.Empty, "sCorreo", string.Empty);
            text5 = doc.CreateTextNode(modelo.Email);
            sCorreo.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sCorreo);
            //Insert Nodo sCorreo Solicitante

            //Nodo sCorreoOpcional Solicitante
            XmlElement sCorreoOpcional = doc.CreateElement(string.Empty, "sCorreoOpcional", string.Empty);
            text5 = doc.CreateTextNode(modelo.Email2);
            sCorreoOpcional.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sCorreoOpcional);
            //Insert Nodo sCorreoOpcional Solicitante

            // Nodo sTelefonoMovil  Solicitante
            XmlElement sTelefonoMovil = doc.CreateElement(string.Empty, "sTelefonoMovil", string.Empty);
            text5 = doc.CreateTextNode(modelo.Celular.ToString());
            sTelefonoMovil.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sTelefonoMovil);
            //Insert Nodo sTelefonoMovil Solicitante

            //Nodo sTelefonoFijo Solicitante
            XmlElement sTelefonoFijo = doc.CreateElement(string.Empty, "sTelefonoFijo", string.Empty);
            text5 = doc.CreateTextNode(modelo.TelefonoFijo == 0 ? "" : modelo.TelefonoFijo.ToString());
            sTelefonoFijo.AppendChild(text5);
            idCON_T_MConvalidante.AppendChild(sTelefonoFijo);
            //Insert Nodo sTelefonoFijo Solicitante

            //Nodo idCON_T_MDireccionesNot
            XmlElement idCON_T_MDireccionesNot = doc.CreateElement(string.Empty, "idCON_T_MDireccionesNot", string.Empty);

            //Nodo sDireccion Solicitante 
            XmlElement sDireccion = doc.CreateElement(string.Empty, "sDireccion", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.Direccion);
            sDireccion.AppendChild(text5);
            idCON_T_MDireccionesNot.AppendChild(sDireccion);
            //Insert Nodo sDireccion Solicitante

            //Nodo sCodigoPostal1 Autorizado
            XmlElement sCodigoPostal1 = doc.CreateElement(string.Empty, "sCodigoPostal", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.CodigoPostal);
            sCodigoPostal1.AppendChild(text5);
            idCON_T_MDireccionesNot.AppendChild(sCodigoPostal1);
            //Insert Nodo sCodigoPostal Autorizado

            //Nodo idREF_T_PMunicipio2 Autorizado
            if (!string.IsNullOrEmpty(modelo._autorizado.Ciudad))
            {
                int ciu1 = int.Parse(modelo.Ciudad);
                if (ciu1 > 0)
                {
                    XmlElement idREF_T_PMunicipio2 = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    atr.Value = "sId_Municipio=" + ciu1;
                    idREF_T_PMunicipio2.Attributes.Append(atr);
                    idCON_T_MDireccionesNot.AppendChild(idREF_T_PMunicipio2);
                }
                
            }           
            //Insert Nodo idREF_T_PMunicipio2 Autorizado

            //Insert Nodo idCON_T_MDireccionesNot
            idCON_T_MConvalidante.AppendChild(idCON_T_MDireccionesNot);

            //Nodo idCON_T_TerceroAutorizado
            XmlElement idCON_T_TerceroAutorizado = doc.CreateElement(string.Empty, "idCON_T_TerceroAutorizado", string.Empty);

            //Nodo sNombre1A Autorizado
            XmlElement sNombre1A = doc.CreateElement(string.Empty, "sNombre1", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.Nombre1Aut); 
            sNombre1A.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sNombre1A);
            //Insert Nodo sNombre1A Autorizado

            //Nodo sNombre2A Autorizado
            XmlElement sNombre2A = doc.CreateElement(string.Empty, "sNombre2", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.Nombre2Aut); 
            sNombre2A.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sNombre2A);
            //Insert Nodo sNombre2 Autorizado

            // Nodo sApellido2A Autorizado
            XmlElement sApellido2A = doc.CreateElement(string.Empty, "sApellido2", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.apellido1Aut);  
            sApellido2A.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sApellido2A);
            //Insert Nodo sApellido2A Autorizado

            //Nodo sApellido1A Autorizado
            XmlElement sApellido1A = doc.CreateElement(string.Empty, "sApellido1", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.apellido2Aut); 
            sApellido1A.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sApellido1A);
            //Insert Nodo sApellido1A Autorizado

            //Nodo idREF_T_PTipoDocumento Autorizado
            if (!string.IsNullOrEmpty(modelo._autorizado.TipoDocumentoAutorizadoCod))
            {
                XmlElement idREF_T_PTipoDocumento1 = doc.CreateElement(string.Empty, "idREF_T_PTipoDocumento", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_Tipo_Documento='" + modelo._autorizado.TipoDocumentoAutorizadoCod + "'";
                idREF_T_PTipoDocumento1.Attributes.Append(atr);
                idCON_T_TerceroAutorizado.AppendChild(idREF_T_PTipoDocumento1);
            }            
            //Insert Nodo idREF_T_PTipoDocumento Autorizado

            //Nodo sNumeroDocumento1 Autorizado
            XmlElement sNumeroDocumento1 = doc.CreateElement(string.Empty, "sNumeroDocumento", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.NumeroDocumento);
            sNumeroDocumento1.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sNumeroDocumento1);
            //Insert Nodo sNumeroDocumento1 Autorizado

            //Nodo sTelefonoMovilA Autorizado
            XmlElement sTelefonoMovilA = doc.CreateElement(string.Empty, "sTelefonoMovil", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.Celular.ToString());
            sTelefonoMovilA.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sTelefonoMovilA);
            //Insert Nodo sTelefonoMovilA Autorizado

            //Nodo sTelefonoFijo1 Autorizado
            XmlElement sTelefonoFijo1 = doc.CreateElement(string.Empty, "sTelefonoFijo", string.Empty);
            text5 = doc.CreateTextNode(modelo._autorizado.TelefonoFijo.ToString());
            sTelefonoFijo1.AppendChild(text5);
            idCON_T_TerceroAutorizado.AppendChild(sTelefonoFijo1);
            //Insert Nodo sTelefonoFijo1 Autorizado

            //Insert Nodo idCON_T_TerceroAutorizado
            idCON_T_MConvalidante.AppendChild(idCON_T_TerceroAutorizado);

            //Insert Nodo idCON_T_MConvalidante
            CON_T_MSolicitud.AppendChild(idCON_T_MConvalidante);

            // Nodo eCON_T_MInstituciones 
            XmlElement eCON_T_MInstituciones = doc.CreateElement(string.Empty, "eCON_T_MInstituciones", string.Empty);

            foreach (Institucion inst in modelo._institucionList)
            {
                // Nodo CON_T_MInstituciones 
                XmlElement CON_T_MInstituciones = doc.CreateElement(string.Empty, "CON_T_MInstituciones", string.Empty);

                // Nodo idREF_T_PMunicipio_Extranj 
                if (!inst.Ciudad.Equals("0"))
                {
                    XmlElement idREF_T_PMunicipio_Extranj = doc.CreateElement(string.Empty, "idREF_T_PMunicipio_Extranj", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    atr.Value = "Id_Ciudad_Ext='" + inst.Ciudad + "'";
                    idREF_T_PMunicipio_Extranj.Attributes.Append(atr);
                    CON_T_MInstituciones.AppendChild(idREF_T_PMunicipio_Extranj);
                }                
                //Insert Nodo idREF_T_PMunicipio_Extranj 

                // Nodo IdREF_T_PInstitucionExt 
                if (!inst.Instituto.Equals("0"))
                {
                    XmlElement IdREF_T_PInstitucionExt = doc.CreateElement(string.Empty, "IdREF_T_PInstitucionExt", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    atr.Value = "ID_INSTITUCION='" + inst.Instituto + "'";
                    IdREF_T_PInstitucionExt.Attributes.Append(atr);
                    CON_T_MInstituciones.AppendChild(IdREF_T_PInstitucionExt);
                    text5 = doc.CreateTextNode("");
                }
                else{
                    text5 = doc.CreateTextNode(inst.InstitutoText);
                }
                //Insert Nodo IdREF_T_PInstitucionExt 

                //Nodo sInstitucionManual 
                XmlElement sInstitucionManual = doc.CreateElement(string.Empty, "sInstitucionManual", string.Empty);                
                sInstitucionManual.AppendChild(text5);
                CON_T_MInstituciones.AppendChild(sInstitucionManual);
                //Insert Nodo sInstitucionManual

                // Nodo sEstado 
                XmlElement sEstado = doc.CreateElement(string.Empty, "sEstado", string.Empty);
                text5 = doc.CreateTextNode(inst.Estado);
                sEstado.AppendChild(text5);
                CON_T_MInstituciones.AppendChild(sEstado);
                //Insert Nodo sEstado 

                // Nodo sInstituto 
                XmlElement sInstituto = doc.CreateElement(string.Empty, "sInstituto", string.Empty);
                text5 = doc.CreateTextNode(inst.Facultad);
                sInstituto.AppendChild(text5);
                CON_T_MInstituciones.AppendChild(sInstituto);
                //Insert Nodo sInstituto 

                //// Nodo sNombreInstitucion 
                //XmlElement sNombreInstitucion = doc.CreateElement(string.Empty, "sNombreInstitucion", string.Empty);
                //text5 = doc.CreateTextNode(inst.Instituto);
                //sNombreInstitucion.AppendChild(text5);
                //IdREF_T_PInstitucionExt.AppendChild(sNombreInstitucion);
                ////Insert Nodo sNombreInstitucion 

                //Insert Nodo CON_T_MInstituciones 
                eCON_T_MInstituciones.AppendChild(CON_T_MInstituciones);
            }

            //Insert Nodo eCON_T_MInstituciones 
            CON_T_MSolicitud.AppendChild(eCON_T_MInstituciones);

            //Nodo idCON_T_MPrograma
            XmlElement idCON_T_MPrograma = doc.CreateElement(string.Empty, "idCON_T_MPrograma", string.Empty);

            //Nodo idREF_T_PNivel_Formacion 
            XmlElement idREF_T_PNivel_Formacion = doc.CreateElement(string.Empty, "idREF_T_PNivel_Formacion", string.Empty);
            atr = doc.CreateAttribute("businessKey");
            atr.Value = "Id_Nivel_Formacion='" + modelo._programa.TipoEducacionSuperior + "'";
            idREF_T_PNivel_Formacion.Attributes.Append(atr);            
            idCON_T_MPrograma.AppendChild(idREF_T_PNivel_Formacion);
            //Insert Nodo idREF_T_PNivel_Formacion 

            //Nodo idREF_T_PArea_Conocimiento 
            //int idCine = modelo._programa.CampoAmplioConocimiento == 0 ? 91 : modelo._programa.CampoAmplioConocimiento;
            if(modelo._programa.CampoAmplioConocimiento >= 0)
            {
                XmlElement idREF_T_PArea_Conocimiento = doc.CreateElement(string.Empty, "idREF_T_PArea_Conocimiento", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_CINE='" + modelo._programa.CampoAmplioConocimiento + "'";
                idREF_T_PArea_Conocimiento.Attributes.Append(atr);
                idCON_T_MPrograma.AppendChild(idREF_T_PArea_Conocimiento);
                //Insert Nodo idREF_T_PArea_Conocimiento 

                if (modelo._programa.CampoAmplioConocimiento == 41)
                {
                    //Pregunta contaduría
                    //Nodo bElProgramaesContaduria
                    XmlElement bElProgramaesContaduria = doc.CreateElement(string.Empty, "bElProgramaesContaduria", string.Empty);
                    text5 = doc.CreateTextNode(modelo.solicitudActual.notificacionElectronica.ToString());//Bool cobtaduría
                    bElProgramaesContaduria.AppendChild(text5);
                    idCON_T_MPrograma.AppendChild(bElProgramaesContaduria);
                    //Insert Nodo idREF_T_PArea_Conocimiento 
                }
            }


            //Nodo idREF_T_PEnfasis 
            XmlElement idREF_T_PEnfasis = doc.CreateElement(string.Empty, "idREF_T_PEnfasis", string.Empty);
            atr = doc.CreateAttribute("businessKey");
            atr.Value = "Id_EnFasis='" + modelo._programa.TipoMaestria + "'";
            idREF_T_PEnfasis.Attributes.Append(atr);
            idCON_T_MPrograma.AppendChild(idREF_T_PEnfasis);
            //Insert Nodo idREF_T_PEnfasis 
                        
            if (!modelo._programa.NombrePrograma.Equals("0"))
            {
                //Nodo NombrePrograma
                XmlElement NombrePrograma = doc.CreateElement(string.Empty, "NombrePrograma", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "ID_PROGRAMA_EXTRANJERO=" + modelo._programa.NombrePrograma;
                NombrePrograma.Attributes.Append(atr);
                idCON_T_MPrograma.AppendChild(NombrePrograma);
                //Insert Nodo NombrePrograma
            }
            else
            {
                // Nodo sNombreProgramaManual 
                XmlElement sNombreProgramaManual = doc.CreateElement(string.Empty, "sNombreProgramaManual", string.Empty);
                text5 = doc.CreateTextNode(modelo._programa.NombreProgramaText);
                sNombreProgramaManual.AppendChild(text5);
                idCON_T_MPrograma.AppendChild(sNombreProgramaManual);
                //Insert Nodo sNombreProgramaManual 

                // Nodo sTituloOtorgadoManual 
                XmlElement sTituloOtorgadoManual = doc.CreateElement(string.Empty, "sTituloOtorgadoManual", string.Empty);
                text5 = doc.CreateTextNode(modelo._programa.TituloObtenidoManual);
                sTituloOtorgadoManual.AppendChild(text5);
                idCON_T_MPrograma.AppendChild(sTituloOtorgadoManual);
                //Insert Nodo sNombreProgramaManual 
            }
            
            // Nodo sDuracion_Programa 
            XmlElement sDuracion_Programa = doc.CreateElement(string.Empty, "sDuracion_Programa", string.Empty);
            text5 = doc.CreateTextNode(modelo._programa.Duracion.ToString());
            sDuracion_Programa.AppendChild(text5);
            idCON_T_MPrograma.AppendChild(sDuracion_Programa);
            //Insert Nodo sDuracion_Programa 

            // Nodo idREF_T_PPeriodicidad 
            XmlElement idREF_T_PPeriodicidad = doc.CreateElement(string.Empty, "idREF_T_PPeriodicidad", string.Empty);
            atr = doc.CreateAttribute("businessKey");
            atr.Value = "Id_Periodicidad='" + modelo._programa.TipoDuracion + "'";
            idREF_T_PPeriodicidad.Attributes.Append(atr);
            idCON_T_MPrograma.AppendChild(idREF_T_PPeriodicidad);
            //Insert Nodo sDuracion_Programa 

            // Nodo idREF_T_PMetodologia 
            XmlElement idREF_T_PMetodologia = doc.CreateElement(string.Empty, "idREF_T_PMetodologia", string.Empty);
            atr = doc.CreateAttribute("businessKey");
            //atr.Value = "sId_Metodologia='" + modelo._programa.ModalidadPrograma + "'";
            atr.Value = "sId_Metodologia='50'";
            idREF_T_PMetodologia.Attributes.Append(atr);
            idCON_T_MPrograma.AppendChild(idREF_T_PMetodologia);
            //Insert Nodo idREF_T_PMetodologia 

            // Nodo dFechaTitulo 
            XmlElement dFecha_Titulo1 = doc.CreateElement(string.Empty, "dFecha_Titulo", string.Empty);
            XmlText text51 = doc.CreateTextNode(modelo._programa.FechaTitulo.ToString("o"));
            dFecha_Titulo1.AppendChild(text51);
            idCON_T_MPrograma.AppendChild(dFecha_Titulo1);
            //Insert Nodo dFechaTitulo 

            //Nodo idCON_T_MPregrado 
            XmlElement idCON_T_MPregrado = doc.CreateElement(string.Empty, "idCON_T_MPregrado", string.Empty);

            if (modelo._infoPregrado.CiudadInstitutoPre > 0)
            {
                //Nodo idREF_T_PMunicipio 
                XmlElement idREF_T_PMunicipio11 = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "sId_Municipio='" + modelo._infoPregrado.CiudadInstitutoPre + "'";
                idREF_T_PMunicipio11.Attributes.Append(atr);
                idCON_T_MPregrado.AppendChild(idREF_T_PMunicipio11);
                //Insert Nodo idREF_T_PMunicipio 
            }


            if (!string.IsNullOrEmpty(modelo._infoPregrado.Instituto))
            {
                //Nodo idREF_T_PInstitucion_Educa 
                XmlElement idREF_T_PInstitucion_Educa = doc.CreateElement(string.Empty, "idREF_T_PInstitucion_Educa", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "ID_INSTITUCION='" + modelo._infoPregrado.Instituto + "'";
                idREF_T_PInstitucion_Educa.Attributes.Append(atr);
                idCON_T_MPregrado.AppendChild(idREF_T_PInstitucion_Educa);
                //Insert Nodo idREF_T_PInstitucion_Educa 

                //Nodo IdProgramas 
                XmlElement IdProgramas = doc.CreateElement(string.Empty, "IdProgramas", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "ID_PROGRAMA='" + modelo._infoPregrado.NombreProgramaOtorgaPre + "' AND ID_INSTITUCION='" + modelo._infoPregrado.Instituto + "'"; 
                IdProgramas.Attributes.Append(atr);
                idCON_T_MPregrado.AppendChild(IdProgramas);
                //Insert Nodo IdProgramas 
            }

            // Nodo dFechaTitulo Pregrado
            XmlElement dFecha_Titulo = doc.CreateElement(string.Empty, "dFechaTitulo", string.Empty);
            text5 = doc.CreateTextNode(modelo._infoPregrado.FechaTitulo.ToString("o"));
            dFecha_Titulo.AppendChild(text5);
            idCON_T_MPregrado.AppendChild(dFecha_Titulo);
            //Insert Nodo dFechaTitulo Pregrado

            //Nodo idREF_T_PEntidadesC 
            if (!string.IsNullOrEmpty(modelo._infoPregrado.Entidad))
            {
                XmlElement idREF_T_PEntidadesC = doc.CreateElement(string.Empty, "idREF_T_PEntidadesC", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_Entidad_Convalidante='" + modelo._infoPregrado.Entidad + "'";
                idREF_T_PEntidadesC.Attributes.Append(atr);
                idCON_T_MPregrado.AppendChild(idREF_T_PEntidadesC);
            }           
            //Insert Nodo idREF_T_PEntidadesC 

            // Nodo sNumeroResolucion Pregrado
            XmlElement sNumeroResolucion = doc.CreateElement(string.Empty, "sNumeroResolucion", string.Empty);
            text5 = doc.CreateTextNode(modelo._infoPregrado.NroResolucion);
            sNumeroResolucion.AppendChild(text5);
            idCON_T_MPregrado.AppendChild(sNumeroResolucion);
            //Insert Nodo dFechaTitulo Pregrado

            // Nodo dFechaResolucion Pregrado
            XmlElement dFechaResolucion = doc.CreateElement(string.Empty, "dFechaResolucion", string.Empty);
            text5 = doc.CreateTextNode(modelo._infoPregrado.FechaResolucion.ToString("o"));
            dFechaResolucion.AppendChild(text5);
            idCON_T_MPregrado.AppendChild(dFechaResolucion);
            //Insert Nodo dFechaTitulo Pregrado

            //Insert Nodo idCON_T_MPregrado 
            idCON_T_MPrograma.AppendChild(idCON_T_MPregrado);

            //Nodo idCON_T_MPregradoEspBase 
            XmlElement idCON_T_MPregradoEspBase = doc.CreateElement(string.Empty, "idCON_T_MPregradoEspBase", string.Empty);
            atr = doc.CreateAttribute("entityName");
            atr.Value = "CON_T_MPregrado";
            idCON_T_MPregradoEspBase.Attributes.Append(atr);

            if(modelo._segundaEspecialidad.CiudadInstitutoPre > 0)
            {
                //Nodo idREF_T_PMunicipio  Segunda Especialidad
                XmlElement idREF_T_PMunicipioS = doc.CreateElement(string.Empty, "idREF_T_PMunicipio", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                //atr.Value = "sId_Municipio='05001'"; //OJO CREAR CAMPO CIUDAD EN SEGUNDA ESPECIALIDAD
                atr.Value = "sId_Municipio='" + modelo._segundaEspecialidad.CiudadInstitutoPre + "'";
                idREF_T_PMunicipioS.Attributes.Append(atr);
                idCON_T_MPregradoEspBase.AppendChild(idREF_T_PMunicipioS);
                //Insert Nodo idREF_T_PMunicipio  Segunda Especialidad
            }

            if (!string.IsNullOrEmpty(modelo._segundaEspecialidad.Instituto))
            {
                if (!modelo._segundaEspecialidad.Instituto.Equals("0"))
                {
                    //Nodo idREF_T_PInstitucion_Educa   Segunda Especialidad
                    XmlElement idREF_T_PInstitucion_EducaS = doc.CreateElement(string.Empty, "idREF_T_PInstitucion_Educa", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    atr.Value = "ID_INSTITUCION='" + modelo._segundaEspecialidad.Instituto + "'";
                    idREF_T_PInstitucion_EducaS.Attributes.Append(atr);
                    idCON_T_MPregradoEspBase.AppendChild(idREF_T_PInstitucion_EducaS);
                    //Insert Nodo idREF_T_PInstitucion_Educa   Segunda Especialidad

                    //Nodo IdProgramas  Segunda Especialidad
                    XmlElement IdProgramasS = doc.CreateElement(string.Empty, "IdProgramas", string.Empty);
                    atr = doc.CreateAttribute("businessKey");
                    //atr.Value = "ID_PROGRAMA='53101' AND ID_INSTITUCION = '" + modelo._segundaEspecialidad.Instituto + "'";// OJO AGREGAR PROGRAMA EN MODELO
                    atr.Value = "ID_PROGRAMA='" + modelo._segundaEspecialidad.NombreProgramaOtorgaPre + "' AND ID_INSTITUCION = '" + modelo._segundaEspecialidad.Instituto + "'";
                    IdProgramasS.Attributes.Append(atr);
                    idCON_T_MPregradoEspBase.AppendChild(IdProgramasS);
                    //Insert Nodo IdProgramas  Segunda Especialidad 
                }

            }

            // Nodo dFechaTitulo  Segunda Especialidad
            XmlElement dFecha_TituloS = doc.CreateElement(string.Empty, "dFechaTitulo", string.Empty);
            text5 = doc.CreateTextNode(modelo._segundaEspecialidad.FechaTitulo.ToString("o"));
            dFecha_TituloS.AppendChild(text5);
            idCON_T_MPregradoEspBase.AppendChild(dFecha_TituloS);
            //Insert Nodo dFechaTitulo  Segunda Especialidad

            if (!string.IsNullOrEmpty(modelo._segundaEspecialidad.Entidad))
            {
                //Nodo idREF_T_PEntidadesC  Segunda Especialidad
                XmlElement idREF_T_PEntidadesCS = doc.CreateElement(string.Empty, "idREF_T_PEntidadesC", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "Id_Entidad_Convalidante='" + modelo._segundaEspecialidad.Entidad + "'";
                idREF_T_PEntidadesCS.Attributes.Append(atr);
                idCON_T_MPregradoEspBase.AppendChild(idREF_T_PEntidadesCS);
                //Insert Nodo idREF_T_PEntidadesC  Segunda Especialidad
            }


            // Nodo sNumeroResolucion  Segunda Especialidad
            XmlElement sNumeroResolucionS = doc.CreateElement(string.Empty, "sNumeroResolucion", string.Empty);
            text5 = doc.CreateTextNode(modelo._segundaEspecialidad.NroResolucion);
            sNumeroResolucionS.AppendChild(text5);
            idCON_T_MPregradoEspBase.AppendChild(sNumeroResolucionS);
            //Insert Nodo dFechaTitulo  Segunda Especialidad

            // Nodo dFechaResolucion  Segunda Especialidad
            XmlElement dFechaResolucionS = doc.CreateElement(string.Empty, "dFechaResolucion", string.Empty);
            text5 = doc.CreateTextNode(modelo._segundaEspecialidad.FechaResolucion.ToString("o"));
            dFechaResolucionS.AppendChild(text5);
            idCON_T_MPregradoEspBase.AppendChild(dFechaResolucionS);
            //Insert Nodo dFechaTitulo Segunda Especialidad

            //Insert Nodo idCON_T_MPregradoEspBase 
            idCON_T_MPrograma.AppendChild(idCON_T_MPregradoEspBase);

            //Insert Nodo idCON_T_MPrograma
            CON_T_MSolicitud.AppendChild(idCON_T_MPrograma);

            // Nodo eCON_T_MDocumentos 
            XmlElement eCON_T_MDocumentos = doc.CreateElement(string.Empty, "eCON_T_MDocumentos", string.Empty);

            foreach (Documentos docs in modelo.lstDocumentos)
            {
                // Nodo CON_T_MDocumentos 
                XmlElement CON_T_MDocumentos = doc.CreateElement(string.Empty, "CON_T_MDocumentos", string.Empty);

                // Nodo idCON_T_PDocumentos 
                XmlElement idCON_T_PDocumentos = doc.CreateElement(string.Empty, "idCON_T_PDocumentos", string.Empty);
                atr = doc.CreateAttribute("businessKey");
                atr.Value = "sCodigo='" + docs.codigoDoc + "'";
                idCON_T_PDocumentos.Attributes.Append(atr);
                CON_T_MDocumentos.AppendChild(idCON_T_PDocumentos);
                //Insert Nodo idCON_T_PDocumentos 

                // Nodo sURLDocTraducido 
                XmlElement sURLDocTraducido = doc.CreateElement(string.Empty, "sURLDocTraducido", string.Empty);
                text5 = doc.CreateTextNode(docs.sharepoint);
                sURLDocTraducido.AppendChild(text5);
                CON_T_MDocumentos.AppendChild(sURLDocTraducido);
                //Insert Nodo sURLDocTraducido 

                // Nodo sURLDocOriginal 
                XmlElement sURLDocOriginal = doc.CreateElement(string.Empty, "sURLDocOriginal", string.Empty);
                text5 = doc.CreateTextNode(docs.rutaFisica);
                sURLDocOriginal.AppendChild(text5);
                CON_T_MDocumentos.AppendChild(sURLDocOriginal);
                //Insert Nodo sURLDocTraducido 

                //Insert Nodo CON_T_MDocumentos 
                eCON_T_MDocumentos.AppendChild(CON_T_MDocumentos);
            }

            //Insert Nodo eCON_T_MDocumentos 
            CON_T_MSolicitud.AppendChild(eCON_T_MDocumentos);

            //Insert Nodo CON_T_MSolicitud
            CON_T_MConvalidaciones.AppendChild(CON_T_MSolicitud);

            //Insert Nodo CON_T_MConvalidaciones
            Entities.AppendChild(CON_T_MConvalidaciones);

            //Insert Nodo Entities
            Case.AppendChild(Entities);

            //Insert Nodo Case
            Cases.AppendChild(Case);

            //Insert Nodo Cases
            BizAgiWSParam.AppendChild(Cases);

            //Inserta Nodo princial
            doc.AppendChild(BizAgiWSParam);

            XmlNode resp = doc.FirstChild;
            return resp;
        }
    }
}
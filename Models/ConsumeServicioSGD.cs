using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ConvalidacionEducacionSuperior.Models
{
    public class ConsumeServicioSGD
    {
        public Paso3Model modelo;

        public ConsumeServicioSGD(Paso3Model _modelo)
        {
            modelo = _modelo;
        }

        public string RadicaDocumento()
        {
            string respuesta = string.Empty;
            try
            {
                ServicioSGD.ServiceInteropClient servSgdCliente = new ServicioSGD.ServiceInteropClient();
                ServicioSGD.UsrEntVigilada entVigilada = EntidadVigilada();
                ServicioSGD.DocumentoRadicarIn docRadicar = DocumentoRadicar();

                ServicioSGD.DocumentoRadicarOut response = servSgdCliente.DocumentoRadicar(entVigilada, docRadicar);

                respuesta = response.NumeroRadicado;

                return respuesta;
                
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.InnerException.Message == null ? ex.Message : ex.InnerException.Message;
            }
            
        }

        public ServicioSGD.UsrEntVigilada EntidadVigilada()
        {
            ServicioSGD.UsrEntVigilada entVigilada = new ServicioSGD.UsrEntVigilada();
            entVigilada.idAppEntidad = "ENTIDAD_GENERICA";
            entVigilada.idUsuario = "79804555";
            entVigilada.password = "Convalidaciones2019*";

            return entVigilada;
        }

        public ServicioSGD.DocumentoRadicarIn DocumentoRadicar()
        {
            ServicioSGD.DocumentoRadicarIn docRadicar = new ServicioSGD.DocumentoRadicarIn();
            //docRadicar.documentoXMLTMS = construyeDocXMLTMS();

            string ruta = HttpContext.Current.Server.MapPath("../Archivos") + "\\ModeloXml";
            XElement root = XElement.Load(ruta);
            XmlDocument xD = new XmlDocument();
            xD.LoadXml(root.ToString());
            XmlNode xN = xD.FirstChild;
            docRadicar.documentoXMLTMS = xN.InnerXml;

            System.Runtime.Serialization.ExtensionDataObject extObj = null;

            docRadicar.ExtensionData = extObj;

            return docRadicar;
        }

        public string construyeDocXMLTMS()
        {
            string respuesta = string.Empty;

            XmlDocument doc = new XmlDocument();

            //Nodo Principal
            XmlElement documentoXMLTMS = doc.CreateElement(string.Empty, "documentoXMLTMS", string.Empty);
            //Nodo Principal_0
            XmlElement TMS = doc.CreateElement(string.Empty, "TMS", string.Empty);
            //Nodo Secundario 1 - SalidaCorrespondencia
            XmlElement SalidaCorrespondencia = doc.CreateElement(string.Empty, "SalidaCorrespondencia", string.Empty);

            //Nodo ENCABEZADO
            XmlElement ENCABEZADO = doc.CreateElement(string.Empty, "ENCABEZADO", string.Empty);
            XmlAttribute atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            ENCABEZADO.Attributes.Append(atr);

            //Nodo ID_DOC_PPAL
            XmlElement ID_DOC_PPAL = doc.CreateElement(string.Empty, "ID_DOC_PPAL", string.Empty);
            XmlText text5 = doc.CreateTextNode("");
            ID_DOC_PPAL.AppendChild(text5);
            ENCABEZADO.AppendChild(ID_DOC_PPAL);
            //Agraga Nodo ID_DOC_PPAL

            //Nodo Rotulo_Ciudad
            XmlElement Rotulo_Ciudad = doc.CreateElement(string.Empty, "Rotulo_Ciudad", string.Empty);
            text5 = doc.CreateTextNode("");
            ID_DOC_PPAL.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo_Ciudad);
            //Agraga Nodo Rotulo_Ciudad

            //Nodo Rotulo_Ent_Destino
            XmlElement Rotulo_Ent_Destino = doc.CreateElement(string.Empty, "Rotulo_Ent_Destino", string.Empty);
            text5 = doc.CreateTextNode("");
            ID_DOC_PPAL.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo_Ent_Destino);
            //Agraga Nodo Rotulo_Ent_Destino

            //Nodo Rotulo_Asunto1pte
            XmlElement Rotulo_Asunto1pte = doc.CreateElement(string.Empty, "Rotulo_Asunto1pte", string.Empty);
            text5 = doc.CreateTextNode("Comunicación Registro Convalidaciones IES 1");
            Rotulo_Asunto1pte.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo_Asunto1pte);
            //Agraga Nodo Rotulo_Asunto1pte

            //Nodo Rotulo_Asunto2pte
            XmlElement Rotulo_Asunto2pte = doc.CreateElement(string.Empty, "Rotulo_Asunto2pte", string.Empty);
            text5 = doc.CreateTextNode("16 de julio de 2019");
            Rotulo_Asunto2pte.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo_Asunto2pte);
            //Agraga Nodo Rotulo_Asunto2pte

            //Nodo Rotulo2_cliente
            XmlElement Rotulo2_cliente = doc.CreateElement(string.Empty, "Rotulo2_cliente", string.Empty);
            text5 = doc.CreateTextNode("");
            Rotulo2_cliente.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo2_cliente);
            //Agraga Nodo Rotulo2_cliente

            //Nodo Ciudad_Radicacion
            XmlElement Ciudad_Radicacion = doc.CreateElement(string.Empty, "Ciudad_Radicacion", string.Empty);
            text5 = doc.CreateTextNode("Bogotá D.C");
            Ciudad_Radicacion.AppendChild(text5);
            ENCABEZADO.AppendChild(Ciudad_Radicacion);
            //Agraga Nodo Ciudad_Radicacion

            //Nodo Rotulo2_Direccion
            XmlElement Rotulo2_Direccion = doc.CreateElement(string.Empty, "Rotulo2_Direccion", string.Empty);
            text5 = doc.CreateTextNode("carrera 1 No. 00-01");
            Rotulo2_Direccion.AppendChild(text5);
            ENCABEZADO.AppendChild(Rotulo2_Direccion);
            //Agraga Nodo Rotulo2_Direccion

            SalidaCorrespondencia.AppendChild(ENCABEZADO);
            //Agraga Nodo ENCABEZADO

            //Nodo TMS_RADICACION
            XmlElement TMS_RADICACION = doc.CreateElement(string.Empty, "TMS_RADICACION", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_RADICACION.Attributes.Append(atr);

            //Nodo CTmsTipoDocRadicacion
            XmlElement CTmsTipoDocRadicacion = doc.CreateElement(string.Empty, "CTmsTipoDocRadicacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsTipoDocRadicacion.AppendChild(text5);
            TMS_RADICACION.AppendChild(CTmsTipoDocRadicacion);
            //Agraga Nodo CTmsTipoDocRadicacion

            //Nodo CTmsRadicacion
            XmlElement CTmsRadicacion = doc.CreateElement(string.Empty, "CTmsRadicacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsRadicacion.AppendChild(text5);
            TMS_RADICACION.AppendChild(CTmsRadicacion);
            //Agraga Nodo CTmsRadicacion

            //Nodo FechaRadicacion
            XmlElement FechaRadicacion = doc.CreateElement(string.Empty, "FechaRadicacion", string.Empty);
            text5 = doc.CreateTextNode("");
            FechaRadicacion.AppendChild(text5);
            TMS_RADICACION.AppendChild(FechaRadicacion);
            //Agraga Nodo FechaRadicacion

            SalidaCorrespondencia.AppendChild(TMS_RADICACION);
            //Agraga Nodo TMS_RADICACION

            //Nodo etiqueta_1
            XmlElement etiqueta_1 = doc.CreateElement(string.Empty, "etiqueta_1", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            etiqueta_1.Attributes.Append(atr);

            SalidaCorrespondencia.AppendChild(etiqueta_1);
            //Agraga Nodo etiqueta_1

            //Nodo CLIENTE
            XmlElement CLIENTE = doc.CreateElement(string.Empty, "CLIENTE", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            CLIENTE.Attributes.Append(atr);

            //Nodo CTMS_nom_cliente
            XmlElement CTMS_nom_cliente = doc.CreateElement(string.Empty, "CTMS_nom_cliente", string.Empty);
            text5 = doc.CreateTextNode("Juan Jose Francisco Jiminez Lopez");
            CTMS_nom_cliente.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_nom_cliente);
            //Agraga Nodo CTMS_nom_cliente

            //Nodo CTMS_direccion
            XmlElement CTMS_direccion = doc.CreateElement(string.Empty, "CTMS_direccion", string.Empty);
            text5 = doc.CreateTextNode("carrera 1 No. 00-01");
            CTMS_direccion.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_direccion);
            //Agraga Nodo CTMS_direccion

            //Nodo CTMS_nit
            XmlElement CTMS_nit = doc.CreateElement(string.Empty, "CTMS_nit", string.Empty);
            text5 = doc.CreateTextNode("876543");
            CTMS_nit.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_nit);
            //Agraga Nodo CTMS_nit

            //Nodo V_Ciudad
            XmlElement V_Ciudad = doc.CreateElement(string.Empty, "V_Ciudad", string.Empty);
            text5 = doc.CreateTextNode("Bogota");
            V_Ciudad.AppendChild(text5);
            CLIENTE.AppendChild(V_Ciudad);
            //Agraga Nodo V_Ciudad

            //Nodo Departamento
            XmlElement Departamento = doc.CreateElement(string.Empty, "Departamento", string.Empty);
            text5 = doc.CreateTextNode("Cundinamarca");
            Departamento.AppendChild(text5);
            CLIENTE.AppendChild(Departamento);
            //Agraga Nodo Departamento

            //Nodo Firmante
            XmlElement Firmante = doc.CreateElement(string.Empty, "Firmante", string.Empty);
            text5 = doc.CreateTextNode("Juan Jose Francisco Jiminez Lopez.");
            Firmante.AppendChild(text5);
            CLIENTE.AppendChild(Firmante);
            //Agraga Nodo Firmante

            //Nodo CTMS_id_cliente
            XmlElement CTMS_id_cliente = doc.CreateElement(string.Empty, "CTMS_id_cliente", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_id_cliente.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_id_cliente);
            //Agraga Nodo CTMS_id_cliente

            //Nodo CTMS_id_localizacion
            XmlElement CTMS_id_localizacion = doc.CreateElement(string.Empty, "CTMS_id_localizacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_id_localizacion.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_id_localizacion);
            //Agraga Nodo CTMS_id_localizacion

            //Nodo CTMS_tipo_cliente
            XmlElement CTMS_tipo_cliente = doc.CreateElement(string.Empty, "CTMS_tipo_cliente", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_tipo_cliente.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_tipo_cliente);
            //Agraga Nodo CTMS_tipo_cliente

            //Nodo CTMS_clase_persona
            XmlElement CTMS_clase_persona = doc.CreateElement(string.Empty, "CTMS_clase_persona", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_clase_persona.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_clase_persona);
            //Agraga Nodo CTMS_clase_persona

            //Nodo CTMS_actualizacion
            XmlElement CTMS_actualizacion = doc.CreateElement(string.Empty, "CTMS_actualizacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_actualizacion.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_actualizacion);
            //Agraga Nodo CTMS_actualizacion

            //Nodo CTMS_admonTablabd
            XmlElement CTMS_admonTablabd = doc.CreateElement(string.Empty, "CTMS_admonTablabd", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_admonTablabd.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_admonTablabd);
            //Agraga Nodo CTMS_admonTablabd

            //Nodo CTMS_nombreTabla
            XmlElement CTMS_nombreTabla = doc.CreateElement(string.Empty, "CTMS_nombreTabla", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_nombreTabla.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_nombreTabla);
            //Agraga Nodo CTMS_nombreTabla

            //Nodo CTMS_pkTabla
            XmlElement CTMS_pkTabla = doc.CreateElement(string.Empty, "CTMS_pkTabla", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMS_pkTabla.AppendChild(text5);
            CLIENTE.AppendChild(CTMS_pkTabla);
            //Agraga Nodo CTMS_pkTabla

            //Nodo CTMSIdBloque
            XmlElement CTMSIdBloque = doc.CreateElement(string.Empty, "CTMSIdBloque", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMSIdBloque.AppendChild(text5);
            CLIENTE.AppendChild(CTMSIdBloque);
            //Agraga Nodo CTMSIdBloque

            SalidaCorrespondencia.AppendChild(CLIENTE);
            //Agraga Nodo CLIENTE

            //Nodo ASUNTO
            XmlElement ASUNTO = doc.CreateElement(string.Empty, "ASUNTO", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            ASUNTO.Attributes.Append(atr);

            //Nodo AsuntoSolicitud
            XmlElement AsuntoSolicitud = doc.CreateElement(string.Empty, "AsuntoSolicitud", string.Empty);
            text5 = doc.CreateTextNode("Comunicación Registro Convalidaciones IES");
            AsuntoSolicitud.AppendChild(text5);
            ASUNTO.AppendChild(AsuntoSolicitud);
            //Agraga Nodo AsuntoSolicitud

            //Nodo BDIDPlataforma
            XmlElement BDIDPlataforma = doc.CreateElement(string.Empty, "BDIDPlataforma", string.Empty);
            text5 = doc.CreateTextNode("");
            BDIDPlataforma.AppendChild(text5);
            ASUNTO.AppendChild(BDIDPlataforma);
            //Agraga Nodo BDIDPlataforma

            //Nodo BDIDTipo
            XmlElement BDIDTipo = doc.CreateElement(string.Empty, "BDIDTipo", string.Empty);
            text5 = doc.CreateTextNode("");
            BDIDTipo.AppendChild(text5);
            ASUNTO.AppendChild(BDIDTipo);
            //Agraga Nodo BDIDTipo

            //Nodo Tipo_requerimiento
            XmlElement Tipo_requerimiento = doc.CreateElement(string.Empty, "Tipo_requerimiento", string.Empty);
            text5 = doc.CreateTextNode("");
            Tipo_requerimiento.AppendChild(text5);
            ASUNTO.AppendChild(Tipo_requerimiento);
            //Agraga Nodo Tipo_requerimiento

            //Nodo Tradicion
            XmlElement Tradicion = doc.CreateElement(string.Empty, "Tradicion", string.Empty);
            text5 = doc.CreateTextNode("0");
            Tradicion.AppendChild(text5);
            ASUNTO.AppendChild(Tradicion);
            //Agraga Nodo Tipo_requerimiento

            //Nodo Nombre_Plataforma
            XmlElement Nombre_Plataforma = doc.CreateElement(string.Empty, "Nombre_Plataforma", string.Empty);
            text5 = doc.CreateTextNode("");
            Nombre_Plataforma.AppendChild(text5);
            ASUNTO.AppendChild(Nombre_Plataforma);
            //Agraga Nodo Nombre_Plataforma

            //Nodo s_id_cliente
            XmlElement s_id_cliente = doc.CreateElement(string.Empty, "s_id_cliente", string.Empty);
            text5 = doc.CreateTextNode("");
            s_id_cliente.AppendChild(text5);
            ASUNTO.AppendChild(s_id_cliente);
            //Agraga Nodo s_id_cliente

            //Nodo Asunto
            XmlElement Asunto = doc.CreateElement(string.Empty, "Asunto", string.Empty);
            text5 = doc.CreateTextNode("");
            Asunto.AppendChild(text5);
            ASUNTO.AppendChild(Asunto);
            //Agraga Nodo Asunto

            SalidaCorrespondencia.AppendChild(ASUNTO);
            //Agraga Nodo ASUNTO

            //Nodo Blq_resumen
            XmlElement Blq_resumen = doc.CreateElement(string.Empty, "Blq_resumen", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            Blq_resumen.Attributes.Append(atr);

            //Nodo Resumen
            XmlElement Resumen = doc.CreateElement(string.Empty, "Resumen", string.Empty);
            text5 = doc.CreateTextNode("");
            Resumen.AppendChild(text5);
            Blq_resumen.AppendChild(Resumen);
            //Agraga Nodo Resumen

            SalidaCorrespondencia.AppendChild(Blq_resumen);
            //Agraga Nodo Blq_resumen

            //Nodo etiqueta2
            XmlElement etiqueta2 = doc.CreateElement(string.Empty, "etiqueta2", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            etiqueta2.Attributes.Append(atr);

            SalidaCorrespondencia.AppendChild(etiqueta2);
            //Agraga Nodo etiqueta2

            //Nodo TMS_ASUNTO
            XmlElement TMS_ASUNTO = doc.CreateElement(string.Empty, "TMS_ASUNTO", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_ASUNTO.Attributes.Append(atr);

            //Nodo CTMSNumsRadicacion
            XmlElement CTMSNumsRadicacion = doc.CreateElement(string.Empty, "CTMSNumsRadicacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTMSNumsRadicacion.AppendChild(text5);
            TMS_ASUNTO.AppendChild(CTMSNumsRadicacion);
            //Agraga Nodo CTMSNumsRadicacion

            //Nodo NumeroRadicadicacionOrigen
            XmlElement NumeroRadicadicacionOrigen = doc.CreateElement(string.Empty, "NumeroRadicadicacionOrigen", string.Empty);
            text5 = doc.CreateTextNode("");
            NumeroRadicadicacionOrigen.AppendChild(text5);
            TMS_ASUNTO.AppendChild(NumeroRadicadicacionOrigen);
            //Agraga Nodo NumeroRadicadicacionOrigen

            //Nodo NodeFolios
            XmlElement NodeFolios = doc.CreateElement(string.Empty, "NodeFolios", string.Empty);
            text5 = doc.CreateTextNode("1");
            NodeFolios.AppendChild(text5);
            TMS_ASUNTO.AppendChild(NodeFolios);
            //Agraga Nodo NodeFolios

            //Nodo NodeAnexos
            XmlElement NodeAnexos = doc.CreateElement(string.Empty, "NodeAnexos", string.Empty);
            text5 = doc.CreateTextNode("0");
            NodeAnexos.AppendChild(text5);
            TMS_ASUNTO.AppendChild(NodeAnexos);
            //Agraga Nodo NodeAnexos

            SalidaCorrespondencia.AppendChild(TMS_ASUNTO);
            //Agraga Nodo TMS_ASUNTO

            //Nodo CTMSANEXO
            XmlElement CTMSANEXO = doc.CreateElement(string.Empty, "CTMSANEXO", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            CTMSANEXO.Attributes.Append(atr);

            //Nodo CTmsIdBloqueAnexo
            XmlElement CTmsIdBloqueAnexo = doc.CreateElement(string.Empty, "CTmsIdBloqueAnexo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdBloqueAnexo.AppendChild(text5);
            CTMSANEXO.AppendChild(CTmsIdBloqueAnexo);
            //Agraga Nodo CTmsIdBloqueAnexo

            //Nodo CTmsIdDocumentoAnexo
            XmlElement CTmsIdDocumentoAnexo = doc.CreateElement(string.Empty, "CTmsIdDocumentoAnexo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdDocumentoAnexo.AppendChild(text5);
            CTMSANEXO.AppendChild(CTmsIdDocumentoAnexo);
            //Agraga Nodo CTmsIdDocumentoAnexo

            //Nodo CTmsUrlAnexo
            XmlElement CTmsUrlAnexo = doc.CreateElement(string.Empty, "CTmsUrlAnexo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsUrlAnexo.AppendChild(text5);
            CTMSANEXO.AppendChild(CTmsUrlAnexo);
            //Agraga Nodo CTmsUrlAnexo

            //Nodo CTmsTipoDocumentoAnexo
            XmlElement CTmsTipoDocumentoAnexo = doc.CreateElement(string.Empty, "CTmsTipoDocumentoAnexo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsTipoDocumentoAnexo.AppendChild(text5);
            CTMSANEXO.AppendChild(CTmsTipoDocumentoAnexo);
            //Agraga Nodo CTmsTipoDocumentoAnexo

            //Nodo Validar_Anexo
            XmlElement Validar_Anexo = doc.CreateElement(string.Empty, "Validar_Anexo", string.Empty);
            text5 = doc.CreateTextNode("");
            Validar_Anexo.AppendChild(text5);
            CTMSANEXO.AppendChild(Validar_Anexo);
            //Agraga Nodo Validar_Anexo

            SalidaCorrespondencia.AppendChild(CTMSANEXO);
            //Agraga Nodo CTMSANEXO

            //Nodo etiqueta3
            XmlElement etiqueta3 = doc.CreateElement(string.Empty, "etiqueta3", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            etiqueta3.Attributes.Append(atr);

            SalidaCorrespondencia.AppendChild(etiqueta3);
            //Agraga Nodo etiqueta3

            //Nodo TMS_ORIGEN
            XmlElement TMS_ORIGEN = doc.CreateElement(string.Empty, "TMS_ORIGEN", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_ORIGEN.Attributes.Append(atr);

            //Nodo DescripcionAnexos
            XmlElement DescripcionAnexos = doc.CreateElement(string.Empty, "DescripcionAnexos", string.Empty);
            text5 = doc.CreateTextNode("");
            DescripcionAnexos.AppendChild(text5);
            TMS_ORIGEN.AppendChild(DescripcionAnexos);
            //Agraga Nodo DescripcionAnexos

            //Nodo CTmsOrigenNivel
            XmlElement CTmsOrigenNivel = doc.CreateElement(string.Empty, "CTmsOrigenNivel", string.Empty);
            text5 = doc.CreateTextNode("Subdirección de Aseguramiento de la Calidad");
            CTmsOrigenNivel.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenNivel);
            //Agraga Nodo CTmsOrigenNivel

            //Nodo CTmsOrigenIdNivel
            XmlElement CTmsOrigenIdNivel = doc.CreateElement(string.Empty, "CTmsOrigenIdNivel", string.Empty);
            text5 = doc.CreateTextNode("356");
            CTmsOrigenIdNivel.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenIdNivel);
            //Agraga Nodo CTmsOrigenNivel

            //Nodo CTmsIdBloque
            XmlElement CTmsIdBloque = doc.CreateElement(string.Empty, "CTmsIdBloque", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdBloque.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsIdBloque);
            //Agraga Nodo CTmsIdBloque

            //Nodo CTmsClase
            XmlElement CTmsClase = doc.CreateElement(string.Empty, "CTmsClase", string.Empty);
            text5 = doc.CreateTextNode("O");
            CTmsClase.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsClase);
            //Agraga Nodo CTmsClase

            //Nodo CTmsIdDocumentoDestinop
            XmlElement CTmsIdDocumentoDestinop = doc.CreateElement(string.Empty, "CTmsIdDocumentoDestinop", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdDocumentoDestinop.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsIdDocumentoDestinop);
            //Agraga Nodo CTmsIdDocumentoDestinop

            //Nodo CTmsOrigenIdUsuario
            XmlElement CTmsOrigenIdUsuario = doc.CreateElement(string.Empty, "CTmsOrigenIdUsuario", string.Empty);
            text5 = doc.CreateTextNode("37888643");
            CTmsOrigenIdUsuario.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenIdUsuario);
            //Agraga Nodo CTmsOrigenIdUsuario

            //Nodo CTmsOrigenCargo
            XmlElement CTmsOrigenCargo = doc.CreateElement(string.Empty, "CTmsOrigenCargo", string.Empty);
            text5 = doc.CreateTextNode("Subdirectora de Aseguramiento de la Calidad");
            CTmsOrigenCargo.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenCargo);
            //Agraga Nodo CTmsOrigenCargo

            //Nodo CTmsOrigenTSol
            XmlElement CTmsOrigenTSol = doc.CreateElement(string.Empty, "CTmsOrigenTSol", string.Empty);
            text5 = doc.CreateTextNode("Correspondencia Externa");
            CTmsOrigenTSol.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenTSol);
            //Agraga Nodo CTmsOrigenTSol

            //Nodo CTmsOrigenUsuario
            XmlElement CTmsOrigenUsuario = doc.CreateElement(string.Empty, "CTmsOrigenUsuario", string.Empty);
            text5 = doc.CreateTextNode("RUTH TERESA BERNAL RUIZ");
            CTmsOrigenUsuario.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenUsuario);
            //Agraga Nodo CTmsOrigenUsuario

            //Nodo CTmsOrigenIdCargo
            XmlElement CTmsOrigenIdCargo = doc.CreateElement(string.Empty, "CTmsOrigenIdCargo", string.Empty);
            text5 = doc.CreateTextNode("1391");
            CTmsOrigenIdCargo.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenIdCargo);
            //Agraga Nodo CTmsOrigenIdCargo

            //Nodo IdClase
            XmlElement IdClase = doc.CreateElement(string.Empty, "IdClase", string.Empty);
            text5 = doc.CreateTextNode("");
            IdClase.AppendChild(text5);
            TMS_ORIGEN.AppendChild(IdClase);
            //Agraga Nodo IdClase

            //Nodo CTmsOrigenIdTSol
            XmlElement CTmsOrigenIdTSol = doc.CreateElement(string.Empty, "CTmsOrigenIdTSol", string.Empty);
            text5 = doc.CreateTextNode("0");
            CTmsOrigenIdTSol.AppendChild(text5);
            TMS_ORIGEN.AppendChild(CTmsOrigenIdTSol);
            //Agraga Nodo CTmsOrigenIdTSol

            //Nodo Tipo_Documental
            XmlElement Tipo_Documental = doc.CreateElement(string.Empty, "Tipo_Documental", string.Empty);
            text5 = doc.CreateTextNode("Carta");
            Tipo_Documental.AppendChild(text5);
            TMS_ORIGEN.AppendChild(Tipo_Documental);
            //Agraga Nodo Tipo_Documental

            //Nodo id_tipo_documental
            XmlElement id_tipo_documental = doc.CreateElement(string.Empty, "id_tipo_documental", string.Empty);
            text5 = doc.CreateTextNode("");
            id_tipo_documental.AppendChild(text5);
            TMS_ORIGEN.AppendChild(id_tipo_documental);
            //Agraga Nodo id_tipo_documental

            //Nodo Canal_Salida
            XmlElement Canal_Salida = doc.CreateElement(string.Empty, "Canal_Salida", string.Empty);
            text5 = doc.CreateTextNode("Masivo");
            Canal_Salida.AppendChild(text5);
            TMS_ORIGEN.AppendChild(Canal_Salida);
            //Agraga Nodo Canal_Salida

            //Nodo id_origen
            XmlElement id_origen = doc.CreateElement(string.Empty, "id_origen", string.Empty);
            text5 = doc.CreateTextNode("0");
            id_origen.AppendChild(text5);
            TMS_ORIGEN.AppendChild(id_origen);
            //Agraga Nodo id_origen

            //Nodo currier
            XmlElement currier = doc.CreateElement(string.Empty, "currier", string.Empty);
            text5 = doc.CreateTextNode("COLDELIVERY");
            currier.AppendChild(text5);
            TMS_ORIGEN.AppendChild(currier);
            //Agraga Nodo currier

            //Nodo id_currier
            XmlElement id_currier = doc.CreateElement(string.Empty, "id_currier", string.Empty);
            text5 = doc.CreateTextNode("11");
            id_currier.AppendChild(text5);
            TMS_ORIGEN.AppendChild(id_currier);
            //Agraga Nodo id_currier

            //Nodo id_documento_destino
            XmlElement id_documento_destino = doc.CreateElement(string.Empty, "id_documento_destino", string.Empty);
            text5 = doc.CreateTextNode("");
            id_documento_destino.AppendChild(text5);
            TMS_ORIGEN.AppendChild(id_documento_destino);
            //Agraga Nodo id_documento_destino

            //Nodo id_currier_destino
            XmlElement id_currier_destino = doc.CreateElement(string.Empty, "id_currier_destino", string.Empty);
            text5 = doc.CreateTextNode("11");
            id_currier_destino.AppendChild(text5);
            TMS_ORIGEN.AppendChild(id_currier_destino);
            //Agraga Nodo id_currier_destino

            SalidaCorrespondencia.AppendChild(TMS_ORIGEN);
            //Agraga Nodo TMS_ORIGEN

            //Nodo TMS_DOCEXTERNO
            XmlElement TMS_DOCEXTERNO = doc.CreateElement(string.Empty, "TMS_DOCEXTERNO", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_DOCEXTERNO.Attributes.Append(atr);

            SalidaCorrespondencia.AppendChild(TMS_DOCEXTERNO);
            //Agraga Nodo TMS_DOCEXTERNO

            //Nodo TMS_COPIA
            XmlElement TMS_COPIA = doc.CreateElement(string.Empty, "TMS_COPIA", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_COPIA.Attributes.Append(atr);

            //Nodo CTmsCopiaUsuario
            XmlElement CTmsCopiaUsuario = doc.CreateElement(string.Empty, "CTmsCopiaUsuario", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaUsuario.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaUsuario);
            //Agraga Nodo CTmsCopiaUsuario

            //Nodo CTmsCopiacargo
            XmlElement CTmsCopiacargo = doc.CreateElement(string.Empty, "CTmsCopiacargo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiacargo.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiacargo);
            //Agraga Nodo CTmsCopiacargo

            //Nodo CTmsIdDocumentoDestinoCopia
            XmlElement CTmsIdDocumentoDestinoCopia = doc.CreateElement(string.Empty, "CTmsIdDocumentoDestinoCopia", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdDocumentoDestinoCopia.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsIdDocumentoDestinoCopia);
            //Agraga Nodo CTmsIdDocumentoDestinoCopia

            //Nodo CTmsCopiaTSol
            XmlElement CTmsCopiaTSol = doc.CreateElement(string.Empty, "CTmsCopiaTSol", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaTSol.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaTSol);
            //Agraga Nodo CTmsCopiaTSol

            //Nodo CTmsCopiaClase
            XmlElement CTmsCopiaClase = doc.CreateElement(string.Empty, "CTmsCopiaClase", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaClase.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaClase);
            //Agraga Nodo CTmsCopiaClase

            //Nodo CTmsCopiaIdNivel
            XmlElement CTmsCopiaIdNivel = doc.CreateElement(string.Empty, "CTmsCopiaIdNivel", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaIdNivel.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaIdNivel);
            //Agraga Nodo CTmsCopiaIdNivel

            //Nodo CTmsCopiaIdCargo
            XmlElement CTmsCopiaIdCargo = doc.CreateElement(string.Empty, "CTmsCopiaIdCargo", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaIdCargo.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaIdCargo);
            //Agraga Nodo CTmsCopiaIdCargo

            //Nodo CTmsIdBloqueCopia
            XmlElement CTmsIdBloqueCopia = doc.CreateElement(string.Empty, "CTmsIdBloqueCopia", string.Empty);
            text5 = doc.CreateTextNode("0");
            CTmsIdBloqueCopia.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsIdBloqueCopia);
            //Agraga Nodo CTmsIdBloqueCopia

            //Nodo CTmsCopiaNivel
            XmlElement CTmsCopiaNivel = doc.CreateElement(string.Empty, "CTmsCopiaNivel", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaNivel.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaNivel);
            //Agraga Nodo CTmsCopiaNivel

            //Nodo CTmsCopiaIdUsuario
            XmlElement CTmsCopiaIdUsuario = doc.CreateElement(string.Empty, "CTmsCopiaIdUsuario", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaIdUsuario.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaIdUsuario);
            //Agraga Nodo CTmsCopiaIdUsuario

            //Nodo CTmsCopiaIdTSol
            XmlElement CTmsCopiaIdTSol = doc.CreateElement(string.Empty, "CTmsCopiaIdTSol", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsCopiaIdTSol.AppendChild(text5);
            TMS_COPIA.AppendChild(CTmsCopiaIdTSol);
            //Agraga Nodo CTmsCopiaIdTSol

            //Nodo DescripcionCopia
            XmlElement DescripcionCopia = doc.CreateElement(string.Empty, "DescripcionCopia", string.Empty);
            text5 = doc.CreateTextNode("");
            DescripcionCopia.AppendChild(text5);
            TMS_COPIA.AppendChild(DescripcionCopia);
            //Agraga Nodo DescripcionCopia

            SalidaCorrespondencia.AppendChild(TMS_COPIA);
            //Agraga Nodo TMS_DOCEXTERNO

            //Nodo INFO_PLANILLAS
            XmlElement INFO_PLANILLAS = doc.CreateElement(string.Empty, "INFO_PLANILLAS", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            INFO_PLANILLAS.Attributes.Append(atr);

            //Nodo id_consecutivo
            XmlElement id_consecutivo = doc.CreateElement(string.Empty, "id_consecutivo", string.Empty);
            text5 = doc.CreateTextNode("");
            id_consecutivo.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(id_consecutivo);
            //Agraga Nodo id_consecutivo

            //Nodo id_documento
            XmlElement id_documento = doc.CreateElement(string.Empty, "id_documento", string.Empty);
            text5 = doc.CreateTextNode("");
            id_documento.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(id_documento);
            //Agraga Nodo id_documento

            //Nodo fecha_documento
            XmlElement fecha_documento = doc.CreateElement(string.Empty, "fecha_documento", string.Empty);
            text5 = doc.CreateTextNode("");
            fecha_documento.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(fecha_documento);
            //Agraga Nodo fecha_documento

            //Nodo radicacion_origen
            XmlElement radicacion_origen = doc.CreateElement(string.Empty, "radicacion_origen", string.Empty);
            text5 = doc.CreateTextNode("");
            radicacion_origen.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(radicacion_origen);
            //Agraga Nodo radicacion_origen

            //Nodo Entidad
            XmlElement Entidad = doc.CreateElement(string.Empty, "Entidad", string.Empty);
            text5 = doc.CreateTextNode("");
            Entidad.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(Entidad);
            //Agraga Nodo Entidad

            //Nodo nombre_firmante
            XmlElement nombre_firmante = doc.CreateElement(string.Empty, "nombre_firmante", string.Empty);
            text5 = doc.CreateTextNode("Juan Jose Francisco Jiminez Lopez");
            nombre_firmante.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(nombre_firmante);
            //Agraga Nodo nombre_firmante

            //Nodo Numero_Folios
            XmlElement Numero_Folios = doc.CreateElement(string.Empty, "Numero_Folios", string.Empty);
            text5 = doc.CreateTextNode("1");
            Numero_Folios.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(Numero_Folios);
            //Agraga Nodo Numero_Folios

            //Nodo Tramite
            XmlElement Tramite = doc.CreateElement(string.Empty, "Tramite", string.Empty);
            text5 = doc.CreateTextNode("Correspondencia Enviada");
            Tramite.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(Tramite);
            //Agraga Nodo Tramite

            //Nodo Indireccion
            XmlElement Indireccion = doc.CreateElement(string.Empty, "Indireccion", string.Empty);
            text5 = doc.CreateTextNode("carrera 1 No. 00-01");
            Indireccion.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(Indireccion);
            //Agraga Nodo Indireccion

            //Nodo id_municipio
            XmlElement id_municipio = doc.CreateElement(string.Empty, "id_municipio", string.Empty);
            text5 = doc.CreateTextNode("Soacha");
            id_municipio.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(id_municipio);
            //Agraga Nodo id_municipio

            //Nodo In_NoAnexos
            XmlElement In_NoAnexos = doc.CreateElement(string.Empty, "In_NoAnexos", string.Empty);
            text5 = doc.CreateTextNode("0");
            In_NoAnexos.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(In_NoAnexos);
            //Agraga Nodo In_NoAnexos

            //Nodo in_desc_Anexos
            XmlElement in_desc_Anexos = doc.CreateElement(string.Empty, "in_desc_Anexos", string.Empty);
            text5 = doc.CreateTextNode("");
            in_desc_Anexos.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(in_desc_Anexos);
            //Agraga Nodo in_desc_Anexos

            //Nodo cc_id_currier
            XmlElement cc_id_currier = doc.CreateElement(string.Empty, "cc_id_currier", string.Empty);
            text5 = doc.CreateTextNode("11");
            cc_id_currier.AppendChild(text5);
            INFO_PLANILLAS.AppendChild(cc_id_currier);
            //Agraga Nodo cc_id_currier

            SalidaCorrespondencia.AppendChild(INFO_PLANILLAS);
            //Agraga Nodo INFO_PLANILLAS

            //Nodo TMS_PARANIVEL
            XmlElement TMS_PARANIVEL = doc.CreateElement(string.Empty, "TMS_PARANIVEL", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_PARANIVEL.Attributes.Append(atr);

            //Nodo CTmsDestinoParaNivel1
            XmlElement CTmsDestinoParaNivel1 = doc.CreateElement(string.Empty, "CTmsDestinoParaNivel1", string.Empty);
            text5 = doc.CreateTextNode("DESTINO EXTERNO");
            CTmsDestinoParaNivel1.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsDestinoParaNivel1);
            //Agraga Nodo CTmsDestinoParaNivel1

            //Nodo IdPais
            XmlElement IdPais = doc.CreateElement(string.Empty, "IdPais", string.Empty);
            text5 = doc.CreateTextNode("");
            IdPais.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(IdPais);
            //Agraga Nodo IdPais

            //Nodo CTmsNombreOrganizacion
            XmlElement CTmsNombreOrganizacion = doc.CreateElement(string.Empty, "CTmsNombreOrganizacion", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsNombreOrganizacion.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsNombreOrganizacion);
            //Agraga Nodo CTmsNombreOrganizacion

            //Nodo CTmsIdDocumentoDestinoParaNivel
            XmlElement CTmsIdDocumentoDestinoParaNivel = doc.CreateElement(string.Empty, "CTmsIdDocumentoDestinoParaNivel", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdDocumentoDestinoParaNivel.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsIdDocumentoDestinoParaNivel);
            //Agraga Nodo CTmsIdDocumentoDestinoParaNivel

            //Nodo CTmsDestinoParaNivelDepartamento
            XmlElement CTmsDestinoParaNivelDepartamento = doc.CreateElement(string.Empty, "CTmsDestinoParaNivelDepartamento", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsDestinoParaNivelDepartamento.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsDestinoParaNivelDepartamento);
            //Agraga Nodo CTmsDestinoParaNivelDepartamento

            //Nodo CTmsIdBloqueParaNivel
            XmlElement CTmsIdBloqueParaNivel = doc.CreateElement(string.Empty, "CTmsIdBloqueParaNivel", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsIdBloqueParaNivel.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsIdBloqueParaNivel);
            //Agraga Nodo CTmsIdBloqueParaNivel

            //Nodo CTmsClaseParaNivel
            XmlElement CTmsClaseParaNivel = doc.CreateElement(string.Empty, "CTmsClaseParaNivel", string.Empty);
            text5 = doc.CreateTextNode("O");
            CTmsClaseParaNivel.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsClaseParaNivel);
            //Agraga Nodo CTmsClaseParaNivel

            //Nodo PaisPara
            XmlElement PaisPara = doc.CreateElement(string.Empty, "PaisPara", string.Empty);
            text5 = doc.CreateTextNode("Colombia");
            PaisPara.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(PaisPara);
            //Agraga Nodo PaisPara

            //Nodo CargoPara
            XmlElement CargoPara = doc.CreateElement(string.Empty, "CargoPara", string.Empty);
            text5 = doc.CreateTextNode("");
            CargoPara.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CargoPara);
            //Agraga Nodo CargoPara

            //Nodo CTmsDestinoParaNivel
            XmlElement CTmsDestinoParaNivel = doc.CreateElement(string.Empty, "CTmsDestinoParaNivel", string.Empty);
            text5 = doc.CreateTextNode("DESTINO EXTERNO");
            CTmsDestinoParaNivel.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsDestinoParaNivel);
            //Agraga Nodo CTmsDestinoParaNivel

            //Nodo CTmsTelefono
            XmlElement CTmsTelefono = doc.CreateElement(string.Empty, "CTmsTelefono", string.Empty);
            text5 = doc.CreateTextNode("");
            CTmsTelefono.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsTelefono);
            //Agraga Nodo CTmsTelefono

            //Nodo CTmsDireccion
            XmlElement CTmsDireccion = doc.CreateElement(string.Empty, "CTmsDireccion", string.Empty);
            text5 = doc.CreateTextNode("carrera 1 No. 00-01");
            CTmsDireccion.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsDireccion);
            //Agraga Nodo CTmsDireccion

            //Nodo NIT
            XmlElement NIT = doc.CreateElement(string.Empty, "NIT", string.Empty);
            text5 = doc.CreateTextNode("");
            NIT.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(NIT);
            //Agraga Nodo NIT

            //Nodo CTmsParaIdNivel
            XmlElement CTmsParaIdNivel = doc.CreateElement(string.Empty, "CTmsParaIdNivel", string.Empty);
            text5 = doc.CreateTextNode("0");
            CTmsParaIdNivel.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CTmsParaIdNivel);
            //Agraga Nodo CTmsParaIdNivel

            //Nodo IdEntidad
            XmlElement IdEntidad = doc.CreateElement(string.Empty, "IdEntidad", string.Empty);
            text5 = doc.CreateTextNode("");
            IdEntidad.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(IdEntidad);
            //Agraga Nodo IdEntidad

            //Nodo IdTituloPara
            XmlElement IdTituloPara = doc.CreateElement(string.Empty, "IdTituloPara", string.Empty);
            text5 = doc.CreateTextNode("");
            IdTituloPara.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(IdTituloPara);
            //Agraga Nodo IdTituloPara

            //Nodo CiudadPara
            XmlElement CiudadPara = doc.CreateElement(string.Empty, "CiudadPara", string.Empty);
            text5 = doc.CreateTextNode("0");
            CiudadPara.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(CiudadPara);
            //Agraga Nodo CiudadPara

            //Nodo CTmsDestinoParaNivel1
            XmlElement DepartamentoPara = doc.CreateElement(string.Empty, "DepartamentoPara", string.Empty);
            text5 = doc.CreateTextNode("");
            DepartamentoPara.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(DepartamentoPara);
            //Agraga Nodo DepartamentoPara

            //Nodo id_ciudad
            XmlElement id_ciudad = doc.CreateElement(string.Empty, "id_ciudad", string.Empty);
            text5 = doc.CreateTextNode("");
            id_ciudad.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(id_ciudad);
            //Agraga Nodo CTmsDestinoParaNivel1

            //Nodo id_departamento
            XmlElement id_departamento = doc.CreateElement(string.Empty, "id_departamento", string.Empty);
            text5 = doc.CreateTextNode("");
            id_departamento.AppendChild(text5);
            TMS_PARANIVEL.AppendChild(id_departamento);
            //Agraga Nodo id_departamento

            SalidaCorrespondencia.AppendChild(TMS_PARANIVEL);
            //Agraga Nodo TMS_PARANIVEL

            //Nodo TMS_TIPOSERVICIOCOBERTURA
            XmlElement TMS_TIPOSERVICIOCOBERTURA = doc.CreateElement(string.Empty, "TMS_TIPOSERVICIOCOBERTURA", string.Empty);
            atr = doc.CreateAttribute("Bloque");
            atr.Value = "true";
            TMS_TIPOSERVICIOCOBERTURA.Attributes.Append(atr);

            //Nodo CTmsIdCobertura
            XmlElement CTmsIdCobertura = doc.CreateElement(string.Empty, "CTmsIdCobertura", string.Empty);
            text5 = doc.CreateTextNode("8");
            CTmsIdCobertura.AppendChild(text5);
            TMS_TIPOSERVICIOCOBERTURA.AppendChild(CTmsIdCobertura);
            //Agraga Nodo CTmsIdCobertura

            //Nodo CTmsNomCobertura
            XmlElement CTmsNomCobertura = doc.CreateElement(string.Empty, "CTmsNomCobertura", string.Empty);
            text5 = doc.CreateTextNode("Urbano");
            CTmsNomCobertura.AppendChild(text5);
            TMS_TIPOSERVICIOCOBERTURA.AppendChild(CTmsNomCobertura);
            //Agraga Nodo CTmsIdCobertura

            //Nodo CTmsIdTipoServicio
            XmlElement CTmsIdTipoServicio = doc.CreateElement(string.Empty, "CTmsIdTipoServicio", string.Empty);
            text5 = doc.CreateTextNode("2");
            CTmsIdTipoServicio.AppendChild(text5);
            TMS_TIPOSERVICIOCOBERTURA.AppendChild(CTmsIdTipoServicio);
            //Agraga Nodo CTmsIdCobertura

            //Nodo CTmsNomTipoServicio
            XmlElement CTmsNomTipoServicio = doc.CreateElement(string.Empty, "CTmsNomTipoServicio", string.Empty);
            text5 = doc.CreateTextNode("Normal");
            CTmsNomTipoServicio.AppendChild(text5);
            TMS_TIPOSERVICIOCOBERTURA.AppendChild(CTmsNomTipoServicio);
            //Agraga Nodo CTmsIdCobertura

            SalidaCorrespondencia.AppendChild(TMS_TIPOSERVICIOCOBERTURA);
            //Agraga Nodo TMS_TIPOSERVICIOCOBERTURA

            TMS.AppendChild(SalidaCorrespondencia);
            //Agraga Nodo Secundario 1 - SalidaCorrespondencia

            //Nodo Secundario 2 - registro_control
            XmlElement registro_control = doc.CreateElement(string.Empty, "registro_control", string.Empty);

            //Nodo tipo_llamado
            XmlElement tipo_llamado = doc.CreateElement(string.Empty, "tipo_llamado", string.Empty);
            atr = doc.CreateAttribute("asin");
            atr.Value = "false";
            tipo_llamado.Attributes.Append(atr);

            registro_control.AppendChild(tipo_llamado);
            //Agraga Nodo tipo_llamado

            //Nodo Usuario
            XmlElement Usuario = doc.CreateElement(string.Empty, "Usuario", string.Empty);

            //Nodo id_usuario
            XmlElement id_usuario = doc.CreateElement(string.Empty, "id_usuario", string.Empty);
            text5 = doc.CreateTextNode("79804555");
            id_usuario.AppendChild(text5);
            Usuario.AppendChild(id_usuario);
            //Agraga Nodo id_usuario

            //Nodo clave
            XmlElement clave = doc.CreateElement(string.Empty, "clave", string.Empty);
            text5 = doc.CreateTextNode("Convalidaciones2019*");
            clave.AppendChild(text5);
            Usuario.AppendChild(clave);
            //Agraga Nodo clave

            registro_control.AppendChild(Usuario);
            //Agraga Nodo Usuario

            //Nodo documento
            XmlElement documento = doc.CreateElement(string.Empty, "documento", string.Empty);
            atr = doc.CreateAttribute("tipo_documento");
            atr.Value = "165";
            documento.Attributes.Append(atr);

            atr = doc.CreateAttribute("num_caracteres");
            atr.Value = "0";
            documento.Attributes.Append(atr);

            //Nodo id_documento
            XmlElement id_documento1 = doc.CreateElement(string.Empty, "id_documento", string.Empty);
            text5 = doc.CreateTextNode("");
            id_documento1.AppendChild(text5);
            documento.AppendChild(id_documento1);
            //Agraga Nodo id_documento

            //Nodo NombreRegistro
            XmlElement NombreRegistro = doc.CreateElement(string.Empty, "NombreRegistro", string.Empty);
            text5 = doc.CreateTextNode("SalidaCorrespondencia");
            NombreRegistro.AppendChild(text5);
            documento.AppendChild(NombreRegistro);
            //Agraga Nodo NombreRegistro

            //Nodo id_xslGC
            XmlElement id_xslGC = doc.CreateElement(string.Empty, "id_xslGC", string.Empty);
            text5 = doc.CreateTextNode("179");
            id_xslGC.AppendChild(text5);
            documento.AppendChild(id_xslGC);
            //Agraga Nodo id_xslGC

            registro_control.AppendChild(documento);
            //Agraga Nodo documento

            //Nodo cliente
            XmlElement cliente = doc.CreateElement(string.Empty, "cliente", string.Empty);

            //Nodo nit
            XmlElement nit = doc.CreateElement(string.Empty, "nit", string.Empty);
            text5 = doc.CreateTextNode("876543");
            nit.AppendChild(text5);
            cliente.AppendChild(nit);
            //Agraga Nodo nit

            registro_control.AppendChild(cliente);
            //Agraga Nodo cliente

            //Nodo solicitud
            XmlElement solicitud = doc.CreateElement(string.Empty, "solicitud", string.Empty);

            //Nodo aplicativo
            XmlElement aplicativo = doc.CreateElement(string.Empty, "aplicativo", string.Empty);
            text5 = doc.CreateTextNode("");
            aplicativo.AppendChild(text5);
            solicitud.AppendChild(aplicativo);
            //Agraga Nodo aplicativo

            //Nodo id_solicitud
            XmlElement id_solicitud = doc.CreateElement(string.Empty, "id_solicitud", string.Empty);
            text5 = doc.CreateTextNode("");
            id_solicitud.AppendChild(text5);
            solicitud.AppendChild(id_solicitud);
            //Agraga Nodo id_solicitud

            //Nodo url_respuesta
            XmlElement url_respuesta = doc.CreateElement(string.Empty, "url_respuesta", string.Empty);
            text5 = doc.CreateTextNode("");
            url_respuesta.AppendChild(text5);
            solicitud.AppendChild(url_respuesta);
            //Agraga Nodo url_respuesta

            //Nodo id_usuario_remoto
            XmlElement id_usuario_remoto = doc.CreateElement(string.Empty, "id_usuario_remoto", string.Empty);
            text5 = doc.CreateTextNode("");
            id_usuario_remoto.AppendChild(text5);
            solicitud.AppendChild(id_usuario_remoto);
            //Agraga Nodo id_usuario_remoto

            //Nodo clave_usuario_remoto
            XmlElement clave_usuario_remoto = doc.CreateElement(string.Empty, "clave_usuario_remoto", string.Empty);
            text5 = doc.CreateTextNode("");
            clave_usuario_remoto.AppendChild(text5);
            solicitud.AppendChild(clave_usuario_remoto);
            //Agraga Nodo clave_usuario_remoto

            registro_control.AppendChild(solicitud);
            //Agraga Nodo solicitud

            //Nodo DocumentosAnexos
            XmlElement DocumentosAnexos = doc.CreateElement(string.Empty, "DocumentosAnexos", string.Empty);

            //Nodo Anexos
            XmlElement Anexos = doc.CreateElement(string.Empty, "Anexos", string.Empty);
            atr = doc.CreateAttribute("Tipo");
            atr.Value = "FTP";
            Anexos.Attributes.Append(atr);
            atr = doc.CreateAttribute("IdConfiguracion");
            atr.Value = "0";
            Anexos.Attributes.Append(atr);
            atr = doc.CreateAttribute("Ruta");
            atr.Value = "";
            Anexos.Attributes.Append(atr);

            //Nodo File
            XmlElement File = doc.CreateElement(string.Empty, "File", string.Empty);
            text5 = doc.CreateTextNode("");
            File.AppendChild(text5);
            Anexos.AppendChild(File);
            //Agraga Nodo File

            DocumentosAnexos.AppendChild(Anexos);
            //Agraga Nodo Anexos

            registro_control.AppendChild(DocumentosAnexos);
            //Agraga Nodo DocumentosAnexos

            //Nodo Radicador
            XmlElement Radicador = doc.CreateElement(string.Empty, "Radicador", string.Empty);

            //Nodo IdRadicador
            XmlElement IdRadicador = doc.CreateElement(string.Empty, "IdRadicador", string.Empty);
            text5 = doc.CreateTextNode("79804555");
            IdRadicador.AppendChild(text5);
            Radicador.AppendChild(IdRadicador);
            //Agraga Nodo clave_usuario_remoto

            registro_control.AppendChild(Radicador);
            //Agraga Nodo Radicador

            TMS.AppendChild(registro_control);
            //Agraga Nodo Secundario 2 - registro_control

            documentoXMLTMS.AppendChild(TMS);
            //Agraga Nodo Principal_O

            doc.AppendChild(documentoXMLTMS);
            //Agraga Nodo Principal

            return doc.InnerXml;
        }
    }
}

/*

<tms:documentoXMLTMS>
    <![CDATA[
        <TMS>
	        <SalidaCorrespondencia>
		        <ENCABEZADO Bloque="true">
			        <ID_DOC_PPAL/>
			        <Rotulo_Ciudad/>
			        <Rotulo_Ent_Destino/>
			        <Rotulo_Asunto1pte>Comunicación Registro Convalidaciones IES 1</Rotulo_Asunto1pte>
			        <Rotulo_Asunto2pte>16 de julio de 2019</Rotulo_Asunto2pte>
			        <Rotulo2_cliente/>	
			        <Ciudad_Radicacion>Bogotá D.C</Ciudad_Radicacion>
			        <Rotulo2_Direccion>carrera 1 No. 00-01</Rotulo2_Direccion>
		        </ENCABEZADO>
		        <TMS_RADICACION Bloque="true">
			        <CTmsTipoDocRadicacion/>
			        <CTmsRadicacion/>
			        <FechaRadicacion/>
		        </TMS_RADICACION>
		        <etiqueta_1 Bloque="true"/>
		        <CLIENTE Bloque="true">
			        <CTMS_nom_cliente>Juan Jose Francisco Jiminez Lopez</CTMS_nom_cliente>
			        <CTMS_direccion>carrera 1 No. 00-01</CTMS_direccion>
			        <CTMS_nit>876543</CTMS_nit>
			        <V_Ciudad>Bogota</V_Ciudad>
			        <Departamento>Cundinamarca</Departamento>
			        <Firmante>Juan Jose Francisco Jiminez Lopez</Firmante>
			        <CTMS_id_cliente/>
			        <CTMS_id_localizacion/>
			        <CTMS_tipo_cliente/>
			        <CTMS_clase_persona/>
			        <CTMS_actualizacion/>
			        <CTMS_admonTablabd/>
			        <CTMS_nombreTabla/>
			        <CTMS_pkTabla/>
			        <CTMSIdBloque/>
		        </CLIENTE>
		        <ASUNTO Bloque="true">
			        <AsuntoSolicitud>Comunicación Registro Convalidaciones IES</AsuntoSolicitud>
			        <BDIDPlataforma/>
			        <BDIDTipo/>
			        <Tipo_requerimiento/>
			        <Tradicion>0</Tradicion>
			        <Nombre_Plataforma/>
			        <s_id_cliente/>
			        <Asunto>Comunicación Registro Convalidaciones IES</Asunto>
		        </ASUNTO>
		        <Blq_resumen Bloque="true">
			        <Resumen/>
		        </Blq_resumen>
		        <etiqueta2 Bloque="true"/>
		        <TMS_ASUNTO Bloque="true">			
			        <CTMSNumsRadicacion/>
			        <NumeroRadicadicacionOrigen/>
			        <NodeFolios>1</NodeFolios>
			        <NodeAnexos>0</NodeAnexos>
		        </TMS_ASUNTO>
		        <CTMSANEXO Bloque="true">
			        <CTmsIdBloqueAnexo/>
			        <CTmsIdDocumentoAnexo/>
			        <CTmsUrlAnexo/>
			        <CTmsTipoDocumentoAnexo/>
			        <Validar_Anexo/>
		        </CTMSANEXO>
		        <etiqueta3 Bloque="true"/>
		        <TMS_ORIGEN Bloque="true">
			        <DescripcionAnexos/>
			        <CTmsOrigenNivel>Subdirección de Aseguramiento de la Calidad</CTmsOrigenNivel>
			        <CTmsOrigenIdNivel>356</CTmsOrigenIdNivel>			
			        <CTmsIdBloque/>
			        <CTmsClase>O</CTmsClase>
			        <CTmsIdDocumentoDestinop/>
			        <CTmsOrigenIdUsuario>37888643</CTmsOrigenIdUsuario>
			        <CTmsOrigenCargo>Subdirectora de Aseguramiento de la Calidad</CTmsOrigenCargo>
			        <CTmsOrigenTSol>Correspondencia Externa</CTmsOrigenTSol>
			        <CTmsOrigenUsuario>RUTH TERESA BERNAL RUIZ</CTmsOrigenUsuario>
			        <CTmsOrigenIdCargo>1391</CTmsOrigenIdCargo>
			        <IdClase/>
			        <CTmsOrigenIdTSol>0</CTmsOrigenIdTSol>
			        <Tipo_Documental>Carta</Tipo_Documental>
			        <id_tipo_documental/>
			        <Canal_Salida>Masivo</Canal_Salida>
			        <id_origen>0</id_origen>
			        <currier>COLDELIVERY</currier>
			        <id_currier>11</id_currier>
			        <id_documento_destino/>
			        <id_currier_destino>11</id_currier_destino>
		        </TMS_ORIGEN>
		        <TMS_DOCEXTERNO Bloque="true"/>
		        <TMS_COPIA Bloque="true">
			        <CTmsCopiaUsuario/>
			        <CTmsCopiacargo/>
			        <CTmsIdDocumentoDestinoCopia/>
			        <CTmsCopiaTSol/>
			        <CTmsCopiaClase/>
			        <CTmsCopiaIdNivel/>
			        <CTmsCopiaIdCargo/>
			        <CTmsIdBloqueCopia>0</CTmsIdBloqueCopia>
			        <CTmsCopiaNivel/>
			        <CTmsCopiaIdUsuario/>
			        <CTmsCopiaIdTSol/>
			        <DescripcionCopia/>
		        </TMS_COPIA>
		        <INFO_PLANILLAS Bloque="true">
			        <id_consecutivo/>
			        <id_documento/>
			        <fecha_documento/>
			        <radicacion_origen/>
			        <Entidad/>
			        <nombre_firmante>Juan Jose Francisco Jiminez Lopez</nombre_firmante>
			        <Numero_Folios>1</Numero_Folios>
			        <Tramite>Correspondencia Enviada</Tramite>
			        <Indireccion>carrera 1 No. 00-01</Indireccion>
			        <id_municipio>Soacha</id_municipio>
			        <In_NoAnexos>0</In_NoAnexos>
			        <in_desc_Anexos/>
			        <cc_id_currier>11</cc_id_currier>
		        </INFO_PLANILLAS>
		        <TMS_PARANIVEL Bloque="true">
			        <CTmsDestinoParaNivel1>DESTINO EXTERNO</CTmsDestinoParaNivel1>
			        <IdPais/>
			        <CTmsNombreOrganizacion/>
			        <CTmsIdDocumentoDestinoParaNivel/>
			        <CTmsDestinoParaNivelDepartamento/>
			        <CTmsIdBloqueParaNivel/>
			        <CTmsClaseParaNivel>O</CTmsClaseParaNivel>
			        <PaisPara>Colombia</PaisPara>
			        <CargoPara/>
			        <CTmsDestinoParaNivel>DESTINO EXTERNO</CTmsDestinoParaNivel>
			        <CTmsTelefono/>
			        <CTmsDireccion>carrera 1 No. 00-01</CTmsDireccion>
			        <NIT/>
			        <CTmsParaIdNivel>0</CTmsParaIdNivel>
			        <IdEntidad/>
			        <IdTituloPara/>
			        <CiudadPara>0</CiudadPara>
			        <DepartamentoPara/>
			        <id_ciudad/>
			        <id_departamento/>
		        </TMS_PARANIVEL>
		        <TMS_TIPOSERVICIOCOBERTURA Bloque="true">
			        <CTmsIdCobertura>8</CTmsIdCobertura>
			        <CTmsNomCobertura>Urbano</CTmsNomCobertura>
			        <CTmsIdTipoServicio>2</CTmsIdTipoServicio>
			        <CTmsNomTipoServicio>Normal</CTmsNomTipoServicio>
		        </TMS_TIPOSERVICIOCOBERTURA>
	        </SalidaCorrespondencia>
	        <registro_control>
		        <tipo_llamado asin="false"/>
		        <Usuario>
			        <id_usuario>79804555</id_usuario>
			        <clave>Convalidaciones2019*</clave>
		        </Usuario>
		        <documento tipo_documento="165" num_caracteres="0">
			        <id_documento/>
			        <NombreRegistro>SalidaCorrespondencia</NombreRegistro>
			        <id_xslGC>179</id_xslGC>
		        </documento>
		        <cliente>
			        <nit>876543</nit>
		        </cliente>
		        <solicitud>
			        <aplicativo/>
			        <id_solicitud/>
			        <url_respuesta/>
			        <id_usuario_remoto/>
			        <clave_usuario_remoto/>
		        </solicitud>
		        <DocumentosAnexos>
			        <Anexos Tipo="FTP" IdConfiguracion="0" Ruta="">
				        <File/>
			        </Anexos>
		        </DocumentosAnexos>
		        <Radicador>
			        <IdRadicador>79804555</IdRadicador>
		        </Radicador>
	        </registro_control>
        </TMS>
    ]]>
</tms:documentoXMLTMS>
 */

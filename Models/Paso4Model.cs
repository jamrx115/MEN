using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConvalidacionEducacionSuperior.Models
{
    public class Paso4Model
    {
        bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        public List<tbDocumentos> documentos { get; set; }
        public tbSolicitud solicitudActual { get; set; }

        //Visualizaciones
        public bool displayProgramaAcademico
        {
            get
            {
                int[] filtros = { 11, 42, 91 };
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (filtros.Contains((int)program.CampoAmplioConocimiento))
                {
                    return true;
                }
                else
                {
                    if((int)program.CampoAmplioConocimiento == 41)
                    {
                        //preguntar si es contaduria
                        return true;
                    }
                    else
                    {
                        if ((bool)solicitudActual.acreditacionCalidad)
                            return false;
                        else
                            return true;
                    }                    
                }
            }
        }

        public bool displayProgramaAcademicoPosgrado
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    return true;
                }
                else
                {
                    if ((bool)solicitudActual.acreditacionCalidad)
                        return false;
                    else
                        return true; 
                }
            }
        }

        public bool displayProductosInvestigacion
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    if (program.TipoEducacionSuperior == 2 && program.TipoMaestria.Equals("2"))
                        return true;
                    else
                        return false;                    
                }
                else
                {
                    if ((bool)solicitudActual.acreditacionCalidad)
                    {
                        return false;
                    }
                    else
                    {
                        if (program.TipoEducacionSuperior == 2)
                            return true;
                        else
                            return false;
                    }                    
                }
            }
        }

        public bool displayTesis
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    if (program.TipoEducacionSuperior == 3)
                        return true;
                    else if (program.TipoEducacionSuperior == 2 && program.TipoMaestria.Equals("1"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayEspecialidadBaseNal
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    if ((bool)program.SegundaEspecialidad && (bool)program.SegundaEspecialidadNal)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayRecorProcedimientos
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    if (program.TipoEducacionSuperior == 1)
                        return true;
                    else if (program.TipoEducacionSuperior == 2 && program.TipoMaestria.Equals("1"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayTrabajoInvestigacion
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    if (program.TipoEducacionSuperior == 3)
                        return true;
                    else if (program.TipoEducacionSuperior == 2 && program.TipoMaestria.Equals("2"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (program.TipoEducacionSuperior == 3)
                        if ((bool)solicitudActual.acreditacionCalidad)
                            return false;
                        else
                            return true;
                    else if (program.TipoEducacionSuperior == 2 && program.TipoMaestria.Equals("2"))
                        if ((bool)solicitudActual.acreditacionCalidad)
                            return false;
                        else
                            return true;
                    else
                        return false;
                    
                }
            }
        }
        public bool displaySaludSegundaEsp
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91 && (bool)program.SegundaEspecialidad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displaySalud
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 91)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayDerecho
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 42)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayContaduria
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 41)
                {
                    //VALIDAR SI ES CONTAQDURIA
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool displayEducacion
        {
            get
            {
                tbPrograma program = db.tbPrograma.Where(p => p.Solicitud == solicitudActual.SolicitudId).FirstOrDefault();
                if (program.CampoAmplioConocimiento == 11)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Archivos Originales       
        // ---- Posgrado
        [Required(ErrorMessage = "Seleccione copia documento Identidad")]
        public string documentoIdentidad { get; set; }
        public string rutaDocumentoIdentidad
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoIdentidad;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del diploma de posgrado")]
        public string diplomaPosgrado { get; set; }
        public string rutaDiplomaPosgrado
        {
            get{
                return "../Archivos/Documentos/Originales/" + diplomaPosgrado;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del titulo de pregrado")]
        public string tituloPregrado { get; set; }
        public string rutaTituloPregrado
        {
            get{
                return "../Archivos/Documentos/Originales/" + tituloPregrado;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del documento de convalidación del pregrado")]
        public string convalidacionPregrado { get; set; }
        public string rutaconvalidacionPregrado
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + convalidacionPregrado;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del documento de convalidación de la segunda especialidad base")]
        public string convalidacionEspecialidadBase { get; set; }
        public string rutaconvalidacionEspecialidadBase
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + convalidacionEspecialidadBase;
            }
        }


        [Required(ErrorMessage = "Seleccione copia del certificado de asignaturas")]
        public string certificadoAsignaturas { get; set; }
        public string rutacertificadoAsignaturas
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + certificadoAsignaturas;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del certificado del programa de pregrado")]
        public string certificadoProgramapre { get; set; }
        public string rutacertificadoProgramapre
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + certificadoProgramapre;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del formato de investigación")]
        public string formatoInvestigacion { get; set; }
        public string rutaformatoInvestigacion
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + formatoInvestigacion;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del acta de sustentación")]
        public string actaSustentacion { get; set; }
        public string rutaactaSustentacion
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + actaSustentacion;
            }
        }

        [Required(ErrorMessage = "Seleccione copia de la tesis de la maestría")]
        public string tesisMaestrias { get; set; }
        public string rutatesisMaestrias
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + tesisMaestrias;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del título de la especialidad")]
        public string tituloEspecialidad { get; set; }
        public string rutatituloEspecialidad
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + tituloEspecialidad;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del record de procedimientos")]
        public string recordProcedimientos { get; set; }
        public string rutarecordProcedimientos
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + recordProcedimientos;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del certificado de actividades académicas")]
        public string certificadoActividadesAcademicas { get; set; }
        public string rutacertificadoActividadesAcademicas
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + certificadoActividadesAcademicas;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del certificado de calificaciones del Posgrado")]
        public string certificadoCalificacionesPos { get; set; }
        public string rutacertificadoCalificacionesPos
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + certificadoCalificacionesPos;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del certificado del programa")]
        public string certificadoPrograma { get; set; }
        public string rutacertificadoPrograma
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + certificadoPrograma;
            }
        }

        [Required(ErrorMessage = "Seleccione copia documento de requisito especial de salud")]
        public string requisitoEspecialSalud { get; set; }
        public string rutarequisitoEspecialSalud
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + requisitoEspecialSalud;
            }
        }

        [Required(ErrorMessage = "Seleccione copia documento requisisto especial de contaduría")]
        public string requisitoEspecialContaduria { get; set; }
        public string rutarequisitoEspecialContaduria
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + requisitoEspecialContaduria;
            }
        }

        [Required(ErrorMessage = "Seleccione copia documento requisisto especial de derecho")]
        public string requisitoEspecialDerecho { get; set; }
        public string rutarequisitoEspecialDerecho
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + requisitoEspecialDerecho;
            }
        }

        [Required(ErrorMessage = "Seleccione copia documento requisisto especial de educación")]
        public string requisitoEspecialEducacion { get; set; }
        public string rutarequisitoEspecialEducacion
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + requisitoEspecialEducacion;
            }
        }

        //Archivos Traducidos        
        public string documentoIdentidadTraducido { get; set; }
        public string rutadocumentoIdentidadTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + documentoIdentidadTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del diploma traducido.")]
        public string diplomaPosgradoTraducido { get; set; }            
        public string rutadiplomaPosgradoTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + diplomaPosgradoTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del título de pregrado traducido.")]
        public string tituloPregradoTraducido { get; set; }
        public string rutatituloPregradoTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + tituloPregradoTraducido;
            }
        }

        public string convalidacionPregradoTraducido { get; set; }
        public string rutaconvalidacionPregradoTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + convalidacionPregradoTraducido;
            }
        }

        public string convalidacionEspecialidadBaseTraducido { get; set; }
        public string rutaconvalidacionEspecialidadBaseTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + convalidacionEspecialidadBaseTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del certificado de asignaturas traducido")]
        public string certificadoAsignaturasTraducido { get; set; }
        public string rutacertificadoAsignaturasTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoAsignaturasTraducido;
            }
        }

        public string certificadoProgramapreTraducido { get; set; }
        public string rutacertificadoProgramapreTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoProgramapreTraducido;
            }
        }

        public string formatoInvestigacionTraducido { get; set; }
        public string rutaformatoInvestigacionTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + formatoInvestigacionTraducido;
            }
        }

        public string actaSustentacionTraducido { get; set; }
        public string rutaactaSustentacionTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + actaSustentacionTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia de la tesisi de la maestría traducida")]
        public string tesisMaestriasTraducido { get; set; }
        public string rutatesisMaestriasTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + tesisMaestriasTraducido;
            }
        }

        public string tituloEspecialidadTraducido { get; set; }
        public string rutatituloEspecialidadTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + tituloEspecialidadTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del documento de record de procedimientos traducido")]
        public string recordProcedimientosTraducido { get; set; }
        public string rutarecordProcedimientosTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + recordProcedimientosTraducido;
            }
        }

        public string certificadoActividadesAcademicasTraducido { get; set; }
        public string rutacertificadoActividadesAcademicasTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoActividadesAcademicasTraducido;
            }
        }

        public string certificadoCalificacionesPosTraducido { get; set; }
        public string rutacertificadoCalificacionesPosTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoCalificacionesPosTraducido;
            }
        }

        public string documentoIdentidadPreTraducido { get; set; }
        public string rutadocumentoIdentidadPreTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + documentoIdentidadPreTraducido;
            }
        }

        public string diplomaPregradoTraducido { get; set; }
        public string rutadiplomaPregradoTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + diplomaPregradoTraducido;
            }
        }

        public string certificadoCalificacionesPreTraducido { get; set; }
        public string rutacertificadoCalificacionesPreTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoCalificacionesPreTraducido;
            }
        }

        [Required(ErrorMessage = "Seleccione copia del documento de certificado del programa traducido")]
        public string certificadoProgramaTraducido { get; set; }
        public string rutacertificadoProgramaTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + certificadoProgramaTraducido;
            }

        }

        [Required(ErrorMessage = "Seleccione copia del certificado de prácticas profesionales traducido")]
        public string requisitoEspecialSaludTraducido { get; set; }
        public string rutarequisitoEspecialSaludTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + requisitoEspecialSaludTraducido;
            }
        }

        public string requisitoEspecialContaduriaTraducido { get; set; }
        public string rutarequisitoEspecialContaduriaTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + requisitoEspecialContaduriaTraducido;
            }
        }

        public string requisitoEspecialDerechoTraducido { get; set; }
        public string rutarequisitoEspecialDerechoTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + requisitoEspecialDerechoTraducido;
            }
        }

        public string requisitoEspecialEducacionTraducido { get; set; }
        public string rutarequisitoEspecialEducacionTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + requisitoEspecialEducacionTraducido;
            }
        }

        // ---- Archivos Adicionales
        public string documentoAdicional1 { get; set; }
        public string rutaDocumentoAdicional1
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoAdicional1;
            }
        }

        public string documentoAdicional2 { get; set; }
        public string rutaDocumentoAdicional2
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoAdicional2;
            }
        }

        public string documentoAdicional3 { get; set; }
        public string rutaDocumentoAdicional3
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoAdicional3;
            }
        }

        public string documentoAdicional4 { get; set; }
        public string rutaDocumentoAdicional4
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoAdicional4;
            }
        }

        public string documentoAdicional5 { get; set; }
        public string rutaDocumentoAdicional5
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + documentoAdicional5;
            }
        }

    }

    public enum nombreDocumentos
    {
        //Posgrado
        diplomaPosgrado,//0
        tituloPregrado,//1
        certificadoAsignaturas,//2
        documentoIdentidad,//3
        certificadoProgramapre,//4
        formatoInvestigacion,//5
        actaSustentacion,//6
        tesisMaestrias,//7
        tituloEspecialidad,//8
        recordProcedimientos,//9
        certificadoActividadesAcademicas,//10
        certificadoCalificacionesPos,//11
        //Pregrado
        diplomaPregrado,//12
        certificadoCalificacionesPre,//13
        documentoIdentidadPre,//14
        certificadoPrograma,//15
        requisitoEspecialSalud,//16
        requisitoEspecialContaduria,//17
        requisitoEspecialDerecho,//18
        requisitoEspecialEducacion,//19
        convalidacionPregrado,//20
        convalidacionEspecialidadBase,//21
        //adicionales
        documentoAdicional1,//22
        documentoAdicional2,//23
        documentoAdicional3,//24
        documentoAdicional4,//25
        documentoAdicional5//26
    }

    public class Documentos
    {
        public int documentoId { get; set; }
        public int solicitudId { get; set; }
        public string nombre { get; set; }
        public string nombreTraduccion { get; set; }
        public string rutaFisica { get; set; }
        public string sharepoint { get; set; }
        public int tipoDoc { get; set; }
        public string nombreDoc { get; set; }
        public string codigoDoc { get; set; }

        public string nombreDocumentoCompleto_1 {
            get{
                string nomreCom = string.Empty;
                switch (tipoDoc)
                {
                    case 0:
                        nomreCom = "Diploma o título (debidamente legalizado por vía diplomática o con sello de apostilla).";
                        break;
                    case 1:
                        nomreCom = "Copia del título de pregrado otorgado por la institución de educación superior reconocida en Colombia";
                        break;
                    case 2:
                        nomreCom = "Copia del Certificado de asignaturas (debidamente legalizado por vía diplomática o con sello de apostilla) Traducción Oficial en caso que los documentos se obtengan en idioma diferente al castellano)";
                        break;
                    case 3:
                        nomreCom = "Documento de identidad (No se admite como documento de identidad la contraseña ni certificaciones. Además, los documentos deben estar vigentes).";
                        break;
                    case 4:
                        nomreCom = "Certificado de Programa Académico.";
                        break;
                    case 5:
                        nomreCom = "Formato de resumen de productos de investigación.";
                        break;
                    case 6:
                        nomreCom = "Acta de sustentación del trabajo de investigaciòn.  Traducción oficial en caso que los documentos se obtengan en idioma diferente al castellano).";
                        break;
                    case 7:
                        nomreCom = "Trabajo de grado  o tesis/Constancia de la institución formadora en la que se describan las caracteristicas del requisito que conllevó al otorgamiento del título," +
                                   " o si la información exigida en esta constancia hace parte  del certificado de programa académico aportar este documento.";
                        break;
                    case 8:
                        nomreCom = "Copia del documento de pregrado de especialidad base o primera espeialidad otorgado por la Institución de Educación Superior legalmente reconocida en Colombia."; 
                            /*"Título de especialidad base o primera especialidad otorgado por la IES colombiana o información del número de la Resolución de convalidación.";*/
                        break;
                    case 9:
                        nomreCom = "Para títulos de posgrado de especialidades médicas y quirúrgicas y odontológicas y maestrías en profundización clínica en salud. / Récord de procedimientos.";
                        break;
                    case 10:
                        nomreCom = "Para títulos de posgrado de especialidades médicas y quirúrgicas y odontológicas y maestrías en profundización clínica en salud. / Certificación de actividades académicas y asistenciales.";
                        break;
                    case 11:
                        nomreCom = "Certificado de calificaciones / Certificado actividades de investigación.";
                        break;
                    case 12:
                        nomreCom = "Diploma o título (debidamente legalizado por vía diplomática o con sello de apostilla).  Traducción oficial en caso que los documentos se obtengan en idioma diferente al español)";
                        break;
                    case 13:
                        nomreCom = "Certificado de calificaciones (debidamente legalizado por vía diplomática o con sello de apostilla) Traducción Oficial en caso que los documentos se obtengan en idioma diferente al castellano).";
                        break;
                    case 14:
                        nomreCom = "Documento de identidad (No se admite como documento de identidad la contraseña ni certificaciones, adicionalmente los documentos deben estar vigentes).";
                        break;
                    case 15:
                        nomreCom = "Copia del Certificado de Programa Académico, o si la institución formadora no emite el anterior documento deberá adjuntar Documento oficial emitido y suscrito por la institución formadora," +
                                  " en la que se describa la manera como se desarrolló el programa cursado. (Traducción Oficial en caso que los documentos se obtengan en idioma diferente al castellano).";
                        break;
                    case 16:
                        nomreCom = "Certificación de prácticas profesionales o internado rotatorio. (debidamente legalizado por vía diplomática o con sello de apostilla y traducido si se encuentra en idioma distinto al español).";
                        break;
                    case 17:
                        nomreCom = "Certificación de aprobación de estudios específicos de la legislación colombiana en: 1. Derecho comercial, tributario y laboral,  2. Normas contables y conceptos sobre Normas Internacionales de Información Financiera NIIF.";
                        break;
                    case 18:
                        nomreCom = "Certificación de aprobación de estudios específicos de la legislación colombiana en: 1. Derecho constitucional, 2. Derecho Administrativo, 3. Derecho Procesal: Civil, Penal y Laboral.";
                        break;
                    case 19:
                        nomreCom = "Certificado de prácticas educativas y pedagógicas con la cual se pueda demostrar una equivalencia de creditos u horas de practica.";
                        break;
                    case 20:
                        nomreCom = "Copia de la Resolución que otorga la convalidación del título de pregrado emitida.";
                        //"Resolución de convalidación pregrado";
                        break;
                    case 21:
                        nomreCom = "Copia de la Resolución de especialidad base que otorga la convalidación del título de pregrado emitida.";
                        //"Resolución de convalidación especialidad Base";
                        break;
                    case 22:
                        nomreCom = "Documento adicional 1";
                        break;
                    case 23:
                        nomreCom = "Documento adicional 2";
                        break;
                    case 24:
                        nomreCom = "Documento adicional 3";
                        break;
                    case 25:
                        nomreCom = "Documento adicional 4";
                        break;
                    case 26:
                        nomreCom = "Documento adicional 5";
                        break;
                }
                return nomreCom;
            }
        }

        public string rutaNombre
        {
            get
            {
                return "../Archivos/Documentos/Originales/" + nombre;
            }
        }

        public string rutaNombreTraducido
        {
            get
            {
                return "../Archivos/Documentos/Traducidos/" + nombreTraduccion;
            }
        }

        public List<tbDocumentos> convertDocumento(List<Documentos> doctos)
        {
            List<tbDocumentos> tbDoctos = new List<tbDocumentos>();

            foreach (Documentos doc in doctos)
            {
                tbDocumentos tbDocto = new tbDocumentos();
                tbDocto.documentoId = doc.documentoId;
                tbDocto.solicitudId = doc.solicitudId;
                tbDocto.nombre = doc.nombre;
                tbDocto.nombreTraduccion = doc.nombreTraduccion;
                tbDocto.rutaFisica = doc.rutaFisica;
                tbDocto.sharepoint = doc.sharepoint;
                tbDocto.tipoDoc = doc.tipoDoc;
                tbDocto.nombreDoc = doc.nombreDoc;
                tbDocto.codigoDoc = doc.codigoDoc;

                tbDoctos.Add(tbDocto);
            }

            return tbDoctos;
        }

        public List<Documentos> convertTbDocumento(List<tbDocumentos> tbDoctos)
        {
            List<Documentos> doctos = new List<Documentos>();

            foreach (tbDocumentos tbDoc in tbDoctos)
            {
                Documentos doc = new Documentos();
                doc.documentoId = tbDoc.documentoId;
                doc.solicitudId = tbDoc.solicitudId;
                doc.nombre = tbDoc.nombre;
                doc.nombreTraduccion = tbDoc.nombreTraduccion;
                doc.rutaFisica = tbDoc.rutaFisica;
                doc.sharepoint = tbDoc.sharepoint;
                doc.tipoDoc = (int)tbDoc.tipoDoc;
                doc.nombreDoc = tbDoc.nombreDoc;
                doc.codigoDoc = tbDoc.codigoDoc;

                doctos.Add(doc);
            }

            return doctos;
        }
    }


}
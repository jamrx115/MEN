using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConvalidacionEducacionSuperior.Models
{
    public class Paso3Model
    {
        bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        //Campos de validacion
        public string estilo { get; set; }
        public string esMaestria { get; set; }
        public string areaSalud { get; set; }
        public int areaSaludParam { get; set; }
        public bool segundaEspecialidadSalud { get; set; }
        public bool EspecialidadMedicaSalud { get; set; }
        public bool segundaEspecialidadSaludNal { get; set; }

        public bool acreditacionCalidad { get; set; }
        public string SegundaEspecialidadSaludDisplay { get; set; }
        public string EspecialidadMedicaSaludDisplay { get; set; }
        
        public string SegundaEspecialidadSaludDisplaySI { get; set; }
        public string SegundaEspecialidadSaludDisplayNO { get; set; }
        public string displayAutorizado { get; set; }
        public string displayPropio { get; set; }
        public string displayNotificaElectronica { get; set; }
        public string PaisDisplay { get; set; }
        public string PaisDisplay_II { get; set; }
        public string displayNombrePrograma { get; set; }
        public string displayInstitucionSalud { get; set; }
        public string displayInstitucionPre { get; set; }
        public string displayValidacionInstituto { get; set; }

        public string DepartamentoNotificacion { get; set; }
        public string CiudadNotificacion { get; set; }

        public string ModalidadPrograma { get; set; }

        //Usuario
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoNombre { get; set; }
        public string SegundoApellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int GeneroId { get; set; }
        public string Genero { get; set; }

        //Informacio Usuario Adicional
        [Required(ErrorMessage = "Campo Ciudad de Expedición es obligatorio")]
        //[RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "No se adimiten caracteres especiales")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string CiudadExpedicion { get; set; }
        [Required(ErrorMessage = "Campo País de Expedición es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string PaisExpedicion { get; set; }

        //Informacio Usuario Adicional
        //[Required(ErrorMessage = "Campo Ciudad de Expedición es obligatorio")]
        ////[RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "No se adimiten caracteres especiales")]
        //[RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string CiudadExpedicionCC { get; set; }

        public int CiudadExpedicionCCCod { get; set; }

        public string DeptoExpedicion { get; set; }

        //Informacio Usuario Adicional
        [Required(ErrorMessage = "Campo Ciudad de Expedición es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Ciudad")]
        public int CiudadInstitutoPre { get; set; }

        public string CiudadInstitutoPreText
        {
            get
            {
                return db.tbCiudad.Find(CiudadInstitutoPre).ciudad;
            }
        }

        [Required(ErrorMessage = "Campo País de Expedición es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Departamento")]
        public int DptoInstitutoPre { get; set; }

        public string DptoInstitutoPreText
        {
            get
            {
                return db.tbDepartamento.Find(DptoInstitutoPre).departamento;
            }
        }

        //Informacio Usuario Adicional
        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Ciudad")]
        public int CiudadInstitutoPreSeg { get; set; }

        public string CiudadInstitutoPreSegText
        {
            get
            {
                return db.tbCiudad.Find(CiudadInstitutoPreSeg).ciudad;
            }
        }

        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Departamento")]
        public int DptoInstitutoPreSeg { get; set; }

        public string DptoInstitutoPreSegText
        {
            get
            {
                return db.tbDepartamento.Find(DptoInstitutoPreSeg).departamento;
            }
        }

        public string Nacionalidad { get; set; }
        [Required(ErrorMessage = "Campo Dirección de Residencia es obligatorio")]
        public string Direccion { get; set; }

        public string CodigoPostal { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Formato de Email errado")]
        [Required(ErrorMessage = "Campo Correo Electrónico es obligatorio")]
        public string Email { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Formato de Email errado")]
        //[System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "El correo electrónico para notificación No coincide")]
        [Required(ErrorMessage = "Campo Confirma Correo Electrónico es obligatorio")]
        public string ConfirmaEmail { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Formato de Email errado")]
        public string Email2 { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Formato de Email errado")]
        //[System.ComponentModel.DataAnnotations.Compare("Email2", ErrorMessage = "El correo electrónico opcional No coincide")]        
        public string ConfirmaEmail2 { get; set; }

        [Range(0, 10000000, ErrorMessage = "Solo se adimiten 7 digitos de números positivos")]
        public int? TelefonoFijo { get; set; }

        [Required(ErrorMessage = "Campo Teléfono Móvil es obligatorio")]
        [Range(1, 10000000000, ErrorMessage = "Solo se adimiten 10 digitos de números positivos")]
        public long Celular { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione País")]
        public int Pais { get; set; }
        public string PaisNombre
        {
            get
            {
                tbPais paisOri = db.tbPais.Where(p => p.paisId == Pais).FirstOrDefault();
                return paisOri == null ? "COLOMBIA" : paisOri.pais;
            }
        }
        public string Departamento { get; set; }
        public string DepartamentNombre
        {
            get
            {
                int dpto = string.IsNullOrEmpty(Departamento) ? 0 : int.Parse(Departamento);
                tbDepartamento Ori = db.tbDepartamento.Where(p => p.departamentoId == dpto).FirstOrDefault();
                return Ori == null ? "BOGOTÁ, D.C." : Ori.departamento;
            }
        }
        public string Ciudad { get; set; }
        public string CiudadNombre
        {
            get
            {
                if (!PaisNombre.Equals("COLOMBIA"))
                {
                    return Ciudad;
                }
                int city = string.IsNullOrEmpty(Ciudad) ? 0 : int.Parse(Ciudad);
                tbCiudad Ori = db.tbCiudad.Where(p => p.ciudadId == city).FirstOrDefault();
                return Ori == null ? "BOGOTÁ, D.C." : Ori.ciudad;
            }
        }

        [Required(ErrorMessage = "Campo Ciudad de Residencia es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string Ciudad_II { get; set; }


        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        //Solicitud
        public tbSolicitud solicitudActual { get; set; }


        //AUTORIZADO
        public Autorizado _autorizado { get; set; }
        public string tipoDoc { get; set; }

        //INSTITUCION
        public Institucion _institucion { get; set; }
        public List<Institucion> _institucionList { get; set; }
        public string NombreInstitucion { get; set; }
        public string PaisInstituto { get; set; }
        public string DepartamentoInstituto { get; set; }
        public string CiudadInstituto { get; set; }

        //PROGRAMA
        public Programa _programa { get; set; }
        public string NombrePrograma { get; set; }
        public string TipoEducacionSuperior { get; set; }
        public string TipoMaestria { get; set; }
        public string TipoDuracion { get; set; }
        public int CampoAmplioConocimiento { get; set; }
        //public string _programa_TituloObtenido { get; set; }

        //PREGRADO SOPORTE
        public InfoPregrado _infoPregrado { get; set; }
        public string NombreInstitucionOtorgaPre { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Programa")]
        public int NombreProgramaOtorgaPre { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Seleccione Programa")]
        public int NombreProgramaOtorgaPreSeg { get; set; }

        public string _infoPregrado_Entidad { get; set; }
        //SEGUNDA ESPECIALIDAD
        public InfoPregrado _segundaEspecialidad { get; set; }
        public string NombreInstitucionSegundaEsp { get; set; }

        public string _segundaEsp_Entidad { get; set; }
       

        //Documentos
        public List<Documentos> lstDocumentos { get; set; }
        public List<tbDocumentos> lstTbDocumentos { get; set; }
    }

    public class Autorizado
    {
        bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        public int AutorizadoId { get; set; }
        [Required(ErrorMessage = "Campo Nombre dompleto del autorizado es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Campo Primer nombre de Autorizado es obligatorio")]
        public string Nombre1Aut { get; set; }
        public string Nombre2Aut { get; set; }
        [Required(ErrorMessage = "Campo Primer apellido de Autorizado es obligatorio")]
        public string apellido1Aut { get; set; }
        public string apellido2Aut { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoDocumentoNombre
        {
            get
            {
                int tiDoc = int.Parse(TipoDocumento);
                tbTipoDocumento Ori = db.tbTipoDocumento.Where(p => p.itpoDocumentoId == tiDoc).FirstOrDefault();
                return Ori == null ? "BOGOTÁ, D.C." : Ori.TipoDocumento;
            }
        }

        public string TipoDocumentoAutorizadoCod
        {
            get; set;
            //get
            //{
            //    int tiDoc = int.Parse(TipoDocumento);
            //    tbTipoDocumento Ori = db.tbTipoDocumento.Where(p => p.itpoDocumentoId == tiDoc).FirstOrDefault();
            //    return Ori == null ? "CC" : Ori.TipoDocumentoCod;
            //}
            //set{
            //    int tiDoc = int.Parse(TipoDocumento);
            //    tbTipoDocumento Ori = db.tbTipoDocumento.Where(p => p.itpoDocumentoId == tiDoc).FirstOrDefault();
            //    TipoDocumentoAutorizadoCod = Ori == null ? "CC" : Ori.TipoDocumentoCod;
            //}
        }

        [Required(ErrorMessage = "Campo documento del autorizado es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Campo Dirección del autorizado es obligatorio")]
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }

        [Range(0, 10000000, ErrorMessage = "Solo se adimiten 7 digitos de números positivos")]
        public int? TelefonoFijo { get; set; }

        [Range(0, 10000000000, ErrorMessage = "Solo se adimiten 10 digitos de números positivos")]
        public long? Celular { get; set; }
        [Required(ErrorMessage = "Campo País del autorizado es obligatorio")]
        public string Pais { get; set; }
        public string PaisNombre
        {
            get
            {
                int pais = string.IsNullOrEmpty(Pais) ? 0 : int.Parse(Pais);
                tbPais paisOri = db.tbPais.Where(p => p.paisId == pais).FirstOrDefault();
                return paisOri == null ? "COLOMBIA" : paisOri.pais;
            }
        }
        public string Departamento { get; set; }
        public string DepartamentNombre
        {
            get
            {
                int dpto = string.IsNullOrEmpty(Departamento) ? 0 : int.Parse(Departamento);
                tbDepartamento Ori = db.tbDepartamento.Where(p => p.departamentoId == dpto).FirstOrDefault();
                return Ori == null ? "BOGOTÁ, D.C." : Ori.departamento;
            }
        }
        public string Ciudad { get; set; }
        public string CiudadNombre
        {
            get
            {
                int city = string.IsNullOrEmpty(Ciudad) ? 0 :  int.Parse(Ciudad);
                tbCiudad Ori = db.tbCiudad.Where(p => p.ciudadId == city).FirstOrDefault();
                return Ori == null ? "BOGOTÁ, D.C." : Ori.ciudad;
            }
        }

        public tbDatosContacto convertAutorizado(Autorizado _autorizado)
        {
            bdConvalidacionesEntities db = new bdConvalidacionesEntities();
            tbDatosContacto _tbAutorizado = new tbDatosContacto();
            //_tbAutorizado.NombreCompletoAutorizado = _autorizado.NombreCompleto;
            _tbAutorizado.nombre1Autorizado = _autorizado.Nombre1Aut;
            _tbAutorizado.nombre2Autorizado = _autorizado.Nombre2Aut;
            _tbAutorizado.apellido1Autorizado = _autorizado.apellido1Aut;
            _tbAutorizado.apellido2Autorizado = _autorizado.apellido2Aut;
            tbTipoDocumento tipoDoc = db.tbTipoDocumento.Where(d => d.TipoDocumento.Equals(_autorizado.TipoDocumento)).FirstOrDefault();
            if(tipoDoc != null)
                _tbAutorizado.TipoDocumentoAutorizado = tipoDoc.itpoDocumentoId ;
            _tbAutorizado.NumeroDocumentoAutorizado = _autorizado.NumeroDocumento;
            _tbAutorizado.DireccionAutorizado = _autorizado.Direccion;
            _tbAutorizado.CodigoPostalAutorizado = _autorizado.CodigoPostal;
            //if (_autorizado.TelefonoFijo == null)
            //    _autorizado.TelefonoFijo = 0;
            _tbAutorizado.TelefonoAutorizado = _autorizado.TelefonoFijo.ToString();
            //if (_autorizado.Celular == null)
            //    _autorizado.Celular = 0;
            _tbAutorizado.CelularAutorizado = _autorizado.Celular.ToString();
            _tbAutorizado.PaisAutorizado = _autorizado.Pais;
            _tbAutorizado.DepartamentoAutorizado = _autorizado.Departamento;
            _tbAutorizado.CiudadAutorizado = _autorizado.Ciudad;
            _tbAutorizado.TipoDocumentoAutorizadoCod = _autorizado.TipoDocumentoAutorizadoCod;
            
            return _tbAutorizado;
        }

        public Autorizado convertTbAutorizado(tbDatosContacto _tbAutorizado)
        {
            Autorizado _autorizado = new Autorizado();
            _autorizado.AutorizadoId = _tbAutorizado.datosContactoId;
            //_autorizado.NombreCompleto = _tbAutorizado.NombreCompletoAutorizado;
            _autorizado.Nombre1Aut = _tbAutorizado.nombre1Autorizado ;
            _autorizado.Nombre2Aut = _tbAutorizado.nombre2Autorizado ;
            _autorizado.apellido1Aut = _tbAutorizado.apellido1Autorizado;
            _autorizado.apellido2Aut = _tbAutorizado.apellido2Autorizado;
            if (_tbAutorizado.tbTipoDocumento != null)
                _autorizado.TipoDocumento = _tbAutorizado.tbTipoDocumento.TipoDocumento;
            _autorizado.NumeroDocumento = _tbAutorizado.NumeroDocumentoAutorizado;
            _autorizado.Direccion = _tbAutorizado.DireccionAutorizado;
            _autorizado.CodigoPostal = _tbAutorizado.CodigoPostalAutorizado;
            _autorizado.TelefonoFijo = string.IsNullOrEmpty(_tbAutorizado.TelefonoAutorizado) ? 0 : int.Parse(_tbAutorizado.TelefonoAutorizado); 
            _autorizado.Celular = string.IsNullOrEmpty(_tbAutorizado.CelularAutorizado) ? 0 : long.Parse(_tbAutorizado.CelularAutorizado);
            _autorizado.Pais = _tbAutorizado.PaisAutorizado;
            _autorizado.Departamento = _tbAutorizado.DepartamentoAutorizado;
            _autorizado.Ciudad = _tbAutorizado.CiudadAutorizado;
            _autorizado.TipoDocumentoAutorizadoCod = _tbAutorizado.TipoDocumentoAutorizadoCod;

            return _autorizado;
        }
    }

    public class InfoPregrado
    {
        public int pregradoId { get; set; }

        [Required(ErrorMessage = "Campo Institucion que otorga Título es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string Instituto { get; set; }

        [Required(ErrorMessage = "Campo Institucion que otorga Título es obligatorio")]
        public string InstitutoText { get; set; }

        [Required(ErrorMessage = "Campo Título Obtenido es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Campo Fecha Título Obtenido es obligatorio")]
        [ValidateDateRange]
        public System.DateTime FechaTitulo { get; set; }
        [Required(ErrorMessage = "Seleccione el archivo de copia del título")]
        public string RutaCopiaTitulo { get; set; }

        public string Entidad { get; set; }

        [Required(ErrorMessage = "Campo Número de resolución es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string NroResolucion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Campo Fecha de resolución es obligatorio")]
        [ValidateDateRange]
        public System.DateTime FechaResolucion { get; set; }
        
        public string RutaCopiaResolucion { get; set; }

        //Informacio Usuario Adicional
        [Required(ErrorMessage = "Campo Ciudad de Expedición es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione Ciudad")]
        public int CiudadInstitutoPre { get; set; }

        [Required(ErrorMessage = "Campo País de Expedición es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione Departamento")]
        public int DptoInstitutoPre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Seleccione Programa")]
        public int NombreProgramaOtorgaPre { get; set; }

        public tbPregrado convertPregrado(InfoPregrado _pregrado, bool esNacional)
        {
            bdConvalidacionesEntities db = new bdConvalidacionesEntities();
            tbPregrado _tbPregrado = new tbPregrado();
            _tbPregrado.PreGradoId = _pregrado.pregradoId;
            _tbPregrado.RutaCopiaTitulo = _pregrado.RutaCopiaTitulo;
            _tbPregrado.RutaCopiaResolucion = _pregrado.RutaCopiaResolucion;
            if (esNacional)
            {
                _tbPregrado.Instituto = _pregrado.Instituto;
                _tbPregrado.InstitutoText = _pregrado.InstitutoText;
                _tbPregrado.Titulo = _pregrado.Titulo;
                _tbPregrado.FechaTitulo = _pregrado.FechaTitulo;
                _tbPregrado.ciudad = _pregrado.CiudadInstitutoPre;
                _tbPregrado.departamento = _pregrado.DptoInstitutoPre;
                _tbPregrado.programa = _pregrado.NombreProgramaOtorgaPre;
            }
            else
            {
                _tbPregrado.Entidad = string.IsNullOrEmpty(_pregrado.Entidad) ? 1 : int.Parse(_pregrado.Entidad); // db.tbEntidad.Where(e => e.Entidad.Equals(_pregrado.Entidad)).FirstOrDefault().EntidadId;
                _tbPregrado.Resolucion = _pregrado.NroResolucion;
                _tbPregrado.FechaResolucion = _pregrado.FechaResolucion;
            }

            return _tbPregrado;
        }

        public InfoPregrado convertTbPregrado(tbPregrado _tbPregrado, bool esNacional)
        {
            InfoPregrado _pregrado = new InfoPregrado();
            _pregrado.pregradoId = _tbPregrado.PreGradoId;
            if (esNacional)
            {
                _pregrado.Instituto = _tbPregrado.Instituto;
                _pregrado.InstitutoText = _tbPregrado.InstitutoText;
                _pregrado.Titulo = _tbPregrado.Titulo;

                if (_tbPregrado.FechaTitulo != null)
                    _pregrado.FechaTitulo = (DateTime)_tbPregrado.FechaTitulo;
                else
                    _pregrado.FechaTitulo = DateTime.Now;

                _pregrado.RutaCopiaTitulo = _tbPregrado.RutaCopiaTitulo;

                _pregrado.CiudadInstitutoPre = _tbPregrado.ciudad == null ? 0: (int)_tbPregrado.ciudad;
                _pregrado.DptoInstitutoPre = _tbPregrado.departamento == null ? 0 : (int)_tbPregrado.departamento;
                _pregrado.NombreProgramaOtorgaPre = _tbPregrado.programa == null ? 0 : (int)_tbPregrado.programa;
            }
            else
            {
                if (_tbPregrado.tbEntidad != null)
                    _pregrado.Entidad = _tbPregrado.Entidad.ToString();

                if (_tbPregrado.FechaResolucion != null)
                    _pregrado.FechaResolucion = (DateTime)_tbPregrado.FechaResolucion;
                else
                    _pregrado.FechaResolucion = DateTime.Now;

                _pregrado.NroResolucion = _tbPregrado.Resolucion;
                _pregrado.RutaCopiaResolucion = _tbPregrado.RutaCopiaResolucion;
            }

            return _pregrado;
        }
    }

    public class Institucion
    {
        bdConvalidacionesEntities db1 = new bdConvalidacionesEntities();
        public int institucionId { get; set; }

        [Required(ErrorMessage = "Seleccione País")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Campo Institución es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string Instituto { get; set; }

        [Required(ErrorMessage = "Campo Institución es obligatorio")]
        public string InstitutoText { get; set; }

        [Required(ErrorMessage = "Campo Ciudad es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Campo Ciudad es obligatorio")]
        public string CiudadText { get; set; }

        public string Estado { get; set; }

        public string Facultad { get; set; }
        public string Instructivo { get; set; }

        public string RutaInstructivo
        {
            get
            {
                return string.IsNullOrEmpty(Instructivo) ? string.Empty : Instructivo.StartsWith("Señor") ? string.Empty : Instructivo;
            }
        }

        //Display Caja de texto
        public string DisplasyInstitutoText
        {
            get
            {
                return Instituto == "0" ? "display:inline" : "display:none";
            }
        }

        //Display Caja de texto
        public string DisplaysCiudadExtrText
        {
            get
            {
                return Ciudad == "0" ? "display:inline" : "display:none";
            }
        }

        //LISTAS

        public IEnumerable<SelectListItem> ListaPaises
        {
            get
            {
                List<SelectListItem> listaReturn = new SelectList(db1.tbPais.Where(P => P.pais.ToUpper() != "COLOMBIA").OrderBy(p => p.pais).ToList(), "pais", "pais", Pais).ToList();
                listaReturn.Insert(0, (new SelectListItem { Text = "SELECCIONE PAIS", Value = "" }));
                return listaReturn;
            }
        }

        public IEnumerable<SelectListItem> ListaCiudades
        {
            get
            {
                List<SelectListItem> listaReturn = new List<SelectListItem>();
                tbPais paisTb = db1.tbPais.Where(p => p.pais.ToUpper().Equals(Pais)).FirstOrDefault();
                if(paisTb != null)
                {
                    int idPais = paisTb.paisId;
                    listaReturn = new SelectList(db1.REF_MUNICIPIO_EXTRANJERO.Where(P => P.ID_PAIS.Equals(idPais.ToString())).OrderBy(p => p.CIUDAD_EXTRANJERA).ToList(), "ID_CIUDAD_EXT", "CIUDAD_EXTRANJERA", Ciudad).ToList();
                }                
                listaReturn.Insert(0, (new SelectListItem { Text = "OTRA", Value = "0" }));
                return listaReturn;
            }
        }

        public IEnumerable<SelectListItem> ListaInstituciones
        {
            get
            {
                 List<SelectListItem> cmbInstituciones = new SelectList(db1.INS_INSTITUCION_EXTRANJERA.OrderBy(p => p.NOMBRE_EN_ESPANOL).ToList(), "ID_INSTITUCION", "NOMBRE_EN_ESPANOL", Instituto).ToList();
                cmbInstituciones.Insert(0, (new SelectListItem { Text = "OTRO", Value = "0" }));
                return cmbInstituciones;
            }
        }

        public tbInstitucion convertInstituto(Institucion _instituto)
        {
            tbInstitucion _tbInstituto = new tbInstitucion();
            _tbInstituto.InstitucionId = _instituto.institucionId;
            _tbInstituto.Instituto = _instituto.Instituto;
            _tbInstituto.InstitutoText = _instituto.InstitutoText;
            _tbInstituto.Pais = _instituto.Pais;
            _tbInstituto.Ciudad = _instituto.Ciudad;
            _tbInstituto.Estado = _instituto.Estado;
            _tbInstituto.Facultad = _instituto.Facultad;
            _tbInstituto.Instructivo = _instituto.Instructivo;
            _tbInstituto.ID_CIUDAD_EXT = _instituto.CiudadText;
            return _tbInstituto;
        }

        public List<tbInstitucion> convertListInstituto(List<Institucion> _tbInstituto)
        {
            List<tbInstitucion> lstReturn = new List<tbInstitucion>();
            foreach (Institucion inst in _tbInstituto)
            {
                tbInstitucion _instituto = convertInstituto(inst);
                lstReturn.Add(_instituto);
            }

            return lstReturn;
        }

        public Institucion convertTbInstituto(tbInstitucion _tbInstituto)
        {
            Institucion _instituto = new Institucion();
            _instituto.institucionId = _tbInstituto.InstitucionId;
            _instituto.Instituto = _tbInstituto.Instituto;
            _instituto.InstitutoText = _tbInstituto.InstitutoText;
            _instituto.Pais = _tbInstituto.Pais;
            _instituto.Ciudad = _tbInstituto.Ciudad;
            _instituto.Estado = _tbInstituto.Estado;
            _instituto.Facultad = _tbInstituto.Facultad;
            _instituto.Instructivo = _tbInstituto.Instructivo;
            _instituto.CiudadText = _tbInstituto.ID_CIUDAD_EXT;
            return _instituto;
        }

        public List<Institucion> convertListTbInstituto(List<tbInstitucion> _tbInstituto)
        {
            List<Institucion> lstReturn = new List<Institucion>();
            foreach (tbInstitucion inst in _tbInstituto)
            {
                Institucion _instituto = convertTbInstituto(inst);
                lstReturn.Add(_instituto);
            }

            return lstReturn;
        }
    }

    public class ValidateDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if(value == null)
            {
                value = DateTime.Now;
            }
            DateTime feha = DateTime.Parse(value.ToString());
            if (feha >= DateTime.Now || feha <= DateTime.Now.AddYears(-200))
            {
                return new ValidationResult("La fecha no está en el rango requerido.");
            }
            else
            {
                return ValidationResult.Success; 
            }
        }
    }

    public class Programa
    {
        bdConvalidacionesEntities db = new bdConvalidacionesEntities();
        public int programaId { get; set; }
        //Cuerpo       

        [Required(ErrorMessage = "Campo Nombre del Programa es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string NombrePrograma { get; set; }

        [Required(ErrorMessage = "Campo Nombre del Programa Manual es obligatorio")]
        public string NombreProgramaText { get; set; }

        //[Required(ErrorMessage = "Campo Título Otorgado es obligatorio")]
        //[RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string TituloObtenido { get; set; }

        [Required(ErrorMessage = "Campo Título obtenido es obligatorio")]
        public string TituloObtenidoManual { get; set; }

        public string TipoEducacionSuperior { get; set; } // NIvel de formación
        public string TipoEducacionSuperiorNombre
        {
            get
            {
                int edu = string.IsNullOrEmpty(TipoEducacionSuperior) ? 0 : int.Parse(TipoEducacionSuperior);
                tbTipoEducacionSuperior Ori = db.tbTipoEducacionSuperior.Where(p => p.tipoEducacionSupId == edu).FirstOrDefault();
                return Ori == null ? "" : Ori.TipoEducacionSuperior;
            }
        }
        public string TipoMaestria { get; set; }
        public string TipoMaestriaNombre
        {
            get
            {
                int maest = string.IsNullOrEmpty(TipoMaestria) ? 0 : int.Parse(TipoMaestria);
                tbEnfasis Ori = db.tbEnfasis.Where(p => p.enfasisId == maest).FirstOrDefault();
                return Ori == null ? "" : Ori.enfasis;
            }
        }

        [Required(ErrorMessage = "Campo Duración es obligatorio")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El campo no acepta números negativos")]
        [Range(1, 99, ErrorMessage = "El campo no acepta números negativos. Solo se aceptan números > 0 de 2 cifras.")]
        public int Duracion { get; set; }

        public string TipoDuracion { get; set; }
        public string TipoDuracionNombre
        {
            get
            {
                int maest = string.IsNullOrEmpty(TipoDuracion) ? 0 : int.Parse(TipoDuracion);
                tbTipoDuracion Ori = db.tbTipoDuracion.Where(p => p.tipoDuracionId == maest).FirstOrDefault();
                return Ori == null ? "" : Ori.TipoDuracion;
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "Campo Fecha del Título es obligatorio")]
        [ValidateDateRange]
        public System.DateTime FechaTitulo { get; set; }

        public string ModalidadPrograma { get; set; }
        public string ModalidadProgramaNombre
        {
            get
            {
                int maest = string.IsNullOrEmpty(ModalidadPrograma) ? 0 : int.Parse(ModalidadPrograma);
                tbModalidadPrograma Ori = db.tbModalidadPrograma.Where(p => p.modalidadProgramaId == maest).FirstOrDefault();
                return Ori == null ? "" : Ori.ModalidadPrograma;
            }
        }

        [Required(ErrorMessage = "Campo Denominación es obligatorio")]
        [RegularExpression(@"^[^$%&|<>#@*]*$", ErrorMessage = "No se adimiten caracteres especiales")]
        public string DenominacionTitulo { get; set; }
        public int CampoAmplioConocimiento { get; set; }
        public string CampoAmplioConocimientoNombre
        {
            get
            {
                tbCampoAmplioConocimiento Ori = db.tbCampoAmplioConocimiento.Where(p => p.CampoAmplioConocimientoId == CampoAmplioConocimiento).FirstOrDefault();
                return Ori == null ? "" : Ori.CampoAmplioConocimiento;
            }
        }

        public bool SegundaEspecialidad { get; set; }
        public bool SegundaEspecialidadNal { get; set; }

        public bool EspecialidadMedicaSalud { get; set; }

        public tbPrograma convertPrograma(Programa _Programa)
        {
            
            tbPrograma _tbPrograma = new tbPrograma();
            _tbPrograma.programaId = _Programa.programaId;
            _tbPrograma.NombrePrograma = _Programa.NombrePrograma;            
            _tbPrograma.TipoEducacionSuperior = int.Parse(_Programa.TipoEducacionSuperior);
            _tbPrograma.TipoMaestria = _Programa.TipoMaestria;
            _tbPrograma.Duracion = _Programa.Duracion;
            _tbPrograma.TipoDuracion = int.Parse(_Programa.TipoDuracion);
            _tbPrograma.FechaTitulo = _Programa.FechaTitulo;
            _tbPrograma.ModalidadPrograma = int.Parse(_Programa.ModalidadPrograma);
            _tbPrograma.DenominacionTitulo = _Programa.DenominacionTitulo;
            if (!string.IsNullOrEmpty(_Programa.NombrePrograma))
            {
                if (_Programa.NombrePrograma.Equals("0"))
                {
                    _tbPrograma.TituloObtenido = _Programa.TituloObtenidoManual;
                    _tbPrograma.NombreProgramaText = _Programa.NombreProgramaText;
                }
                else
                {
                    _tbPrograma.TituloObtenido = _Programa.TituloObtenido;
                    _tbPrograma.NombreProgramaText = db.PRO_PROGRAMA_EXTRANJERO.Find(int.Parse(_Programa.NombrePrograma)).DENOMINACION;
                }
            }                
            else
            {
                _tbPrograma.TituloObtenido = _Programa.TituloObtenidoManual;
                _tbPrograma.NombreProgramaText = _Programa.NombreProgramaText;
            }   
            _tbPrograma.SegundaEspecialidad = _Programa.SegundaEspecialidad;
            _tbPrograma.SegundaEspecialidadNal = _Programa.SegundaEspecialidadNal;
            _tbPrograma.EspecialidadMedicaSalud = _Programa.EspecialidadMedicaSalud;
            _tbPrograma.CampoAmplioConocimiento = _Programa.CampoAmplioConocimiento; // db.tbCampoAmplioConocimiento.Where(d => d.CampoAmplioConocimiento.Equals(_Programa.CampoAmplioConocimiento)).FirstOrDefault().CampoAmplioConocimientoId;

            return _tbPrograma;
        }

        public Programa convertTbPrograma(tbPrograma _tbPrograma)
        {
            Programa _programa = new Programa();
            _programa.programaId = _tbPrograma.programaId;
            _programa.NombrePrograma = _tbPrograma.NombrePrograma;
            _programa.NombreProgramaText = _tbPrograma.NombreProgramaText;
            _programa.TipoEducacionSuperior = _tbPrograma.TipoEducacionSuperior.ToString();
            _programa.TipoMaestria = _tbPrograma.TipoMaestria;
            _programa.Duracion = (int)_tbPrograma.Duracion;
            _programa.TipoDuracion = _tbPrograma.TipoDuracion.ToString();
            _programa.FechaTitulo = (DateTime)_tbPrograma.FechaTitulo;
            _programa.ModalidadPrograma = _tbPrograma.ModalidadPrograma.ToString();
            _programa.DenominacionTitulo = _tbPrograma.DenominacionTitulo;
            if (!string.IsNullOrEmpty(_programa.NombrePrograma))
            {
                if (_tbPrograma.NombrePrograma.Equals("0"))
                    _programa.TituloObtenidoManual = _tbPrograma.TituloObtenido;
                else
                    _programa.TituloObtenido = _tbPrograma.TituloObtenido;
            }
            else
            {
                _programa.TituloObtenidoManual = _tbPrograma.TituloObtenido;
                _programa.TituloObtenido = "N/A";
            }
                

            _programa.SegundaEspecialidad = (bool)_tbPrograma.SegundaEspecialidad;
            _programa.SegundaEspecialidadNal = (bool)_tbPrograma.SegundaEspecialidadNal;
            _programa.EspecialidadMedicaSalud = (bool)_tbPrograma.EspecialidadMedicaSalud;
            _programa.CampoAmplioConocimiento = (int)_tbPrograma.CampoAmplioConocimiento;// _tbPrograma.tbCampoAmplioConocimiento.CampoAmplioConocimiento;

            return _programa;
        }
    }

    
}
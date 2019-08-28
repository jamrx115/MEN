using ConvalidacionEducacionSuperiorDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConvalidacionEducacionSuperior.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite Usuario")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "Digite contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Seleccione tipo de Documento")]
        public string  tipoDoc { get; set; }
        public string tipoValidacion { get; set; }
        public UsuarioModel datosUsuario { get; set; }
        public List<tbSolicitud> lstSolicitudes { get; set; }
        public tbSolicitud solicitudActual { get; set; }

        public List<tbSolicitud> lstSolicitudesTramite { get; set; }
    }

    public class UsuarioModel
    {
        bdConvalidacionesEntities db1 = new bdConvalidacionesEntities();
        public string login { get; set; }
        [Required(ErrorMessage = "Digite Primer Nombre")]
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        [Required(ErrorMessage = "Digite Primer Apellido")]
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string genero { get; set; }
        public int generoId { get; set; }

        [Required(ErrorMessage = "Digite contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Seleccione tipo de Documento")]
        public string tipoDoc { get; set; }

        public string nombretipoDoc
        {
            get
            {
                return db1.tbTipoDocumento.Find(int.Parse(tipoDoc)).TipoDocumento;
            }
        }

        public string codigotipoDoc
        {
            get
            {
                return db1.tbTipoDocumento.Find(int.Parse(tipoDoc)).TipoDocumentoCod;
            }
        }

        [Required(ErrorMessage = "Seleccione número de Documento")]
        public string numeroDocumento { get; set; }
        [Required(ErrorMessage = "Digite Dirección")]
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string pais { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }
        public string ciudad_II { get; set; }
        [Required(ErrorMessage = "Digite Correo electrónico")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Formato de correo electrónico invalido")]
        public string email { get; set; }
        public string emailOpcional { get; set; }
        [Required(ErrorMessage = "Digite Celular")]
        public string celular { get; set; }
        public string nacionalidad { get; set; }
        [Required(ErrorMessage = "Digite ciudad de expedición")]
        public string ciudadExpedicion { get; set; }
        public string codigoPostal { get; set; }
        //Visibilidad de Campos
        public string PaisDisplay { get; set; }
        public string PaisDisplay_II { get; set; }
    }

    public class CambioContrasena
    {
        [Required(ErrorMessage = "Digite contraseña anterior")]
        [Compare("validPassword", ErrorMessage = "la contraseña actual no es válida")]
        public string PasswordOld { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{2,}", ErrorMessage = "La contraseña no cumple con el formato requerido.")]
        [StringLength(50, ErrorMessage = "la contraseña debe contener minimo {2} caracteres.", MinimumLength = 8)]
        [validaPswIgual]
        [Required(ErrorMessage = "Digite nueva contraseña")]
        public string PasswordNew { get; set; }
        [Required(ErrorMessage = "Confirme nueva contraseña")]
        [Compare("PasswordNew", ErrorMessage = "No coincide la contraseña")]
        public string PasswordNewConfirma { get; set; }
        public string user { get; set; }
        public string validPassword { get; set; }
    }

    public class validaPswIgualAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contra = (CambioContrasena)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(contra.PasswordNew))
                return new ValidationResult("Digite nueva contraseña.");

            if (contra.PasswordNew.Equals(contra.PasswordOld))
                return new ValidationResult("La contraseña no puede ser igual a la anterior.");

            if (contra.PasswordNew.Equals(contra.user))
                return new ValidationResult("La contraseña no puede ser igual al login de usuario.");

            return ValidationResult.Success;
        }
    }

    //public class ComparaPswAttribute : ValidationAttribute
    //{
    //    string _valor;
    //    string _mensaje;

    //    public ComparaPswAttribute(string valor, string mensaje)
    //    {
    //        _valor = valor;
    //        _mensaje = mensaje;
    //    }

    //    protected override ValidationResult IsValid(
    //        object value, ValidationContext validationContext)
    //    {
    //        var contra = (CambioContrasena)validationContext.ObjectInstance;            
    //        string valorComparar;
    //        switch (_valor)
    //        {
    //            case "user":
    //                valorComparar = contra.user;
    //                break;
    //            case "PasswordOld": valorComparar = contra.PasswordOld;
    //                break;
    //            default:
    //                valorComparar = string.Empty;
    //                break;
    //        }

    //        if (contra.PasswordNew.Equals(valorComparar))
    //        {
    //            return new ValidationResult(_mensaje);
    //        }

    //        return ValidationResult.Success;
    //    }
    //}

}
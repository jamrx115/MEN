﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConvalidacionEducacionSuperior.AutenticacionLogin {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.convalidaciones.msl.com/", ConfigurationName="AutenticacionLogin.ConsultaWSWebRegistry")]
    public interface ConsultaWSWebRegistry {
        
        // CODEGEN: El parámetro 'return' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.convalidaciones.msl.com/ConsultaWSWebRegistry/getWebRegistryRequest", ReplyAction="http://ws.convalidaciones.msl.com/ConsultaWSWebRegistry/getWebRegistryResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse getWebRegistry(ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.convalidaciones.msl.com/ConsultaWSWebRegistry/getWebRegistryRequest", ReplyAction="http://ws.convalidaciones.msl.com/ConsultaWSWebRegistry/getWebRegistryResponse")]
        System.Threading.Tasks.Task<ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse> getWebRegistryAsync(ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.convalidaciones.msl.com/")]
    public partial class consultaRespuestaDTO : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoCiudadExpedicionField;
        
        private string correoElectronicoField;
        
        private string descripcionCiudadExpedicionField;
        
        private string numeroIdentificacionField;
        
        private string primerApellidoField;
        
        private string primerNombreField;
        
        private respuestaDTO respuestaField;
        
        private string segundoApellidoField;
        
        private string segundoNombreField;
        
        private string tipoIdentificacionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string codigoCiudadExpedicion {
            get {
                return this.codigoCiudadExpedicionField;
            }
            set {
                this.codigoCiudadExpedicionField = value;
                this.RaisePropertyChanged("codigoCiudadExpedicion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string correoElectronico {
            get {
                return this.correoElectronicoField;
            }
            set {
                this.correoElectronicoField = value;
                this.RaisePropertyChanged("correoElectronico");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string descripcionCiudadExpedicion {
            get {
                return this.descripcionCiudadExpedicionField;
            }
            set {
                this.descripcionCiudadExpedicionField = value;
                this.RaisePropertyChanged("descripcionCiudadExpedicion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string numeroIdentificacion {
            get {
                return this.numeroIdentificacionField;
            }
            set {
                this.numeroIdentificacionField = value;
                this.RaisePropertyChanged("numeroIdentificacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string primerApellido {
            get {
                return this.primerApellidoField;
            }
            set {
                this.primerApellidoField = value;
                this.RaisePropertyChanged("primerApellido");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string primerNombre {
            get {
                return this.primerNombreField;
            }
            set {
                this.primerNombreField = value;
                this.RaisePropertyChanged("primerNombre");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public respuestaDTO respuesta {
            get {
                return this.respuestaField;
            }
            set {
                this.respuestaField = value;
                this.RaisePropertyChanged("respuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string segundoApellido {
            get {
                return this.segundoApellidoField;
            }
            set {
                this.segundoApellidoField = value;
                this.RaisePropertyChanged("segundoApellido");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string segundoNombre {
            get {
                return this.segundoNombreField;
            }
            set {
                this.segundoNombreField = value;
                this.RaisePropertyChanged("segundoNombre");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string tipoIdentificacion {
            get {
                return this.tipoIdentificacionField;
            }
            set {
                this.tipoIdentificacionField = value;
                this.RaisePropertyChanged("tipoIdentificacion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3190.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.convalidaciones.msl.com/")]
    public partial class respuestaDTO : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int codigoRespuestaField;
        
        private bool codigoRespuestaFieldSpecified;
        
        private string mensajeRespuestaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public int codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
                this.RaisePropertyChanged("codigoRespuesta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool codigoRespuestaSpecified {
            get {
                return this.codigoRespuestaFieldSpecified;
            }
            set {
                this.codigoRespuestaFieldSpecified = value;
                this.RaisePropertyChanged("codigoRespuestaSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getWebRegistry", WrapperNamespace="http://ws.convalidaciones.msl.com/", IsWrapped=true)]
    public partial class getWebRegistryRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.convalidaciones.msl.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CorreoElectronico;
        
        public getWebRegistryRequest() {
        }
        
        public getWebRegistryRequest(string CorreoElectronico) {
            this.CorreoElectronico = CorreoElectronico;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getWebRegistryResponse", WrapperNamespace="http://ws.convalidaciones.msl.com/", IsWrapped=true)]
    public partial class getWebRegistryResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.convalidaciones.msl.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ConvalidacionEducacionSuperior.AutenticacionLogin.consultaRespuestaDTO @return;
        
        public getWebRegistryResponse() {
        }
        
        public getWebRegistryResponse(ConvalidacionEducacionSuperior.AutenticacionLogin.consultaRespuestaDTO @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ConsultaWSWebRegistryChannel : ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ConsultaWSWebRegistryClient : System.ServiceModel.ClientBase<ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry>, ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry {
        
        public ConsultaWSWebRegistryClient() {
        }
        
        public ConsultaWSWebRegistryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ConsultaWSWebRegistryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaWSWebRegistryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ConsultaWSWebRegistryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry.getWebRegistry(ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest request) {
            return base.Channel.getWebRegistry(request);
        }
        
        public ConvalidacionEducacionSuperior.AutenticacionLogin.consultaRespuestaDTO getWebRegistry(string CorreoElectronico) {
            ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest inValue = new ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest();
            inValue.CorreoElectronico = CorreoElectronico;
            ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse retVal = ((ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry)(this)).getWebRegistry(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse> ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry.getWebRegistryAsync(ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest request) {
            return base.Channel.getWebRegistryAsync(request);
        }
        
        public System.Threading.Tasks.Task<ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryResponse> getWebRegistryAsync(string CorreoElectronico) {
            ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest inValue = new ConvalidacionEducacionSuperior.AutenticacionLogin.getWebRegistryRequest();
            inValue.CorreoElectronico = CorreoElectronico;
            return ((ConvalidacionEducacionSuperior.AutenticacionLogin.ConsultaWSWebRegistry)(this)).getWebRegistryAsync(inValue);
        }
    }
}
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMShound.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int SegundosEntreCiclos {
            get {
                return ((int)(this["SegundosEntreCiclos"]));
            }
            set {
                this["SegundosEntreCiclos"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MensajesProcesados.csv")]
        public string FicheroSalida {
            get {
                return ((string)(this["FicheroSalida"]));
            }
            set {
                this["FicheroSalida"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("debug.txt")]
        public string FicheroError {
            get {
                return ((string)(this["FicheroError"]));
            }
            set {
                this["FicheroError"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("enlaces.txt")]
        public string FicheroEnlaces {
            get {
                return ((string)(this["FicheroEnlaces"]));
            }
            set {
                this["FicheroEnlaces"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("10")]
        public int MaximoMensajesRepetidosHastaFin {
            get {
                return ((int)(this["MaximoMensajesRepetidosHastaFin"]));
            }
            set {
                this["MaximoMensajesRepetidosHastaFin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SMTPservidor {
            get {
                return ((string)(this["SMTPservidor"]));
            }
            set {
                this["SMTPservidor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("25")]
        public int SMTPpuerto {
            get {
                return ((int)(this["SMTPpuerto"]));
            }
            set {
                this["SMTPpuerto"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SMTPusarSSL {
            get {
                return ((bool)(this["SMTPusarSSL"]));
            }
            set {
                this["SMTPusarSSL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SMTPusuario {
            get {
                return ((string)(this["SMTPusuario"]));
            }
            set {
                this["SMTPusuario"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SMTPclave {
            get {
                return ((string)(this["SMTPclave"]));
            }
            set {
                this["SMTPclave"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("palabras_clave.txt")]
        public string FicheroPalabrasClave {
            get {
                return ((string)(this["FicheroPalabrasClave"]));
            }
            set {
                this["FicheroPalabrasClave"] = value;
            }
        }
    }
}
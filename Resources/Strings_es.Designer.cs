﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BattleShips.Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings_es {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings_es() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BattleShips.Resources.Strings_es", typeof(Strings_es).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Vamos a... BattleShipearrrrr.
        /// </summary>
        internal static string lets_battleships {
            get {
                return ResourceManager.GetString("lets_battleships", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Bienvenido a BattleShips!.
        /// </summary>
        internal static string welcome_message1 {
            get {
                return ResourceManager.GetString("welcome_message1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Pulsa ENTER para empezar....
        /// </summary>
        internal static string welcome_message2 {
            get {
                return ResourceManager.GetString("welcome_message2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Pulsa ESC para salir....
        /// </summary>
        internal static string welcome_message3 {
            get {
                return ResourceManager.GetString("welcome_message3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a HAS PERDIDO!!.
        /// </summary>
        internal static string you_loose_message {
            get {
                return ResourceManager.GetString("you_loose_message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a HAS GANADO!!.
        /// </summary>
        internal static string you_win_message {
            get {
                return ResourceManager.GetString("you_win_message", resourceCulture);
            }
        }
    }
}

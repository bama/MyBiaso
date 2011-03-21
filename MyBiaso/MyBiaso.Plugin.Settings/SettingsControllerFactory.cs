using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBiaso.Core.Setting.Controller;

namespace MyBiaso.Plugin.Settings {
    
    /// <summary>
    /// Fabrik für den Controller
    /// </summary>
    public class SettingsControllerFactory:ISettingsControllerFactory {
        
        
        /// <summary>
        /// Erzeugt den Controller
        /// </summary>
        /// <returns>Controller</returns>
        public ISettingsController CreateSettingsController() {
            // zurückgeben
            return new SettingsController();
        }
    }
}

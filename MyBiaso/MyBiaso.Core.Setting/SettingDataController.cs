using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Setting.DataStore;

namespace MyBiaso.Core.Setting {

    
    /// <summary>
    /// Controller für die Daten 
    /// </summary>
    public class SettingDataController {

        /// <summary>
        /// Kern der Anwendung
        /// </summary>
        private ICoreInterface core;

        /// <summary>
        /// Lädt den Controller
        /// </summary>
        /// <param name="coreInterface">Core</param>
        /// <param name="programPath">Pfad</param>
        public void Load(ICoreInterface coreInterface, string programPath) {
            core = coreInterface;

            // Dao initialisieren
            DaoFactory.Initialize(coreInterface.DatabaseConnection);
            // Einstellungsklasse registrieren
            coreInterface.DatabaseConnection.AddMappingClass(typeof(Model.Setting));


        }

    }
}

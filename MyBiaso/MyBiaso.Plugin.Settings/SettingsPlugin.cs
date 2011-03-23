using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Setting;

namespace MyBiaso.Plugin.Settings {

    /// <summary>
    /// Plugin für die Einstellungen
    /// </summary>
    public class SettingsPlugin:IPlugin {

        /// <summary>
        /// Controller für den Zugriff
        /// </summary>
        private readonly SettingDataController controller = new SettingDataController();

        /// <summary>
        /// Lädt das Plugin
        /// </summary>
        /// <param name="ci">Schnittstelle zum Kern</param>
        /// <param name="programPath">Programmpfad</param>
        public void Load(ICoreInterface ci, string programPath) {
            // Fabrik erstellen
            SettingFactories.SettingsControllerFactory = new SettingsControllerFactory();
            SettingFactories.SettingsViewFactory = new SettingsViewFactory();
            ci.FactoryManager.RegisterFactory(SettingFactories.SettingsControllerFactory);
            controller.Load(ci, programPath);
        }

        /// <summary>
        /// Führt ein Unload des Plugins aus
        /// </summary>
        public void Unload() {
            
        }

        /// <summary>
        /// Überschrift
        /// </summary>
        /// <returns>Überschrift</returns>
        public string GetCaption() {
            return "Einstellungen";
        }
    }
}

using AiFrame.InterfaceLib.Plugins;

namespace MyBiaso.Core.Setting.Controller {
    
    /// <summary>
    /// Factory für den Controller der Einstellungen.
    /// </summary>
    public interface ISettingsControllerFactory:IFactory {

        /// <summary>
        /// Erzeugt den Controller für die Einstellungen.
        /// </summary>
        /// <returns>Controller</returns>
        ISettingsController CreateSettingsController();

    }
}

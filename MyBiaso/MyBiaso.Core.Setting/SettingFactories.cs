using MyBiaso.Core.Setting.Controller;

namespace MyBiaso.Core.Setting {

    /// <summary>
    /// Zugriff auf die Fabriken der Einstellungen
    /// </summary>
    public class SettingFactories {

        /// <summary>
        /// SettingsControllerFactory
        /// </summary>
        public static ISettingsControllerFactory SettingsControllerFactory { get; set; }

    }
}

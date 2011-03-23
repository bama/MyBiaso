using MyBiaso.Core.Setting.Controller;
using MyBiaso.Core.Setting.Views;

namespace MyBiaso.Core.Setting {

    /// <summary>
    /// Zugriff auf die Fabriken der Einstellungen
    /// </summary>
    public class SettingFactories {

        /// <summary>
        /// SettingsControllerFactory
        /// </summary>
        public static ISettingsControllerFactory SettingsControllerFactory { get; set; }
        /// <summary>
        /// SettingsViewFactory
        /// </summary>
        public static ISettingsViewFactory SettingsViewFactory { get; set; }

    }
}

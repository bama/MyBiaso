using System.Collections.Generic;

namespace MyBiaso.Core.Setting.Controller {

    /// <summary>
    /// Controller
    /// </summary>
    public interface ISettingsController {

        /// <summary>
        /// Liefert die Einstellung mit dem angegebenen Schlüssel.
        /// </summary>
        /// <param name="key">Schlüssel</param>
        /// <returns>Einstellung</returns>
        Model.Setting GetSetting(string key);

        /// <summary>
        /// Speichert die übergebene Einstellung
        /// </summary>
        /// <param name="setting">Einstellung</param>
        void StoreSetting(Model.Setting setting);
    }
}

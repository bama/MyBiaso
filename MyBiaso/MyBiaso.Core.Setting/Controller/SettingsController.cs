using System;
using MyBiaso.Core.Setting.DataStore;
using NHibernate;

namespace MyBiaso.Core.Setting.Controller {
    
    /// <summary>
    /// ViewModel für die Einstellungen
    /// </summary>
    public class SettingsController:ISettingsController {

        /// <summary>
        /// Bestimmt die Einstellung mit dem angegebenen Schlüssel.
        /// </summary>
        /// <param name="key">Schlüssel</param>
        /// <returns>Einstellung mit dem Schlüssel</returns>
        public Model.Setting GetSetting(string key) {
            if(String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
            // suchen und zurückgeben   
            var setting = DaoFactory.Instance.SettingStore.GetSettingById(key);
            // prüfen (wenn nicht vorhanden -> null zurück)
            return setting ?? new Model.Setting {Key = key};
        }

        /// <summary>
        /// Speichert die übergebene Einstellung.
        /// </summary>
        /// <param name="setting">Einstellung zum Speichern</param>
        public void StoreSetting(Model.Setting setting) {
            if(null == setting) throw new ArgumentNullException("setting");
            // speichern
            DaoFactory.Instance.SettingStore.SaveOrUpdate(setting);
        }

    }
}

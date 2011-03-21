using System;
using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Setting.DataStore {

    /// <summary>
    /// Stellt den Speicher für die Einstellungen dar
    /// </summary>
    public interface ISettingStore:IBasicDao<Model.Setting, String> {

        /// <summary>
        /// Bestimmt eine Einstellung anhand des Schlüssels.
        /// </summary>
        /// <param name="key">Schlüssel</param>
        /// <returns>Einstellung, null wenn nicht vorhanden</returns>
        Model.Setting GetSettingById(string key);
    }
}

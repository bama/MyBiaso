using System;
using AiFrame.InterfaceLib.Data.Access.NHibernateAccess;
using NHibernate;

namespace MyBiaso.Core.Setting.DataStore {
    /// <summary>
    /// Speicher für die Einstellungen
    /// </summary>
    public class NHibernateSettingStore:BasicDaoNHibernate<Model.Setting, String>, ISettingStore {

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="session">Session</param>
        public NHibernateSettingStore(ISession session):base(session) {}


        /// <summary>
        /// Bestimmt eine Einstellung anhand des Schlüssels.
        /// </summary>
        /// <param name="key">Schlüssel</param>
        /// <returns>Einstellung, null wenn nicht vorhanden</returns>
        public Model.Setting GetSettingById(string key) {
            var query = _session.CreateQuery("from Setting s where s.Key = :searchKey");
            query.SetString("searchKey", key);

            var settings = query.List<Model.Setting>();
            return (settings.Count > 0 ? settings[0] : null);
        }
    }
}

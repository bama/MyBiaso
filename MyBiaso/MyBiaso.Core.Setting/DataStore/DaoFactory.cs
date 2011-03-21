using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Setting.DataStore {

    /// <summary>
    /// Fabrik
    /// </summary>
    public class DaoFactory:BasicDaoFactory<DaoFactory> {

        /// <summary>
        /// Speicher für die Einstellungen
        /// </summary>
        public ISettingStore SettingStore {
            get { return new NHibernateSettingStore(DatabaseConnection.GetSession()); }
        }

        
        
            
    }

    
}

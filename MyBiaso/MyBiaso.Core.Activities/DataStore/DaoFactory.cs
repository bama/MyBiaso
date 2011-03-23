using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Activities.DataStore {
    
    /// <summary>
    /// Fabrik zur Erstellung des Datenzugriffs
    /// </summary>
    public class DaoFactory:BasicDaoFactory<DaoFactory> {

        /// <summary>
        /// Zugriff auf den Speicher
        /// </summary>
        public IActivitiesStore ActivitiesStore {
            get { return new NHibernateActivitiesStore(DatabaseConnection.GetSession()); }
        }

        /// <summary>
        /// Zugriff auf die Kunden  
        /// </summary>
        public ICustomerStore CustomerStore {
            get { return new NHibernateCustomerStore(DatabaseConnection.GetSession()); }
        }
    }


}

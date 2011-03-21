using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Customer.DataStore {
    
    /// <summary>
    /// Fabrik zur Erstellung des Dao
    /// </summary>
    public class DaoFactory:BasicDaoFactory<DaoFactory> {

        /// <summary>
        /// Zugriff auf den Kundenspeicher
        /// </summary>
        public ICustomerStore CustomerStore {
            get { return new NHibernateCustomerStore(DatabaseConnection.GetSession()); }
        }

    }
}

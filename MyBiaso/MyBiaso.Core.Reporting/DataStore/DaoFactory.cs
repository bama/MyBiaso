using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Reporting.DataStore {
    public class DaoFactory:BasicDaoFactory<DaoFactory> {

        public IActivitiesStore ActivitiesStore {
            get { return new NHibernateActivitiesStore(DatabaseConnection.GetSession()); }
        }


    }
}

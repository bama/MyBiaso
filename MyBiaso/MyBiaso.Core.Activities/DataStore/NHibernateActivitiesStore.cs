using System;
using AiFrame.InterfaceLib.Data.Access.NHibernateAccess;
using NHibernate;

namespace MyBiaso.Core.Activities.DataStore {
    /// <summary>
    /// Zugriff per NHibernate auf die Aktivitäten
    /// </summary>
    public class NHibernateActivitiesStore:BasicDaoNHibernate<Model.HomeVisit, Guid>, IActivitiesStore {

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="session">Session</param>
        public NHibernateActivitiesStore(ISession session):base(session){}
    }
}

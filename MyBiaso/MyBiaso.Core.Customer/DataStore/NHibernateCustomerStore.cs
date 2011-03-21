using System;
using AiFrame.InterfaceLib.Data.Access.NHibernateAccess;
using NHibernate;

namespace MyBiaso.Core.Customer.DataStore {
    
    /// <summary>
    /// Stellt den Zugriff auf den Kundenspeicher per NHibernate dar.
    /// </summary>
    public class NHibernateCustomerStore:BasicDaoNHibernate<Model.Customer, Guid>, ICustomerStore {
        
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="session">Session</param>
        public NHibernateCustomerStore(ISession session) : base(session) {}
    }
}

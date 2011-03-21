using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.InterfaceLib.Data.Access.NHibernateAccess;
using MyBiaso.Core.Model;
using NHibernate;

namespace MyBiaso.Core.Activities.DataStore {

    public class NHibernateCustomerStore:BasicDaoNHibernate<Customer, Guid>, ICustomerStore {
        
        public NHibernateCustomerStore(ISession session) : base(session) {}
    }
}

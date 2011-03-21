using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.InterfaceLib.Data.Access;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities.DataStore {
    
    public interface ICustomerStore:IBasicDao<Customer, Guid> {
    }
}

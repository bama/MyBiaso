using System;
using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Customer.DataStore {
    
    /// <summary>
    /// Definiert den Speicher für Customer
    /// </summary>
    public interface ICustomerStore:IBasicDao<Model.Customer, Guid> {
    }
}

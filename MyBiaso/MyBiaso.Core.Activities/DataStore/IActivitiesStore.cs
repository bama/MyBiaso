using System;
using AiFrame.InterfaceLib.Data.Access;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities.DataStore {
    
    /// <summary>
    /// Auslesen der Aktivitäten
    /// </summary>
    public interface IActivitiesStore:IBasicDao<HomeVisit, Guid> {
    }
}

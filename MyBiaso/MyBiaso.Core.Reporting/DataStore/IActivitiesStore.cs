using System;
using AiFrame.InterfaceLib.Data.Access;

namespace MyBiaso.Core.Reporting.DataStore {
    
    /// <summary>
    /// Schnittstelle für das Auslesen der Aktivitäten.
    /// </summary>
    public interface IActivitiesStore:IBasicDao<Model.HomeVisit, Guid> {
    }
}

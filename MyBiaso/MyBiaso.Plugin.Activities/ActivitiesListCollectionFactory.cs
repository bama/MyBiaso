using System.Collections.ObjectModel;
using System.ComponentModel;
using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Model;

namespace MyBiaso.Plugin.Activities {
    
    /// <summary>
    /// Fabrik für die Collection der Aktivitäten.
    /// </summary>
    public class ActivitiesListCollectionFactory:IActivitiesCollectionFactory {
        
        
        public Collection<HomeVisit> CreateBindableActivitiesCollection() {
            return new BindingList<HomeVisit>();
        }
    }
}

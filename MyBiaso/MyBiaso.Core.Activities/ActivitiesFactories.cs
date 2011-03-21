using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Activities.Views;

namespace MyBiaso.Core.Activities {
    
    /// <summary>
    /// Fabriken für Aktivitäten
    /// </summary>
    public class ActivitiesFactories {


        /// <summary>
        /// Factory für die Aktivitäten
        /// </summary>
        public static IActivitiesCollectionFactory ActivitiesCollectionFactory {
            get; set;
        }

        /// <summary>
        /// Factory für die Listenansicht
        /// </summary>
        public static IActivitiesViewFactory ActivitiesViewFactory {
            get; set;
        }
    }
}

namespace MyBiaso.Core.Activities.Views {
    
    /// <summary>
    /// Fabrik zur Erstellung einer ListView
    /// </summary>
    public interface IActivitiesViewFactory {

        /// <summary>
        /// Erzeugt die View
        /// </summary>
        IActivitiesListView CreateListView();

        /// <summary>
        /// Erzeugh die View zur Darstellung einer einzelnen Datenansicht
        /// </summary>
        /// <returns>Datenansicht</returns>
        IHomeVisitDataView CreateHomeVisitDataView();
    }
}

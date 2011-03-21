using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Activities.ViewModel;

namespace MyBiaso.Core.Activities.Views {

    /// <summary>
    /// Ansicht für eine Liste von Aktivitäten
    /// </summary>
    public interface IActivitiesListView:AiFrame.InterfaceLib.MVVM.IDataListView<Model.HomeVisit>, IWindow {

        /// <summary>
        /// Bindet die View an das Model
        /// </summary>
        /// <param name="activitiesListViewModel">ViewModel</param>
        void BindToViewModel(ActivitiesListViewModel activitiesListViewModel);

        /// <summary>
        /// Liefert das Model zurück,
        /// </summary>
        /// <returns>Model</returns>
        ActivitiesListViewModel  GetModel();
    }
}

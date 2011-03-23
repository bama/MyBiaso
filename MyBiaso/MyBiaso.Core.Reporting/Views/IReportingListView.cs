using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Reporting.ViewModel;

namespace MyBiaso.Core.Reporting.Views {

    /// <summary>
    /// View für die Listendarstellung der Report-Variablen.
    /// </summary>
    public interface IReportingListView : AiFrame.InterfaceLib.MVVM.IDataListView<Model.ReportingValues>, IWindow {

        /// <summary>
        /// Bindet die View an das ViewModel.
        /// </summary>
        /// <param name="model">ViewModel</param>
        void BindToViewModel(ReportingListViewModel model);

    }
}

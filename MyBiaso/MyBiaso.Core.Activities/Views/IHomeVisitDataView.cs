using AiFrame.InterfaceLib.MVP;
using AiFrame.InterfaceLib.MVVM;
using MyBiaso.Core.Activities.ViewModel;

namespace MyBiaso.Core.Activities.Views {
    
    /// <summary>
    /// Datenfenster für Hausbesuch
    /// </summary>
    public interface IHomeVisitDataView:IAbstractDataView, IInformUser, IErrorDisplayable {

        void BindToViewModel(HomeVisitDataViewModel model);

    }
}

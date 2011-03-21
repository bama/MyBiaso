using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Customer.ViewModel;

namespace MyBiaso.Core.Customer.Views {
    
    /// <summary>
    /// Definition der Darstellung eines Kunden
    /// </summary>
    public interface ICustomerDataView: IWindow, IInformUser, IErrorDisplayable {

        /// <summary>
        /// Bindet die View an das Model
        /// </summary>
        /// <param name="model"></param>
        void BindViewToModel(CustomerDataViewModel model);

        /// <summary>
        /// Liefert das Model an das die View gebunden ist
        /// </summary>
        /// <returns></returns>
        CustomerDataViewModel GetModel();
        
        /// <summary>
        /// Container
        /// </summary>
        ISubWindowContainer<Model.Customer> SubWindowContainer { get; }

        /// <summary>
        /// Setzt die Datenquelle zurück.
        /// </summary>
        void ResetCustomersDataSource();

        /// <summary>
        /// Öffnet den Editiermodus. In diesem Modus sind alle Textfelder editierbar.
        /// </summary>
        void OpenEditMode();

        /// <summary>
        /// Beendet den Editiermodus. In diesem Modus sind alle Textfelder geschlossen.
        /// </summary>
        void CloseEditMode();

        /// <summary>
        /// Erlaubt die Editierung eines Kunden.
        /// </summary>
        void AllowEditMode();

        /// <summary>
        /// Verweigert die Editierung eines Kunden.
        /// </summary>
        void DisallowEditMode();

    }
}

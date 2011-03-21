namespace MyBiaso.Core.Customer.Views {

    /// <summary>
    /// Fabrik zur Erstellung einer CustomerDataView
    /// </summary>
    public interface ICustomerDataViewFactory {

        /// <summary>
        /// Erzeugt eine neue View.
        /// </summary>
        /// <returns>Neu erzeugte View</returns>
        ICustomerDataView CreateCustomerView();
    }
}

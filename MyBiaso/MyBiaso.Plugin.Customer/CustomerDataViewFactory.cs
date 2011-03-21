using MyBiaso.Core.Customer.Views;
using MyBiaso.Plugin.Customer.Window;

namespace MyBiaso.Plugin.Customer {

    /// <summary>
    /// Fabrik für die View zur Anzeige eines Kunden.
    /// </summary>
    public class CustomerDataViewFactory:ICustomerDataViewFactory {

        /// <summary>
        /// Erzeugt die View.
        /// </summary>
        /// <returns>View auf die Kundendaten</returns>
        public ICustomerDataView CreateCustomerView() {
            // Frame verwenden
            return new CustomerFrame();
        }
    }
}

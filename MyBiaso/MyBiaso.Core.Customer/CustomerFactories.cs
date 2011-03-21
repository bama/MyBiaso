using MyBiaso.Core.Customer.Views;

namespace MyBiaso.Core.Customer {

    /// <summary>
    /// Zugriff auf die Fabriken.
    /// </summary>
    public class CustomerFactories {

        /// <summary>
        /// Fabrik auf die View 
        /// </summary>
        public static ICustomerDataViewFactory CustomerDataViewFactory { get; set; }
    }
}

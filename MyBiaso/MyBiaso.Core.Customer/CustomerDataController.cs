using System;
using System.Drawing;
using System.Resources;
using AiFrame.InterfaceLib.Plugins;
using AiFrame.InterfaceLib.Resources;
using AiFrame.InterfaceLib.Windows.Controls;
using MyBiaso.Core.Customer.DataStore;
using MyBiaso.Core.Customer.ViewModel;
using MyBiaso.Core.Customer.Views;

namespace MyBiaso.Core.Customer {
    
    /// <summary>
    /// Controller für die Kundendaten
    /// </summary>
    public class CustomerDataController {

        private ICoreInterface _ci;

        public void Load(ICoreInterface ci, string programPath) {
            // Save the CoreInterface
            _ci = ci;

            // Initialize DaoFactory.
            DaoFactory.Initialize(ci.DatabaseConnection);

            // Initialize the registry in order to save the CoreInterface and program path
            // there.
            CustomerDataRegistry.Instance.Initialize(ci, programPath);

            // Register the customer class.
            ci.DatabaseConnection.AddMappingClass(typeof(Model.Customer));

            ResourceImages resourceImages = new ResourceImages();

            ResourceManager resourceManager = new ResourceManager("MyBiaso.Core.Customer.Properties.Resources", this.GetType().Assembly);

            // Create a new data navigation bar.
            var dataNavigationBar = ci.NavigationBar.CreateDataNavigationBar();
            dataNavigationBar.AddButton("newCustomerButton", "Neuen Kunden hinzufügen", resourceImages.DatasetAdd, null, 
                delegate {
                    ci.WindowManager.BringWindowToFront<ICustomerDataView>();
                    var view = ((ICustomerDataView) ci.WindowManager.ActiveWindow);
                    view.GetModel().UserWantsToAddCustomer();
                });
            
            ci.NavigationBar.AddButton("customerDataButton", "Kundenverwaltung", null, (Image)resourceManager.GetObject("customers"), dataNavigationBar, OnNavBarButtonClick);

            // Register menu items.
            //object menuItem = ci.MainMenu.AddMainMenuItem("Customers", "customerDataMenuItem", "Customers",
            //                                              (object)null, null);
            //ci.MainMenu.AddMainMenuItem("Add new customer...", "newCustomerMenuItem", "Add new customer", menuItem,
            //                            OnNewCustomerMenuItemClick);


        }

        /// <summary>
        /// Wird ausge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewCustomerMenuItemClick(object sender, EventArgs e) {
            
        }

        private void OnNavBarButtonClick(object sender, EventArgs e) {
            // Check if a customer window exists already    
            if (!_ci.WindowManager.ExistsWindow<ICustomerDataView>()) {
                ICustomerDataView customerView = CustomerFactories.CustomerDataViewFactory.CreateCustomerView();
                CustomerDataViewModel viewModel = new CustomerDataViewModel(customerView);

                _ci.WindowManager.RegisterWindow(customerView);
            } else
                _ci.WindowManager.BringWindowToFront<ICustomerDataView>();
        }

        public void Unload() {
            // TODO: Unregister
            // Do something
        }
    }
}

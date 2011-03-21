using System;
using System.Windows.Forms;
using AiFrame.Interface.WinUI.Windows;
using AiFrame.InterfaceLib.MVP;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Customer;
using MyBiaso.Core.Customer.ViewModel;
using MyBiaso.Core.Customer.Views;

namespace MyBiaso.Plugin.Customer.Window {
    /// <summary>
    /// Darstellung eines Kunden
    /// </summary>
    public sealed partial class CustomerFrame : UserControl, ICustomerDataView {
        #region Felder
        /// <summary>
        /// Container
        /// </summary>
        private ISubWindowContainer<Core.Model.Customer> subWindowContainer;
        /// <summary>
        /// Model der View
        /// </summary>
        private CustomerDataViewModel viewModel;
        /// <summary>
        /// Quelle für die Bindungen
        /// </summary>
        private BindingSource bindingSource;
        /// <summary>
        /// Provider für Fehler
        /// </summary>
        private ErrorProvider errorProvider;

        /// <summary>
        /// Buttons für den Toolstrip zur Editierung
        /// </summary>
        private ToolStripButton saveButton, openButton, cancelButton, deleteButton;
        #endregion

        /// <summary>
        /// Konstruktor
        /// </summary>
        public CustomerFrame() {
            InitializeComponent();
            Dock = DockStyle.Fill;
            bindingSource = new BindingSource();

            errorProvider = new ErrorProvider {
                                                  ContainerControl = this,
                                                  BlinkStyle = ErrorBlinkStyle.NeverBlink,
                                                  DataSource = bindingSource
                                              };
        }


        private void ShowToolbar() {
            var resourceImages = new ResourceImages();
            var toolStripButtons = new ToolStrip();

            openButton = new ToolStripButton("Editieren", resourceImages.DatasetOpen, OnToolItemClick) {
                                                                                                           Enabled =
                                                                                                               false,
                                                                                                           Tag = "Edit"
                                                                                                       };
            deleteButton = new ToolStripButton("Löschen", resourceImages.DatasetDelete, OnToolItemClick)
                           {Enabled = false, Tag = "Delete"};
            saveButton = new ToolStripButton("Speichern", null, OnToolItemClick) {Tag = "Save", Enabled = false};
            cancelButton = new ToolStripButton("Abbrechen", null, OnToolItemClick) { Tag="Cancel", Enabled = false};
            

            toolStripButtons.Items.Add(openButton);
            toolStripButtons.Items.Add(deleteButton);
            toolStripButtons.Items.Add(saveButton);
            toolStripButtons.Items.Add(cancelButton);

            CustomerDataRegistry.Instance.CoreInterface.ToolBarManager.AddToolGroup(toolStripButtons, false, 1);
        }

        private void OnToolItemClick(object sender, EventArgs e) {
            switch (((ToolStripItem) sender).Tag.ToString()) {
                case "Save":
                    viewModel.UserWantsToSaveCustomer();
                    break;
                case "Edit":
                    viewModel.UserWantsToEditCustomer();
                    break;
                case "Delete":
                    viewModel.UserWantsToDeleteCustomer();
                    break;
                case "Cancel":
                    viewModel.UserWantsToCancelEdit();
                    break;
            }
        }

        public void RefreshView() {
            Invalidate(true);
        }

        public void SetFree() {
            Dispose();
        }

        public void SetParentWindow(Control parent) {
            Parent = parent;
        }

        public void BringWindowToFront() {
            BringToFront();
            ShowToolbar();
        }

        public string GetCaption() {
            return "Kundendaten";
        }

        /// <summary>
        /// Liefert das Model an das die View gebunden ist
        /// </summary>
        /// <returns>Model</returns>
        public CustomerDataViewModel GetModel() {
            // zurückgeben
            return viewModel;
        }

        public void InformUser(string message) {
            // anzeigen
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DisplayError(string message) {
            // anzeigen
            MessageBox.Show(message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void BindViewToModel(CustomerDataViewModel model) {
            viewModel = model;
            bindingSource.DataSource = viewModel;

            BindingHelper.SetBinding(txtCustomerNumber, "Text", bindingSource, "CustomerNumber", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtLastname, "Text", bindingSource, "Lastname", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtFirstname, "Text", bindingSource, "Firstname", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtStreet, "Text", bindingSource, "Street", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtHousenumber, "Text", bindingSource, "Housenumber", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtCity, "Text", bindingSource, "City", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtZipCode, "Text", bindingSource, "ZipCode", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(txtPhone, "Text", bindingSource, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);

            ResetCustomersDataSource();

            BindingHelper.SetBinding(cmbAllCustomers, "SelectedValue", bindingSource, "Customer", true, DataSourceUpdateMode.OnPropertyChanged);
            // subWindowContainer = new CustomerTabSubWindowContainer(pluginTabControl, viewModel);
            // CustomerDataRegistry.Instance.CoreInterface.PluginManager.InvokeNewDatasetAssignedEvent<Core.Model.Customer>(subWindowContainer);
                
        }

        public ISubWindowContainer<Core.Model.Customer> SubWindowContainer {
            get { return subWindowContainer; }
        }

        public void ResetCustomersDataSource() {
            cmbAllCustomers.DataSource = null;
            cmbAllCustomers.DisplayMember = "Value";
            cmbAllCustomers.ValueMember = "Key";
            cmbAllCustomers.DataSource = viewModel.CustomerListValues;
        }

        /// <summary>
        /// Erlaubt die Editierung eines Kunden.
        /// </summary>
        public void AllowEditMode() {
            // setzen
            deleteButton.Enabled = true;
            openButton.Enabled = true;
        }

        /// <summary>
        /// Verweigert die Editierung eines Kunden.
        /// </summary>
        public void DisallowEditMode() {
            // setzen
            deleteButton.Enabled = false;
            openButton.Enabled = false;
        }

        /// <summary>
        /// Öffnet den Editiermodus. In diesem Modus sind alle Textfelder editierbar.
        /// </summary>
        public void OpenEditMode() {
            // Controls aktivieren
            SetControlsEnable(true);
        }

        /// <summary>
        /// Beendet den Editiermodus. In diesem Modus sind alle Textfelder geschlossen.
        /// </summary>
        public void CloseEditMode() {
            // Controls deaktivieren
            SetControlsEnable(false);
        }

        /// <summary>
        /// Aktiviert/Deaktiviert die Controls für die Editierung.
        /// </summary>
        /// <param name="enabled">True, wenn die Editierung möglich sein soll</param> 
        private void SetControlsEnable(bool enabled) {
            txtCity.Enabled = enabled;
            txtFirstname.Enabled = enabled;
            txtHousenumber.Enabled = enabled;
            txtLastname.Enabled = enabled;
            txtPhone.Enabled = enabled;
            txtStreet.Enabled = enabled;
            txtZipCode.Enabled = enabled;

            // Buttons für die Editierung setzen
            saveButton.Enabled = enabled;
            cancelButton.Enabled = enabled;
            openButton.Enabled = !enabled;
            deleteButton.Enabled = !enabled;
            // Auswahlfeld entsprechend setzen
            cmbAllCustomers.Enabled = !enabled;
        }

    }


    class CustomerTabSubWindowContainer : TabControlSubWindowContainer<Core.Model.Customer> {
        public CustomerTabSubWindowContainer(TabControl tabControl, IDataSourceProvider<Core.Model.Customer> dataSourceProvider)
            : base(tabControl, dataSourceProvider) {
        }

        public override string ID {
            get { return "EDIT_CUSTOMER"; }
        }
    }
}

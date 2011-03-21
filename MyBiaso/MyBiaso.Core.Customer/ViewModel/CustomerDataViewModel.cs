using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Customer.DataStore;
using MyBiaso.Core.Customer.Views;
using MyBiaso.Core.Setting.Controller;

namespace MyBiaso.Core.Customer.ViewModel {

    /// <summary>
    /// Model für die Darstellung eines Kunden
    /// </summary>
    public class CustomerDataViewModel:ViewModelBase, IDataSourceProvider<Model.Customer> {

        #region Felder
        /// <summary>
        /// View
        /// </summary>
        private readonly ICustomerDataView view;
        /// <summary>
        /// Aktueller Kunde
        /// </summary>
        private Model.Customer customer;
        /// <summary>
        /// Liste
        /// </summary>
        private IList list;
        #endregion

        #region Konstruktor
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">View die verwaltet werden soll</param>
        public CustomerDataViewModel(ICustomerDataView view) {
            this.view = view;
            customer = new Model.Customer();
            list = LoadCustomerList();
            view.BindViewToModel(this);

        }
        #endregion

        #region Properties
        /// <summary>
        /// Kundennummer
        /// </summary>
        public virtual string CustomerNumber { 
            get { return customer.CustomerNumber; }
            set { customer.CustomerNumber = HandlePropertySet(value, customer.CustomerNumber, "CustomerNumber"); }
        }
        /// <summary>
        /// Vorname
        /// </summary>
        public virtual string Firstname {
            get { return customer.Firstname;}
            set { customer.Firstname = HandlePropertySet(value, customer.Firstname, "Firstname"); }
        }
        /// <summary>
        /// Nachname
        /// </summary>
        public virtual string Lastname {
            get { return customer.Lastname; }
            set { customer.Lastname = HandlePropertySet(value, customer.Lastname, "Lastname"); }
        }

        /// <summary>
        /// Telefonnummer
        /// </summary>
        public virtual string Phone {
            get { return customer.Phone; }
            set { customer.Phone = HandlePropertySet(value, customer.Phone, "Phone"); }
        }
        /// <summary>
        /// Straße
        /// </summary>
        public virtual string Street {
            get { return customer.Street; }
            set { customer.Street = HandlePropertySet(value, customer.Street, "Street"); }
        }
        /// <summary>
        /// Hausnummer
        /// </summary>
        public virtual string Housenumber {
            get { return customer.Housenumber;  }
            set { customer.Housenumber = HandlePropertySet(value, customer.Housenumber, "Housenumber"); }
        }
        /// <summary>
        /// PLZ
        /// </summary>
        public virtual string ZipCode { get { return customer.ZipCode; }
            set { customer.ZipCode = HandlePropertySet(value, customer.ZipCode, "ZipCode"); }
        }
        /// <summary>
        /// Ort
        /// </summary>
        public virtual string City {
            get { return customer.City; }
            set { customer.City = HandlePropertySet(value, customer.City, "City"); }
        }


        /// <summary>
        /// Aktueller Kunde
        /// </summary>
        public string CurrentCustomerDisplay {
            get {
                return string.Format("Aktueller Kunde: {0}, {1} (Kundennummer: {2}", customer.Lastname,
                                     customer.Firstname, customer.CustomerNumber);
            }
        }

        /// <summary>
        /// Liste mit den Werten
        /// </summary>
        public IList CustomerListValues {
            get {
                if (0 == list.Count)
                    list.Add(new KeyValuePair<Model.Customer, string>(null, "<No dataset available>"));

                return list;
            }
            set {
                list = value;
                OnPropertyChanged("CustomerListValues");
                view.ResetCustomersDataSource();
            }
        }

        /// <summary>
        /// Customer
        /// </summary>
        public Model.Customer Customer {
            get { return customer; }
            set {
                if(null != value) {
                    if (value.Equals(customer)) return;
                    
                    customer = DaoFactory.Instance.CustomerStore.FindByID(value.Id);    
                    view.AllowEditMode();
                } else {
                    view.DisallowEditMode();
                }

                // aktualisieren
                CustomerDataRegistry.Instance.CoreInterface.PluginManager.InvokeNewDatasetAssignedEvent<Model.Customer>(view.SubWindowContainer);
                OnPropertyChanged("Customer");
            }
        }
        #endregion

        #region Methoden
        /// <summary>
        /// Behandelt das Setzen einer Property.
        /// </summary>
        /// <param name="newValue">Neuer Wert</param>
        /// <param name="oldValue">Alter Wert</param>
        /// <param name="propertyName">Name der Property</param>
        /// <returns>Wert der gesetzt werden soll</returns>
        private string HandlePropertySet(string newValue, string oldValue, string propertyName) {
            if(oldValue != newValue) {
                OnPropertyChanged(propertyName);
            }

            return newValue;
        }

        public void SetDataSource(Model.Customer dataSource) {
            customer = dataSource;
        }

        public Model.Customer GetDataSource() {
            return customer;
        }

        private static IList LoadCustomerList() {
            var customerList = DaoFactory.Instance.CustomerStore.FindAll().ToList();
            var loadCustomerList = new ArrayList(customerList.Count);

            foreach (var key in customerList) {
                loadCustomerList.Add(new KeyValuePair<Model.Customer, string>(key,
                                                            string.Format("{0}: {1} {2}", key.CustomerNumber,
                                                                          key.Firstname, key.Lastname)));
            }
            return loadCustomerList;
        }
        #endregion

        /// <summary>
        /// Wird ausgelöst, wenn der Benutzer einen Kunden hinzufügen möchte.
        /// </summary>
        public void UserWantsToAddCustomer() {
            // neues Objekt erstellen
            customer = new Model.Customer();
            // Editiermodus öffnen
            view.OpenEditMode();
            // PropertyChanged ändern
            OnPropertyChanged("Customer");
        }

        /// <summary>
        /// Wird ausgelöst, wenn das Benutzer einen Kunden speichern möchte.
        /// </summary>
        public void UserWantsToSaveCustomer() {
            try {
                // Editierung abbrechen
                view.CloseEditMode();
                // prüfen, ob noch keine Kundennummer (wenn ja erstellen)
                if (String.IsNullOrEmpty(customer.CustomerNumber)) customer.CustomerNumber = GetNextCustomerNumber();
                // speichern
                DaoFactory.Instance.CustomerStore.SaveOrUpdate(customer);
                // neu laden
                CustomerListValues = LoadCustomerList();
            } catch (Exception e) {
                // Fehler zeigen
                view.DisplayError(String.Format("Es ist folgender Fehler aufgetreten: {0}", e.Message));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn der Benutzer einen Kunden bearbeiten möchte.
        /// </summary>
        public void UserWantsToEditCustomer() {
            try {
                view.OpenEditMode();
            } catch (Exception e) {
                // Fehler zeigen
                view.DisplayError(String.Format("Es ist folgender Fehler aufgetreten: {0}", e.Message));
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn der Benutzer einen Kunden löschen möchte.
        /// </summary>
        public void UserWantsToDeleteCustomer() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wird ausgelöst, wenn der Benutzer die Editierung abbricht.
        /// </summary>
        public void UserWantsToCancelEdit() {
            // abbrechen der Editierung
            view.CloseEditMode();
            // prüfen, ob kein neuer Kunde
            if(Guid.Empty.Equals(customer.Id)) {
                // Abbruch einer Neuerzeugung -> auf null setzen
                customer = null;
                // sperren der Editierung
                view.DisallowEditMode();
            } else {
                // und neu laden
                DaoFactory.Instance.CustomerStore.Refresh(customer);
            }
            // View aktualisieren
            OnPropertyChanged("Customer");
        }

        /// <summary>
        /// Bestimmt die nächste Kundennummer für den aktuellen Kunden.
        /// </summary>
        /// <returns>Kundennummer</returns>
        private static string GetNextCustomerNumber() {
            // Controller für die Einstellungen bestimmen
            var settingsController = CustomerDataRegistry.Instance.CoreInterface.FactoryManager.GetFactory<ISettingsControllerFactory>().CreateSettingsController();
            // Einstellung auslesen
            var setting = settingsController.GetSetting("numbers.customer.customer_number");
            // vorbereiten
            int current;
            // Wert prüfen
            int.TryParse(setting.Value, out current);
            // erhöhen
            current++;
            // setzen 
            var customerNumber = string.Format("C{0}", current.ToString().PadLeft(6, '0'));
            // aktuellen Wert übernehmen
            setting.Value = current.ToString();
            // speichern
            settingsController.StoreSetting(setting);
            // zurückgeben
            return customerNumber;
        }
    }
}

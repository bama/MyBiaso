using System;
using System.Collections;
using System.Collections.Generic;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Activities.Views;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities.ViewModel {

    /// <summary>
    /// Model für die Darstellung von Aktivitäten.
    /// </summary>
    public class HomeVisitDataViewModel: ViewModelBase {
        
        #region Felder
        /// <summary>
        /// View
        /// </summary>
        private IHomeVisitDataView view;
        /// <summary>
        /// Hausbesuch zur Bearbeitung
        /// </summary>
        private HomeVisit homeVisit;
        /// <summary>
        /// Liste der Kunden
        /// </summary>
        private IList customerList;

        #endregion

        #region Konstruktor
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">View</param>
        public HomeVisitDataViewModel(IHomeVisitDataView view) {
            this.view = view;
            homeVisit = new HomeVisit {Begin = DateTime.Now, End = DateTime.Now};
            customerList = LoadCustomerList();
            view.BindToViewModel(this);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Beginn
        /// </summary>
        public virtual DateTime Begin {
            get { return homeVisit.Begin; }
            set { homeVisit.Begin = HandlePropertySet(value, homeVisit.Begin, "Begin"); }
        }

        /// <summary>
        /// Ende
        /// </summary>
        public virtual  DateTime End {
            get { return homeVisit.End; }
            set { homeVisit.End = HandlePropertySet(value, homeVisit.End, "End"); }
        }

        /// <summary>
        /// Nach Besuch nach Hause gefahren
        /// </summary>
        public virtual bool DrivenHome {
            get { return homeVisit.DrivenHome; }
            set { homeVisit.DrivenHome = HandlePropertySet(value, homeVisit.DrivenHome, "DrivenHome"); }
        }

        /// <summary>
        /// Kunde
        /// </summary>
        public virtual Customer Customer {
            get { return homeVisit.Customer; }
            set { homeVisit.Customer = HandlePropertySet(value, homeVisit.Customer, "Customer"); }
        }

        /// <summary>
        /// Aktivität/Hausbesuch zur Bearbeitung
        /// </summary>
        public virtual  HomeVisit HomeVisit {
            get { return homeVisit; }
            set { homeVisit = value; }
        }

        /// <summary>
        /// Liste der Kunden
        /// </summary>
        public IList CustomerListValues {
            get {
                if (customerList.Count == 0)
                    customerList.Add(new KeyValuePair<Customer, string>(null, "<No dataset available>"));

                return customerList;
            }
            set {
                customerList = value;
                OnPropertyChanged("CustomerListValues");
            }
        }

        #endregion

        #region Methoden

        private T HandlePropertySet<T>(T newValue, T oldValue, string property) where T:IEquatable<T>  {
            if(newValue is ValueType) {
                if(!oldValue.Equals(newValue))
                    OnPropertyChanged(property);
            } else {
                if(newValue != null) {
                    if (!newValue.Equals(oldValue))
                        OnPropertyChanged(property);
                } else if (oldValue != null) {
                    OnPropertyChanged(property);
                }
            }

            return newValue;
        }

        public void SetDataSource(HomeVisit visit) {
            homeVisit = visit;
        }

        public HomeVisit GetDataSource() {
            return homeVisit;
        }

        private static IList LoadCustomerList() {
            var customerList = DaoFactory.Instance.CustomerStore.FindAll();
            var loadCustomerList = new ArrayList(customerList.Count);

            foreach (var customer in customerList) {
                loadCustomerList.Add(new KeyValuePair<Customer, string>(customer,
                    string.Format("{0}: {1} {2}", customer.CustomerNumber,
                                                                          customer.Firstname, customer.Lastname)));   
            }

            return loadCustomerList;
        }
        #endregion

        public void UserWantsToSave() {
            DaoFactory.Instance.ActivitiesStore.SaveOrUpdate(homeVisit);
            view.Close();
        }

        public void UserWantsToCancel() {
            if(!Guid.Empty.Equals(homeVisit.Id))
                DaoFactory.Instance.ActivitiesStore.Refresh(homeVisit);

            view.Close();
        }
    }
}

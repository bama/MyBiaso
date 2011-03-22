using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Activities.Views;
using MyBiaso.Core.DistanceCalculation;
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
        private readonly IHomeVisitDataView view;
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

            var calc = new Calculation();

            if (null != homeVisit.Customer) {
                homeVisit.DistanceTravelled = calc.CalculateDistanceTravelled(homeVisit, GetHomeVisitBefore(homeVisit));
            }

            DaoFactory.Instance.ActivitiesStore.SaveOrUpdate(homeVisit);

            // eventuell muss neu berechnet werden -> auslösen
            RecalculateDistances(homeVisit);

            view.Close();
        }

        public void UserWantsToCancel() {
            if(!Guid.Empty.Equals(homeVisit.Id))
                DaoFactory.Instance.ActivitiesStore.Refresh(homeVisit);

            view.Close();
        }

        /// <summary>
        /// Führt eine Neuberechnung der zurückgelegten Distanzen durch.
        /// </summary>
        /// <param name="triggeredBy">Hausbesuch, der die Neuberechnung ausgelöst hat</param>
        private static void RecalculateDistances(HomeVisit triggeredBy) {
            IList<HomeVisit> activities = new List<HomeVisit>();
            // abfragen
            var daoActivities = DaoFactory.Instance.ActivitiesStore.FindAll();
            // absteigend sortieren, damit die letzte Aktivität oben steht
            Array.ForEach(daoActivities.OrderBy(a => a.Begin).ToArray(), activities.Add);

            // Index bestimmen
            var index = daoActivities.IndexOf(triggeredBy);
            // weiterlaufen
            if((index != -1) && (index >= 0) && (index + 1 < daoActivities.Count)) {
                // Es muss immer nur der nächste Hausbesuch mit berechnet werden
                var nextVisit = daoActivities[index + 1];
                if ((null != nextVisit.Customer) && (null != triggeredBy.Customer)) {
                    // berechnen
                    nextVisit.DistanceTravelled = (new Calculation()).CalculateDistanceTravelled(nextVisit, triggeredBy);
                    // speichern
                    DaoFactory.Instance.ActivitiesStore.SaveOrUpdate(nextVisit);
                }
            }
        }

        /// <summary>
        /// Bestimmt den Hausbesuch, der zuvor durchgeführt wurde.
        /// </summary>
        /// <param name="visit">Hausbesuch</param>
        /// <returns>Hausbesuch der zuvor durchgeführt wurde</returns>
        private static HomeVisit GetHomeVisitBefore(HomeVisit visit) {
            IList<HomeVisit> activities = new List<HomeVisit>();
            // abfragen
            var daoActivities = DaoFactory.Instance.ActivitiesStore.FindAll();
            // prüfen ob enthalten
            if(!daoActivities.Contains(visit)) {
                // nicht enthalten -> einfügen
                daoActivities.Add(visit);
            }
            // absteigend sortieren, damit die letzte Aktivität oben steht
            Array.ForEach(daoActivities.OrderBy(a => a.Begin).Reverse().ToArray(), activities.Add);

            // suchen nach der Position der übergebenen Aktivität
            var indexBefore = activities.IndexOf(visit) + 1;
            // prüfen ob vorhanden
            if (indexBefore > 0 && indexBefore < activities.Count)
                return activities[indexBefore];

            // nicht gefunden -> null zurück
            return null;
        }
    }
}

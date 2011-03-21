using System;
using System.Collections.ObjectModel;
using System.Linq;
using AiFrame.InterfaceLib.MVVM;
using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Activities.Views;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities.ViewModel {

    /// <summary>
    /// Modell zur Darstellung einer Aktivitätenliste.
    /// </summary>
    public class ActivitiesListViewModel:IAllocableDataProvider<HomeVisit> {
        
        /// <summary>
        /// Datenfunktion
        /// </summary>
        private GetAllocableDataFunction<HomeVisit> dataFunction;
        /// <summary>
        /// View
        /// </summary>
        private readonly IActivitiesListView view;
        /// <summary>
        /// Liste der Aktivitäten
        /// </summary>
        private readonly Collection<HomeVisit> activities;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="view">View</param>
        public ActivitiesListViewModel(IActivitiesListView view) {
            this.view = view;
            activities = ActivitiesFactories.ActivitiesCollectionFactory.CreateBindableActivitiesCollection();
            this.view.BindToViewModel(this);
        }
        
        /// <summary>
        /// Aktivitäten
        /// </summary>
        public Collection<HomeVisit> ActivitiesList {
            get { return activities; }
        }

        /// <summary>
        /// Lädt die Objekte.
        /// </summary>
        public void LoadObjects() {
            activities.Clear();

            var daoActivities = null != dataFunction ? dataFunction() : DaoFactory.Instance.ActivitiesStore.FindAll();

            Array.ForEach(daoActivities.ToArray(), a => activities.Add(a));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocableDataFunction"></param>
        public void SetAllocableDataFunction(GetAllocableDataFunction<HomeVisit> allocableDataFunction) {
            dataFunction = allocableDataFunction;
        }


        /// <summary>
        /// Benutzer möchte eine Aktivität hinzufügen.
        /// </summary>
        public void UserWantsToAddActivity() {
            var dataView = ActivitiesFactories.ActivitiesViewFactory.CreateHomeVisitDataView();
            var viewModel = new HomeVisitDataViewModel(dataView);
            dataView.SetCaption("Aktivität erstellen");
            dataView.ShowView();

            // übergeordnete View refreshen (die Liste der Aktivitäten) -> Anzeige der Änderungen
            view.RefreshView();
        }

        /// <summary>
        /// Benutzer möchte den übergebenen Eintrag löschen.
        /// </summary>
        /// <param name="dataItem">Eintrag der gelöscht werden soll</param>
        public void UserWantsToDeleteActivity(HomeVisit dataItem) {
            // löschen im eigentlichen Datenspeicher
            DaoFactory.Instance.ActivitiesStore.Delete(dataItem);
            // Aktualisierung ausführen (in der View)
            view.RefreshView();
        }

        /// <summary>
        /// Benutzer möchte den übergebenen Eintrag bearbeiten.
        /// </summary>
        /// <param name="dataItem">Eintrag zum Bearbeiten</param>
        public void UserWantsToEditActivity(HomeVisit dataItem) {
            var dataView = ActivitiesFactories.ActivitiesViewFactory.CreateHomeVisitDataView();
            var viewModel = new HomeVisitDataViewModel(dataView) {HomeVisit = dataItem};
            dataView.SetCaption("Aktivität bearbeiten");
            dataView.ShowView();

            // übergeordnete View (Liste) refreshen 
            view.RefreshView();
        }
    }
}

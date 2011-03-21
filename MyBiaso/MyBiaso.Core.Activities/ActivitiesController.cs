using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiFrame.InterfaceLib.Plugins;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Activities.DataStore;
using MyBiaso.Core.Activities.ViewModel;
using MyBiaso.Core.Activities.Views;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Activities {
    /// <summary>
    /// Controller für die Aktivitäten
    /// </summary>
    public class ActivitiesController {
        
        /// <summary>
        /// Lädt den Controller.
        /// </summary>
        /// <param name="core">Zugriff auf den Kern</param>
        /// <param name="programPath">Programmpfad</param>
        public void Load(ICoreInterface core, string programPath) {
            // Datenspeicher initialisieren
            DaoFactory.Initialize(core.DatabaseConnection);
            // Registrierung initialisieren
            ActivitiesRegistry.Instance.Initialize(core, programPath);

            // Klasse registrieren (HomeVisits)
            core.DatabaseConnection.AddMappingClass(typeof(HomeVisit));

            // Navigation erstellen
            var dataNavigationBar = core.NavigationBar.CreateDataNavigationBar();
            // Navigationsbutton einfügen
            core.NavigationBar.AddButton("activitiesDataButton", "Aktivitäten", null, null, dataNavigationBar, OnNavBarButtonClick);

            // Erstellen eines Buttons zum Hinzufügen von Aktivitäten
            var resourceImages = new ResourceImages();
            dataNavigationBar.AddButton(
                "newActivityButton", "Neue Aktivität hinzufügen", resourceImages.DatasetAdd, null, 
                delegate {
                    core.WindowManager.BringWindowToFront<IActivitiesListView>();
                    var view = ((IActivitiesListView) core.WindowManager.ActiveWindow);
                    view.GetModel().UserWantsToAddActivity();
                    }
                );

            
        }

        /// <summary>
        /// Wird ausglöst, wenn auf die Navigation geklickt wird.
        /// </summary>
        /// <param name="sender">Auslöser</param>
        /// <param name="e">Argumente</param>
        private void OnNavBarButtonClick(object sender, EventArgs e) {
            // prüfen ob das Fenster bereits existiert
            if(ActivitiesRegistry.Instance.CoreInterface.WindowManager.ExistsWindow<IActivitiesListView>()) {
                // anzeigen da bereits exitiert
                ActivitiesRegistry.Instance.CoreInterface.WindowManager.BringWindowToFront<IActivitiesListView>();
            } else {
                var view = ActivitiesFactories.ActivitiesViewFactory.CreateListView();
                var viewModel = new ActivitiesListViewModel(view);
                viewModel.LoadObjects();
                ActivitiesRegistry.Instance.CoreInterface.WindowManager.RegisterWindow(view);
            }
        }

        public void Unload() {
            
        }
    }
}

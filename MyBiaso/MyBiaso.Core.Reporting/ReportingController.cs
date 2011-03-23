using System;
using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Model;
using MyBiaso.Core.Reporting.DataStore;
using MyBiaso.Core.Reporting.ViewModel;
using MyBiaso.Core.Reporting.Views;

namespace MyBiaso.Core.Reporting {

    /// <summary>
    /// Controller für das Reporting.
    /// </summary>
    public class ReportingController {

        /// <summary>
        /// Lädt den Controller für das Reporting.
        /// </summary>
        /// <param name="core">Kern</param>
        /// <param name="programPath">Programmpfad</param>
        public void Load(ICoreInterface core, string programPath) {
            // Datenspeicher initialisieren
            DaoFactory.Initialize(core.DatabaseConnection);
            // Registrierung aktivieren
            ReportingRegistry.Instance.Initialize(core, programPath);
            
            // Navigation erstellen
            var navigationBar = core.NavigationBar.CreateDataNavigationBar();
            // Button einfügen
            core.NavigationBar.AddButton("reportingButton", "Berichte", null, null, navigationBar, OnNavBarButtonClick);
        }

        /// <summary>
        /// Reagiert auf die Auswahl des Navigationsbuttons.
        /// </summary>
        /// <param name="sender">Auslöser</param>
        /// <param name="e">Argumente</param>
        private static void OnNavBarButtonClick(object sender, EventArgs e) {
            // prüfen ob das Fenster bereits existiert
            if(ReportingRegistry.Instance.CoreInterface.WindowManager.ExistsWindow<IReportingListView>()) {
                // anzeigen (existiert bereits)
                ReportingRegistry.Instance.CoreInterface.WindowManager.BringWindowToFront<IReportingListView>();
            } else {
                // erzeugen
                var view = ReportingFactories.ReportingViewFactory.CreateListView();
                var viewModel = new ReportingListViewModel(view);
                viewModel.LoadObjects();
                ReportingRegistry.Instance.CoreInterface.WindowManager.RegisterWindow(view);
            }


        }
    }
}

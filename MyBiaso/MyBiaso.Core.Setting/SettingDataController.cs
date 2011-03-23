using System;
using System.Resources;
using AiFrame.InterfaceLib.MVP;
using AiFrame.InterfaceLib.Plugins;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Setting.DataStore;
using MyBiaso.Core.Setting.ViewModel;
using MyBiaso.Core.Setting.Views;

namespace MyBiaso.Core.Setting {

    
    /// <summary>
    /// Controller für die Daten 
    /// </summary>
    public class SettingDataController {

        /// <summary>
        /// Kern der Anwendung
        /// </summary>
        private ICoreInterface core;

        /// <summary>
        /// Lädt den Controller
        /// </summary>
        /// <param name="coreInterface">Core</param>
        /// <param name="programPath">Pfad</param>
        public void Load(ICoreInterface coreInterface, string programPath) {
            core = coreInterface;


            // Initialize the registry in order to save the CoreInterface and program path
            // there.
            SettingsDataRegistry.Instance.Initialize(coreInterface, programPath);

            // Dao initialisieren
            DaoFactory.Initialize(coreInterface.DatabaseConnection);
            // Einstellungsklasse registrieren
            coreInterface.DatabaseConnection.AddMappingClass(typeof(Model.Setting));

            ResourceImages resourceImages = new ResourceImages();

            ResourceManager resourceManager = new ResourceManager("MyBiaso.Core.Customer.Properties.Resources", this.GetType().Assembly);

            var dataNavigationBar = core.NavigationBar.CreateDataNavigationBar();

            core.NavigationBar.AddButton("settingsButton", "Einstellungen", null, null, dataNavigationBar, OnNavBarButtonClick);
        }

        private void OnNavBarButtonClick(object sender, EventArgs e) {
            if(core.WindowManager.ExistsWindow<ISettingsView>()) {
                core.WindowManager.BringWindowToFront<ISettingsView>();
            } else {
                var view = SettingFactories.SettingsViewFactory.CreateSettingsView();
                SettingsViewModel viewModel = new SettingsViewModel(view);
                core.WindowManager.RegisterWindow(view);
            }
        }
    }
}

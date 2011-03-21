using System;
using AiFrame.InterfaceLib.Plugins;
using AiFrame.InterfaceLib.Plugins.Events;
using AiFrame.InterfaceLib.Windows.Controls;
using MyBiaso.Plugin.Startup.Window;

namespace MyBiaso.Plugin.Startup {



    public class StartupPlugin:IPlugin, IConnectionReadyEvent {

        private ICoreInterface coreInterface;

        public void Load(ICoreInterface ci, string programPath) {
            coreInterface = ci;

            StartupRegistry.Instance.Initialize(ci, programPath);

            IDataNavigationBar navigationBar = ci.NavigationBar.CreateDataNavigationBar();
            ci.NavigationBar.AddButton("navbarStartCenterBtn", "Start", "Startfenster", null,
                                       navigationBar, OnStartCenterButtonClick);
        }

        private void OnStartCenterButtonClick(object sender, EventArgs e) {
            ShowStartupScreen();
        }

        public void Unload() {
            
        }

        public string GetCaption() {
            return "Willkommen";
        }

        public void OnConnectionReadyEvent() {
            ShowStartupScreen();
        }

        private void ShowStartupScreen() {
            // prüfen ob bereits existiert 
            if(!coreInterface.WindowManager.ExistsWindow<StartupFrame>()) {
                // noch nicht vorhanden -> erzeugen und zeigen
                coreInterface.WindowManager.RegisterWindow(new StartupFrame());
            } else {
                // vorhandenes Fenster anzeigen
                coreInterface.WindowManager.BringWindowToFront<StartupFrame>();
            }
        }
    }
}

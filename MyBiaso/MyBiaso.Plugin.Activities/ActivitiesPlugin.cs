using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Activities;

namespace MyBiaso.Plugin.Activities {
    
    /// <summary>
    /// Plugin für die Aktivitätenverwaltung
    /// </summary>
    public class ActivitiesPlugin:IPlugin {

        /// <summary>
        /// Controller
        /// </summary>
        private readonly ActivitiesController controller = new ActivitiesController();

        public void Load(ICoreInterface ci, string programPath) {
            // Fabrik für die View erstellen
            ActivitiesFactories.ActivitiesViewFactory = new ActivitiesViewFactory();
            ActivitiesFactories.ActivitiesCollectionFactory = new ActivitiesListCollectionFactory();
            controller.Load(ci, programPath);
        }

        public void Unload() {
            controller.Unload();
        }

        public string GetCaption() {
            return "Aktivitäten Plugin";
        }
    }
}

using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Reporting;

namespace MyBiaso.Plugin.Reporting {

    /// <summary>
    /// Definition des Plugins für das Reporting.
    /// </summary>
    public class ReportingPlugin:IPlugin {
        
        private readonly ReportingController controller = new ReportingController();

        public void Load(ICoreInterface ci, string programPath) {
            ReportingFactories.ReportingCollectionFactory = new ReportingCollectionFactory();
            ReportingFactories.ReportingViewFactory = new ReportingViewFactory();
            controller.Load(ci, programPath);
        }

        public void Unload() {
            
        }

        public string GetCaption() {
            return "Berichte";
        }
    }
}

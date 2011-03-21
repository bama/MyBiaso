using AiFrame.InterfaceLib.Plugins;
using MyBiaso.Core.Customer;

namespace MyBiaso.Plugin.Customer {

    /// <summary>
    /// Definition des Plugins für die Kundenverwaltung.
    /// </summary>
    public class CustomerPlugin:IPlugin {

        /// <summary>
        /// Controller
        /// </summary>
        private readonly CustomerDataController controller = new CustomerDataController();

        /// <summary>
        /// Lädt das Plugin.
        /// </summary>
        /// <param name="ci">Core-Interface</param>
        /// <param name="programPath">Programmpfad</param>
        public void Load(ICoreInterface ci, string programPath) {
            // Fabrik für die View setzen
            CustomerFactories.CustomerDataViewFactory = new CustomerDataViewFactory();
            controller.Load(ci, programPath);
        }

        /// <summary>
        /// Entfernt das Plugin
        /// </summary>
        public void Unload() {
            controller.Unload();
        }

        /// <summary>
        /// Überschrift
        /// </summary>
        /// <returns>Überschrift</returns>
        public string GetCaption() {
            return "Kunden Plugin";
        }
    }
}

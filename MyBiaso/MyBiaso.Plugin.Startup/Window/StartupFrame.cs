using System.Windows.Forms;
using AiFrame.InterfaceLib.MVP;

namespace MyBiaso.Plugin.Startup.Window {
    public sealed partial class StartupFrame : UserControl, IWindow {
        public StartupFrame() {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public void RefreshView() {
            
        }

        public void SetFree() {
            Dispose();
        }

        public void SetParentWindow(Control parent) {
            Parent = parent;
        }

        public void BringWindowToFront() {
            BringToFront();
        }

        public string GetCaption() {
            return "Willkommen";
        }
    }
}

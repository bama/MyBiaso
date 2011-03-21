using System;
using System.Windows.Forms;
using AiFrame.Base.Core;
using AiFrame.Base.WinUI;
using AiFrame.Base.WinUI.Windows.UI;
using MyBiaso.Core;

namespace MyBiaso {

    /// <summary>
    /// Startklasse
    /// </summary>
    static class Program {
    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args) {
            // Anwendungsoberfläche initialisieren
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
                // Initalisieren
                var initialization = new BaseInitialization(args);
                initialization.PreInitialize();

                CoreInitialization.Initialize();

                initialization.PostInitialize(new DefaultStartupView());
            
           
        }
    }
}

using System;
using MyBiaso.Core.Reporting.Views;
using MyBiaso.Plugin.Reporting.Window;

namespace MyBiaso.Plugin.Reporting {
    
    /// <summary>
    /// Fabrikmuster 
    /// </summary>
    public class ReportingViewFactory:IReportingViewFactory {
        
        
        public IReportingListView CreateListView() {
            return new ReportingFrame();
        }
    }
}

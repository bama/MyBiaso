using System.Collections.ObjectModel;
using System.ComponentModel;
using MyBiaso.Core.Model;
using MyBiaso.Core.Reporting.DataStore;

namespace MyBiaso.Plugin.Reporting {

    public class ReportingCollectionFactory:IReportingCollectionFactory {
        
        public Collection<ReportingValues> CreateBindableReportingCollection() {
            return new BindingList<ReportingValues>();
        }
    }
}

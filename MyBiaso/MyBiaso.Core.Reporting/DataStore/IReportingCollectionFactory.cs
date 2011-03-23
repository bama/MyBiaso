using System.Collections.ObjectModel;
using MyBiaso.Core.Model;

namespace MyBiaso.Core.Reporting.DataStore {

    public interface IReportingCollectionFactory {

        Collection<ReportingValues> CreateBindableReportingCollection();

    }
}

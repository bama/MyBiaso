using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBiaso.Core.Reporting.Views {
    public interface IReportingViewFactory {

        IReportingListView CreateListView();
    }
}

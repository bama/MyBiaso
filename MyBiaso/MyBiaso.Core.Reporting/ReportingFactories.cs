using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBiaso.Core.Reporting.DataStore;
using MyBiaso.Core.Reporting.Views;

namespace MyBiaso.Core.Reporting {
    public class ReportingFactories {

        /// <summary>
        /// 
        /// </summary>
        public static IReportingCollectionFactory ReportingCollectionFactory { get; set; }

        public static IReportingViewFactory ReportingViewFactory { get; set; }
    }
}

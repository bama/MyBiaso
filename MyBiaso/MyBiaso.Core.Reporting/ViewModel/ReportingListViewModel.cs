using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using AiFrame.InterfaceLib.MVVM;
using MyBiaso.Core.Model;
using MyBiaso.Core.Reporting.DataStore;
using MyBiaso.Core.Reporting.Views;

namespace MyBiaso.Core.Reporting.ViewModel {
    
    /// <summary>
    /// ViewModel für die Listendarstellung des Reportings.
    /// </summary>
    public class ReportingListViewModel:IAllocableDataProvider<ReportingValues> {
        private readonly IReportingListView view;
        
        private readonly Collection<ReportingValues> yearlyReportingValues;

        private readonly Collection<ReportingValues> monthlyReportingValues;

        private readonly Collection<ReportingValues> quarterlyReportingValues;

        public ReportingListViewModel(IReportingListView view) {
            this.view = view;
            yearlyReportingValues = ReportingFactories.ReportingCollectionFactory.CreateBindableReportingCollection();
            monthlyReportingValues = ReportingFactories.ReportingCollectionFactory.CreateBindableReportingCollection();
            quarterlyReportingValues = ReportingFactories.ReportingCollectionFactory.CreateBindableReportingCollection();
            this.view.BindToViewModel(this);
        }

        public Collection<ReportingValues> YearlyReportingValuesList {
            get { return yearlyReportingValues; }
        }

        public Collection<ReportingValues> MonthlyReportingValuesList {
            get { return monthlyReportingValues; }
        }

        public Collection<ReportingValues> QuarterlyReportingValuesList {
            get { return quarterlyReportingValues; }
        }

        public void LoadObjects() {
            var yearly = new List<ReportingValues>();
            var monthly = new List<ReportingValues>();
            var quarterly = new List<ReportingValues>();

            var daoActivities = DaoFactory.Instance.ActivitiesStore.FindAll();

            foreach (var daoActivity in daoActivities) {
                var yearlyValue = yearly.FirstOrDefault(a => a.Year.Equals(daoActivity.Begin.Year));
                var quarterValue = quarterly.FirstOrDefault(a => a.Quarter.Equals(((daoActivity.Begin.Month + 2)/3)));
                var monthlyValue = monthly.FirstOrDefault(a => a.Year.Equals(daoActivity.Begin.Year) && a.Month.Equals(daoActivity.Begin.Month));

                if (null == yearlyValue) {
                    yearlyValue = new ReportingValues {Year = daoActivity.Begin.Year};
                    yearly.Add(yearlyValue);
                }

                if(null == quarterValue) {
                    quarterValue = new ReportingValues {Year = daoActivity.Begin.Year, Quarter = ((daoActivity.Begin.Month + 2)/3)};
                    quarterly.Add(quarterValue);
                }

                if(null == monthlyValue) {
                    monthlyValue = new ReportingValues {Year = daoActivity.Begin.Year, Month = daoActivity.Begin.Month};
                    monthly.Add(monthlyValue);
                }

                yearlyValue.CustomerVisits += 1;
                quarterValue.CustomerVisits += 1;
                monthlyValue.CustomerVisits += 1;
                yearlyValue.DistanceTravelled += daoActivity.DistanceTravelled;
                quarterValue.DistanceTravelled += daoActivity.DistanceTravelled;
                monthlyValue.DistanceTravelled += daoActivity.DistanceTravelled;
            }


            yearlyReportingValues.Clear();
            quarterlyReportingValues.Clear();
            monthlyReportingValues.Clear();

            yearly.Sort((x,y) => -1 * x.Year.CompareTo(y.Year));
            monthly.Sort((x, y) => -1 * (x.Year == y.Year ? x.Month.CompareTo(y.Month) : x.Year.CompareTo(y.Year)));
            quarterly.Sort((x, y) => -1 * (x.Year == y.Year ? x.Quarter.CompareTo(y.Quarter) : x.Year.CompareTo(y.Year)));

            yearly.ForEach(yearlyReportingValues.Add);
            quarterly.ForEach(quarterlyReportingValues.Add);
            monthly.ForEach(monthlyReportingValues.Add);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allocableDataFunction"></param>
        public void SetAllocableDataFunction(GetAllocableDataFunction<ReportingValues> allocableDataFunction) {}
    }
}

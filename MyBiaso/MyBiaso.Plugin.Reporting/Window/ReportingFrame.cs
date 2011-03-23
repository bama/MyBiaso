using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Model;
using MyBiaso.Core.Reporting.ViewModel;
using MyBiaso.Core.Reporting.Views;

namespace MyBiaso.Plugin.Reporting.Window {
    
    /// <summary>
    /// Frame zum Reporting.
    /// </summary>
    public partial class ReportingFrame : UserControl, IReportingListView {
        
        /// <summary>
        /// ViewModel für die Liste
        /// </summary>
        private ReportingListViewModel viewModel;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ReportingFrame() {

            InitializeComponent();
            Dock = DockStyle.Fill;
            grdReport.Dock = DockStyle.Fill;

            grdReport.ReadOnly = true;
            grdReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdReport.AutoGenerateColumns = false;
            grdReport.AllowUserToAddRows = false;
            grdReport.MultiSelect = false;


            grdReport.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(234, 234, 234)
            };

            AddColumns();
        }

        /// <summary>
        /// Fügt die Spalten in das Grid ein.
        /// </summary>
        private void AddColumns() {

            DataGridViewCell cell = new DataGridViewTextBoxCell();

            grdReport.Columns.Add(new DataGridViewColumn(cell)
                                  {HeaderText = "", DataPropertyName = "TimeSpan", Width = 130});

            grdReport.Columns.Add(new DataGridViewColumn(cell) { HeaderText = "Kundenbesuche", DataPropertyName = "CustomerVisits", Width = 100 });

            grdReport.Columns.Add(new DataGridViewColumn(cell)
                                  {HeaderText = "Gefahrene Km", DataPropertyName = "DistanceTravelled", Width = 100});


        }

        public int GetSelectedIndex() {
            return grdReport.SelectedRows.Count > 0 ? grdReport.SelectedRows[0].Index : -1;
        }

        public IList<ReportingValues> DataItems {
            get {
                return grdReport.DataSource as IList<ReportingValues>;
                
            }
        }

        public void RefreshView() {
            viewModel.LoadObjects();
            grdReport.DataSource = GetSelectedDatasource();
        }

        public void SetFree() {
            Dispose();
        }

        public void SetParentWindow(Control parent) {
            Parent = parent;
        }

        public void BringWindowToFront() {
            cmbTimeSpanChoice.SelectedIndex = 0;
            RefreshView();
            BringToFront();
        }

        public string GetCaption() {
            return "Berichte";
        }

        public void BindToViewModel(ReportingListViewModel model) {
            viewModel = model;
            grdReport.DataSource = GetSelectedDatasource();
        }

        private IList<ReportingValues> GetSelectedDatasource() {
            if (Equals(cmbTimeSpanChoice.SelectedItem, "Jahr"))
                return viewModel.YearlyReportingValuesList;
            if (Equals(cmbTimeSpanChoice.SelectedItem, "Monat"))
                return viewModel.MonthlyReportingValuesList;
            if (Equals(cmbTimeSpanChoice.SelectedItem, "Quartal"))
                return viewModel.QuarterlyReportingValuesList;

            return null;
        }

        private void cmbTimeSpanChoice_SelectedIndexChanged(object sender, EventArgs e) {
            BindToViewModel(viewModel);
        }
    }
}

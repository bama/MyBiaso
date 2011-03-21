using System;
using System.Windows.Forms;
using AiFrame.InterfaceLib.MVP;
using MyBiaso.Core.Activities.ViewModel;
using MyBiaso.Core.Activities.Views;

namespace MyBiaso.Plugin.Activities.Window {
    public partial class ActivityDataWindow : Form, IHomeVisitDataView {

        /// <summary>
        /// ViewModel
        /// </summary>
        private HomeVisitDataViewModel viewModel;

        /// <summary>
        /// Flag, true wenn die Werte in der Darstellung aktualisiert werden
        /// </summary>
        private bool refreshing = false;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ActivityDataWindow() {
            InitializeComponent();
        }

        public void ShowView() {
            refreshing = true;
            // Zeitangaben müssen durch die fehlende automatische Bindung explizit gesetzt werden
            dtpBegin.Value = viewModel.Begin;
            dtpBeginTime.Value = viewModel.Begin;
            dtpEnd.Value = viewModel.End;
            dtpEndTime.Value = viewModel.End;
            refreshing = false;

            // als Dialog anzeigen (sperren der hinteren Ansicht)
            ShowDialog();
        }

        public void SetCaption(string s) {
            Text = s;
        }

        public void InformUser(string message) {
            MessageBox.Show(this, message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DisplayError(string message) {
            MessageBox.Show(this, message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void BindToViewModel(HomeVisitDataViewModel model) {
            viewModel = model;
            cmbCustomers.DataSource = model.CustomerListValues;
            cmbCustomers.DisplayMember = "Value";
            cmbCustomers.ValueMember = "Key";
            BindingHelper.SetBinding(cmbCustomers, "SelectedValue", model, "Customer", true, DataSourceUpdateMode.OnPropertyChanged);
            BindingHelper.SetBinding(chkDrivenHome, "Checked", model, "DrivenHome", true, DataSourceUpdateMode.OnPropertyChanged);

            dtpBegin.Value = model.Begin;
            dtpBeginTime.Value = model.Begin;
            dtpEnd.Value = model.End;
            dtpEndTime.Value = model.End;
        }


        private void OnBeginChanged(object sender, EventArgs e) {
            if(!refreshing)
                viewModel.Begin = DateTime.Parse(dtpBegin.Value.ToString("dd.MM.yyyy") + " " + dtpBeginTime.Value.ToString("HH:mm"));
        }

        private void OnEndChanged(object sender, EventArgs e) {
            if(!refreshing)
                viewModel.End = DateTime.Parse(dtpEnd.Value.ToString("dd.MM.yyyy") + " " + dtpEndTime.Value.ToString("HH:mm"));
        }

        private void butOK_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            viewModel.UserWantsToSave();
        }

        private void butCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            viewModel.UserWantsToCancel();
        }

        
    }
}

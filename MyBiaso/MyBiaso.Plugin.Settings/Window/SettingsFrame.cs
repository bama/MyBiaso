using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AiFrame.InterfaceLib.MVP;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Setting;
using MyBiaso.Core.Setting.ViewModel;
using MyBiaso.Core.Setting.Views;

namespace MyBiaso.Plugin.Settings.Window {


    public partial class SettingsFrame : UserControl, ISettingsView {
        
        private SettingsViewModel viewModel;
        /// <summary>
        /// Buttons für den Toolstrip zur Editierung
        /// </summary>
        private ToolStripButton saveButton, editButton, cancelButton;
        
        public SettingsFrame() {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public void BindToViewModel(SettingsViewModel model) {
            viewModel = model;

            var settings = viewModel.GetDataSource();

            txtCurrentCustomerNumber.Text = settings["numbers.customer.customer_number"].Value;
        }

        public void OpenEditMode() {
            SetControlsEnable(true);
        }

        public void DisplayError(string format) {
            MessageBox.Show(format, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseEditMode() {
            SetControlsEnable(false);
        }

        public void RefreshView() {
            Invalidate(true);
        }

        public void SetFree() {
            Dispose();
        }

        public void SetParentWindow(Control parent) {
            Parent = parent;
        }

        public void BringWindowToFront() {
            BringToFront();
            ShowToolbar();
        }

        private void ShowToolbar() {
            var resourceImages = new ResourceImages();
            var toolStripButtons = new ToolStrip();

            editButton = new ToolStripButton("Editieren", resourceImages.DatasetOpen, OnToolItemClick)
            {
                Enabled =
                    true,
                Tag = "Edit"
            };
            saveButton = new ToolStripButton("Speichern", null, OnToolItemClick) { Tag = "Save", Enabled = false };
            cancelButton = new ToolStripButton("Abbrechen", null, OnToolItemClick) { Tag = "Cancel", Enabled = false };


            toolStripButtons.Items.Add(editButton);
            toolStripButtons.Items.Add(saveButton);
            toolStripButtons.Items.Add(cancelButton);

            SettingsDataRegistry.Instance.CoreInterface.ToolBarManager.AddToolGroup(toolStripButtons, false, 1);

        }

        private void OnToolItemClick(object sender, EventArgs e) {
            switch (((ToolStripItem)sender).Tag.ToString()) {
                case "Save":
                    viewModel.GetDataSource()["numbers.customer.customer_number"].Value = txtCurrentCustomerNumber.Text;
                    viewModel.UserWantsToSave();
                    break;
                case "Edit":
                    viewModel.UserWantsToEdit();
                    break;
                case "Cancel":
                    viewModel.UserWantsToCancelEdit();
                    break;
            }
        }

        public string GetCaption() {
            return "Einstellungen";
        }

        private void SetControlsEnable(bool enable) {
            txtCurrentCustomerNumber.Enabled = enable;

            saveButton.Enabled = enable;
            cancelButton.Enabled = enable;
            editButton.Enabled = !enable;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AiFrame.InterfaceLib.Resources;
using MyBiaso.Core.Activities;
using MyBiaso.Core.Activities.ViewModel;
using MyBiaso.Core.Activities.Views;
using MyBiaso.Core.Model;

namespace MyBiaso.Plugin.Activities.Window {
    
    /// <summary>
    /// Frame zur Darstellung der Aktivitäten.
    /// </summary>
    public sealed partial class ActivitiesListFrame : UserControl, IActivitiesListView {

        /// <summary>
        /// ViewModel
        /// </summary>
        private ActivitiesListViewModel viewModel;

        private ToolStripButton openButton, deleteButton;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ActivitiesListFrame() {
            InitializeComponent();
            Dock = DockStyle.Fill;
            gridActivities.Dock = DockStyle.Fill;

            gridActivities.ReadOnly = true;
            gridActivities.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridActivities.AutoGenerateColumns = false;
            gridActivities.AllowUserToAddRows = false;
            gridActivities.MultiSelect = false;


            gridActivities.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(234, 234, 234)
            };

            AddColumns();
        }

        private void AddColumns() {
            DataGridViewCell dataGridViewCell = new DataGridViewTextBoxCell();

            gridActivities.CellFormatting += gridActivities_CellFormatting;

            gridActivities.Columns.Add(new DataGridViewColumn(dataGridViewCell)
            {
                HeaderText = "Beginn",
                DataPropertyName = "Begin",
                Width = 250
            });

            gridActivities.Columns.Add(new DataGridViewColumn(dataGridViewCell)
            {
                HeaderText = "Ende",
                DataPropertyName = "End",
                Width = 200
            });

            gridActivities.Columns.Add(new DataGridViewColumn(dataGridViewCell) {
                                                                                    HeaderText = "Kunde",
                                                                                    DataPropertyName = "Customer",
                                                                                    Width = 300
                                                                                });

            gridActivities.Columns.Add(new DataGridViewColumn(new DataGridViewCheckBoxCell()) {
                                                                                                  HeaderText =
                                                                                                      "Nach Hause",
                                                                                                  DataPropertyName =
                                                                                                      "DrivenHome",
                                                                                                  Width = 50
                                                                                              });

            gridActivities.Columns.Add(new DataGridViewColumn(dataGridViewCell) {
                                                                                    HeaderText = "Distanz",
                                                                                    DataPropertyName = "DistanceTravelled",
                                                                                    Width = 80
                                                                                });
        }

        /// <summary>
        /// Handler für das Ereignis, wenn eine Zelle formatiert werden soll.
        /// </summary>
        /// <param name="sender">Auslöser</param>
        /// <param name="e">Argumente</param>
        void gridActivities_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            // prüfen ob dies die Kundenspalte ist
            if(2 == e.ColumnIndex) {
                // formatieren
                var value = e.Value as Customer;
                // setzen
                if(null != value) {
                    // formatieren
                    e.Value = string.Format("{0}, {1} (Kundennummer: {2})", value.Lastname,
                                     value.Firstname, value.CustomerNumber);
                } else {
                    // leere Zeichenkette
                    e.Value = String.Empty;
                }
                // kennzeichnen das formatiert wurde
                e.FormattingApplied = true;
            } else if(4 == e.ColumnIndex) {
                if(null != e.Value) {
                    var value = (Single) e.Value;
                    e.Value = String.Format("{0:f2} km", (value / 1000));
                } else {
                    e.Value = String.Empty;
                }
                e.FormattingApplied = true;
            }
        }

        /// <summary>
        /// Zeigt die Toolbar an.
        /// </summary>
        private void ShowToolbar() {
            var resources = new ResourceImages();

            // Leiste erstellen
            var toolstripButtons = new ToolStrip();

            // Buttons erstellen
            openButton = new ToolStripButton("Editieren", resources.DatasetOpen, OnToolItemClick)
            {
                Enabled =
                    false,
                Tag = "Edit"
            };
            deleteButton = new ToolStripButton("Löschen", resources.DatasetDelete, OnToolItemClick) { Enabled = false, Tag = "Delete" };
            
            // einfügen in die Leiste
            toolstripButtons.Items.Add(openButton);
            toolstripButtons.Items.Add(deleteButton);

            // einfügen der Leiste
            ActivitiesRegistry.Instance.CoreInterface.ToolBarManager.AddToolGroup(toolstripButtons, false, 1);
        }

        private void OnToolItemClick(object sender, EventArgs e) {
            switch((((ToolStripItem) sender).Tag.ToString())) {
                case "Edit": 
                    if(GetSelectedIndex() != -1) {
                        viewModel.UserWantsToEditActivity(DataItems[GetSelectedIndex()]);
                    }
                    break;
                case "Delete": 
                    if(GetSelectedIndex() != -1) {
                        // nachfragen, ob gelöscht werden soll
                        if (MessageBox.Show(this, "Möchten Sie den ausgewählten Eintrag löschen?", 
                            "Löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                            viewModel.UserWantsToDeleteActivity(DataItems[GetSelectedIndex()]);
                        }
                    }
                    break;
            }
        }


        public int GetSelectedIndex() {
            return (gridActivities.SelectedRows.Count > 0 ? gridActivities.SelectedRows[0].Index : -1);
        }

        public IList<HomeVisit> DataItems {
            get {
                return gridActivities.DataSource as IList<HomeVisit>;
            }
        }

        public void RefreshView() {
            viewModel.LoadObjects();
            gridActivities.DataSource = viewModel.ActivitiesList;
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


        public string GetCaption() {
            return "Aktivitäten";
        }

        public void BindToViewModel(ActivitiesListViewModel activitiesListViewModel) {
            viewModel = activitiesListViewModel;
            gridActivities.DataSource = viewModel.ActivitiesList;
        }

        public ActivitiesListViewModel GetModel() {
            return viewModel;
        }

        /// <summary>
        /// Handler für die Auswahl einer zeile im Datagrid.
        /// </summary>
        /// <param name="sender">Auslöser</param>
        /// <param name="e">Argumente</param>
        private void GridActivitiesSelectionChanged(object sender, EventArgs e) {
            if(null != deleteButton) deleteButton.Enabled = (GetSelectedIndex() != -1);
            if (null != openButton) openButton.Enabled = (GetSelectedIndex() != -1);
        }
    }
}

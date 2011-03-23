namespace MyBiaso.Plugin.Reporting.Window {
    partial class ReportingFrame {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.grdReport = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTimeSpanChoice = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdReport
            // 
            this.grdReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReport.Location = new System.Drawing.Point(0, 0);
            this.grdReport.Name = "grdReport";
            this.grdReport.Size = new System.Drawing.Size(648, 331);
            this.grdReport.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbTimeSpanChoice);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdReport);
            this.splitContainer1.Size = new System.Drawing.Size(648, 417);
            this.splitContainer1.SplitterDistance = 82;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auswertungsart";
            // 
            // cmbTimeSpanChoice
            // 
            this.cmbTimeSpanChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeSpanChoice.FormattingEnabled = true;
            this.cmbTimeSpanChoice.Items.AddRange(new object[] {
            "Monat",
            "Quartal",
            "Jahr"});
            this.cmbTimeSpanChoice.Location = new System.Drawing.Point(17, 28);
            this.cmbTimeSpanChoice.Name = "cmbTimeSpanChoice";
            this.cmbTimeSpanChoice.Size = new System.Drawing.Size(192, 21);
            this.cmbTimeSpanChoice.TabIndex = 1;
            this.cmbTimeSpanChoice.SelectedIndexChanged += new System.EventHandler(this.cmbTimeSpanChoice_SelectedIndexChanged);
            // 
            // ReportingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportingFrame";
            this.Size = new System.Drawing.Size(648, 417);
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdReport;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbTimeSpanChoice;
        private System.Windows.Forms.Label label1;
    }
}

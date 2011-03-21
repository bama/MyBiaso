namespace MyBiaso.Plugin.Activities.Window {
    sealed partial class ActivitiesListFrame {
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
            this.gridActivities = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridActivities)).BeginInit();
            this.SuspendLayout();
            // 
            // gridActivities
            // 
            this.gridActivities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridActivities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridActivities.Location = new System.Drawing.Point(0, 0);
            this.gridActivities.Name = "gridActivities";
            this.gridActivities.Size = new System.Drawing.Size(590, 357);
            this.gridActivities.TabIndex = 0;
            this.gridActivities.SelectionChanged += new System.EventHandler(this.GridActivitiesSelectionChanged);
            // 
            // ActivitiesListFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridActivities);
            this.Name = "ActivitiesListFrame";
            this.Size = new System.Drawing.Size(590, 357);
            ((System.ComponentModel.ISupportInitialize)(this.gridActivities)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridActivities;
    }
}

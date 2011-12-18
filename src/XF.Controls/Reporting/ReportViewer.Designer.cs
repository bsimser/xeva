namespace XF.Controls {
   partial class ReportViewer {
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
         this._reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
         this._statusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
         this.SuspendLayout();
         // 
         // _reportViewer
         // 
         this._reportViewer.LocalReport.DisplayName = "Claim Summary";
         this._reportViewer.LocalReport.ReportEmbeddedResource = "ClaimSummary.rdlc";
         this._reportViewer.LocalReport.ReportPath = "Reports\\ClaimSummary.rdlc";
         this._reportViewer.Location = new System.Drawing.Point(0, 8);
         this._reportViewer.Name = "_reportViewer";
         this._reportViewer.Size = new System.Drawing.Size(745, 454);
         this._reportViewer.TabIndex = 1;
         // 
         // _statusBar
         // 
         this._statusBar.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
         this._statusBar.Location = new System.Drawing.Point(0, 539);
         this._statusBar.Name = "_statusBar";
         this._statusBar.Size = new System.Drawing.Size(748, 23);
         this._statusBar.TabIndex = 2;
         // 
         // ReportViewer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._statusBar);
         this.Controls.Add(this._reportViewer);
         this.Name = "ReportViewer";
         this.Size = new System.Drawing.Size(748, 562);
         this.ResumeLayout(false);

      }

      #endregion

      private Microsoft.Reporting.WinForms.ReportViewer _reportViewer;
      private Infragistics.Win.UltraWinStatusBar.UltraStatusBar _statusBar;
   }
}

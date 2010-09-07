namespace XF.Controls
{
   partial class ToolTipControl
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         this._toolTip = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
         ((System.ComponentModel.ISupportInitialize)(this._toolTip)).BeginInit();
         this.SuspendLayout();
         // 
         // _toolTip
         // 
         appearance1.BackColor = System.Drawing.SystemColors.Info;
         appearance1.BorderColor = System.Drawing.Color.Transparent;
         this._toolTip.Appearance = appearance1;
         this._toolTip.BackColor = System.Drawing.SystemColors.Info;
         this._toolTip.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
         this._toolTip.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007;
         this._toolTip.Dock = System.Windows.Forms.DockStyle.Fill;
         this._toolTip.Location = new System.Drawing.Point(0, 0);
         this._toolTip.Multiline = true;
         this._toolTip.Name = "_toolTip";
         this._toolTip.ReadOnly = true;
         this._toolTip.Size = new System.Drawing.Size(371, 87);
         this._toolTip.TabIndex = 0;
         this._toolTip.UseAppStyling = false;
         // 
         // ToolTipControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.SystemColors.Info;
         this.ClientSize = new System.Drawing.Size(371, 87);
         this.Controls.Add(this._toolTip);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ToolTipControl";
         this.ShowIcon = false;
         this.Load += new System.EventHandler(this.OnToolLoad);
         ((System.ComponentModel.ISupportInitialize)(this._toolTip)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private Infragistics.Win.UltraWinEditors.UltraTextEditor _toolTip;


   }
}
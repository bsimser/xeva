namespace XF.Controls
{
   partial class DeleteCancelControl
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

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._cancelButton = new Infragistics.Win.Misc.UltraButton();
         this._deleteButton = new Infragistics.Win.Misc.UltraButton();
         this.SuspendLayout();
         // 
         // _cancelButton
         // 
         this._cancelButton.Location = new System.Drawing.Point(72, 3);
         this._cancelButton.Name = "_cancelButton";
         this._cancelButton.Size = new System.Drawing.Size(64, 23);
         this._cancelButton.TabIndex = 19;
         this._cancelButton.Text = "Cancel";
         this._cancelButton.Click += new System.EventHandler(this.OnCancelClick);
         // 
         // _deleteButton
         // 
         this._deleteButton.Location = new System.Drawing.Point(2, 3);
         this._deleteButton.Name = "_deleteButton";
         this._deleteButton.Size = new System.Drawing.Size(64, 23);
         this._deleteButton.TabIndex = 18;
         this._deleteButton.Text = "Delete";
         this._deleteButton.Click += new System.EventHandler(this.OnDeleteClick);
         // 
         // DeleteCancelControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._cancelButton);
         this.Controls.Add(this._deleteButton);
         this.Name = "DeleteCancelControl";
         this.Size = new System.Drawing.Size(140, 29);
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraButton _cancelButton;
      private Infragistics.Win.Misc.UltraButton _deleteButton;
   }
}

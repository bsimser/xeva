namespace XF.Controls {
   partial class EditSaveCancel {
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
         this._btnEdit = new Infragistics.Win.Misc.UltraButton();
         this._btnCancel = new Infragistics.Win.Misc.UltraButton();
         this._btnSave = new Infragistics.Win.Misc.UltraButton();
         this.SuspendLayout();
         // 
         // _btnEdit
         // 
         this._btnEdit.Location = new System.Drawing.Point(73, 3);
         this._btnEdit.Name = "_btnEdit";
         this._btnEdit.Size = new System.Drawing.Size(64, 23);
         this._btnEdit.TabIndex = 0;
         this._btnEdit.Text = "Edit";
         this._btnEdit.Click += new System.EventHandler(this.OnEditClick);
         // 
         // _btnCancel
         // 
         this._btnCancel.Location = new System.Drawing.Point(73, 3);
         this._btnCancel.Name = "_btnCancel";
         this._btnCancel.Size = new System.Drawing.Size(64, 23);
         this._btnCancel.TabIndex = 1;
         this._btnCancel.Text = "Cancel";
         this._btnCancel.Visible = false;
         this._btnCancel.Click += new System.EventHandler(this.OnCancelClick);
         // 
         // _btnSave
         // 
         this._btnSave.Location = new System.Drawing.Point(3, 3);
         this._btnSave.Name = "_btnSave";
         this._btnSave.Size = new System.Drawing.Size(64, 23);
         this._btnSave.TabIndex = 2;
         this._btnSave.Text = "Save";
         this._btnSave.Visible = false;
         this._btnSave.Click += new System.EventHandler(this.OnSaveClick);
         // 
         // EditSaveCancel
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._btnSave);
         this.Controls.Add(this._btnCancel);
         this.Controls.Add(this._btnEdit);
         this.Name = "EditSaveCancel";
         this.Size = new System.Drawing.Size(141, 29);
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraButton _btnEdit;
      private Infragistics.Win.Misc.UltraButton _btnCancel;
      private Infragistics.Win.Misc.UltraButton _btnSave;
   }
}

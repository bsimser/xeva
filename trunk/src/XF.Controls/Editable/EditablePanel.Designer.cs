namespace XF.Controls {
   partial class EditablePanel {
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
         this.components = new System.ComponentModel.Container();
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
         this._internalBox = new System.Windows.Forms.GroupBox();
         this._internalEdit = new Infragistics.Win.Misc.UltraButton();
         this._internalCancel = new Infragistics.Win.Misc.UltraButton();
         this._internalSave = new Infragistics.Win.Misc.UltraButton();
         this._toolTip = new System.Windows.Forms.ToolTip(this.components);
         this._spacer = new System.Windows.Forms.Panel();
         this._internalBox.SuspendLayout();
         this.SuspendLayout();
         // 
         // _internalBox
         // 
         this._internalBox.Controls.Add(this._internalEdit);
         this._internalBox.Controls.Add(this._internalCancel);
         this._internalBox.Controls.Add(this._internalSave);
         this._internalBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this._internalBox.Location = new System.Drawing.Point(0, 3);
         this._internalBox.Name = "_internalBox";
         this._internalBox.Size = new System.Drawing.Size(246, 59);
         this._internalBox.TabIndex = 0;
         this._internalBox.TabStop = false;
         this._internalBox.Text = "text";
         // 
         // _internalEdit
         // 
         this._internalEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         appearance1.BorderColor = System.Drawing.Color.Transparent;
         appearance1.Image = global::XF.Controls.Properties.Resources.edit_16;
         this._internalEdit.Appearance = appearance1;
         this._internalEdit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
         this._internalEdit.Location = new System.Drawing.Point(215, -3);
         this._internalEdit.Name = "_internalEdit";
         this._internalEdit.ShowOutline = false;
         this._internalEdit.Size = new System.Drawing.Size(21, 22);
         this._internalEdit.TabIndex = 1;
         this._internalEdit.TabStop = false;
         this._internalEdit.UseAppStyling = false;
         this._internalEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._internalEdit.Click += new System.EventHandler(this.OnInternalEditClick);
         // 
         // _internalCancel
         // 
         this._internalCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         appearance3.BorderColor = System.Drawing.Color.Transparent;
         appearance3.Image = global::XF.Controls.Properties.Resources.Cancel2_32;
         this._internalCancel.Appearance = appearance3;
         this._internalCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
         this._internalCancel.Location = new System.Drawing.Point(214, -4);
         this._internalCancel.Name = "_internalCancel";
         this._internalCancel.ShowOutline = false;
         this._internalCancel.Size = new System.Drawing.Size(25, 24);
         this._internalCancel.TabIndex = 3;
         this._internalCancel.TabStop = false;
         this._internalCancel.UseAppStyling = false;
         this._internalCancel.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._internalCancel.Visible = false;
         this._internalCancel.Click += new System.EventHandler(this.OnCancelClick);
         // 
         // _internalSave
         // 
         this._internalSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         appearance2.BorderColor = System.Drawing.Color.Transparent;
         appearance2.Image = global::XF.Controls.Properties.Resources.disk_blue;
         appearance2.ImageHAlign = Infragistics.Win.HAlign.Center;
         appearance2.ImageVAlign = Infragistics.Win.VAlign.Middle;
         this._internalSave.Appearance = appearance2;
         this._internalSave.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
         this._internalSave.Location = new System.Drawing.Point(188, -4);
         this._internalSave.Name = "_internalSave";
         this._internalSave.ShowOutline = false;
         this._internalSave.Size = new System.Drawing.Size(25, 24);
         this._internalSave.TabIndex = 0;
         this._internalSave.UseAppStyling = false;
         this._internalSave.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._internalSave.Visible = false;
         this._internalSave.Click += new System.EventHandler(this.OnSaveClick);
         // 
         // _toolTip
         // 
         this._toolTip.AutoPopDelay = 5000;
         this._toolTip.InitialDelay = 100;
         this._toolTip.ReshowDelay = 100;
         // 
         // _spacer
         // 
         this._spacer.Dock = System.Windows.Forms.DockStyle.Top;
         this._spacer.Location = new System.Drawing.Point(0, 0);
         this._spacer.Name = "_spacer";
         this._spacer.Size = new System.Drawing.Size(246, 3);
         this._spacer.TabIndex = 1;
         // 
         // EditablePanel
         // 
         this.AllowDrop = true;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._internalBox);
         this.Controls.Add(this._spacer);
         this.Name = "EditablePanel";
         this.Size = new System.Drawing.Size(246, 62);
         this.Load += new System.EventHandler(this.OnLoad);
         this._internalBox.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox _internalBox;
      private Infragistics.Win.Misc.UltraButton _internalEdit;
      private Infragistics.Win.Misc.UltraButton _internalCancel;
      private Infragistics.Win.Misc.UltraButton _internalSave;
      private System.Windows.Forms.ToolTip _toolTip;
      private System.Windows.Forms.Panel _spacer;
   }
}

namespace XF.Controls {
   partial class ContactControl {
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         this._name = new XF.Controls.LabelControl();
         this._address = new XF.Controls.LabelControl();
         this._phone1 = new XF.Controls.EditableMaskedControl();
         this._phone2 = new XF.Controls.EditableMaskedControl();
         this._fax = new XF.Controls.EditableMaskedControl();
         this._email = new XF.Controls.LabelControl();
         this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
         this.SuspendLayout();
         // 
         // _name
         // 
         this._name.Label = "Name:";
         this._name.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._name.LabelText = "Name:";
         this._name.LabelWidth = 100;
         this._name.Location = new System.Drawing.Point(14, 33);
         this._name.Name = "_name";
         this._name.ReadOnly = false;
         this._name.Size = new System.Drawing.Size(440, 23);
         this._name.StyleSetName = "";
         this._name.TabIndex = 0;
         this._name.Value = "Value";
         this._name.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._name.ValueVAlign = Infragistics.Win.VAlign.Middle;
         this._name.WrapText = false;
         // 
         // _address
         // 
         this._address.Label = "Address:";
         this._address.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._address.LabelText = "Address:";
         this._address.LabelWidth = 100;
         this._address.Location = new System.Drawing.Point(14, 61);
         this._address.Name = "_address";
         this._address.ReadOnly = false;
         this._address.Size = new System.Drawing.Size(440, 46);
         this._address.StyleSetName = "";
         this._address.TabIndex = 1;
         this._address.Value = "Value";
         this._address.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._address.ValueVAlign = Infragistics.Win.VAlign.Top;
         this._address.WrapText = false;
         // 
         // _phone1
         // 
         this._phone1.ControlMode = XF.Controls.Mode.View;
         this._phone1.EditorHorizontalAlignment = Infragistics.Win.HAlign.Default;
         this._phone1.Label = "Phone:";
         this._phone1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._phone1.LabelText = "Phone:";
         this._phone1.LabelWidth = 100;
         this._phone1.Location = new System.Drawing.Point(14, 111);
         this._phone1.MaskType = XF.MaskedType.Phone;
         this._phone1.Name = "_phone1";
         this._phone1.ReadOnly = false;
         this._phone1.Size = new System.Drawing.Size(440, 23);
         this._phone1.TabIndex = 2;
         this._phone1.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._phone1.ValidationColor = System.Drawing.Color.Empty;
         this._phone1.Value = "";
         this._phone1.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         // 
         // _phone2
         // 
         this._phone2.ControlMode = XF.Controls.Mode.View;
         this._phone2.EditorHorizontalAlignment = Infragistics.Win.HAlign.Default;
         this._phone2.Label = "Phone:";
         this._phone2.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._phone2.LabelText = "Phone:";
         this._phone2.LabelWidth = 100;
         this._phone2.Location = new System.Drawing.Point(14, 138);
         this._phone2.MaskType = XF.MaskedType.Phone;
         this._phone2.Name = "_phone2";
         this._phone2.ReadOnly = false;
         this._phone2.Size = new System.Drawing.Size(440, 23);
         this._phone2.TabIndex = 3;
         this._phone2.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._phone2.ValidationColor = System.Drawing.Color.Empty;
         this._phone2.Value = "";
         this._phone2.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         // 
         // _fax
         // 
         this._fax.ControlMode = XF.Controls.Mode.View;
         this._fax.EditorHorizontalAlignment = Infragistics.Win.HAlign.Default;
         this._fax.Label = "Fax:";
         this._fax.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._fax.LabelText = "Fax:";
         this._fax.LabelWidth = 100;
         this._fax.Location = new System.Drawing.Point(14, 163);
         this._fax.MaskType = XF.MaskedType.Phone;
         this._fax.Name = "_fax";
         this._fax.ReadOnly = false;
         this._fax.Size = new System.Drawing.Size(440, 23);
         this._fax.TabIndex = 4;
         this._fax.TextBoxFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._fax.ValidationColor = System.Drawing.Color.Empty;
         this._fax.Value = "";
         this._fax.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         // 
         // _email
         // 
         this._email.Label = "Email:";
         this._email.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._email.LabelText = "Email:";
         this._email.LabelWidth = 100;
         this._email.Location = new System.Drawing.Point(14, 189);
         this._email.Name = "_email";
         this._email.ReadOnly = false;
         this._email.Size = new System.Drawing.Size(440, 23);
         this._email.StyleSetName = "";
         this._email.TabIndex = 5;
         this._email.Value = "Value";
         this._email.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._email.ValueVAlign = Infragistics.Win.VAlign.Middle;
         this._email.WrapText = false;
         // 
         // ultraButton1
         // 
         appearance1.Image = global::XF.Controls.Properties.Resources.envelope_icon;
         this.ultraButton1.Appearance = appearance1;
         this.ultraButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
         this.ultraButton1.Location = new System.Drawing.Point(435, 3);
         this.ultraButton1.Name = "ultraButton1";
         this.ultraButton1.Size = new System.Drawing.Size(28, 23);
         this.ultraButton1.TabIndex = 6;
         this.ultraButton1.UseAppStyling = false;
         this.ultraButton1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         // 
         // ContactControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.SystemColors.Info;
         this.ClientSize = new System.Drawing.Size(475, 219);
         this.Controls.Add(this.ultraButton1);
         this.Controls.Add(this._email);
         this.Controls.Add(this._fax);
         this.Controls.Add(this._phone2);
         this.Controls.Add(this._phone1);
         this.Controls.Add(this._address);
         this.Controls.Add(this._name);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ContactControl";
         this.ShowIcon = false;
         this.Text = "Contact Information";
         this.ResumeLayout(false);

      }

      #endregion

      private LabelControl _name;
      private LabelControl _address;
      private EditableMaskedControl _phone1;
      private EditableMaskedControl _phone2;
      private EditableMaskedControl _fax;
      private LabelControl _email;
      private Infragistics.Win.Misc.UltraButton ultraButton1;
   }
}
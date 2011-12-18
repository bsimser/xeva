namespace XF.Controls {
   partial class DatePickerControl {
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
         Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
         this._label = new Infragistics.Win.Misc.UltraLabel();
         this._datePicker = new System.Windows.Forms.DateTimePicker();
         this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // _label
         // 
         appearance13.TextVAlignAsString = "Middle";
         this._label.Appearance = appearance13;
         this._label.Location = new System.Drawing.Point(3, 0);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(100, 21);
         this._label.TabIndex = 20;
         this._label.Text = "Label";
         // 
         // _datePicker
         // 
         this._datePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._datePicker.Location = new System.Drawing.Point(106, 0);
         this._datePicker.Name = "_datePicker";
         this._datePicker.Size = new System.Drawing.Size(295, 20);
         this._datePicker.TabIndex = 21;
         // 
         // _errorProvider
         // 
         this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this._errorProvider.ContainerControl = this;
         // 
         // DatePickerControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._datePicker);
         this.Controls.Add(this._label);
         this.Name = "DatePickerControl";
         this.Size = new System.Drawing.Size(417, 22);
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraLabel _label;
      private System.Windows.Forms.DateTimePicker _datePicker;
      private System.Windows.Forms.ErrorProvider _errorProvider;
   }
}

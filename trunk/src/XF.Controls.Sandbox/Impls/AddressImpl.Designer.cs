namespace XF.Controls.Sandbox.Impls {
   partial class AddressImpl {
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
         this._addressControl = new XF.Controls.AddressControl();
         this._addressBindingSource = new System.Windows.Forms.BindingSource(this.components);
         ((System.ComponentModel.ISupportInitialize)(this._addressBindingSource)).BeginInit();
         this.SuspendLayout();
         // 
         // _addressControl
         // 
         this._addressControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this._addressControl.Label = "Address:";
         this._addressControl.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._addressControl.LabelHAlign = Infragistics.Win.HAlign.Default;
         this._addressControl.LabelWidth = 100;
         this._addressControl.Location = new System.Drawing.Point(0, 0);
         this._addressControl.Name = "_addressControl";
         this._addressControl.ReadOnly = false;
         this._addressControl.Size = new System.Drawing.Size(476, 46);
         this._addressControl.TabIndex = 0;
         this._addressControl.Value = null;
         this._addressControl.ValueLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         // 
         // _addressBindingSource
         // 
         this._addressBindingSource.DataSource = typeof(XF.Controls.Sandbox.AddressMessage);
         // 
         // AddressImpl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._addressControl);
         this.Name = "AddressImpl";
         this.Size = new System.Drawing.Size(476, 265);
         this.Load += new System.EventHandler(this.OnFormLoad);
         ((System.ComponentModel.ISupportInitialize)(this._addressBindingSource)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.BindingSource _addressBindingSource;
      private AddressControl _addressControl;
   }
}

namespace XF.Controls
{
   partial class PlaceHolderControl
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
         this.components = new System.ComponentModel.Container();
         this._placeHolderPanel = new System.Windows.Forms.Panel();
         this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // _placeHolderPanel
         // 
         this._placeHolderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._placeHolderPanel.Location = new System.Drawing.Point(110, 1);
         this._placeHolderPanel.Margin = new System.Windows.Forms.Padding(0);
         this._placeHolderPanel.Name = "_placeHolderPanel";
         this._placeHolderPanel.Size = new System.Drawing.Size(237, 21);
         this._placeHolderPanel.TabIndex = 1;
         // 
         // _errorProvider
         // 
         this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this._errorProvider.ContainerControl = this;
         // 
         // PlaceHolderControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._placeHolderPanel);
         this.Name = "PlaceHolderControl";
         this.Enter += new System.EventHandler(this.OnEnter);
         this.Controls.SetChildIndex(this._placeHolderPanel, 0);
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Panel _placeHolderPanel;
      private System.Windows.Forms.ErrorProvider _errorProvider;
   }
}

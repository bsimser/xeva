namespace XF.Controls
{
   public partial class LabelControl
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
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         this._valueLabel = new Infragistics.Win.Misc.UltraLabel();
         this.SuspendLayout();
         // 
         // _valueLabel
         // 
         this._valueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         appearance1.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         appearance1.TextVAlignAsString = "Middle";
         this._valueLabel.Appearance = appearance1;
         this._valueLabel.Location = new System.Drawing.Point(110, 1);
         this._valueLabel.Name = "_valueLabel";
         this._valueLabel.Size = new System.Drawing.Size(406, 21);
         this._valueLabel.TabIndex = 1;
         this._valueLabel.Text = "Value";
         this._valueLabel.WrapText = false;
         this._valueLabel.MouseLeave += new System.EventHandler(this.OnLabelMouseLeave);
         this._valueLabel.Click += new System.EventHandler(this.OnLabelClick);
         // 
         // LabelControl
         // 
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
         this.Controls.Add(this._valueLabel);
         this.Name = "LabelControl";
         this.Size = new System.Drawing.Size(519, 23);
         this.MouseLeave += new System.EventHandler(this.OnLabelMouseLeave);
         this.Controls.SetChildIndex(this._valueLabel, 0);
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraLabel _valueLabel;
   }
}

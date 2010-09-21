namespace XF.Controls
{
   partial class XFControlBase
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
         this._label = new Infragistics.Win.Misc.UltraLabel();
         this.SuspendLayout();
         // 
         // _label
         // 
         appearance1.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         appearance1.TextVAlignAsString = "Middle";
         this._label.Appearance = appearance1;
         this._label.Location = new System.Drawing.Point(4, 1);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(100, 21);
         this._label.TabIndex = 0;
         this._label.Text = "Label";
         this._label.WrapText = false;
         this._label.Click += new System.EventHandler(this.OnClicked);
         // 
         // XFControlBase
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._label);
         this.Name = "XFControlBase";
         this.Size = new System.Drawing.Size(350, 23);
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraLabel _label;
   }
}

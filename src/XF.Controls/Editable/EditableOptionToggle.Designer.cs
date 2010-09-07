namespace XF.Controls {
   partial class EditableOptionToggle {
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
         Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
         Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
         Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         this._optionToggle = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
         this._label = new Infragistics.Win.Misc.UltraLabel();
         this._valueLabel = new Infragistics.Win.Misc.UltraLabel();
         this._internalEdit = new Infragistics.Win.Misc.UltraButton();
         ((System.ComponentModel.ISupportInitialize)(this._optionToggle)).BeginInit();
         this.SuspendLayout();
         // 
         // _optionToggle
         // 
         this._optionToggle.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
         this._optionToggle.CheckedIndex = 0;
         this._optionToggle.GlyphStyle = Infragistics.Win.GlyphStyle.Office2007;
         valueListItem1.DataValue = "ItemOne";
         valueListItem1.DisplayText = "ItemOne";
         valueListItem2.DataValue = "ItemTwo";
         valueListItem2.DisplayText = "ItemTwo";
         this._optionToggle.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2});
         this._optionToggle.ItemSpacingHorizontal = 20;
         this._optionToggle.ItemSpacingVertical = 8;
         this._optionToggle.Location = new System.Drawing.Point(106, 0);
         this._optionToggle.Name = "_optionToggle";
         this._optionToggle.Size = new System.Drawing.Size(224, 21);
         this._optionToggle.TabIndex = 0;
         this._optionToggle.Text = "ItemOne";
         this._optionToggle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
         this._optionToggle.Click += new System.EventHandler(this.OnOptionToggleClick);
         this._optionToggle.ValueChanged += new System.EventHandler(this.OnValueChanged);
         // 
         // _label
         // 
         appearance3.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         appearance3.TextVAlignAsString = "Middle";
         this._label.Appearance = appearance3;
         this._label.Location = new System.Drawing.Point(3, 0);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(100, 21);
         this._label.TabIndex = 2;
         this._label.Text = "Label";
         this._label.WrapText = false;
         // 
         // _valueLabel
         // 
         this._valueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         appearance5.TextVAlignAsString = "Middle";
         this._valueLabel.Appearance = appearance5;
         this._valueLabel.Location = new System.Drawing.Point(106, 0);
         this._valueLabel.Name = "_valueLabel";
         this._valueLabel.Size = new System.Drawing.Size(224, 21);
         this._valueLabel.TabIndex = 4;
         this._valueLabel.Text = "Value Label";
         this._valueLabel.Visible = false;
         this._valueLabel.WrapText = false;
         // 
         // _internalEdit
         // 
         this._internalEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         appearance1.BorderColor = System.Drawing.Color.Transparent;
         appearance1.Image = global::XF.Controls.Properties.Resources.edit_16;
         this._internalEdit.Appearance = appearance1;
         this._internalEdit.ButtonStyle = Infragistics.Win.UIElementButtonStyle.FlatBorderless;
         this._internalEdit.Location = new System.Drawing.Point(329, 0);
         this._internalEdit.Name = "_internalEdit";
         this._internalEdit.ShowOutline = false;
         this._internalEdit.Size = new System.Drawing.Size(21, 22);
         this._internalEdit.TabIndex = 7;
         this._internalEdit.UseAppStyling = false;
         this._internalEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._internalEdit.Visible = false;
         this._internalEdit.Click += new System.EventHandler(this.OnEditClick);
         // 
         // EditableOptionToggle
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._internalEdit);
         this.Controls.Add(this._label);
         this.Controls.Add(this._optionToggle);
         this.Controls.Add(this._valueLabel);
         this.Name = "EditableOptionToggle";
         this.Size = new System.Drawing.Size(350, 24);
         ((System.ComponentModel.ISupportInitialize)(this._optionToggle)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.UltraWinEditors.UltraOptionSet _optionToggle;
      private Infragistics.Win.Misc.UltraLabel _label;
      private Infragistics.Win.Misc.UltraLabel _valueLabel;
      private Infragistics.Win.Misc.UltraButton _internalEdit;
   }
}

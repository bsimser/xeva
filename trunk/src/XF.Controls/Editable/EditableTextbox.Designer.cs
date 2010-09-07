namespace XF.Controls {
   partial class EditableTextbox {
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
         Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
         this._label = new Infragistics.Win.Misc.UltraLabel();
         this._valueLabel = new Infragistics.Win.Misc.UltraLabel();
         this._requiredLabel = new Infragistics.Win.Misc.UltraLabel();
         this._textboxValue = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
         this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         this._internalEdit = new Infragistics.Win.Misc.UltraButton();
         ((System.ComponentModel.ISupportInitialize)(this._textboxValue)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // _label
         // 
         appearance3.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         appearance3.TextVAlignAsString = "Middle";
         this._label.Appearance = appearance3;
         this._label.Location = new System.Drawing.Point(4, 1);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(100, 21);
         this._label.TabIndex = 0;
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
         this._valueLabel.Location = new System.Drawing.Point(110, 1);
         this._valueLabel.Name = "_valueLabel";
         this._valueLabel.Size = new System.Drawing.Size(215, 21);
         this._valueLabel.TabIndex = 2;
         this._valueLabel.Text = "Value Label";
         this._valueLabel.Visible = false;
         this._valueLabel.WrapText = false;
         // 
         // _requiredLabel
         // 
         this._requiredLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         appearance4.ForeColor = System.Drawing.Color.Red;
         appearance4.TextHAlignAsString = "Center";
         appearance4.TextVAlignAsString = "Middle";
         this._requiredLabel.Appearance = appearance4;
         this._requiredLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._requiredLabel.Location = new System.Drawing.Point(333, 0);
         this._requiredLabel.Name = "_requiredLabel";
         this._requiredLabel.Size = new System.Drawing.Size(17, 23);
         this._requiredLabel.TabIndex = 4;
         this._requiredLabel.Text = "*";
         this._requiredLabel.Visible = false;
         // 
         // _textboxValue
         // 
         this._textboxValue.AlwaysInEditMode = true;
         this._textboxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._textboxValue.AutoSize = false;
         this._textboxValue.Location = new System.Drawing.Point(110, 1);
         this._textboxValue.Name = "_textboxValue";
         this._textboxValue.Size = new System.Drawing.Size(215, 21);
         this._textboxValue.TabIndex = 5;
         this._textboxValue.Visible = false;
         this._textboxValue.ValueChanged += new System.EventHandler(this.OnValueChanged);
         this._textboxValue.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.OnEditorButtonClicked);
         // 
         // _errorProvider
         // 
         this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this._errorProvider.ContainerControl = this;
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
         this._internalEdit.TabIndex = 6;
         this._internalEdit.UseAppStyling = false;
         this._internalEdit.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._internalEdit.Visible = false;
         this._internalEdit.Click += new System.EventHandler(this.OnEditClick);
         // 
         // EditableTextbox
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._requiredLabel);
         this.Controls.Add(this._valueLabel);
         this.Controls.Add(this._label);
         this.Controls.Add(this._textboxValue);
         this.Controls.Add(this._internalEdit);
         this.Name = "EditableTextbox";
         this.Size = new System.Drawing.Size(350, 23);
         ((System.ComponentModel.ISupportInitialize)(this._textboxValue)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraLabel _label;
      private Infragistics.Win.Misc.UltraLabel _valueLabel;
      private Infragistics.Win.Misc.UltraLabel _requiredLabel;
      private Infragistics.Win.UltraWinEditors.UltraTextEditor _textboxValue;
      private System.Windows.Forms.ErrorProvider _errorProvider;
      private Infragistics.Win.Misc.UltraButton _internalEdit;
   }
}

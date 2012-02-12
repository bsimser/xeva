
namespace XF.Controls {
   partial class SelectorControl {
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
         Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("LookupMessage", -1);
         Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
         Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatusColor");
         Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
         Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DisplayOrder");
         Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
         this._paramaterButton = new Infragistics.Win.Misc.UltraDropDownButton();
         this.ultraPopupControlContainer1 = new Infragistics.Win.Misc.UltraPopupControlContainer(this.components);
         this._parameterGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
         this._parameterBindingSource = new System.Windows.Forms.BindingSource(this.components);
         this._label = new Infragistics.Win.Misc.UltraLabel();
         this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this._parameterGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._parameterBindingSource)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // _paramaterButton
         // 
         this._paramaterButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._paramaterButton.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsVistaButton;
         this._paramaterButton.DialogResult = System.Windows.Forms.DialogResult.OK;
         this._paramaterButton.Location = new System.Drawing.Point(109, 0);
         this._paramaterButton.Name = "_paramaterButton";
         this._paramaterButton.PopupItemKey = "_parameterGrid";
         this._paramaterButton.PopupItemProvider = this.ultraPopupControlContainer1;
         this._paramaterButton.ShowFocusRect = false;
         this._paramaterButton.Size = new System.Drawing.Size(295, 20);
         this._paramaterButton.Style = Infragistics.Win.Misc.SplitButtonDisplayStyle.DropDownButtonOnly;
         this._paramaterButton.StyleSetName = "PrintControls";
         this._paramaterButton.TabIndex = 18;
         this._paramaterButton.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
         this._paramaterButton.Leave += new System.EventHandler(this.OnButtonLostFocus);
         this._paramaterButton.ClosedUp += new System.EventHandler(this.OnClosedUp);
         // 
         // ultraPopupControlContainer1
         // 
         this.ultraPopupControlContainer1.PopupControl = this._parameterGrid;
         // 
         // _parameterGrid
         // 
         this._parameterGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._parameterGrid.DataSource = this._parameterBindingSource;
         appearance1.BackColor = System.Drawing.SystemColors.Window;
         appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
         this._parameterGrid.DisplayLayout.Appearance = appearance1;
         this._parameterGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
         ultraGridBand1.ColHeadersVisible = false;
         ultraGridColumn1.Header.VisiblePosition = 1;
         ultraGridColumn1.Hidden = true;
         ultraGridColumn1.Width = 214;
         ultraGridColumn2.Header.VisiblePosition = 2;
         ultraGridColumn2.Hidden = true;
         ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
         ultraGridColumn3.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
         ultraGridColumn3.Header.VisiblePosition = 0;
         ultraGridColumn3.RowLayoutColumnInfo.OriginX = 1;
         ultraGridColumn3.RowLayoutColumnInfo.OriginY = 0;
         ultraGridColumn3.RowLayoutColumnInfo.SpanX = 2;
         ultraGridColumn3.RowLayoutColumnInfo.SpanY = 2;
         ultraGridColumn3.Width = 213;
         ultraGridColumn4.Header.VisiblePosition = 3;
         ultraGridColumn4.Hidden = true;
         ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
         ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
         ultraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
         ultraGridBand1.Override.MaxSelectedRows = 1;
         ultraGridBand1.UseRowLayout = true;
         this._parameterGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
         this._parameterGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
         this._parameterGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
         appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
         appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
         appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
         appearance14.BorderColor = System.Drawing.SystemColors.Window;
         this._parameterGrid.DisplayLayout.GroupByBox.Appearance = appearance14;
         appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
         this._parameterGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
         this._parameterGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
         appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
         appearance16.BackColor2 = System.Drawing.SystemColors.Control;
         appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
         appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
         this._parameterGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
         this._parameterGrid.DisplayLayout.MaxColScrollRegions = 1;
         this._parameterGrid.DisplayLayout.MaxRowScrollRegions = 1;
         appearance17.BackColor = System.Drawing.SystemColors.Window;
         appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
         this._parameterGrid.DisplayLayout.Override.ActiveCellAppearance = appearance17;
         this._parameterGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
         this._parameterGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
         appearance18.BackColor = System.Drawing.SystemColors.Window;
         this._parameterGrid.DisplayLayout.Override.CardAreaAppearance = appearance18;
         appearance20.BorderColor = System.Drawing.Color.Silver;
         appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         this._parameterGrid.DisplayLayout.Override.CellAppearance = appearance20;
         this._parameterGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
         this._parameterGrid.DisplayLayout.Override.CellPadding = 0;
         appearance21.BackColor = System.Drawing.SystemColors.Control;
         appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
         appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
         appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
         appearance21.BorderColor = System.Drawing.SystemColors.Window;
         this._parameterGrid.DisplayLayout.Override.GroupByRowAppearance = appearance21;
         appearance22.TextHAlignAsString = "Left";
         this._parameterGrid.DisplayLayout.Override.HeaderAppearance = appearance22;
         this._parameterGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
         this._parameterGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
         appearance23.BackColor = System.Drawing.SystemColors.Window;
         appearance23.BorderColor = System.Drawing.Color.Silver;
         this._parameterGrid.DisplayLayout.Override.RowAppearance = appearance23;
         this._parameterGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
         appearance24.BackColor = System.Drawing.SystemColors.Highlight;
         appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
         this._parameterGrid.DisplayLayout.Override.SelectedRowAppearance = appearance24;
         appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
         this._parameterGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
         this._parameterGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
         this._parameterGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
         this._parameterGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
         this._parameterGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._parameterGrid.Location = new System.Drawing.Point(177, 30);
         this._parameterGrid.Name = "_parameterGrid";
         this._parameterGrid.Size = new System.Drawing.Size(240, 109);
         this._parameterGrid.TabIndex = 20;
         this._parameterGrid.Visible = false;
         this._parameterGrid.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnParameterSelected);
         // 
         // _parameterBindingSource
         // 
         this._parameterBindingSource.DataSource = typeof(XF.LookupMessage);
         // 
         // _label
         // 
         appearance13.TextVAlignAsString = "Middle";
         this._label.Appearance = appearance13;
         this._label.Location = new System.Drawing.Point(3, 0);
         this._label.Name = "_label";
         this._label.Size = new System.Drawing.Size(100, 21);
         this._label.TabIndex = 19;
         this._label.Text = "Label";
         // 
         // _errorProvider
         // 
         this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this._errorProvider.ContainerControl = this;
         // 
         // SelectorControl
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._paramaterButton);
         this.Controls.Add(this._label);
         this.Controls.Add(this._parameterGrid);
         this.Name = "SelectorControl";
         this.Size = new System.Drawing.Size(417, 22);
         this.Load += new System.EventHandler(this.OnControlLoad);
         this.Resize += new System.EventHandler(this.OnResized);
         ((System.ComponentModel.ISupportInitialize)(this._parameterGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._parameterBindingSource)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraDropDownButton _paramaterButton;
      private Infragistics.Win.Misc.UltraLabel _label;
      private Infragistics.Win.UltraWinGrid.UltraGrid _parameterGrid;
      private Infragistics.Win.Misc.UltraPopupControlContainer ultraPopupControlContainer1;
      private System.Windows.Forms.BindingSource _parameterBindingSource;
      private System.Windows.Forms.ErrorProvider _errorProvider;
   }
}

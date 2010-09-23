namespace XF.Controls {
   partial class DropdownSelector {
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
         Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected", 0);
         Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
         Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
         this._internalButton = new Infragistics.Win.Misc.UltraDropDownButton();
         this._popupControlContainer = new Infragistics.Win.Misc.UltraPopupControlContainer(this.components);
         this._itemsGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
         this._itemsBS = new System.Windows.Forms.BindingSource(this.components);
         this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
         ((System.ComponentModel.ISupportInitialize)(this._itemsGrid)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._itemsBS)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
         this.SuspendLayout();
         // 
         // _internalButton
         // 
         this._internalButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._internalButton.Location = new System.Drawing.Point(110, 0);
         this._internalButton.Name = "_internalButton";
         this._internalButton.PopupItemKey = "_itemsGrid";
         this._internalButton.PopupItemProvider = this._popupControlContainer;
         this._internalButton.Size = new System.Drawing.Size(223, 22);
         this._internalButton.Style = Infragistics.Win.Misc.SplitButtonDisplayStyle.DropDownButtonOnly;
         this._internalButton.TabIndex = 1;
         this._internalButton.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
         this._internalButton.DroppingDown += new System.ComponentModel.CancelEventHandler(this.OnOpeningUp);
         this._internalButton.Leave += new System.EventHandler(this.OnLostFocus);
         this._internalButton.ClosedUp += new System.EventHandler(this.OnClosedUp);
         // 
         // _popupControlContainer
         // 
         this._popupControlContainer.PopupControl = this._itemsGrid;
         // 
         // _itemsGrid
         // 
         this._itemsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this._itemsGrid.DataSource = this._itemsBS;
         appearance1.BackColor = System.Drawing.SystemColors.Window;
         appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
         this._itemsGrid.DisplayLayout.Appearance = appearance1;
         this._itemsGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns;
         ultraGridBand1.ColHeadersVisible = false;
         ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
         ultraGridColumn1.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
         ultraGridColumn1.Header.VisiblePosition = 3;
         ultraGridColumn1.Hidden = true;
         ultraGridColumn1.Width = 214;
         ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
         ultraGridColumn2.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
         ultraGridColumn2.Header.VisiblePosition = 1;
         ultraGridColumn2.Hidden = true;
         ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
         ultraGridColumn3.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
         ultraGridColumn3.Header.VisiblePosition = 2;
         ultraGridColumn3.RowLayoutColumnInfo.OriginX = 1;
         ultraGridColumn3.RowLayoutColumnInfo.OriginY = 0;
         ultraGridColumn3.RowLayoutColumnInfo.SpanX = 2;
         ultraGridColumn3.RowLayoutColumnInfo.SpanY = 2;
         ultraGridColumn3.Width = 305;
         ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
         ultraGridColumn4.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
         ultraGridColumn4.Header.VisiblePosition = 4;
         ultraGridColumn4.Hidden = true;
         ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
         ultraGridColumn5.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
         ultraGridColumn5.DataType = typeof(bool);
         ultraGridColumn5.Header.Caption = "";
         ultraGridColumn5.Header.VisiblePosition = 0;
         ultraGridColumn5.Hidden = true;
         ultraGridColumn5.RowLayoutColumnInfo.OriginX = 0;
         ultraGridColumn5.RowLayoutColumnInfo.OriginY = 0;
         ultraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(43, 0);
         ultraGridColumn5.RowLayoutColumnInfo.SpanX = 1;
         ultraGridColumn5.RowLayoutColumnInfo.SpanY = 2;
         ultraGridColumn5.Width = 88;
         ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
         ultraGridBand1.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
         ultraGridBand1.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
         ultraGridBand1.Override.MaxSelectedRows = 1;
         this._itemsGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
         this._itemsGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
         this._itemsGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
         appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
         appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
         appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
         appearance2.BorderColor = System.Drawing.SystemColors.Window;
         this._itemsGrid.DisplayLayout.GroupByBox.Appearance = appearance2;
         appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
         this._itemsGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
         this._itemsGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
         appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
         appearance4.BackColor2 = System.Drawing.SystemColors.Control;
         appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
         appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
         this._itemsGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
         this._itemsGrid.DisplayLayout.MaxColScrollRegions = 1;
         this._itemsGrid.DisplayLayout.MaxRowScrollRegions = 1;
         appearance5.BackColor = System.Drawing.SystemColors.Window;
         appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
         this._itemsGrid.DisplayLayout.Override.ActiveCellAppearance = appearance5;
         appearance6.BackColor = System.Drawing.SystemColors.Highlight;
         appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
         this._itemsGrid.DisplayLayout.Override.ActiveRowAppearance = appearance6;
         this._itemsGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
         this._itemsGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
         appearance7.BackColor = System.Drawing.SystemColors.Window;
         this._itemsGrid.DisplayLayout.Override.CardAreaAppearance = appearance7;
         appearance8.BorderColor = System.Drawing.Color.Silver;
         appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
         this._itemsGrid.DisplayLayout.Override.CellAppearance = appearance8;
         this._itemsGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
         this._itemsGrid.DisplayLayout.Override.CellPadding = 0;
         appearance9.BackColor = System.Drawing.SystemColors.Control;
         appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
         appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
         appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
         appearance9.BorderColor = System.Drawing.SystemColors.Window;
         this._itemsGrid.DisplayLayout.Override.GroupByRowAppearance = appearance9;
         appearance10.TextHAlignAsString = "Left";
         this._itemsGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
         this._itemsGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
         this._itemsGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
         appearance11.BackColor = System.Drawing.SystemColors.Window;
         appearance11.BorderColor = System.Drawing.Color.Silver;
         this._itemsGrid.DisplayLayout.Override.RowAppearance = appearance11;
         this._itemsGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
         appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
         this._itemsGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
         this._itemsGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
         this._itemsGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
         this._itemsGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
         this._itemsGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this._itemsGrid.Location = new System.Drawing.Point(110, 30);
         this._itemsGrid.Name = "_itemsGrid";
         this._itemsGrid.Size = new System.Drawing.Size(307, 80);
         this._itemsGrid.TabIndex = 21;
         this._itemsGrid.Visible = false;
         this._itemsGrid.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnItemSelected);
         // 
         // _itemsBS
         // 
         this._itemsBS.DataSource = typeof(XF.LookupMessage);
         // 
         // _errorProvider
         // 
         this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
         this._errorProvider.ContainerControl = this;
         // 
         // DropdownSelector
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.Controls.Add(this._itemsGrid);
         this.Controls.Add(this._internalButton);
         this.Name = "DropdownSelector";
         this.Load += new System.EventHandler(this.OnControlLoad);
         this.Controls.SetChildIndex(this._internalButton, 0);
         this.Controls.SetChildIndex(this._itemsGrid, 0);
         ((System.ComponentModel.ISupportInitialize)(this._itemsGrid)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._itemsBS)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private Infragistics.Win.Misc.UltraDropDownButton _internalButton;
      private Infragistics.Win.UltraWinGrid.UltraGrid _itemsGrid;
      private Infragistics.Win.Misc.UltraPopupControlContainer _popupControlContainer;
      private System.Windows.Forms.ErrorProvider _errorProvider;
      private System.Windows.Forms.BindingSource _itemsBS;
   }
}

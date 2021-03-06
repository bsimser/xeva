﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Infragistics.Win;
using XF;
using XF.UI.Smart;
using Color = System.Drawing.Color;

namespace XF.Controls {
   public partial class EditableComboEditor : UserControl, IEditable {
      private bool _required;
      private object _nullValue;
      private Mode _controlMode;

      public EditableComboEditor() {
         InitializeComponent();
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;

         this.Resize += (EditableResize);
      }

      public event EventHandler EditClicked;
      public event EventHandler DroppingDown;
      public event EventHandler ValueChanged;
      public event EventHandler SelectionChanged;

      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _comboValue.Visible = false;
            _requiredLabel.Visible = false;
         }
         else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _comboValue.Visible = true;
            _requiredLabel.Visible = _required;
         }
      }
      public void SetToEdit(bool isInEdit) {
         ControlMode = isInEdit ? Mode.Edit : Mode.View;
         if (ControlMode == Mode.Edit)
            _comboValue.SelectedIndex = _comboValue.FindStringExact(InputValue);
      }

      public void EnableEdit(bool isInEdit) {
         _internalEdit.Visible = isInEdit;
         if (isInEdit) _internalEdit.BringToFront();
      }

      public void ResetValue() {
         Value = InputValue;
      }

      public void SaveValue() {
         if(_comboValue.SelectedItem == null) return;
         InputValue = _comboValue.SelectedItem.DisplayText;
      }

      public object EditedValue {
         get {
            return _comboValue.SelectedItem != null ? _comboValue.SelectedItem.DataValue : Value;
         }
      }

      public IEditable ToIEditable(string controlLabel, object controlValue, List<IListMessage> lookupList) {
         Label = controlLabel;
         Value = controlValue;
         DisplayMember = "Name";
         ValueMember = "ID";
         BindingSource = new BindingSource { DataSource = lookupList };
         Bind();

         return this;
      }

      private void EditableResize(object sender, EventArgs e) {
         SetControlPositions();
      }

      private void SetControlPositions() {
         int left = _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         var valueLabel = _valueLabel as Control;
         valueLabel.Left = left;
         valueLabel.Width = Width - left - (ControlConstants.CONTROL_PADDING);

         var comboValue = _comboValue as Control;
         comboValue.Left = left;
         comboValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public string InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnSelectionChanged(object sender, EventArgs e) {
         if (_comboValue.SelectedItem == null) return;

         if (_comboValue.SelectedItem.DataValue == null && !IsDirty) return;

         if (_comboValue.SelectedItem.DataValue == null && InputValue == null) {
            IsDirty = false;
            _valueLabel.Text = string.Empty;
            return;
         }

         IsDirty = true;
         _valueLabel.Text = _comboValue.SelectedItem.DisplayText;

         if (SelectionChanged != null)
            SelectionChanged(this, EventArgs.Empty);
      }

      public void OnValueChanged(object sender, EventArgs e) {
         if (_comboValue.Value == null && !IsDirty) return;

         if (_comboValue.Value == null && InputValue == null) {
            IsDirty = false;
            _valueLabel.Text = string.Empty;
            return;
         }

         IsDirty = true;
         _valueLabel.Text = _comboValue.Text;

         if (ValueChanged != null)
            ValueChanged(this, EventArgs.Empty);
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _comboValue.BackColor = ValidationColor;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _comboValue.ResetBackColor();
      }

      public Color ControlBackcolor {
         set { BackColor = value; }
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual Font LabelFont {
         get { return _label.Font; }
         set { _label.Font = value; }
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string LabelText {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public int LabelWidth {
         get { return _label.Width; }
         set {
            _label.Width = value;
            SetControlPositions();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public HAlign ValueHAlign {
         get { return _valueLabel.Appearance.TextHAlign; }
         set { _valueLabel.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Mode ControlMode {
         get {
            return _controlMode;
         }
         set {
            _controlMode = value;
            SetupControlMode();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      [DefaultValue(true)]
      public bool LabelVisible {
         get { return _label.Visible; }
         set { _label.Visible = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public bool Required {
         set {
            _required = value;
            if (ControlMode == Mode.Edit)
               _requiredLabel.Visible = value;
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public HAlign EditorHorizontalAlignment {
         get { return _comboValue.Appearance.TextHAlign; }
         set { _comboValue.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ComboEditorFont {
         get { return _comboValue.Font; }
         set { _comboValue.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor { get; set; }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string DisplayMember { get; set; }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string ValueMember { get; set; }

      [Bindable(true)]
      [Browsable(false)]
      public object Value {
         get {
            return _comboValue.SelectedItem != null ? _comboValue.SelectedItem.DataValue : NullValue;
         }
         set {
            InputValue = value != null ? value.ToString() : string.Empty;
            _valueLabel.Text = InputValue;
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public object NullValue {
         get {
            return _nullValue;
         }
         set {
            _nullValue = value;
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string NullValueText {
         get {
            return _comboValue.NullText;
         }
         set {
            _comboValue.NullText = value;
         }
      }

      private BindingSource _bindingSource;

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public BindingSource BindingSource {
         get { return _bindingSource; }
         set { _bindingSource = value; }
      }

      public void Bind() {
         _comboValue.DataSource = BindingSource;
         _comboValue.DisplayMember = DisplayMember;
         _comboValue.ValueMember = ValueMember;
         _comboValue.Value = null;
         _comboValue.DataBind();
      }

      public bool ReadOnly { get; set; }

      public void SetSelectedItem(string selection) {
         _comboValue.SelectedIndex = _comboValue.FindStringExact(selection);
      }

      public void SetSelectedByPartialMatch(string selection) {
         _comboValue.SelectedIndex =
            _comboValue.Items.IndexOf(
               _comboValue.Items.Cast<ValueListItem>().FirstOrDefault(x => x.DisplayText.Contains(selection)));

      }

      public void ClearControl() {
         _comboValue.SelectedItem = null;
         _comboValue.DataSource = null;
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }

      private void OnControlDroppingDown(object sender, CancelEventArgs e) {
         if (DroppingDown != null)
            DroppingDown(this, new EventArgs());
      }
   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using XF;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class EditableCurrencyEditor : UserControl, IEditable {
      private bool _required;
      private Mode _controlMode;

      public EditableCurrencyEditor() {
         InitializeComponent();
         AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;

         Resize += (EditableResize);
      }

      public event EventHandler EditClicked;

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _currencyEditor.BackColor = ValidationColor;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _currencyEditor.ResetBackColor();
      }

      public void SetToEdit(bool isInEdit) {
         ControlMode = isInEdit ? Mode.Edit : Mode.View;
      }

      public void EnableEdit(bool isInEdit) {
         _internalEdit.Visible = isInEdit;
         if (isInEdit) _internalEdit.BringToFront();
      }

      public void SaveValue() {
         InputValue = (decimal)Value;
      }

      public void ResetValue() {
         Value = InputValue;
      }

      public object EditedValue {
         get { return Value; }
      }

      public IEditable ToIEditable(string controlLabel, object controlValue, List<IListMessage> lookupList) {
         Label = controlLabel;
         Value = controlValue;

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

         var textboxValue = _currencyEditor as Control;
         textboxValue.Left = left;
         textboxValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public decimal InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnValueChanged(object sender, System.EventArgs e) {
         //if (_currencyEditor.Value == null && !IsDirty) return;

         //if (_currencyEditor.Value == null && InputValue == null) {
         //   IsDirty = false;
         //   _valueLabel.Text = string.Empty;
         //   return;
         //}

         //IsDirty = true;
         //var value = _currencyEditor.Value != null ? _currencyEditor.Value.ToString() : string.Empty;
         _valueLabel.Text = _currencyEditor.Value.ToString("c");
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      [Bindable(true)]
      //[Browsable(false)]
      public object Value {
         get {
            return _currencyEditor.Value;
         }
         set {
            if (value == null) return;
            InputValue = Convert.ToDecimal(value.ToString().Replace("$", "").Replace("_", ""));

            _valueLabel.Text = InputValue.ToString("c");
            _currencyEditor.Value = InputValue;
         }
      }

      public Color ControlBackcolor {
         set { BackColor = value; }
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
         get { return _currencyEditor.Appearance.TextHAlign; }
         set { _currencyEditor.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font TextBoxFont {
         get { return _currencyEditor.Font; }
         set { _currencyEditor.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }


      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor { get; set; }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public HAlign LabelHAlign {
         get { return _label.Appearance.TextHAlign; }
         set { _label.Appearance.TextHAlign = value; }
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

      public bool ReadOnly { get; set; }

      private void OnClicked(object sender, EventArgs e) {
         _currencyEditor.Focus();
      }

      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _currencyEditor.Visible = false;
            _requiredLabel.Visible = false;
         }
         else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _currencyEditor.Visible = true;
            _requiredLabel.Visible = _required;
         }
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }
   }
}

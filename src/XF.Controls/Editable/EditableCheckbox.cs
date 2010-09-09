using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using XF;
using XF.UI.Smart;

namespace XF.Controls {
   [ToolboxItem(true)]
   public partial class EditableCheckbox : UserControl, IEditable {
      private Color _validationColor;
      private bool _required;
      private Mode _controlMode;

      public EditableCheckbox() {
         InitializeComponent();
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;
         this.Resize += (EditableResize);
      }

      public event EventHandler EditClicked;

      private void EditableResize(object sender, EventArgs e) {
         SetControlPositions();
      }

      private void SetControlPositions() {
         int left = _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         var valueLabel = _valueLabel as Control;
         valueLabel.Left = left;
         valueLabel.Width = Width - left - (ControlConstants.CONTROL_PADDING);

         var checkboxValue = _checkboxValue as Control;
         checkboxValue.Left = left;
         checkboxValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _checkboxValue.Visible = false;
            _requiredLabel.Visible = false;
         }
         else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _checkboxValue.Visible = true;
            _requiredLabel.Visible = _required;
         }
      }

      public void SetToEdit(bool isInEdit) {
         ControlMode = isInEdit ? Mode.Edit : Mode.View;
      }

      public void EnableEdit(bool isInEdit) {
         _internalEdit.Visible = isInEdit;
         if (isInEdit) _internalEdit.BringToFront();
      }

      public void SaveValue() {
         InputValue = _checkboxValue.Checked;
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

      public Color ControlBackcolor {
         set { BackColor = value; }
      }

      public bool InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnValueChanged(object sender, EventArgs e) {
         if (InputValue == _checkboxValue.Checked && !IsDirty) return;

         IsDirty = true;

         _valueLabel.Text = ResolveBool(_checkboxValue.Checked);
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _checkboxValue.BackColor = _validationColor;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _checkboxValue.ResetBackColor();
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
         get { return _checkboxValue.Appearance.TextHAlign; }
         set { _checkboxValue.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font CheckBoxFont {
         get { return _checkboxValue.Font; }
         set { _checkboxValue.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor {
         get { return _validationColor; }
         set { _validationColor = value; }
      }

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

      public bool ReadOnly { get; set; }

      [Bindable(true)]
      public object Value {
         get {
            return _checkboxValue.Checked;
         }
         set {
            if (value == null) return;
            InputValue = (bool)value;
            _valueLabel.Text = ResolveBool(InputValue);
            _checkboxValue.Checked = InputValue;
         }
      }

      private string ResolveBool(bool inputValue) {
         return inputValue ? "Yes" : "No";
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }
   }
}
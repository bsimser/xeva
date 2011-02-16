using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using XF;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class EditablePercentageEditor : UserControl, IEditable {
      private bool _required;
      private Mode _controlMode;
      private string _numericType = "double";

      public EditablePercentageEditor() {
         InitializeComponent();
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;
         this.Resize += (EditableResize);
      }

      public event EventHandler EditClicked;

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _numericEditor.BackColor = ValidationColor;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _numericEditor.ResetBackColor();
      }

      public void SetToEdit(bool isInEdit) {
         ControlMode = isInEdit ? Mode.Edit : Mode.View;
      }

      public void EnableEdit(bool isInEdit) {
         _internalEdit.Visible = isInEdit;
         if (isInEdit) _internalEdit.BringToFront();
      }

      public void SaveValue() {
         InputValue = decimal.Parse(Value.ToString());
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

         var textboxValue = _numericEditor as Control;
         textboxValue.Left = left;
         textboxValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public decimal InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnValueChanged(object sender, EventArgs e) {
         var percentage = decimal.Parse(_numericEditor.Value.ToString()) / 100m;
         _valueLabel.Text = percentage.ToString("p");
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      //[Browsable(false)]
      [Bindable(true)]
      public object Value {
         get {
            return decimal.Parse(_numericEditor.Value.ToString()) / 100;
         }
         set {
            if (value == null) return;
            InputValue = Convert.ToDecimal(value) * 100;

            _valueLabel.Text = !value.ToString().Equals("0") ? ((decimal)value).ToString("p") : string.Empty;
            _numericEditor.Value = InputValue;
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
         get { return _numericEditor.Appearance.TextHAlign; }
         set { _numericEditor.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font TextBoxFont {
         get { return _numericEditor.Font; }
         set { _numericEditor.Font = value; }
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
         _numericEditor.Focus();
      }

      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _numericEditor.Visible = false;
            _requiredLabel.Visible = false;
         }
         else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _numericEditor.Visible = true;
            _requiredLabel.Visible = _required;
         }
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }

      private void OnEnter(object sender, EventArgs e) {
         _numericEditor.SelectAll();
      }
   }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using XF;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class EditableTextbox : UserControl, IEditable {
      private bool _required;
      private Mode _controlMode;
      private Mode _editMode;

      public EditableTextbox() {
         InitializeComponent();
         AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;

         Resize += (EditableResize);
      }

      public event EventHandler EditorButtonClicked;
      public event EventHandler EditClicked;

      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _textboxValue.Visible = false;
            _requiredLabel.Visible = false;
         }
         else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _textboxValue.Visible = true;
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
         InputValue = Value.ToString();
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

         var textboxValue = _textboxValue as Control;
         textboxValue.Left = left;
         textboxValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public string InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnValueChanged(object sender, System.EventArgs e) {
         if (_textboxValue.Value == null && !IsDirty) return;

         if (_textboxValue.Value == null && string.IsNullOrEmpty(InputValue)) {
            IsDirty = false;
            _valueLabel.Text = string.Empty;
            return;
         }

         IsDirty = true;
         var value = _textboxValue.Value != null ? _textboxValue.Value.ToString() : string.Empty;
         _valueLabel.Text = value;
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _textboxValue.BackColor = ValidationColor;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _textboxValue.ResetBackColor();
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
            _label.Appearance.TextHAlign = HAlign.Default;
            _label.Width = value;
            SetControlPositions();
         }
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
         get { return _textboxValue.Appearance.TextHAlign; }
         set { _textboxValue.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font TextBoxFont {
         get { return _textboxValue.Font; }
         set { _textboxValue.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public bool Multiline {
         get { return _textboxValue.Multiline; }
         set { _textboxValue.Multiline = value; }
      }



      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor { get; set; }

      [Bindable(true)]
      public object Value {
         get {
            return _textboxValue.Text;
         }
         set {
            if (value == null) return;
            InputValue = value.ToString();
            _valueLabel.Text = InputValue;
            _textboxValue.Text = InputValue;
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public EditorButtonsCollection ButtonsRight {
         get {
            return _textboxValue.ButtonsRight;
         }
      }

      public bool ReadOnly {
         get {
            return _textboxValue.ReadOnly;
         }
         set {
            _textboxValue.ReadOnly = value;
         }
      }

      private void OnClicked(object sender, EventArgs e) {
         _textboxValue.Focus();
      }

      private void OnEditorButtonClicked(object sender, EditorButtonEventArgs e) {
         if (EditorButtonClicked != null)
            EditorButtonClicked(this, new EditorButtonEventArgs(e.Button, e.Context));
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }
   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using XF;
using XF.UI.Smart;
using Color=System.Drawing.Color;

namespace XF.Controls 
{
   public partial class EditableMaskedTextbox : UserControl , IEditable
   {
      private bool _required;
      private Mode _controlMode;

      public EditableMaskedTextbox() 
      {
         InitializeComponent();
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         ControlMode = Mode.View;

         this.Resize += (EditableResize);
      }

      public event EventHandler EditorButtonClicked;
      public event EventHandler EditClicked;

      private void SetupControlMode() 
      {
         if (ControlMode == Mode.View) 
         {
            _valueLabel.Visible = true;
            _maskedValue.Visible = false;
            _requiredLabel.Visible = false;
         } 
         else if (ControlMode == Mode.Edit) 
         {
            _valueLabel.Visible = false;
            _maskedValue.Visible = true;
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

      public void ResetValue()
      {
         Value = InputValue;
      }

      public object EditedValue
      {
         get { return Value; }
      }

      public IEditable ToIEditable(string controlLabel, object controlValue, List<IListMessage> lookupList)
      {
         Label = controlLabel;
         Value = controlValue;

         return this;
      }

      private void EditableResize(object sender, EventArgs e)
      {
         SetControlPositions();
      }

      private void SetControlPositions()
      {
         int left = _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         var valueLabel = _valueLabel as Control;
         valueLabel.Left = left;
         valueLabel.Width = Width - left - (ControlConstants.CONTROL_PADDING);

         var maskedValue = _maskedValue as Control;
         maskedValue.Left = left;
         maskedValue.Width = Width - left - (ControlConstants.CONTROL_PADDING);                     
      }

      public string InputValue { get; private set; }

      public bool IsDirty { get; private set; }

      private void OnValueChanged(object sender, System.EventArgs e) 
      {
         if (_maskedValue.Value == null && !IsDirty) return;

         if (_maskedValue.Value==null && string.IsNullOrEmpty(InputValue))
         {
            IsDirty = false;
            _valueLabel.Text = string.Empty;
            return;
         }

         IsDirty = true;
         _valueLabel.Text = _maskedValue.Value.ToString();
      }

      public void ShowError(string message)
      {
         _errorProvider.SetError(this, message);
         _maskedValue.BackColor = ValidationColor;
      }

      public void ClearError()
      {
         _errorProvider.Clear();
         _maskedValue.ResetBackColor();
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
      public Mode ControlMode 
      {
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
         set
         {
            _required = value;
            if (ControlMode == Mode.Edit)
               _requiredLabel.Visible = value;
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public HAlign EditorHorizontalAlignment {
         get { return _maskedValue.Appearance.TextHAlign; }
         set { _maskedValue.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public System.Drawing.Font TextBoxFont {
         get { return _maskedValue.Font; }
         set { _maskedValue.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public System.Drawing.Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Color ValidationColor { get; set; }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string InputFormat {
         set { _maskedValue.InputMask = value; }
      }

      [Bindable(true)]
      public object Value
      {
         get 
         {
            return _maskedValue.Text;
         }
         set 
         {
            InputValue = value.ToString();
            _valueLabel.Text = InputValue;
            _maskedValue.Text = InputValue;
         }
      }

      public bool ReadOnly { get; set; }

      private void OnClick(object sender, EventArgs e) 
      {
         _maskedValue.Focus();
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public EditorButtonsCollection ButtonsRight {
         get {
            return _maskedValue.ButtonsRight;
         }
      }

      private void OnEditorButtonClick(object sender, EditorButtonEventArgs e) {
         if (EditorButtonClicked != null) {
            EditorButtonClicked(this, new EditorButtonEventArgs(e.Button, e.Context));
         }
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }
   }
}

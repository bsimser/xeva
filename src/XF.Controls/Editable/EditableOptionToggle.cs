using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using XF;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class EditableOptionToggle : UserControl, IEditable {
      private Mode _controlMode;

      public EditableOptionToggle() {
         InitializeComponent();
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;

         _optionToggle.Items.Clear();

         ControlMode = Mode.View;

         this.Resize += (EditableResize);
      }

      public event EventHandler EditClicked;
      
      private void SetupControlMode() {
         if (ControlMode == Mode.View) {
            _valueLabel.Visible = true;
            _optionToggle.Visible = false;
         } else if (ControlMode == Mode.Edit) {
            _valueLabel.Visible = false;
            _optionToggle.Visible = true;
         }
      }

      public void ShowError(string messsage) {
      }

      public void ClearError() {
      }

      public void SetToEdit(bool isInEdit) {
         ControlMode = isInEdit ? Mode.Edit : Mode.View;
      }

      public void EnableEdit(bool isInEdit) {
         _internalEdit.Visible = isInEdit;
         if (isInEdit) _internalEdit.BringToFront();
      }

      public void SaveValue() {
         InputValue = _optionToggle.CheckedItem.DisplayText;
         _valueLabel.Text = _optionToggle.CheckedItem.DisplayText;
      }

      public void ResetValue() {
         _optionToggle.CheckedIndex = 0;
      }

      public object EditedValue {
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

      private void SetControlPositions() {
         int left = _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         var valueLabel = _valueLabel as Control;
         valueLabel.Left = left;
         valueLabel.Width = Width - left - (ControlConstants.CONTROL_PADDING);

         var editControl = _optionToggle as Control;
         editControl.Left = left;
         editControl.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      [Browsable(false)]
      public object Value {
         get {
            if (_optionToggle.CheckedItem == null)
               return "";
            return _optionToggle.CheckedItem.ToString();
         }
         set {
            InputValue = value != null ? value.ToString() : string.Empty;
            _valueLabel.Text = InputValue;
         }
      }
      [Browsable(false)]
      public string Option_Default {
         set {
            var listItem = new ValueListItem(value, value);
            if (!_optionToggle.Items.Contains(listItem))
            {
               _optionToggle.Items.Add(listItem);
               _optionToggle.CheckedIndex = 0;
            }
         }
      }
      [Browsable(false)]
      public string Option_Other {
         set {
            var listItem = new ValueListItem(value, value);
            if (!_optionToggle.Items.Contains(listItem))
               _optionToggle.Items.Add(listItem);
         }
      }

      public string InputValue { get; private set; }

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
      public HAlign EditorHorizontalAlignment {
         get { return _optionToggle.Appearance.TextHAlign; }
         set { _optionToggle.Appearance.TextHAlign = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font TextBoxFont {
         get { return _optionToggle.Font; }
         set { _optionToggle.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public Mode ControlMode {
         get {
            return _controlMode;
         }
         set 
         {
            _controlMode = value;
            SetupControlMode();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public HAlign ValueHAlign {
         get { return _valueLabel.Appearance.TextHAlign; }
         set { _valueLabel.Appearance.TextHAlign = value; }
      }

      public bool ReadOnly { get; set; }

      private void OnOptionToggleClick(object sender, EventArgs e) {
         _optionToggle.Focus();
      }

      public event EventHandler ValueChanged;
      private void OnValueChanged(object sender, EventArgs e) {
         if (ValueChanged != null)
            ValueChanged(this, EventArgs.Empty);
      }

      private void OnEditClick(object sender, EventArgs e) {
         if (EditClicked != null)
            EditClicked(this, new EventArgs());
      }
   }
}

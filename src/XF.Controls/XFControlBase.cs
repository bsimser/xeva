using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using XF.UI.Smart;
using XF.Validation;

namespace XF.Controls {
   [ToolboxItem(false)]
   public partial class XFControlBase : UserControl, IControl, IPropertyControlAdapter {
      private Control _control;
      private INotifyPropertyChanged _subject;
      private string _subjectProperty;
      private object _value;

      public XFControlBase() {
         InitializeComponent();
         this.AutoScaleMode = AutoScaleMode.Inherit;
         this.Resize += new EventHandler(InputControlBase_Resize);
      }

      public event EventHandler ValueChanged;

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string LabelText {
         get { return _label.Text; }
         set { _label.Text = value; }
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

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public int LabelWidth {
         get { return _label.Width; }
         set {
            _label.Width = value;
            SetEditorControlPosition();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      [DefaultValue(true)]
      public bool LabelVisible {
         get { return _label.Visible; }
         set { _label.Visible = value; }
      }

      public virtual object Value {
         get { return "Value is undetermined"; }
         set { }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual bool ReadOnly {
         get { return false; }
         set { }
      }

      protected virtual bool LabelBold {
         get { return _label.Appearance.FontData.Bold == DefaultableBoolean.True ? true : false; }
         set { _label.Appearance.FontData.Bold = value ? DefaultableBoolean.True : DefaultableBoolean.False; }
      }

      protected Control EditorControl {
         set {
            _control = value;
            SetEditorControlPosition();
         }
      }

      public void SetSubject(INotifyPropertyChanged dto, string property) {
      }

      public void SetNotification(ValidationResult validationResult) {

      }

      private void InputControlBase_Resize(object sender, EventArgs e) {
         if (_control == null)
            return;
         this.SetEditorControlPosition();
      }

      protected virtual void OnValueChanged() {
         if (ValueChanged != null)
            ValueChanged(this, EventArgs.Empty);
      }

      protected virtual void SetEditorControlPosition() {
         if (_control == null)
            return;

         int left =
            _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         _control.Left = left;

         _control.Width = this.Width - left - (ControlConstants.CONTROL_PADDING);
      }

      protected void SetLabelStyle(string styleSetName) {
         _label.StyleSetName = styleSetName;
      }

      private void OnClicked(object sender, EventArgs e) {
         _control.Focus();
      }

      public virtual void ShowError(string messsage) {
      }

      public virtual void ClearError() {
      }
   }
}
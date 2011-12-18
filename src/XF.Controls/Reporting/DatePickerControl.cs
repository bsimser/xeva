using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class DatePickerControl : UserControl, IControl {
      public DatePickerControl() {
         InitializeComponent();
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public string Label {
         get { return _label.Text; }
         set { _label.Text = value; }
      }

      public object Value {
         get { return _datePicker.Value; }
         set { _datePicker.Value = DateTime.Parse(value.ToString()); }
      }

      public bool ReadOnly {
         get { return false; }
         set { }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public int LabelWidth {
         get { return _label.Width; }
         set {
            _label.Width = value;
            SetEditorControlPosition();
         }
      }

      protected virtual void SetEditorControlPosition() {
         int left =
            _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         _datePicker.Left = left;

         _datePicker.Width = this.Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public void ShowError(string message) {
         _errorProvider.SetError(this, message);
         _datePicker.BackColor = Color.Red;
      }

      public void ClearError() {
         _errorProvider.Clear();
         _datePicker.ResetBackColor();
      }
   }
}

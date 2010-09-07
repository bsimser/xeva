using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;

namespace XF.Controls {

   [ToolboxItem(true)]
   public partial class LabelControl : XFControlBase {
      private ToolTipControl _toolTip;
      private bool _hotTrack;

      public LabelControl() {
         InitializeComponent();
         EditorControl = _valueLabel;
         SetEditorControlPosition();
         DoubleBuffered = true;
      }


      [DefaultValue(false)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public bool HotTrack {
         get { return _hotTrack; }
         set {
            _hotTrack = value;

            _valueLabel.UseHotTracking =
               _hotTrack ? DefaultableBoolean.True : DefaultableBoolean.False;
         }
      }

      [Bindable(true), Category(ControlConstants.PROPERTY_CATEGORY)]
      public string StyleSetName {
         get {
            return _valueLabel.StyleSetName;
         }
         set {
            _valueLabel.StyleSetName = value;
            base.SetLabelStyle(value);
         }
      }

      [Bindable(true)]
      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public override object Value {
         get { return _valueLabel.Text; }
         set {
            if (value == null) return;

            string oldValue = _valueLabel.Text;
            string newValue = String.Empty;

            if (value.ToString() != String.Empty)
               newValue = (value.ToString()).Replace("&", "&&");

            _valueLabel.Text = newValue;

            if (oldValue != newValue)
               base.OnValueChanged();
         }
      }

      [Category(ControlConstants.PROPERTY_CATEGORY)]
      public virtual Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      private void OnLabelClick(object sender, EventArgs e) {
         if (_valueLabel.Text == String.Empty) return;

         CloseToolTip();

         _toolTip = new ToolTipControl {
            Text = base.LabelText,
            ToolTip = _valueLabel.Text,
            StartPosition = FormStartPosition.Manual,
            Location = new Point(Cursor.Position.X + 15, Cursor.Position.Y + 15)
         };

         _toolTip.Show();
      }

      private void OnLabelMouseLeave(object sender, EventArgs e) {
         CloseToolTip();
      }

      private void CloseToolTip() {
         if (_toolTip != null && !_toolTip.IsPinned)
            _toolTip.Dispose();
      }

   }
}

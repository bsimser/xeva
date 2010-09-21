using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using XF.UI.Smart;

namespace XF.Controls {
   public partial class AddressControl : UserControl, IControl {
      public AddressControl() {
         InitializeComponent();
      }

      public void ShowError(string messsage) {
         throw new System.NotImplementedException();
      }

      public void ClearError() {
         throw new System.NotImplementedException();
      }

      public object Value { get; set; }
      public bool ReadOnly { get; set; }

      public Color ControlBackcolor {
         set { BackColor = value; }
      }

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
      public Font ValueLabelFont {
         get { return _valueLabel.Font; }
         set { _valueLabel.Font = value; }
      }

      private void SetControlPositions() {
         var left = _label.Width + _label.Left + (ControlConstants.LABEL_TO_CONTROL_SPACING);

         var valueLabel = _valueLabel as Control;
         valueLabel.Left = left;
         valueLabel.Width = Width - left - (ControlConstants.CONTROL_PADDING);
      }

      public void LoadAddressFromMessage(object message) {
         if (message == null) return;
         var properties = new List<PropertyInfo>(message.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance));
         var address1Prop = properties.First(match => match.Name.ToLower().Contains("address1"));
         var address2Prop = properties.First(match => match.Name.ToLower().Contains("address2"));
         var cityProp = properties.First(match => match.Name.ToLower().Contains("city"));
         var stateProp = properties.First(match => match.Name.ToLower().Contains("state"));
         var zipProp = properties.First(match => match.Name.ToLower().Contains("zip"));
         var address1 = address1Prop != null ? address1Prop.GetValue(message, null).ToString() : string.Empty;
         var address2 = address2Prop != null ? address2Prop.GetValue(message, null).ToString() : string.Empty;
         var city = cityProp != null ? cityProp.GetValue(message, null).ToString() : string.Empty;
         var state = stateProp != null ? stateProp.GetValue(message, null).ToString() : string.Empty;
         var zip = zipProp != null ? zipProp.GetValue(message, null).ToString() : string.Empty;

         LoadAddress(address1, address2, city, state, zip);
      }

      public void LoadAddress(string address1, string address2, string city, string state, string zip) {
         var address = new StringBuilder();
         address.AppendLine(address1);
         if (!string.IsNullOrEmpty(address2)) address.AppendLine(address2);
         address.Append(string.Format("{0}, {1} {2}", city, state, zip));

         _valueLabel.Text = address.Length > 5 ? address.ToString() : string.Empty;
      }
   }
}

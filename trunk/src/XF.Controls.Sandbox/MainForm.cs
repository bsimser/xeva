using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Infragistics.Win.UltraWinToolbars;
using XF.Controls.Sandbox.Impls;

namespace XF.Controls.Sandbox {
   public partial class MainForm : Form {
      public MainForm() {
         InitializeComponent();
      }

      private void OnToolClick(object sender, ToolClickEventArgs e) {
         switch (e.Tool.Key) {
            case "Address":
               LaunchAddressControl();
               break;
            case "DropDownButton":
               LaunchDropDownButtonControl();
               break;
            case "Calculator":
               LaunchCalculatorControl();
               break;
            case "CurrencyEditor":
               LaunchCurrencyEditor();
               break;
            case "EditablePanel":
               LaunchEditablePanel();
               break;
            case "ComboBoxEditor":
               LaunchComboBoxEditor();
               break;
            case "PercentageEditor":
               LaunchPercentageEditor();
               break;
         }
      }

      private void LaunchCurrencyEditor() {
         var control = new EditableCurrencyEditor();
         control.SetToEdit(true);
         LoadControlImpl(control);
      }

      private void LaunchAddressControl() {
         var control = new AddressImpl();
         LoadControlImpl(control);
      }

      private void LaunchDropDownButtonControl() {
         var control = new DropdownSelector();
         var items = new List<LookupMessage>{new LookupMessage{DisplayOrder = 2, ID=Guid.NewGuid(), Name="NY"},
                                             new LookupMessage{DisplayOrder = 1, ID=Guid.NewGuid(), Name="NJ"},
                                             new LookupMessage{DisplayOrder = 0, ID=Guid.NewGuid(), Name="PA"}};
         control.Items = items;
         LoadControlImpl(control);
      }

      private void LaunchComboBoxEditor() {
         var control = new EditableComboBoxEditor {DisplayMember = "Name", ValueMember = "ID", LabelText = "ComboBox"};
         var items = new List<LookupMessage>{new LookupMessage{DisplayOrder = 2, ID=Guid.NewGuid(), Name="N                Y"},
                                             new LookupMessage{DisplayOrder = 1, ID=Guid.NewGuid(), Name="N                J"},
                                             new LookupMessage{DisplayOrder = 0, ID=Guid.NewGuid(), Name="P                A"}};
         var bs = new BindingSource {DataSource = items};
         control.BindingSource = bs;
         control.Bind();
         control.SetToEdit(true);
         LoadControlImpl(control);
      }

      private void LoadControlImpl(UserControl control) {
         //control.Dock = DockStyle.Fill;
         _mainForm_Fill_Panel.Controls.Clear();
         _mainForm_Fill_Panel.Controls.Add(control);
      }

      private void LaunchCalculatorControl() {
         var calc = new CalculatorSample();
         calc.Show();
      }

      private void LaunchEditablePanel() {
         var panel = new EditablePanel {Editable = true, Dock = DockStyle.Fill};
         LoadControlImpl(panel);
      }

      private void LaunchPercentageEditor() {
         var control = new EditablePercentageEditor { Value = .25m, ControlMode = Mode.View };
         LoadControlImpl(control);
         //var text = .26m;
         //var control = new Infragistics.Win.Misc.UltraLabel {Text = text.ToString("p")};
         //_mainForm_Fill_Panel.Controls.Add(control);
      }

   }
}

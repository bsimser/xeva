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
            case "Calculator":
               LaunchCalculatorControl();
               break;
         }
      }

      private void LaunchAddressControl() {
         var control = new AddressImpl();
         LoadControlImpl(control);
      }

      private void LoadControlImpl(UserControl control) {
         control.Dock = DockStyle.Fill;
         _mainForm_Fill_Panel.Controls.Clear();
         _mainForm_Fill_Panel.Controls.Add(control);
      }

      private void LaunchCalculatorControl() {
         var calc = new CalculatorSample();
         calc.Show();
      }
   }
}

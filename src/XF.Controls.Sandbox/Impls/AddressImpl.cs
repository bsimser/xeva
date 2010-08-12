using System;
using System.Windows.Forms;

namespace XF.Controls.Sandbox.Impls {
   public partial class AddressImpl : UserControl {
      public AddressImpl() {
         InitializeComponent();
      }

      private void OnFormLoad(object sender, EventArgs e) {
         var message = new AddressMessage {
                                             Address1 = "121 Somewhere St",
                                             Address2 = "Suite 500",
                                             City = "Cicero",
                                             State = "NY",
                                             Zip = "13090-4005"
                                          };
         _addressBindingSource.SuspendBinding();
         _addressBindingSource.DataSource = message;
         _addressBindingSource.ResumeBinding();

         _addressControl.LoadAddressFromMessage(message);
      }

   }
}

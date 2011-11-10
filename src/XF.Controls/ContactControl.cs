using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XF.Controls {
   public partial class ContactControl : Form {
      public ContactControl(ContactMessage contactMessage) {
         InitializeComponent();
         InitializeMessage(contactMessage);
      }

      private void InitializeMessage(ContactMessage contactMessage) {
         _name.Value = contactMessage.Name;
         _address.Value = contactMessage.Address;
         _phone1.Value = contactMessage.Phone1;
         _phone2.Value = contactMessage.Phone2;
         _fax.Value = contactMessage.Fax;
         _email.Value = contactMessage.Email;
      }
   }
}

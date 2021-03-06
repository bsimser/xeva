using System;
using System.Drawing;
using System.Windows.Forms;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public partial class Window : Form
   {
      public Window()
      {
         InitializeComponent();
      }

      public void InitializeUI(object ui)
      {
         Control control = ui as Control;
         if (control == null)
            throw new ArgumentException("Property 'ui' must be of type control.", "ui");
         panel1.Controls.Clear();
         panel1.Controls.Add(control);
         control.Dock = DockStyle.Fill;
      }

   }
}
using System;
using System.Windows.Forms;

namespace XF.Controls
{
   public partial class DeleteCancelControl : UserControl
   {
      public DeleteCancelControl()
      {
         InitializeComponent();
      }

      public event EventHandler DeleteClick;
      public event EventHandler CancelClick;

      private void OnCancelClick(object sender, System.EventArgs e)
      {
         if (CancelClick != null)
            CancelClick(this, EventArgs.Empty);
      }

      private void OnDeleteClick(object sender, EventArgs e)
      {
         if (DeleteClick != null)
            DeleteClick(this, EventArgs.Empty);
      }
   }
}
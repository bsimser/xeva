using System;
using System.Windows.Forms;

namespace XF.Controls
{
   public partial class SaveDeleteCancelControl : UserControl
   {
      public SaveDeleteCancelControl()
      {
         InitializeComponent();
      }

      public event EventHandler SaveClick;
      public event EventHandler DeleteClick;
      public event EventHandler CancelClick;

      private void OnSaveClick(object sender, System.EventArgs e)
      {
         if (SaveClick != null)
            SaveClick(this, EventArgs.Empty);
      }

      private void OnDeleteClick(object sender, System.EventArgs e)
      {
         if (DeleteClick != null)
            DeleteClick(this, EventArgs.Empty);
      }

      private void OnCancelClick(object sender, System.EventArgs e)
      {
         if (CancelClick != null)
            CancelClick(this, EventArgs.Empty);
      }
   }
}
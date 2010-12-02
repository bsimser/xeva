using System;
using System.Windows.Forms;

namespace XF.Controls
{
   public partial class SaveCancelControl : UserControl
   {
      public SaveCancelControl()
      {
         InitializeComponent();
      }

      public bool SaveEnabled
      {
         get { return _saveButton.Enabled; }
         set { _saveButton.Enabled = value;}
      }

      public bool CancelEnabled
      {
         get { return _cancelButton.Enabled; }
         set { _cancelButton.Enabled = value; }
      }

      public event EventHandler SaveClick;
      public event EventHandler CancelClick;

      private void OnSaveClick(object sender, System.EventArgs e)
      {
         if (SaveClick != null)
            SaveClick(this, EventArgs.Empty);
      }

      private void OnCancelClick(object sender, System.EventArgs e)
      {
         if (CancelClick != null)
            CancelClick(this, EventArgs.Empty);
      }
   }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class WindowAdapter : IWindowAdapter
   {
      private bool _modal;
      private readonly Window _window;

      public WindowAdapter()
      {
         _window = new Window();
         _window.Closed += OnWindowClosed;
      }

      private void OnWindowClosed(object sender, EventArgs e)
      {
         if (this.Closed != null)
            this.Closed(this, EventArgs.Empty);
      }

      public void Close()
      {
//         if (_window.InvokeRequired)
//         {
//            Action a = _window.Close;
//            _window.Invoke(a);
//         }
//         else
            _window.Close();
      }

      public event EventHandler Closed;

      public void InitializeUI(object ui)
      {
         _window.InitializeUI(ui);
      }

      public void ApplyOptions(IWindowOptions options)
      {
         _modal = options.Modal;
         _window.Width = options.Width;
         _window.Height = options.Height;
         _window.Left = options.Left;
         _window.Top = options.Top;
      }

      public void Show()
      {
         if (_modal) _window.ShowDialog();
         if (!_modal) _window.Show();
      }

      public void Hide()
      {
         _window.Hide();
      }

      public void ChangeCaption(string caption)
      {
         _window.Text = caption;
      }
   }
}
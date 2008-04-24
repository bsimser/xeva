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
      private int _width;
      private int _height;
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

      #region IWindowAdapter Members

      public void Close()
      {
         _window.Close();
      }

      public event EventHandler Closed;

      public void InitializeUI(object ui)
      {
         _window.InitializeUI(ui);
      }

      #endregion  

      #region IWindowController Members

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

      #endregion

      #region IWindowOptions Members

      public bool Modal
      {
         get { return _modal; }
         set { _modal = value; }
      }

      public int Width
      {
         get { return _window.Width; }
         set { _window.Width = value; }
      }

      public int Height
      {
         get { return _window.Height; }
         set { _window.Height = value; }
      }

      #endregion
   }
}
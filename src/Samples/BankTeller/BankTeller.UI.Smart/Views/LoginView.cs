using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BankTeller.UI.Smart.Presenters;

namespace BankTeller.UI.Smart.Views
{
   public partial class LoginView : UserControl, ILoginView
   {
      private ILoginCallbacks _callbacks;

      public LoginView()
      {
         InitializeComponent();
         WireEvents();
      }

      private void WireEvents()
      {
         _loginButton.Click += OnLoginClicked;
         _cancelButton.Click += OnCancelClicked;
      }

      private void OnLoginClicked(object sender, EventArgs e)
      {
         _callbacks.Login();
      }

      private void OnCancelClicked(object sender, EventArgs e)
      {

      }

      public string Username
      {
         get { return _usernameTextbox.Text; }
      }

      public string Password
      {
         get { return _passwordTextbox.Text; }
      }

      public void ShowError(string message)
      {
         _errorMessageLabel.Visible = true;
         _errorMessageLabel.Text = message;
      }

      public object UI
      {
         get { return this; }
      }

      public void Attach(ILoginCallbacks callback)
      {
         _callbacks = callback;
      }

      public void ShowWaiting()
      {
         this.BackColor = Color.Turquoise;
      }

      public void HideWaiting()
      {
         this.BackColor = Color.White;
      }
   }
}

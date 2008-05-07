using System;
using System.Windows.Forms;
using BankTeller.UI.Smart.Presenters;
using Castle.Windsor;
using XF;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class ShellContext : ApplicationContext
   {
      private readonly IWindowManager _windowManager;
      private ILoginPresenter _loginPresenter;
      private IWindowAdapter _loginWindow;
      private IShellPresenter _shellPresenter;
      private IWindowAdapter _shellWindow;

      public ShellContext()
      {
         InitializeContainer();

         _windowManager = IoC.Resolve<IWindowManager>();

         ShowLogin();
      }

      private void ShowLogin()
      {
         _loginWindow = _windowManager.Create(new WindowOptions(true, 400, 300));
         _loginWindow.Closed += OnWindowAdapterClosed;

         _loginPresenter = IoC.Resolve<ILoginPresenter>();
         _loginPresenter.LoginSuccess += (o, e) => ShowShell();
         _loginPresenter.DisplayIn(_loginWindow);
         _loginPresenter.Start();
      }

      private void ShowShell()
      {
         _loginWindow.Closed -= OnWindowAdapterClosed;
         _loginWindow.Close();

         _shellWindow = _windowManager.Create(new WindowOptions {Height = 500, Width = 700, Modal = false});
         _shellWindow.Closed += OnWindowAdapterClosed;

         _shellPresenter = IoC.Resolve<IShellPresenter>();
         _shellPresenter.DisplayIn(_shellWindow);
         _shellPresenter.Start();
      }

      private void OnWindowAdapterClosed(object sender, EventArgs e)
      {
         Application.Exit();
      }

      private void InitializeContainer()
      {
         var windsorConfig = @".\_config\windsor.xml";
         var windsorContainer = new WindsorContainer(windsorConfig);
         IoC.Initialize(windsorContainer);
      }
   }
}
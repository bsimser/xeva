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
      private readonly ILoginPresenter _loginPresenter;
      private IWindowManager _windowManager;

      public ShellContext()
      {
         InitializeContainer();

         _loginPresenter = IoC.Resolve<ILoginPresenter>();
         _windowManager = IoC.Resolve<IWindowManager>();

         IWindowAdapter windowAdapter = _windowManager.Create(new WindowOptions(true, 400, 300));
         windowAdapter.Closed += OnWindowAdapterClosed;

         _loginPresenter.DisplayIn(windowAdapter);
         _loginPresenter.Start();
      }

      private void OnWindowAdapterClosed(object sender, EventArgs e)
      {
         Application.Exit();
      }

      private void InitializeContainer()
      {
         string windsorConfig = @".\_config\windsor.xml";
         WindsorContainer windsorContainer = new WindsorContainer(windsorConfig);
         IoC.Initialize(windsorContainer);
      }
   }
}
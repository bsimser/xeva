using System.Windows.Forms;
using BankTeller.UI.Smart.Presenters;
using Castle.Windsor;
using XEVA.Framework;
using XEVA.Framework.UI.Smart;

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

         _loginPresenter.DisplayIn(_windowManager.Create());
         _loginPresenter.Start();
      }


      private void InitializeContainer()
      {
         string windsorConfig = @".\_config\windsor.xml";
         WindsorContainer windsorContainer = new WindsorContainer(windsorConfig);
         IoC.Initialize(windsorContainer);
      }
   }
}
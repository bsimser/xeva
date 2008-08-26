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
      private bool _loginSuccess;
      private IWindowAdapter _loginWindow;
      private IShellPresenter _shellPresenter;
      private IWindowAdapter _shellWindow;

      public ShellContext()
      {
         InitializeContainer();
         _windowManager = Locator.Resolve<IWindowManager>();
         ShowLogin();
      }

      private void ShowLogin()
      {
         _loginPresenter =
            New
               .Presenter<ILoginPresenter>()
               .ManagedBy(_windowManager)
               .Window.ClosedCallback(OnLoginWindowClosed).Modal.Size(400, 300)
               .Return; 

         _loginPresenter.LoginSuccess += ()=> _loginSuccess = true; 
         _loginPresenter.Activate();
      }

      private void ShowShell()
      {
         New.Presenter<IShellPresenter>()
            .ManagedBy(_windowManager)
            .Window.Size(800, 600).NotModal.ClosedCallback(OnShellWindowClosed)
            .Return.Activate();
      }

      private void OnLoginWindowClosed()
      {
         if (_loginSuccess)
         {
            ShowShell();
            return;
         }
         Application.Exit();
      }

      private void OnShellWindowClosed()
      {
         Application.Exit();
      }

      private void InitializeContainer()
      {
         var windsorConfig = @".\_config\windsor.xml";
         var windsorContainer = new WindsorContainer(windsorConfig);
         Locator.Initialize(windsorContainer);
      }
   }
}
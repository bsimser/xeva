using BankTeller.UI.Smart.Services;
using XF.UI.Smart;

namespace BankTeller.UI.Smart.Presenters
{
   public class LoginPresenter : 
      Presenter<ILoginView, ILoginCallbacks>, ILoginPresenter, 
      ILoginCallbacks
   {
      private readonly IAuthenticationService _authenticationService;
      private readonly ILabelLookup _labelLookup;

      public LoginPresenter(IAuthenticationService authenticationService, ILabelLookup labelLookup)
      {
         _authenticationService = authenticationService;
         _labelLookup = labelLookup;
      }

      public void Login()
      {
         
         if (!_authenticationService.Authenticate(View.Username, View.Password))
         {
            string errorMessage = _labelLookup.Find("INVALID_LOGIN");
            View.ShowError(errorMessage);
         }

      }
   }
}
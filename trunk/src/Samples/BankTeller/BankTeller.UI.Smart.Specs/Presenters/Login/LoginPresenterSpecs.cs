using BankTeller.UI.Smart.Presenters;
using BankTeller.UI.Smart.Services;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_LoginPresenter
{
   [TestFixture]
   public class When_a_user_attempts_login : Spec
   {
      private LoginPresenter _presenter;
      private ILoginView _view;

      protected override void Before_each_spec()
      {
         _presenter = Create<LoginPresenter>();
         _view = Mock<ILoginView>();
         _presenter.View = _view;
      }

      [Test]
      public void Send_credentials_to_the_authentiction_service()
      {
         SetupResult.For(_view.Username).Return("dave");
         SetupResult.For(_view.Password).Return("test123");

         using (Record)
         {
            Expect
               .Call(Get<IAuthenticationService>().Authenticate("dave", "test123"))
               .Return(true);
         }
         using (Playback)
         {
            _presenter.Login();
         }
      }
   }

   [TestFixture]
   public class When_a_user_supplies_invalid_credentials : Spec
   {
      private LoginPresenter _presenter;
      private ILoginView _view;

      protected override void Before_each_spec()
      {
         _presenter = Create<LoginPresenter>();
         _view = Mock<ILoginView>();
         _presenter.View = _view;

         SetupResult.For(_view.Username).Return("dave");
         SetupResult.For(_view.Password).Return("test123");
         SetupResult
            .For(Get<IAuthenticationService>().Authenticate("dave", "test123"))
            .Return(false);
      }

      [Test]
      public void Lookup_the_error_message_from_a_label_service()
      {
         using (Record)
         {
            Expect
               .Call(Get<ILabelLookup>().Find("INVALID_LOGIN"))
               .Return("Invalid login");
         }
         using (Playback)
         {
            _presenter.Login();
         }
      }

      [Test]
      public void Show_the_error_message_returned_from_the_label_lookup()
      {
         SetupResult
            .For(Get<ILabelLookup>().Find("INVALID_LOGIN"))
            .Return("Invalid login");

         using (Record)
         {
            _view.ShowError("Invalid login");
         }
         using (Playback)
         {
            _presenter.Login();
         }
      }
   }

   [TestFixture]
   public class When_login_succeeds : Spec
   {
      private LoginPresenter _presenter;
      private ILoginView _view;

      protected override void Before_each_spec()
      {
         _presenter = Create<LoginPresenter>();

         _view = Mock<ILoginView>();
         _presenter.View = _view;
      }

      [Test]
      public void Notify_of_success()
      {
         using (Record)
         {
            SetupResult.For(_view.Username).Return("dave");
            SetupResult.For(_view.Password).Return("test123");
            SetupResult
               .For(Get<IAuthenticationService>().Authenticate("dave", "test123"))
               .Return(true);
         }

         var eventCalls = 0;
         _presenter.LoginSuccess += (o, e) => { eventCalls += 1; };

         using (Playback)
         {
            _presenter.Login();
         }

         Assert.That(eventCalls, Is.EqualTo(1));
      }
   }
}
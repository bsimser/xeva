using NUnit.Framework;
using XEVA.Framework.Specs;
using XEVA.Framework.UI.Smart;

namespace Specs_for_Presenter
{
   [TestFixture]
   public class When_starting_a_presenter : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IRequest _request;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         _request = new Request();
         _request.SetItem<string>("data", "test");
      }

      [Test]
      public void Attach_callbacks_to_the_view()
      {
         using (Record)
         {
            Get<IExampleWidgetView>().Attach(_presenter);
         }

         using (Playback)
         {
            _presenter.Start(_request);
         }
      }

      [Test, ExpectedException(typeof(ViewNotAvailableException))]
      public void Fail_if_the_view_is_not_available()
      {
         _presenter.View = null;
         _presenter.Start(_request);
      }

      [Test]
      public void Call_specific_startup_code()
      {
         Assert.AreEqual(0, _presenter.StartCount);
         _presenter.Start(_request);
         Assert.AreEqual(1, _presenter.StartCount);
      }

      [Test]
      public void Validate_request_data()
      {
         Assert.AreEqual(0, _presenter.ValidateCount);
         _presenter.Start(_request);
         Assert.AreEqual(1, _presenter.ValidateCount);
      }

      [Test, ExpectedException(typeof(InvalidRequestException))]
      public void Throw_an_invalid_request_exception_if_validation_fails()
      {
         Request invalidRequest = new Request();
         _presenter.Start(invalidRequest);
      }
   }

   [TestFixture]
   public class When_starting_an_already_started_presenter : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IRequest _request;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         
         _request = new Request();
         _request.SetItem<string>("data", "test");

         _presenter.Start(_request);
      }

      [Test]
      public void Ensure_the_presenter_is_only_started_once()
      {
         _presenter.Start(new NullRequest());
         Assert.AreEqual(1, _presenter.StartCount);
      }
   }

   [TestFixture]
   public class When_starting_an_presenter_that_does_not_implement_a_callbacks_interface : Spec
   {
      private ExampleInvalidPresenter _presenter;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleInvalidPresenter>();
      }

      [Test, ExpectedException(typeof (NoCallbacksImplementationException))]
      public void Throw_an_exception()
      {
         _presenter.Start(new NullRequest());
      }
   }
}
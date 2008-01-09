using NUnit.Framework;
using Rhino.Mocks;
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

      public void Provide_the_request_to_specific_startup_code()
      {
         Assert.AreEqual(string.Empty, _presenter.RequestDataFromCustomStartup);
      }

      [Test]
      public void Initialize_request_data()
      {
         Assert.AreEqual(0, _presenter.InitializeCount);
         _presenter.Start(_request);
         Assert.AreEqual(1, _presenter.InitializeCount);
      }

      [Test, ExpectedException(typeof(MissingRequiredRequestItemException))]
      public void Throw_an_exception_if_data_is_missing_from_the_request()
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
   public class When_finishing_a_presenter : Spec
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
      public void Call_custom_finishing_code()
      {
         _presenter.Start(_request);
         Assert.AreEqual(0, _presenter.FinishCount);
         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);
      }

      [Test]
      public void Do_not_finish_if_never_started()
      {
         Assert.AreEqual(0, _presenter.FinishCount);
         _presenter.Finish();
         Assert.AreEqual(0, _presenter.FinishCount);
      }

      [Test]
      public void Do_not_finished_if_previously_finished()
      {
         _presenter.Start(_request);
         Assert.AreEqual(0, _presenter.FinishCount);
         
         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);

         _presenter.Finish();         
         Assert.AreEqual(1, _presenter.FinishCount);
      }
   }

   [TestFixture]
   public class When_validating_an_object_used_in_the_presenter : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IPresenterValidator _presenterValidator;
 
      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         _presenterValidator = Mock<IPresenterValidator>();
         _presenter.InitializeValidator(_presenterValidator);
      }

      [Test]
      public void Return_true_if_the_target_object_is_valid()
      {
         using (Record)
         {
            Expect.Call(_presenterValidator.Validate(null, null))
               .IgnoreArguments()
               .Return(true);
         }

         using (Playback)
         {
            Assert.IsTrue(_presenter.Validate(null));
         }
      }

      [Test]
      public void Return_false_if_the_target_object_is_not_valid()
      {
         using (Record)
         {
            Expect.Call(_presenterValidator.Validate(null, null))
               .IgnoreArguments()
               .Return(false);
         }

         using (Playback)
         {
            Assert.IsFalse(_presenter.Validate(null));
         }
      }
   }

   [TestFixture]
   public class When_starting_a_presenter_without_a_view_callback_implementation : Spec
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
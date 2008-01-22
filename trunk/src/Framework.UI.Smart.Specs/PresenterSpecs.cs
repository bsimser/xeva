using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using XF.Specs;
using XF.UI.Smart;

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
         _request.SetItem("data", "test");
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

      [Test, ExpectedException(typeof(RequestItemRequiredException))]
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
         _request.SetItem("data", "test");

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
         _request.SetItem("data", "test");
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

   [TestFixture]
   public class When_a_presenter_displaying_a_presenter_in_a_window : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IWindow _window;
      private IRequest _request;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         _window = Mock<IWindow>();
         _request = new Request();
         _request.SetItem("data", "test");
      }

      [Test]
      public void Insert_the_presenter_UI_into_the_window()
      {
         using (Record)
         {
            _window.InitializeUI(_presenter.UI);
         }
         using (Playback)
         {
            _presenter.DisplayIn(_window);
         }
      }

      [Test, ExpectedException(typeof(NoUserInterfaceObjectException))]
      public void Throw_an_exception_if_the_window_ui_is_null()
      {
         using (Record)
         {
            _window.InitializeUI(_presenter.UI);
         }
         using (Playback)
         {
            _presenter.SetNullWindowUI();
            _presenter.DisplayIn(_window);
         }
      }

      [Test]
      public void Provide_a_means_of_controlling_the_window_from_the_presenter()
      {
         Assert.That(_presenter.Window, Is.TypeOf(typeof(NoWindowControls)));
         _presenter.DisplayIn(_window);
         Assert.That(_presenter.Window, Is.Not.TypeOf(typeof(NoWindowControls)));
      }

      [Test]
      public void Show_the_window_when_the_presenter_is_started()
      {
         using (Record)
         {
            _window.Show();
         }
         using (Playback)
         {
            _presenter.DisplayIn(_window);
            _presenter.Start(_request);
         }
      }

      [Test]
      public void Stop_tracking_the_window_when_the_presenter_is_closed()
      {
         using (Record)
         {
            _window.Closed -= null;
            LastCall.IgnoreArguments();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_window);
            _presenter.Start(_request);
            _presenter.Finish();
         }
      }

      [Test]
      public void Close_the_window_when_the_presenter_is_finished()
      {
         using (Record)
         {
            _window.Close();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_window);
            _presenter.Start(_request);
            _presenter.Finish();
         }
      }

      [Test]
      public void Track_when_the_window_is_closed()
      {
         using (Record)
         {
            _window.Closed += null;
            LastCall.IgnoreArguments();
         }
         using (Playback)
         {
           _presenter.DisplayIn(_window);
         }
      }

      [Test]
      public void Finish_the_presenter_when_the_window_is_closed()
      {
         IEventRaiser raiser;

         using (Record)
         {
            _window.Closed += null;
            LastCall.IgnoreArguments();
            raiser = LastCall.GetEventRaiser();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_window);
            _presenter.Start(_request);
            raiser.Raise(null, null);
            Assert.AreEqual(1, _presenter.FinishCount);
         }
      }
   }
}
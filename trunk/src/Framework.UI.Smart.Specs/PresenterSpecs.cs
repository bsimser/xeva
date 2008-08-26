using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_Presenter
{
   [TestFixture]
   public class When_activating_a_presenter_for_the_first_time : Spec
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
            _presenter.Activate(_request);
         }
      }

      [Test]
      public void Call_the_first_activation_hook()
      {
         Assert.AreEqual(0, _presenter.FirstActivationCallCount);
         _presenter.Activate(_request);
         Assert.AreEqual(1, _presenter.FirstActivationCallCount);
      }

      [Test, ExpectedException(typeof (ViewNotAvailableException))]
      public void Fail_if_the_view_is_not_available()
      {
         _presenter.View = null;
         _presenter.Activate(_request);
      }

      [Test]
      public void Initialize_request_data()
      {
         Assert.AreEqual(0, _presenter.HandleRequestCallCount);
         _presenter.Activate(_request);
         Assert.AreEqual(1, _presenter.HandleRequestCallCount);
      }

      [Test, ExpectedException(typeof (RequestItemRequiredException))]
      public void Throw_an_exception_if_data_is_missing_from_the_request()
      {
         var invalidRequest = new Request();
         _presenter.Activate(invalidRequest);
      }
   }

   [TestFixture]
   public class When_activating_a_presenter_multiple_times : Spec
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
      public void Only_call_the_first_activation_hook_once()
      {
         Assert.AreEqual(0, _presenter.FirstActivationCallCount);
         _presenter.Activate(_request);
         _presenter.Activate(_request);
         Assert.AreEqual(1, _presenter.FirstActivationCallCount);
      }

      [Test]
      public void Handle_the_request_for_each_activation()
      {
         Assert.AreEqual(0, _presenter.HandleRequestCallCount);
         _presenter.Activate(_request);
         _presenter.Activate(_request);
         Assert.AreEqual(2, _presenter.HandleRequestCallCount);
      }

      [Test]
      public void Call_the_every_activation_hook_every_time()
      {
         Assert.AreEqual(0, _presenter.EveryActivationCallCount);
         _presenter.Activate(_request);
         _presenter.Activate(_request);
         Assert.AreEqual(2, _presenter.HandleRequestCallCount);
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
         _presenter.Activate(_request);
         Assert.AreEqual(0, _presenter.FinishCount);
         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);
      }

      [Test]
      public void Do_not_finish_if_never_activated()
      {
         Assert.AreEqual(0, _presenter.FinishCount);
         _presenter.Finish();
         Assert.AreEqual(0, _presenter.FinishCount);
      }

      [Test]
      public void Fire_an_event_when_finished()
      {
         var finishedFired = 0;

         _presenter.Activate();
         _presenter.Finished += (s, e) => { finishedFired += 1; };
         _presenter.Finish();

         Assert.That(finishedFired, Is.EqualTo(1));
      }

      [Test]
      public void Only_finish_once()
      {
         _presenter.Activate(_request);
         Assert.AreEqual(0, _presenter.FinishCount);

         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);

         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);
      }

      [Test]
      public void Return_the_key_of_the_presenter_in_the_finished_event()
      {
         var knownKey = Guid.NewGuid().ToString();
         var eventKey = string.Empty;

         _presenter.Activate();
         _presenter.Key = knownKey;
         _presenter.Finished += (s, e) => { eventKey = e.Key; };
         _presenter.Finish();

         Assert.That(eventKey, Is.EqualTo(knownKey));
      }
   }

   [TestFixture]
   public class When_validating_data_used_in_the_presenter : Spec
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
      public void Return_false_if_the_data_is_not_valid()
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

      [Test]
      public void Return_true_if_the_data_is_valid()
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
   }

   [TestFixture]
   public class When_activating_a_presenter_that_does_not_implement_a_view_callback : Spec
   {
      private ExampleInvalidPresenter _presenter;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleInvalidPresenter>();
      }

      [Test, ExpectedException(typeof (NoCallbacksImplementationException))]
      public void Throw_an_exception()
      {
         _presenter.Activate(new NullRequest());
      }
   }

   [TestFixture]
   public class When_a_presenter_is_displayed_in_a_window : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IWindowManager _windowManager;
      private IRequest _request;
      private IWindowAdapter _windowAdapter;
      private IWindowOptions _windowOptions;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         _windowManager = Mock<IWindowManager>();
         _windowAdapter = Mock<IWindowAdapter>();
         _windowOptions = Mock<IWindowOptions>();
         _request = new Request();
         _request.SetItem("data", "test");
         SetupResult.For(_windowManager.CreateWindowFor(_presenter)).Return(_windowAdapter);
      }

      [Test]
      public void Close_the_window_when_the_presenter_is_finished()
      {
         using (Record)
         {
            _windowAdapter.Close();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_windowManager);
            _presenter.Activate(_request);
            _presenter.Finish();
         }
      }

      [Test]
      public void Do_not_show_the_window_if_the_presenter_is_not_activated()
      {
         using (Record)
         {
            _windowAdapter.Show();
            LastCall.Repeat.Never();
         }
         using (Playback)
         {
            _presenter.DisplayIn(_windowManager);
         }
      }

      [Test]
      public void Finish_the_presenter_when_the_window_is_closed()
      {
         IEventRaiser raiser;

         using (Record)
         {
            _windowAdapter.Closed += null;
            LastCall.IgnoreArguments();
            raiser = LastCall.GetEventRaiser();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_windowManager);
            _presenter.Activate(_request);
            raiser.Raise(null, null);
            Assert.AreEqual(1, _presenter.FinishCount);
         }
      }

      [Test]
      public void Provide_a_means_of_controlling_the_window_from_the_presenter()
      {
         using (Record)
         {
         }
         using (Playback)
         {
            Assert.That(_presenter.Window, Is.TypeOf(typeof (NoWindowControls)));
            _presenter.DisplayIn(_windowManager, _windowOptions);
            Assert.That(_presenter.Window, Is.Not.TypeOf(typeof (NoWindowControls)));
         }
      }

      [Test]
      public void Show_an_existing_window_after_the_presenter_has_started()
      {

         using (Record)
         {
            _windowAdapter.Show();
         }
         using (Playback)
         {
            _presenter.DisplayIn(_windowManager);
            _presenter.Activate();
         }
      }

      [Test]
      public void Show_the_window_if_the_presenter_has_been_activated()
      {
         using (Record)
         {
            _windowAdapter.Show();
         }
         using (Playback)
         {
            _presenter.Activate();
            _presenter.DisplayIn(_windowManager);
         }
      }

      [Test]
      public void Stop_tracking_window_closure_when_the_presenter_is_closed()
      {
         using (Record)
         {
            _windowAdapter.Closed -= null;
            LastCall.IgnoreArguments();
         }
         using (Playback)
         {
            Assert.AreEqual(0, _presenter.FinishCount);
            _presenter.DisplayIn(_windowManager);
            _presenter.Activate(_request);
            _presenter.Finish();
         }
      }

      [Test, ExpectedException(typeof (NoUserInterfaceObjectException))]
      public void Throw_an_exception_if_the_window_ui_is_null()
      {
         using (Record)
         {
         }
         using (Playback)
         {
            _presenter.SetNullWindowUI();
            _presenter.DisplayIn(_windowManager, _windowOptions);
         }
      }

      [Test]
      public void Track_when_the_window_is_closed()
      {
         using (Record)
         {
            _windowAdapter.Closed += null;
            LastCall.IgnoreArguments();
         }
         using (Playback)
         {
            _presenter.DisplayIn(_windowManager);
         }
      }
   }

   [TestFixture]
   public class When_refreshing_child_presenters : Spec
   {
      private ExampleWidgetPresenter _presenter;
      private IRefreshable _childPresenter1;
      private IRefreshable _childPresenter2;

      protected override void Before_each_spec()
      {
         _presenter = new ExampleWidgetPresenter();
         _childPresenter1 = Mock<IRefreshable>();
         _childPresenter2 = Mock<IRefreshable>(); 
      }

      [Test]
      public void Refresh_each_registered_child_presenter()
      {
         using (Record)
         {
            _childPresenter1.Refresh();
            _childPresenter2.Refresh();
         }
         using (Playback)
         {
            _presenter.Register(_childPresenter1);
            _presenter.Register(_childPresenter2);
            _presenter.RefreshAll();
         }
      }
   }
}
using System;
using Castle.Windsor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using XF;
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
      public void Call_custom_startup_code()
      {
         Assert.AreEqual(0, _presenter.StartCount);
         _presenter.Start(_request);
         Assert.AreEqual(1, _presenter.StartCount);
      }

      [Test]
      public void Only_start_once()
      {
         _presenter.Start(new NullRequest());
         Assert.AreEqual(1, _presenter.StartCount);
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
         var invalidRequest = new Request();
         _presenter.Start(invalidRequest);
      }
   }

   [TestFixture]
   public class When_starting_an_asynchronous_presenter : Spec
   {
      private ExampleAsyncPresenter _presenter;
      private IRequest _request;
      private IWindsorContainer _container;
      private IAsyncWorker _worker;

      protected override void Before_each_spec()
      {
         _worker = Mock<IAsyncWorker>();
         _container = Mock<IWindsorContainer>();
         Locator.Initialize(_container);

         _presenter = Create<ExampleAsyncPresenter>();
         _request = new Request();
         _request.SetItem("data", "test");
      }

      [Test]
      public void Attach_callbacks_to_the_view()
      {
         SetupResult
            .For(_container.Resolve<IAsyncWorker>())
            .Return(_worker);

         using (Record)
         {
            Get<IExampleAsyncView>().Attach(_presenter);
         }
         using (Playback)
         {
            _presenter.Start(_request);
         }
      }

      [Test]
      public void Get_an_async_work_and_initialize()
      {

         using (Record)
         {
            Expect
               .Call(_container.Resolve<IAsyncWorker>())
               .Return(_worker);
         }

         using (Playback)
         {
            _presenter.Start(_request);
         }
      }

      [Test]
      public void Run_the_async_worker_and_resume()
      {

         SetupResult
           .For(_container.Resolve<IAsyncWorker>())
           .Return(_worker);

         _worker.DoWork += null;
         IEventRaiser doWork = LastCall.IgnoreArguments().GetEventRaiser();

         _worker.RunWorkerCompleted += null;
         IEventRaiser workerDone = LastCall.IgnoreArguments().GetEventRaiser();

         using (Record)
         {
            Get<IExampleAsyncView>().ShowWaiting();
            _worker.RunWorkerAsync();
            Get<IExampleAsyncView>().HideWaiting();
         }

         using (Playback)
         {
            _presenter.Start(_request);
            doWork.Raise(null, null);
            workerDone.Raise(null,null);
         }
      }

   }

   [TestFixture]
   public class When_reinitializing_a_presenter : Spec
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
      public void Initialize_request_data()
      {
         Assert.AreEqual(0, _presenter.InitializeCount);
         _presenter.ReInitialize(_request);
         Assert.AreEqual(1, _presenter.InitializeCount);
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
      public void Only_finish_once()
      {
         _presenter.Start(_request);
         Assert.AreEqual(0, _presenter.FinishCount);
         
         _presenter.Finish();
         Assert.AreEqual(1, _presenter.FinishCount);

         _presenter.Finish();         
         Assert.AreEqual(1, _presenter.FinishCount);
      }

      [Test]
      public void Fire_an_event_when_finished()
      {
         var finishedFired = 0;

         _presenter.Start();
         _presenter.Finished += (s, e) => { finishedFired += 1; };
         _presenter.Finish();

         Assert.That(finishedFired, Is.EqualTo(1));
      }

      [Test]
      public void Return_the_key_of_the_presenter_in_the_finished_event()
      {
         var knownKey = Guid.NewGuid().ToString();
         var eventKey = string.Empty;

         _presenter.Start();
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
   }

   [TestFixture]
   public class When_starting_a_presenter_that_does_not_implement_a_view_callback : Spec
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
   public class When_a_presenter_is_displayed_in_a_window : Spec
   {
      /* TODO: Rework 
      private ExampleWidgetPresenter _presenter;
      private ExampleWindowManager _windowManager;
      private IRequest _request;

      protected override void Before_each_spec()
      {
         _presenter = Create<ExampleWidgetPresenter>();
         _windowAdapter = Mock<IWindowAdapter>();
         _request = new Request();
         _request.SetItem("data", "test");
      }

      [Test, ExpectedException(typeof(NoUserInterfaceObjectException))]
      public void Throw_an_exception_if_the_window_ui_is_null()
      {
         using (Record)
         {
            _windowAdapter.InitializeUI(_presenter.UI);
         }
         using (Playback)
         {
            _presenter.SetNullWindowUI();
            _presenter.DisplayIn(_windowAdapter);
         }
      }

      [Test]
      public void Provide_a_means_of_controlling_the_window_from_the_presenter()
      {
         Assert.That(_presenter.Window, Is.TypeOf(typeof(NoWindowControls)));
         _presenter.DisplayIn(_windowAdapter);
         Assert.That(_presenter.Window, Is.Not.TypeOf(typeof(NoWindowControls)));
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
            _presenter.DisplayIn(_windowAdapter);
            _presenter.Start(_request);
            _presenter.Finish();
         }
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
            _presenter.DisplayIn(_windowAdapter);
            _presenter.Start(_request);
            _presenter.Finish();
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
           _presenter.DisplayIn(_windowAdapter);
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
            _presenter.DisplayIn(_windowAdapter);
            _presenter.Start(_request);
            raiser.Raise(null, null);
            Assert.AreEqual(1, _presenter.FinishCount);
         }
      }

      [Test]
      public void Show_the_window_if_the_presenter_has_started()
      {
         using (Record)
         {
            _windowAdapter.Show();
         }
         using (Playback)
         {
            _presenter.Start();
            _presenter.DisplayIn(_windowAdapter);
         }
      }

      [Test]
      public void Do_not_show_the_window_if_the_presenter_has_not_started()
      {
         using (Record)
         {
            _windowAdapter.Show();
            LastCall.Repeat.Never();
         }
         using (Playback)
         {
            _presenter.DisplayIn(_windowAdapter);
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
            _presenter.DisplayIn(_windowAdapter);
            _presenter.Start();
         }
       }
       */
   }
}
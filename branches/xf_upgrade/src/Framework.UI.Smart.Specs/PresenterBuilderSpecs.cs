using System.Drawing;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;
using XF;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_PresenterBuilder
{
   public class When_creating_a_new_presenter : Spec
   {
      private IWindsorContainer _mockContainer;
      private IWindowManager _mockWindowManager;
      private IWindowAdapter _mockWindowAdapter;
      private ExampleWidgetPresenter _mockPresenter;

      protected override void Before_each_spec()
      {
         _mockContainer = Mock<IWindsorContainer>();
         Locator.Initialize(_mockContainer);
         _mockWindowManager = Mock<IWindowManager>();
         _mockWindowAdapter = Mock<IWindowAdapter>();
         _mockPresenter = new ExampleWidgetPresenter();
         SetupResult.For(_mockWindowManager.CreateWindowFor(null)).Return(_mockWindowAdapter).IgnoreArguments();
      }

      [Test]
      public void Accept_a_provided_window_manager()
      {
         using (Record)
         {
            SetupResult.For(_mockContainer.Resolve<ExampleWidgetPresenter>())
               .Return(_mockPresenter);
         }
         using (Playback)
         {
            New.Presenter<ExampleWidgetPresenter>();
         }
      }

      [Test]
      public void Obtain_a_presenter_instance_via_the_locator()
      {
         using (Record)
         {
            Expect.Call(_mockContainer.Resolve<ExampleWidgetPresenter>())
               .Return(_mockPresenter);
         }
         using (Playback)
         {
            New.Presenter<ExampleWidgetPresenter>();
         }
      }

      [Test]
      public void Optionally_obtain_a_window_manager_via_the_locator()
      {
         using (Record)
         {
            SetupResult.For(_mockContainer.Resolve<ExampleWidgetPresenter>())
               .Return(_mockPresenter);
            Expect.Call(_mockContainer.Resolve<ExampleWindowManager>())
               .Return(new ExampleWindowManager());
         }
         using (Playback)
         {
            New.Presenter<ExampleWidgetPresenter>().ManagedBy<ExampleWindowManager>();
         }
      }
   }
}
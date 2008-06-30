using BankTeller.UI.Smart;
using NUnit.Framework;
using Rhino.Mocks;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_MainWindowManager
{
   public class When_opening_a_window : Spec
   {
      private MainWindowManager _manager;
      private IWindowAdapter _mockWindowAdapter;
      private IPresenter _mockPresenter;

      protected override void Before_each_spec()
      {
         _manager = Create<MainWindowManager>();
         _mockWindowAdapter = Mock<IWindowAdapter>();
         _mockPresenter = Mock<IPresenter>();

         SetupResult
            .For(_mockPresenter.Key)
            .Return("Test");
      }

      [Test]
      public void Obtain_a_window_from_a_factory()
      {
         using (Record)
         {
            Expect.Call(Get<WindowFactory>().Create()).Return(_mockWindowAdapter);
         }
         using (Playback)
         {
            _manager.CreateWindowFor(_mockPresenter);
         }
      }
   }

   public class When_opening_a_new_window_and_an_existing_window_is_already_open : Spec
   {
      private MainWindowManager _manager;
      private IWindowAdapter _mockWindowAdapterExisting;
      private IPresenter _mockPresenterExisting;
      private IPresenter _mockPresenterNew;

      protected override void Before_each_spec()
      {
         _manager = Create<MainWindowManager>();
         _mockWindowAdapterExisting = Mock<IWindowAdapter>();
         _mockPresenterExisting = Mock<IPresenter>();
         _mockPresenterNew = Mock<IPresenter>();

         SetupResult
            .For(Get<WindowFactory>().Create())
            .Return(_mockWindowAdapterExisting);

         SetupResult
            .For(_mockPresenterExisting.Key)
            .Return("Existing");

         SetupResult
            .For(_mockPresenterNew.Key)
            .Return("New");
      }

      [Test]
      public void Should_hide_the_existing_window()
      {
         using (Record)
         {
            _mockWindowAdapterExisting.Hide();
         }
         using (Playback)
         {
            _manager.CreateWindowFor(_mockPresenterExisting);
            _manager.CreateWindowFor(_mockPresenterNew);
         }
      }
   }

   public class When_a_window_adapter_has_already_been_created : Spec
   {

      private MainWindowManager _manager;
      private IWindowAdapter _mockWindowAdapter;
      private IPresenter _mockPresenterExisting;
      private IPresenter _mockPresenterNew;

      protected override void Before_each_spec()
      {
         _manager = Create<MainWindowManager>();
         _mockWindowAdapter = Mock<IWindowAdapter>();
         _mockPresenterExisting = Mock<IPresenter>();
         _mockPresenterNew = Mock<IPresenter>();

         SetupResult
            .For(_mockPresenterExisting.Key)
            .Return("Existing");

         SetupResult
            .For(_mockPresenterNew.Key)
            .Return("New");
      }      

      [Test]
      public void Reuse_the_existing_adapter()
      {
         using (Record)
         {

            Expect.Call(Get<WindowFactory>().Create())
               .Return(_mockWindowAdapter)
               .Repeat.Twice();
         }
         using (Playback)
         {
            _manager.CreateWindowFor(_mockPresenterExisting);
            _manager.CreateWindowFor(_mockPresenterNew);
            _manager.CreateWindowFor(_mockPresenterExisting);
         }
      }
   }
}
using System;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class ControllerTests
   {
      private MockRepository _mocks;
      private IWindsorContainer _stubContainer;
      private ILayout _stubLayout;

      [SetUp]
      public void Setup()
      {
         this._mocks = new MockRepository();
         this._mocks.Stub<IPresenter>();

         this._stubContainer = _mocks.Stub<IWindsorContainer>();
         this._stubLayout = _mocks.Stub<ILayout>();

         SetupResult
            .For(_stubContainer.Resolve<ILayout>(""))
            .Return(_stubLayout)
            .IgnoreArguments();

         IoC.Initialize(_stubContainer);
      }

      public void Example()
      {
         ControllerBuilder.Configure()
            .AddSharedLayout("Entity", "Layouts.EntityLayout")
            .AddPresenter("One", "Presenters.One")
            .AddPresenter("Two", "Presenters.Two")
            .AddCommand("Refresh", "Commands.Refresh");
      }

      [Test]
      public void Can_create_an_empty_Controller()
      {
         IController controller = ControllerBuilder.Empty;
      }

      [Test]
      public void Can_add_a_Layout_from_configuration()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect.Call(mockContainer.Resolve<ILayout>("")).Return(stubLayout);
            LastCall.IgnoreArguments();
            LastCall.Repeat.Twice();
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            IDynamicController controller = ControllerBuilder.Configure().AddSharedLayout("Test", "SomeConfigKey").Done;
            Assert.IsNotNull(controller.FindLayout("Test"));
         }
      }

      [Test]
      public void Can_add_an_existing_Layout_object()
      {
         ILayout stubLayout = _mocks.CreateMock<ILayout>();

         IDynamicController controller =
            ControllerBuilder.Configure()
               .AddSharedLayout("TestLayout", stubLayout)
               .Done;

         ILayout Layout = controller.FindLayout("TestLayout");
         Assert.IsNotNull(Layout);
      }

      [Test]
      public void Can_add_any_number_of_Layouts()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect
               .Call(mockContainer.Resolve<ILayout>(""))
               .Return(stubLayout)
               .IgnoreArguments()
               .Repeat.Times(3);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            IDynamicController controller =
               ControllerBuilder.Configure()
                  .AddSharedLayout("one", "Layouts.Two")
                  .AddSharedLayout("two", "Layouts.Two")
                  .Done;

            Assert.IsNotNull(controller.FindLayout("one"));
            Assert.IsNotNull(controller.FindLayout("two"));
         }
      }

      [Test]
      public void Has_a_default_Layout()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();

         using (_mocks.Record())
         {
            Expect
               .Call(mockContainer.Resolve<ILayout>(ControllerBuilder.DEFAULT_LAYOUT_IOC_KEY))
               .Return(_stubLayout);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);
            IDynamicController controller = ControllerBuilder.Empty;
            Assert.IsNotNull(controller.FindLayout(ControllerBuilder.DEFAULT_LAYOUT_KEY));
         }
      }

      [Test]
      public void A_Layout_can_be_found_by_a_case_insensitive_key()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();

         using (_mocks.Record())
         {
            Expect.Call(mockContainer.Resolve<ILayout>(ControllerBuilder.DEFAULT_LAYOUT_IOC_KEY))
               .Return(_stubLayout);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            IDynamicController controller = ControllerBuilder.Empty;

            Assert.IsNotNull(controller.FindLayout("Default"));
            Assert.IsNotNull(controller.FindLayout("default"));
            Assert.IsNotNull(controller.FindLayout("DEFAULT"));
         }
      }

      [Test]
      public void Can_add_a_Presenter_from_configuration()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            SetupResult.For(mockContainer.Resolve<ILayout>(""))
               .IgnoreArguments()
               .Return(_stubLayout);

            Expect.Call(mockContainer.Resolve<IPresenter>(""))
               .IgnoreArguments()
               .Return(stubPresenter);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            ControllerBuilder.Configure()
               .AddPresenter("One", "Presenter.One");
         }
      }

      [Test]
      public void Can_add_an_existing_Presenter_object()
      {
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();

         ControllerBuilder.Configure()
               .AddPresenter("One", stubPresenter);
      }

      [Test]
      public void Can_add_multiple_Presenters()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            SetupResult.For(mockContainer.Resolve<ILayout>(""))
               .IgnoreArguments()
               .Return(_stubLayout);

            Expect.Call(mockContainer.Resolve<IPresenter>(""))
               .IgnoreArguments()
               .Return(stubPresenter);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            ControllerBuilder.Configure()
               .AddPresenter("One", stubPresenter)
               .AddPresenter("Two", "Presenter.Two");
         }
      }

      [Test]
      public void Each_Presenter_added_to_the_Controller_gets_an_associated_View_Command()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            SetupResult.For(mockContainer.Resolve<ILayout>(""))
               .IgnoreArguments()
               .Return(_stubLayout);

            mockPresenter.Start();

            Expect.Call(mockPresenter.ParentForm).Return(stubLayout);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            ControllerBuilder.Configure()
               .AddPresenter("One", mockPresenter)
               .Run("One");
         }
      }

      [Test]
      public void Can_add_a_Command_from_configuration()
      {
         IWindsorContainer mockContainer = _mocks.DynamicMock<IWindsorContainer>();
         ICommand stubCommand = _mocks.Stub<ICommand>();

         using (_mocks.Record())
         {
            SetupResult.For(mockContainer.Resolve<ICommand>("Commands.SomeCommand"))
               .IgnoreArguments()
               .Return(stubCommand);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            ControllerBuilder.Configure()
               .AddCommand("One", "SomeCommand");
         }
      }

      [Test]
      public void Can_add_an_existing_Command_object()
      {
         ICommand mockCommand = _mocks.DynamicMock<ICommand>();

         using (_mocks.Record())
         {
            SetupResult.For(mockCommand.Key).PropertyBehavior();
            mockCommand.Execute();
         }

         using (_mocks.Playback())
         {
            ControllerBuilder
               .Configure()
               .AddCommand("One", mockCommand)
               .Run("One");
         }
      }

      [Test, ExpectedException(typeof(UnspecifiedDefaultCommand))]
      public void Throws_an_exception_when_trying_to_run_an_unspecified_default_Command()
      {
         ControllerBuilder
            .Configure()
            .Run();
      }

      [Test]
      public void Can_link_commands()
      {
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();
         ICommand stubCommand = _mocks.Stub<ICommand>();

         ControllerBuilder
            .Configure()
            .AddPresenter("One", stubPresenter)
            .AddCommand("Two", stubCommand)
            .CreateLink("One", "Two");
      }

      [Test, ExpectedException(typeof(CommandNotFoundException))]
      public void Cannot_create_a_link_when_the_source_Command_has_not_been_added_to_the_Controller()
      {
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();

         ControllerBuilder
            .Configure()
            .AddPresenter("One", stubPresenter)
            .CreateLink("One", "Two");
      }

      [Test, ExpectedException(typeof(CommandNotFoundException))]
      public void Cannot_create_a_link_when_the_target_Command_has_not_been_added_to_the_Controller()
      {
         ICommand stubCommand = _mocks.Stub<ICommand>();

         ControllerBuilder
            .Configure()
            .AddCommand("Two", stubCommand)
            .CreateLink("One", "Two");
      }

      [Test, ExpectedException(typeof(RequiresViewCommandException))]
      public void When_creating_a_command_link_the_parent_must_be_a_View_Command()
      {
         ICommand stubCommand1 = _mocks.Stub<ICommand>();
         ICommand stubCommand2 = _mocks.Stub<ICommand>();

         ControllerBuilder
            .Configure()
            .AddCommand("One", stubCommand1)
            .AddCommand("Two", stubCommand2)
            .CreateLink("One", "Two");
      }
   }
}
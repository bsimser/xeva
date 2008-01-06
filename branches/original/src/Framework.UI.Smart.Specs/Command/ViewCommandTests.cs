using System;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class ViewCommandTests
   {
      private MockRepository _mocks;
      private IWindsorContainer _stubContainer;

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
         _stubContainer = _mocks.Stub<IWindsorContainer>();
         IoC.Initialize(_stubContainer);
      }

      [Test]
      public void Starts_the_Presenter_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            mockPresenter.Start();

            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.Execute();
         }
      }

      [Test]
      public void Resolves_the_appropriate_Layout_in_which_to_display_the_Presenter()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            Expect.Call(mockLayoutResolver.GetLayout()).Return(mockLayout);
            Expect.Call(mockPresenter.ParentForm).Return(mockLayout);
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.Execute();
         }
      }

      [Test]
      public void Displays_the_Presenter_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();
         ViewCommand theUnit = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);

            mockLayout.DisplayPresenter(mockPresenter);
         }

         using (_mocks.Playback())
         {
            
            theUnit.LayoutResolver = mockLayoutResolver;
            theUnit.Context = new Context();
            theUnit.Execute();
         }
      }

      [Test]
      public void Shows_the_Layout_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);

            mockLayout.ShowLayout();
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.Execute();
         }
      }

      [Test]
      public void Clears_existing_commands_from_the_Layout_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);
            
            mockLayout.ClearCommands();
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.Execute();
         }
      }

      [Test]
      public void Registers_all_Linked_Commands_with_the_Layout_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         ICommand stubInternalCommand1 = _mocks.Stub<ICommand>();
         LinkedCommand link1 = _mocks.Stub<LinkedCommand>(stubInternalCommand1);

         ICommand stubInternalCommand2 = _mocks.Stub<ICommand>();
         LinkedCommand link2 = _mocks.Stub<LinkedCommand>(stubInternalCommand2);

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         using (_mocks.Record())
         {
            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);

            mockLayout.RegisterLinkCommand(null);
            LastCall.IgnoreArguments().Repeat.Twice();
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.AddLink(link1);
            viewCommand.AddLink(link2);
            viewCommand.Execute();
         }
      }

      [Test]
      public void Registers_all_Child_Commands_with_the_Layout_on_execution()
      {
         IPresenter mockPresenter = _mocks.DynamicMock<IPresenter>();
         ILayoutResolver mockLayoutResolver = _mocks.DynamicMock<ILayoutResolver>();
         ILayout mockLayout = _mocks.DynamicMock<ILayout>();

         Command stubCommand1 = new Command();
         Command stubCommand2 = new Command();

         stubCommand1.Key = "Test1";
         stubCommand2.Key = "Test2";

         ViewCommand viewCommand = new ViewCommand(mockPresenter);

         stubCommand1.Parent = viewCommand;
         stubCommand2.Parent = viewCommand;

         using (_mocks.Record())
         {
            SetupResult.For(mockPresenter.ParentForm).Return(mockLayout);

            mockLayout.RegisterChildCommand(null);
            LastCall.IgnoreArguments().Repeat.Twice();
         }

         using (_mocks.Playback())
         {
            viewCommand.LayoutResolver = mockLayoutResolver;
            viewCommand.Context = new Context();
            viewCommand.Execute();
         }
      }

      [Test]
      public void Scans_the_Presenter_for_Parameters()
      {
         IPresenter stubPresenter = new StubPresenter();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));
         }

         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);
            Assert.AreEqual(3, theUnit.Parameters.Count);
         }
      }

      [Test]
      public void Scans_a_presenter_for_child_commands()
      {
         IPresenter stubPresenter = new StubPresenter();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));
         }

         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);
            Assert.AreEqual(3, theUnit.Children.Count);
         }
      }

      [Test]
      public void Determines_if_a_Command_is_enabled_based_on_whether_its_required_Parameters_are_found()
      {
         IPresenter stubPresenter = new StubPresenter();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));
         }

         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);

            Context context = new Context();
            theUnit.Context = context;

            context["EntityID"] = Guid.NewGuid();
            Assert.IsFalse(theUnit.Enabled);

            context["EntityType"] = "Policy";
            Assert.IsTrue(theUnit.Enabled);
         }
      }

      [Test]
      public void When_an_optional_Parameter_cannot_be_found_in_Context_the_Command_can_still_be_enabled()
      {
         IPresenter stubPresenter = new StubPresenter();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));
         }

         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);

            Context context = new Context();
            theUnit.Context = context;

            context["EntityID"] = Guid.NewGuid();
            Assert.IsFalse(theUnit.Enabled);

            context["EntityType"] = "Policy";
            Assert.IsTrue(theUnit.Enabled);

            // Note I didn't set context["Misc"]!
         }
      }

      [Test]
      public void Enabled_status_is_determined_when_Context_is_set()
      {
         IPresenter stubPresenter = _mocks.Stub<IPresenter>();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));
         }
         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);
            Context context = new Context();

            theUnit.Context = context;
            Assert.IsTrue(theUnit.Enabled);
         }
      }

      [Test]
      public void Parameters_are_set_with_cooresponding_Context_values_on_execution()
      {
         StubPresenter stubPresenter = _mocks.PartialMock<StubPresenter>();
         ILayoutResolver stubLayoutResolver = _mocks.Stub<ILayoutResolver>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         Guid stateGuid = Guid.NewGuid();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<ILabelLookupService>())
               .Return(new LabelLookupService(string.Empty));

            SetupResult.For(stubLayoutResolver.GetLayout()).Return(stubLayout);

            stubPresenter.RequireParameter1 = stateGuid;
            stubPresenter.RequireParameter2 = "Policy";
         }

         using (_mocks.Playback())
         {
            ViewCommand theUnit = new ViewCommand(stubPresenter);
            theUnit.LayoutResolver = stubLayoutResolver;

            Context context = new Context();

            theUnit.Context = context;

            context["EntityID"] = stateGuid;
            context["EntityType"] = "Policy";

            theUnit.Execute();
         }
      }
   }

   public class StubPresenter : IPresenter
   {
      private bool _isStarted;
      private object _UI;
      private ICommandScanner _Commands;
      private object _parentForm;
      private IContext _context = new Context();
      private string _Key;
      private string _Label;
      private string _requiredParameter2;
      private Guid _requiredParameter1;
      private string _optionalParameter3;

      public virtual ICommand Command
      {
         get { throw new NotImplementedException(); }
      }

      public void Start() {}

      public void Finish() {}

      public bool IsStarted
      {
         get { return this._isStarted; }
      }

      public object UI
      {
         get { return this._UI; }
      }

      public object ParentForm
      {
         get { return _parentForm; }
         set { _parentForm = value; }
      }

      public IContext Context
      {
         get { return _context; }
         set { _context = value; }
      }

      [Parameter("EntityID")]
      public virtual Guid RequireParameter1
      {
         set { _requiredParameter1 = value; }
      }

      [Parameter("EntityType")]
      public virtual string RequireParameter2
      {
         set { _requiredParameter2 = value; }
      }

      [Parameter("Misc", false)]
      public virtual string OptionalParameter3
      {
         set { _optionalParameter3 = value; }
      }

      [Command()]
      public void Command1() {}

      [Command()]
      public void Command2() {}

      [Command()]
      public void Command3() {}

      public string Key
      {
         get { return this._Key; }
         set { this._Key = value; }
      }

      public string Label
      {
         get { return this._Label; }
         set { this._Label = value; }
      }
   }
}
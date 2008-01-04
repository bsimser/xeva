using System.Collections.Generic;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class PresenterTests
   {
      private MockRepository _mocks;
      private IWindsorContainer _stubContainer;

      private SamplePresenter SamplePresenter_New()
      {
         return new SamplePresenter();
      }

      private SamplePresenter SamplePresenter_AlreadyStarted()
      {
         SamplePresenter result = new SamplePresenter();
         result.Start();
         return result;
      }

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
         _stubContainer = _mocks.Stub<IWindsorContainer>();
         IoC.Initialize(_stubContainer);
      }

      [Test]
      public void Provides_a_place_for_custom_code_to_be_executed_when_starting()
      {
         SamplePresenter sample = SamplePresenter_New();

         sample.Start();
         Assert.AreEqual(1, sample.CustomStartCalls);
      }

      [Test]
      public void Provides_a_place_for_custom_code_to_be_executed_when_finishing()
      {
         SamplePresenter sample = SamplePresenter_AlreadyStarted();

         sample.Finish();
         Assert.AreEqual(1, sample.CustomFinishCalls);
      }

      [Test]
      public void Will_only_ever_start_once()
      {
         SamplePresenter sample = SamplePresenter_New();

         sample.Start();
         sample.Start();
         Assert.AreEqual(1, sample.CustomStartCalls);
      }

      [Test]
      public void Will_only_ever_finish_once()
      {
         SamplePresenter sample = SamplePresenter_AlreadyStarted();

         sample.Finish();
         sample.Finish();
         Assert.AreEqual(1, sample.CustomFinishCalls);
      }

      [Test]
      public void Can_only_finish_if_it_has_been_started()
      {
         SamplePresenter sample = SamplePresenter_New();

         sample.Finish();
         Assert.AreEqual(0, sample.CustomFinishCalls);

         sample.Start();
         sample.Finish();
         Assert.AreEqual(1, sample.CustomFinishCalls);
      }

      [Test]
      public void Will_load_specified_parameters_into_context_when_started()
      {
         SamplePresenter theUnit = SamplePresenter_New();

         theUnit.Start();
         Assert.AreEqual(theUnit.Context.State.Count, 1);
      }

      [Test]
      public void Can_add_and_remove_state_from_a_presenters_context()
      {
         SamplePresenter theUnit = SamplePresenter_New();

         theUnit.Start();
         theUnit.AddContext("Field2", "Stub Data");
         Assert.AreEqual(theUnit.Context.State.Count, 2);

         theUnit.RemoveContext("Field2");
         Assert.AreEqual(theUnit.Context.State.Count, 1);
      }

      [Test]
      public void Can_create_a_child_presenter_and_set_its_context_and_state()
      {
         SamplePresenter theUnit = SamplePresenter_New();
         theUnit.Field1 = "Stub Data";

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<SamplePresenter>())
               .Return(new SamplePresenter());
         }

         using (_mocks.Playback())
         {
            theUnit.Start();
            SamplePresenter childPresenter = theUnit.CreateChildPresenter<SamplePresenter>();

            Assert.IsTrue(childPresenter.Context.Contains("Field1"));
            Assert.AreEqual(childPresenter.Field1, "Stub Data");
         }
      }

      [Test]
      public void Can_get_a_list_of_context_state()
      {
         SamplePresenter theUnit = SamplePresenter_New();
         theUnit.Field1 = "Stub Data";

         theUnit.Start();
         List<string> result = theUnit.ContextList;

         Assert.AreEqual(result.Count, 1);
      }

      [Test]
      public void Can_get_a_single_piece_of_context_state()
      {
         SamplePresenter theUnit = SamplePresenter_New();
         theUnit.Field1 = "Stub Data";

         theUnit.Start();
         object result = theUnit.GetContext("Field1");

         Assert.AreEqual(result, "Stub Data");
      }

      [Test]
      public void Should_be_able_to_create_a_disconnected_presenter()
      {
         IFormBuilder formBuilder = _mocks.DynamicMock<IFormBuilder>();
         SamplePresenter theUnit = SamplePresenter_New();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<SamplePresenter>())
               .Return(new SamplePresenter());
         }

         using (_mocks.Playback())
         {
            theUnit.FormBuilder = formBuilder;
            theUnit.CreateDisconnectedPresenter<SamplePresenter>("SampleView", "Empty");
         }
      }

      [Test]
      public void Can_create_a_child_presenter_based_on_IoC_container_key()
      {
         SamplePresenter theUnit = SamplePresenter_New();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve("Stub"))
               .Return(new SamplePresenter());
         }

         using (_mocks.Playback())
         {
            theUnit.Start();
            IPresenter child = theUnit.CreateChildPresenter("Stub");

            Assert.AreEqual(child.Context.State.Count, 1);
         }
      }

      [Test]
      public void Should_initialize_a_FormBuilder_from_IOC_Container()
      {
         IFormBuilder builder = _mocks.DynamicMock<IFormBuilder>();
         SamplePresenter theUnit = SamplePresenter_New();

         using (_mocks.Record())
         {
            SetupResult
               .For(_stubContainer.Resolve<IFormBuilder>())
               .Return(builder);
         }

         using (_mocks.Playback())
         {
            IFormBuilder result = theUnit.FormBuilder;

            Assert.AreEqual(result, builder);
         }
      }

      [Test]
      public void Should_be_able_to_return_values_for_all_properties()
      {
         ISampleView view = _mocks.DynamicMock<ISampleView>();
         SamplePresenter theUnit = new SamplePresenter();
         string key = theUnit.Key;
         string label = theUnit.Label;
         object ui = theUnit.UI;
         theUnit.View = view;
         ISampleView view1 = theUnit.View;
      }

      [Test]
      public void Should_Be_able_to_add_a_validation_object_to_the_presenters_collection()
      {
         SamplePresenter sample = SamplePresenter_New();

         Assert.AreEqual(sample.ValidatoinObjects.Count, 0);
         sample.AddValidationObject("test", null);
         Assert.AreEqual(sample.ValidatoinObjects.Count, 1);
      }
   }

   public class SamplePresenter : Presenter<ISampleView>
   {
      public int CustomStartCalls = 0;
      public int CustomFinishCalls = 0;
      private string _field1;

      [Parameter("Field1")]
      public string Field1
      {
         get { return _field1; }
         set { _field1 = value; }
      }

      public override void CustomStart()
      {
         base.CustomStart();
         CustomStartCalls += 1;
      }

      public override void CustomFinish()
      {
         base.CustomFinish();
         CustomFinishCalls += 1;
      }
   }

   public interface ISampleView : IView { }
}
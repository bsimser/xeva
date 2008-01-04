using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class LabelLookupServiceTests
   {
      private MockRepository _mocks;

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
      }

      [Test]
      public void Will_return_the_key_if_the_label_does_not_exist()
      {
         LabelLookupService service = new LabelLookupService(string.Empty);
         Assert.AreEqual("test.123", service.GetLabel("test.123"));
      }

      [Test]
      public void Can_save_a_list_of_labels_stored_internally_to_durable_storage()
      {
         ILabelStore mockLabelStore = _mocks.DynamicMock<ILabelStore>();

         using (_mocks.Record())
         {
            mockLabelStore.PersistLabels(null);
            LastCall.Constraints(List.Count(Is.Equal(2)));
         }

         using (_mocks.Playback())
         {
            LabelLookupService service = new LabelLookupService(mockLabelStore);
            service.GetLabel("Test1");
            service.GetLabel("Test2");
            service.SaveLabels();
         }
      }

      [Test]
      public void Can_read_a_list_of_labels_stored_internally_to_durable_storage()
      {
         ILabelStore mockLabelStore = _mocks.DynamicMock<ILabelStore>();

         using (_mocks.Record())
         {
            Expect.Call(mockLabelStore.ReadLabels()).Return(GetStubLabels(3));
         }

         using (_mocks.Playback())
         {
            LabelLookupService service = new LabelLookupService(mockLabelStore);
            service.ReadLabels();
         }
      }

      [Test]
      public void Can_determine_if_a_given_key_has_a_cooresponding_label()
      {
         LabelLookupService service = new LabelLookupService(string.Empty);

         Assert.IsFalse(service.LabelExists("Test"));

         service.RegisterLabel("Test", "TestLabel");

         Assert.IsTrue(service.LabelExists("Test"));
      }

      private IDictionary<string, string> GetStubLabels(int numberOfLabels)
      {
         Dictionary<string, string> result = new Dictionary<string, string>();

         for (int c = 1; c <= numberOfLabels; c++)
         {
            result.Add(c.ToString(), (c+c).ToString());
         }

         return result;
      }
   }
}
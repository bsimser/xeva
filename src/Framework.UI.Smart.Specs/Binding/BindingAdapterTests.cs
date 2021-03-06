using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Rhino.Mocks;

namespace XF.UI.Smart
{
   [TestFixture]
   public class BindingAdapterTests
   {
      private MockRepository _mocks;
      private IBindingFilter<FakeObject> _filter;
      private IList<FakeObject> _list;
      private BindingAdapter<FakeObject> _theUnit;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _filter = _mocks.CreateMock<IBindingFilter<FakeObject>>();
         _list = GetStubbedInputList();
         _theUnit = new BindingAdapter<FakeObject>(_list);
         _theUnit.BindingFilter = _filter;
      }

      [Test]
      public void Can_instantiate_the_adapter_from_an_IList()
      {
         Assert.AreEqual(_theUnit.Count, _list.Count);
      }

      [Test]
      public void Can_sort_the_list()
      {
         Assert.AreEqual(_theUnit.Items[0].Position, "2");

         FakeObject fakeObject = new FakeObject("", "", "");

         PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(fakeObject);
         PropertyDescriptor sortProperty = properties.Find("Position", false);

         List<ListSortDescription> descriptions = new List<ListSortDescription>();
         descriptions.Add(new ListSortDescription(sortProperty, new ListSortDirection()));

         ListSortDescriptionCollection sorts = new ListSortDescriptionCollection(descriptions.ToArray());

         _theUnit.ApplySort(sorts);

         Assert.AreEqual(_theUnit.Items[0].Position, "1");
      }

      private IList<FakeObject> GetStubbedInputList()
      {
         IList<FakeObject> result = new List<FakeObject>();
         FakeObject obj1 = new FakeObject("2", "User1", "Tester");
         result.Add(obj1);
         FakeObject obj2 = new FakeObject("1", "User2", "Coder");
         result.Add(obj2);
         FakeObject obj3 = new FakeObject("3", "User3", "Manager");
         result.Add(obj3);
         return result;
      }

   }
}
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using XF.Specs;
using XF.Validation;

namespace Specs_for_NotEmptyAttribute
{
   [TestFixture]
   public class When_validating_a_guid_property : Spec
   {
      private Validator _validator;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
      }

      [Test]
      public void Fail_when_the_value_equals_an_empty_guid()
      {
         NotEmptyExampleWithGuid example = new NotEmptyExampleWithGuid();
         example.Guid = Guid.Empty;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Pass_when_the_value_is_not_empty()
      {
         NotEmptyExampleWithGuid example = new NotEmptyExampleWithGuid();
         example.Guid = Guid.NewGuid();

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(0, messages.Count);
      }
   }

   [TestFixture]
   public class When_validating_an_enumerable_property : Spec
   {
      private Validator _validator;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
      }

      [Test]
      public void Fail_if_the_count_is_zero()
      {
         NotEmptyExampleWithCollection example = new NotEmptyExampleWithCollection();

         List<string> list = new List<string>();
         // no elements in list!
         example.List = list;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Pass_if_the_count_is_one_or_more()
      {
         NotEmptyExampleWithCollection example = new NotEmptyExampleWithCollection();

         List<string> list = new List<string>();
         list.Add("one");

         example.List = list;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(0, messages.Count);
      }
   }

   [TestFixture]
   public class When_validating_a_string_property : Spec
   {
      private Validator _validator;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
      }

      [Test]
      public void Fail_if_the_value_is_empty()
      {
         NotEmptyExampleWithString example = new NotEmptyExampleWithString();

         example.String = string.Empty;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Fail_if_the_value_is_null()
      {
         NotEmptyExampleWithString example = new NotEmptyExampleWithString();

         example.String = null;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Pass_if_the_value_is_not_empty()
      {
         NotEmptyExampleWithString example = new NotEmptyExampleWithString();

         example.String = "something";

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(0, messages.Count);
      }
   }

   public class NotEmptyExampleWithString
   {
      private string _stringField;

      [NotEmpty]
      public string String
      {
         get { return _stringField; }
         set { _stringField = value; }
      }
   }

   public class NotEmptyExampleWithGuid
   {
      private Guid _guid;

      [NotEmpty]
      public Guid Guid
      {
         get { return _guid; }
         set { _guid = value; }
      }
   }

   public class NotEmptyExampleWithCollection
   {
      private ICollection<string> _list;

      [NotEmpty]
      public ICollection<string> List
      {
         get { return _list; }
         set { _list = value; }
      }
   }
}
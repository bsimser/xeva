using System;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace XF.Services
{
   [TestFixture]
   public class AdapterTests
   {
      private MockRepository _mocks;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
      }

      [Test]
      public void Should_be_able_to_initialize_an_SerializeAdapter_with_a_given_type()
      {
         ISerializeAdapter theUnit = new XMLSerializeAdapter();
         theUnit.Initialize(typeof(string));
      }

      [Test]
      public void Should_be_able_to_initialize_an_StreamAdapter_with_a_given_type()
      {
         IStreamAdapter theUnit = new StreamAdapter();
         theUnit.Initialize();
         Stream stream = theUnit.Stream;
         bool isOpen = theUnit.IsOpen;
      }

      [Test] public void Should_be_bale_to_close_a_stream()
      {
         IStreamAdapter streamAdapter = new StreamAdapter();
         streamAdapter.Initialize();

         streamAdapter.Close();
      }

      [Test]
      public void Should_be_able_to_serialize_an_object_of_the_same_type_that_was_initialized()
      {
         IStreamAdapter streamAdapter = new StreamAdapter();
         streamAdapter.Initialize();

         FakeObject fakey = new FakeObject();
         fakey.ObjectName = "Fakey";
         fakey.ObjectDescription = "Test object";
         fakey.ObjectID = Guid.NewGuid();

         ISerializeAdapter theUnit = new XMLSerializeAdapter();
         theUnit.Initialize(typeof(FakeObject));
         theUnit.Serialize(streamAdapter, fakey);
        
      }

      [Test]
      public void Will_initialize_the_stream_prior_to_Reading()
      {
         IStreamAdapter streamAdapter = new StreamAdapter();
         streamAdapter.Initialize();
         streamAdapter.Close();

         string xmlResult = streamAdapter.ReadString();
      }

      [Test]
      public void Will_initialize_the_stream_prior_to_Writing()
      {
         IStreamAdapter streamAdapter = new StreamAdapter();
         FakeObject fakey = new FakeObject();
         fakey.ObjectName = "Fakey";
         fakey.ObjectDescription = "Test object";
         fakey.ObjectID = Guid.NewGuid();

         ISerializeAdapter theUnit = new XMLSerializeAdapter();
         theUnit.Initialize(typeof(FakeObject));
         theUnit.Serialize(streamAdapter, fakey);
         string xmlResult = streamAdapter.ReadString();

         streamAdapter.Initialize();
         streamAdapter.Close();
         streamAdapter.WriteString(xmlResult);
         object result = theUnit.Deserialize(streamAdapter);

      }

      [Test]
      public void Should_be_able_to_deserialize_an_object_of_the_same_type_that_was_initialized()
      {
         IStreamAdapter streamAdapter = new StreamAdapter();
         streamAdapter.Initialize();

         FakeObject fakey = new FakeObject();
         fakey.ObjectName = "Fakey";
         fakey.ObjectDescription = "Test object";
         fakey.ObjectID = Guid.NewGuid();

         ISerializeAdapter theUnit = new XMLSerializeAdapter();
         theUnit.Initialize(typeof(FakeObject));
         theUnit.Serialize(streamAdapter, fakey);
         string xmlResult = streamAdapter.ReadString();

         streamAdapter.Initialize();
         streamAdapter.WriteString(xmlResult);
         object result = theUnit.Deserialize(streamAdapter);

      }


   }
}

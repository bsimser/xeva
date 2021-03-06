using System;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace XF.Services
{
   [TestFixture]
   public class Specs_for_BinaryMessageSerializerSpecs
   {
      private MockRepository _mocks;
      private IStreamAdapter _streamAdapter;
      private ISerializeAdapter _serializeAdapter;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();

         _streamAdapter = _mocks.DynamicMock<IStreamAdapter>();
         _serializeAdapter = _mocks.DynamicMock<ISerializeAdapter>();
      }

      [Test]
      public void A_new_MessageSerializer_can_be_initialize_with_an_IStreamAdapter_and_an_ISerializeAdapter()
      {
         using (IBinaryMessageSerializer theUnit = new BinaryMessageSerializer(_streamAdapter, _serializeAdapter))
         {
         }
      }

      [Test]
      public void A_MessageSerializer_will_close_its_IStreamAdapter_when_it_is_disposed()
      {
         SetupResult.For(_streamAdapter.IsOpen)
            .Return(true);

         using (_mocks.Record())
         {
            _streamAdapter.Close();
         }

         using (_mocks.Playback())
         {
            using (IBinaryMessageSerializer theUnit = new BinaryMessageSerializer(_streamAdapter, _serializeAdapter))
            {
            }
         }
      }

      [Test]
      public void Should_serialize_an_object_to_byte_array_then_return_the_document_as_a_string()
      {
         using (_mocks.Record())
         {
            SetupResult
               .For(_streamAdapter.Stream)
               .Return(new MemoryStream());
            SetupResult
               .For(_streamAdapter.ReadString())
               .Return("BinaryText");
         }

         using (_mocks.Playback())
         {
            using (IBinaryMessageSerializer theUnit = new BinaryMessageSerializer(_streamAdapter, _serializeAdapter))
            {
               theUnit.Serialize(Guid.NewGuid());
            }
         }
      }

      [Test]
      public void Should_deserialize_an_binary_document_and_return_the_rehydrated_object()
      {
         using (_mocks.Record())
         {
            _streamAdapter.WriteBinary(new byte[0]);
            SetupResult
               .For(_serializeAdapter.Deserialize(_streamAdapter))
               .Return(Guid.NewGuid());
         }

         using (_mocks.Playback())
         {
            using (IBinaryMessageSerializer theUnit = new BinaryMessageSerializer(_streamAdapter, _serializeAdapter))
            {
               theUnit.Deserialize(new byte[0]);
            }
         }
      }
   }
}
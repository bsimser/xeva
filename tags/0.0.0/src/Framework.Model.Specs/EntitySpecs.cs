using System;
using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Specs;

namespace XEVA.Framework.Model
{

   [TestFixture]
   public class When_determining_the_equality_of_two_entities : Spec 
   {

      [Test]
      public void They_should_be_equivalent_if_they_have_the_same_ID()
      {
         Guid id = Guid.NewGuid();

         Entity entity1 = Partial<Entity>();
         Entity entity2 = Partial<Entity>();

         using (Record)
         {
            Expect.Call(entity1.ID).Return(id);
            LastCall.Repeat.Any();
            Expect.Call(entity2.ID).Return(id);
            LastCall.Repeat.Any();
         }

         using (Playback)
         {
            Assert.IsTrue(entity1.Equals(entity2));
            Assert.IsTrue(entity2.Equals(entity1));
         }
      }

      [Test]
      public void They_should_not_be_equivalent_if_they_have_the_same_ID_but_are_a_different_version()
      {
         Guid id = Guid.NewGuid();

         Entity entity1 = Partial<Entity>();
         Entity entity2 = Partial<Entity>();

         using (Record)
         {
            Expect.Call(entity1.ID).Return(id);
            LastCall.Repeat.Any();
            Expect.Call(entity1.Version).Return(3);
            LastCall.Repeat.Any();
            Expect.Call(entity2.ID).Return(id);
            LastCall.Repeat.Any();
            Expect.Call(entity2.Version).Return(2);
            LastCall.Repeat.Any();
         }

         using (Playback)
         {
            Assert.IsFalse(entity1.Equals(entity2));
            Assert.IsFalse(entity2.Equals(entity1));
         }
      }

      [Test]
      public void An_entity_never_equals_null()
      {
         FakeEntity entity = new FakeEntity();
         Assert.IsFalse(entity.Equals(null));
      }

      [Test]
      public void Entities_of_different_types_are_never_equivalent()
      {
         FakeEntity entityA = new FakeEntity();
         DifferentEntity entityB = new DifferentEntity();

         Assert.IsFalse(entityA.Equals(entityB));
      }
 
   }

   [TestFixture]
   public class When_converting_an_entity_to_a_string : Spec
   {
      [Test]
      public void Use_a_string_representation_of_the_ID()
      {
         Guid id = Guid.NewGuid();
         FakeEntity entity = new FakeEntity();
         entity.ID = id;

         Assert.AreEqual(id.ToString(), entity.ToString());
      }
   }
}
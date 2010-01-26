using System;
using NUnit.Framework;

namespace XF.Model
{
   [TestFixture]
   public class ProjectorSpecs
   {
      
      [Test]
      public void Can_add_criteria_on_root()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.ID).Eq(Guid.Empty).AddCriteria();
      }

      [Test]
      public void Can_specify_criteria_equals_operator()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.ID).Eq(Guid.NewGuid())
                              .AddCriteria();
      }

      [Test]
      public void Can_specify_criteria_equals_operator_with_wildcard()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.Name).Eq("doe*")
                              .AddCriteria();
      }

      [Test]
      public void Can_specify_criteria_with_an_and_conjunction()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.Name).Eq("doe*")
                                         .And(e =>e.Title).Eq("Mr")
                              .AddCriteria();
      }

      [Test]
      public void Can_specify_criteria_with_an_or_conjunction()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.Name).Eq("doe*")
                                         .Or(e => e.Title).Eq("Mr")
                              .AddCriteria();
      }

      [Test]
      public void Can_add_criteria_on_secondary_entity()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Reference<MyEntity2, MyMessage>(e => e.Entity2)
                                 .ReferenceAsProperty()
                                 .Projection(mess => mess.Age).Add()
                                 .Projection(mess => mess.Sex).Add()
                                 .Criteria().Where(e2 => e2.Age).GT(20).AddCriteria()
                                 .AddReference();
      }

      [Test]
      public void Can_add_criteria_for_secondary_entity_with_and()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.Name).Eq("Doe").AddCriteria()
                              .Reference<MyEntity2, MyMessage>(e => e.Entity2)
                                 .ReferenceAsProperty()
                                 .Projection(mess => mess.Age).Add()
                                 .Projection(mess => mess.Sex).Add()
                                 .Criteria().AddWithAnd()
                                    .Where(e2 => e2.Age).GT(20).AddCriteria()
                                 .AddReference();
      }

      [Test]
      public void Can_add_criteria_for_secondary_entity_with_or()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Criteria().Where(e => e.Name).Eq(string.Empty).AddCriteria()
                              .Reference<MyEntity2, MyMessage>(e => e.Entity2)
                                 .ReferenceAsProperty()
                                 .Projection(mess => mess.Age).Add()
                                 .Projection(mess => mess.Sex).Add()
                                 .Criteria().AddWithOr()
                                    .Where(e2 => e2.Age).GT(20)
                                    .And(e2 =>e2.Sex).Eq("Male")
                                    .AddCriteria()
                                 .AddReference();
      }

      [Test]
      public void Can_set_a_criteria_to_null()
      {
         var projector = new EntityProjector<MyEntity, MyMessage>()
                              .Key(e => e.ID, mess => mess.ID)
                              .Projection(mess => mess.Name).Add()
                              .Reference<MyEntity2, MyMessage>(e => e.Entity2)
                                 .ReferenceAsProperty()
                                 .Projection(mess => mess.Age).Add()
                                 .Projection(mess => mess.Sex).Add()
                                 .Criteria().Where(e2 => e2.Sex).IsNull().AddCriteria()
                                 .AddReference()
                              .Project();
      }

   }

   public class MyEntity : Entity
   {
      public string Name { get; set; }
      public string Title { get; set; }
      public DateTime Hired { get; set; }
      public MyEntity2 Entity2 { get; set; }
   }

   public class MyEntity2
   {
      public int Age { get; set; }
      public string Sex { get; set; }
   }

   public class MyMessage
   {
      public object ID { get; set; }
      public string Name { get; set; }
      public string Title { get; set; }
      public DateTime Hired { get; set; }
      public int Age { get; set; }
      public string Sex { get; set; }
   }
}
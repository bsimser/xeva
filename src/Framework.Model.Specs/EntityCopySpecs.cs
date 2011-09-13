using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace XF.Model {
   [TestFixture]
   public class EntityCopySpecs {

      [Test]
      public void Can_copy_from_template() {
         var entity1 = new FakeEntity { Name = "First Instance", Ttile = "#1", Age = 32, DOB = DateTime.Now };
         var entity2 = entity1.TemplateCopy() as FakeEntity;

         Assert.That(entity1.Name == entity2.Name);
      }

      [Test]
      public void Can_copy_from_template_withcopy_actions() {
         var entity1 = new FakeEntity { Name = "First Instance", Ttile = "#1", Age = 32, DOB = DateTime.Now };
         var actions = new List<KeyValuePair<Action<object>, object>>();
         actions.Add(new KeyValuePair<Action<object>, object>(entity1.SetDate, 35));
         var entity2 = entity1.TemplateCopy(actions) as FakeEntity;

         Assert.That(entity1.Name == entity2.Name &&
                     entity1.Age == 32 && entity2.Age == 35);
      }

   }
}
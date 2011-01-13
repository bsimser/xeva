using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace XF.Model {
   public sealed class CloningTool {
      public static Entity GenerateEntityClone(Type entityType, Entity origEntity, Entity parent) {
         var newEntity = Activator.CreateInstance(entityType) as Entity;
         var properties = new List<PropertyInfo>(entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance));

         properties.ForEach(property => {
            try {
               if (property.DeclaringType != entityType) return;

               var attrs = property.GetCustomAttributes(typeof(ModelCopyAttribute), true);
               ModelCopyAttribute copyAttr;

               if (attrs == null || attrs.Length == 0)
                  copyAttr = new ModelCopyAttribute { Method = CopyMethod.Copy };
               else
                  copyAttr = attrs[0] as ModelCopyAttribute;


               switch (copyAttr.Method) {
                  case CopyMethod.Parent:
                     CopyMethodParent(newEntity, parent, property);
                     break;
                  case CopyMethod.Copy:
                     CopyMethodCopy(newEntity, origEntity, property);
                     break;
                  case CopyMethod.Generate:
                     CopyMethodGenerate(newEntity, copyAttr, property);
                     break;
                  case CopyMethod.Clone:
                     if (!property.IsCollection() && property.PropertyType.BaseType != typeof(Entity)) return;
                     if (property.IsCollection())
                        CopyMethodCloneAsList(newEntity, origEntity, property);
                     else
                        CopyMethodCloneAsEntity(newEntity, origEntity, property);
                     break;
               }
            }
            catch (Exception e) {
               throw;
            }
         });
         return newEntity;
      }

      private static void CopyMethodParent(Entity newEntity, Entity parent, PropertyInfo property) {
         property.SetValue(newEntity, parent, null);
      }

      private static void CopyMethodCopy(Entity newEntity, Entity origEntity, PropertyInfo property) {
         property.SetValue(newEntity, property.GetValue(origEntity, null), null);
      }

      private static void CopyMethodGenerate(Entity newEntity, ModelCopyAttribute copyAttr, PropertyInfo property) {
         property.SetValue(newEntity, copyAttr.Generate(), null);
      }

      private static void CopyMethodCloneAsEntity(Entity newEntity, Entity origEntity, PropertyInfo property) {
         var propValue = property.GetValue(origEntity, null) as Entity;
         property.SetValue(newEntity, propValue.Clone(newEntity), null);
      }

      private static void CopyMethodCloneAsList(Entity newEntity, Entity origEntity, PropertyInfo property) {
         var propValue = property.GetValue(origEntity, null) as IEnumerable;
         if (propValue == null) return;

         var enumerable = Activator.CreateInstance(property.PropertyFQN());
         var addMethod = enumerable.GetType().GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
         if (addMethod == null) return;

         property.SetValue(newEntity, enumerable, null);
         foreach (var child in propValue) {
            if (child is Entity)
               addMethod.Invoke(enumerable, new[] { ((Entity)child).Clone(newEntity) });
            else
               addMethod.Invoke(enumerable, new[] { child });
         }
      }

   }
}
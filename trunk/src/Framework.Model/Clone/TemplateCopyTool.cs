using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace XF.Model {
   public sealed class TemplateCopyTool {

      public static Entity GenerateTemplateCopy(Type entityType, Entity origEntity, Entity parent,
                                               List<KeyValuePair<Action<object>, object>> copyActions) {
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
                  case CopyMethod.Template:
                     if (!property.IsCollection() && !property.ContainsType(typeof(IEntity))) return;
                     if (property.IsCollection())
                        CopyMethodTemplateAsList(newEntity, origEntity, property, copyActions);
                     else
                        CopyMethodTemplateAsEntity(newEntity, origEntity, property, copyActions);
                     break;
               }
            }
            catch (Exception e) {
               throw;
            }
         });
         if (copyActions == null) return newEntity;

         copyActions.ForEach(action => {
            var method = newEntity.GetType().GetMethod(action.Key.Method.Name);
            if (method == null) return;
            method.Invoke(newEntity, new[] { action.Value });
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

      private static void CopyMethodTemplateAsEntity(Entity newEntity, Entity origEntity, PropertyInfo property,
                                                 List<KeyValuePair<Action<object>, object>> copyActions) {
         var propValue = property.GetValue(origEntity, null) as Entity;
         var template = propValue.TemplateCopy(newEntity, copyActions);
         property.SetValue(newEntity, template, null);
         if (copyActions == null) return;

         copyActions.ForEach(action => {
            var method = template.GetType().GetMethod(action.Key.Method.Name);
            if (method == null) return;
            method.Invoke(template, new[] { action.Value });
         });
      }

      private static void CopyMethodTemplateAsList(Entity newEntity, Entity origEntity, PropertyInfo property,
                                                List<KeyValuePair<Action<object>, object>> copyActions) {
         var propValue = property.GetValue(origEntity, null) as IEnumerable;
         if (propValue == null) return;

         var enumerable = Activator.CreateInstance(property.PropertyFQN());
         var addMethod = enumerable.GetType().GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
         if (addMethod == null) return;

         property.SetValue(newEntity, enumerable, null);
         foreach (var child in propValue) {
            var copy = child is Entity ? ((Entity)child).TemplateCopy(newEntity, copyActions) : child;
            addMethod.Invoke(enumerable, new[] { copy });
            if (copyActions == null) continue;

            copyActions.ForEach(action => {
               var method = copy.GetType().GetMethod(action.Key.Method.Name);
               if (method == null) return;
               method.Invoke(copy, new[] { action.Value });
            });
         }
      }

   }
}
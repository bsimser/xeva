using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace XF.Model {
   public sealed class TemplateCopyTool {

      public static Entity GenerateTemplateCopy(Type copyType, Entity origEntity, Entity parent,
                                               List<KeyValuePair<Action<object>, object>> copyActions) {
         var copyEntity = Activator.CreateInstance(copyType) as Entity;
         var origProperties = new List<PropertyInfo>(origEntity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance));

         origProperties.ForEach(origProperty => {
            try {

               var origValue = origProperty.GetValue(origEntity, null);
               var attrs = origProperty.GetCustomAttributes(typeof(ModelCopyAttribute), true).ToList();
               var copyAttr = attrs.IsNotEmpty()
                  ? attrs.First() as ModelCopyAttribute
                  : new ModelCopyAttribute { Method = CopyMethod.Copy };
               var copyProperty = copyEntity.GetType().GetProperty(origProperty.Name,
                                                               BindingFlags.Public | BindingFlags.Instance);
               if(origValue == null ||copyProperty == null) return;

               switch (copyAttr.Method) {
                  case CopyMethod.Copy:
                     CopyMethodCopy(copyEntity, copyProperty, origValue);
                     break;
                  case CopyMethod.Parent:
                     CopyMethodParent(copyEntity, copyProperty, parent);
                     break;
                  case CopyMethod.Generate:
                     CopyMethodGenerate(copyEntity, copyProperty, copyAttr);
                     break;
                  case CopyMethod.Template:
                     if (!origProperty.IsCollection() && !origProperty.ContainsType(typeof(IEntity))) return;
                     if (origProperty.IsCollection())
                        CopyMethodTemplateAsList(copyEntity, copyProperty, origValue as IEnumerable, copyActions);
                     else
                        CopyMethodTemplateAsEntity(copyEntity, copyProperty, origValue as Entity, copyActions);
                     break;
               }
            }
            catch (Exception e) {
               throw;
            }
         });
         if (copyActions == null) return copyEntity;

         copyActions.ForEach(action => {
            var method = copyEntity.GetType().GetMethod(action.Key.Method.Name);
            if (method == null) return;
            method.Invoke(copyEntity, new[] { action.Value });
         });
         return copyEntity;
      }

      private static void CopyMethodCopy(Entity newEntity, PropertyInfo property, object value) {
         property.SetValue(newEntity, value, null);
      }

      private static void CopyMethodParent(Entity newEntity, PropertyInfo property, Entity parent) {
         property.SetValue(newEntity, parent, null);
      }

      private static void CopyMethodGenerate(Entity newEntity, PropertyInfo property, ModelCopyAttribute copyAttr) {
         property.SetValue(newEntity, copyAttr.Generate(), null);
      }

      private static void CopyMethodTemplateAsEntity(Entity newEntity, PropertyInfo property, Entity value,
                                                 List<KeyValuePair<Action<object>, object>> copyActions) {
         var template = value.TemplateCopy(newEntity, copyActions);
         property.SetValue(newEntity, template, null);
         if (copyActions == null) return;

         copyActions.ForEach(action => {
            var method = template.GetType().GetMethod(action.Key.Method.Name);
            if (method == null) return;
            method.Invoke(template, new[] { action.Value });
         });
      }

      private static void CopyMethodTemplateAsList(Entity newEntity, PropertyInfo property, IEnumerable value,
                                                List<KeyValuePair<Action<object>, object>> copyActions) {
         if (value == null) return;

         var enumerable = Activator.CreateInstance(property.PropertyFQN());
         var addMethod = enumerable.GetType().GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
         if (addMethod == null) return;

         property.SetValue(newEntity, enumerable, null);
         foreach (var child in value) {
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
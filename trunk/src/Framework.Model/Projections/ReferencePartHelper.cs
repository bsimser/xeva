using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace XF.Model
{
   public static class ReferencePartHelper
   {
      public static bool IsDefaultValue(object value, Type valueType)
      {
         switch (valueType.Name)
         {
            case "String":
               return string.IsNullOrEmpty(value.ToString());
            case "Guid":
               return new Guid(value.ToString()) == Guid.Empty;
            case "Int":
               return int.Parse(value.ToString()) == 0;
            default:
               return false;
         }
      }

      public static bool CollectionContainsKey(IList collection, object keyValue, PropertyInfo keyProperty) {
         return collection.Cast<object>().Any(item => keyProperty.GetValue(item, null).ToString().ToLower() == keyValue.ToString().ToLower());
      }

      public static object GetCollectionItem(IList collection, object keyValue, PropertyInfo keyProperty) {
         return collection.Cast<object>().First(item => keyProperty.GetValue(item, null).ToString().ToLower() == keyValue.ToString().ToLower());
      }

   }
}
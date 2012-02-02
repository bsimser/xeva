using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Iesi.Collections.Generic;

namespace XF {
   public static class XFExtensions {
      public static bool IsZero(this decimal num) {
         return num == 0;
      }

      public static bool IsEmpty<T>(this IEnumerable<T> collection) {
         return collection.Count() == 0;
      }

      public static bool IsNotEmpty<T>(this IEnumerable<T> collection) {
         return !IsEmpty(collection);
      }

      public static string GetDescription(this Enum value) {
         var type = value.GetType();
         var name = Enum.GetName(type, value);
         if (name != null) {
            var field = type.GetField(name);
            if (field != null) {
               var attr =
                      Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) as DescriptionAttribute;
               if (attr != null) {
                  return attr.Description;
               }
            }
         }
         return null;
      }

      /// <summary>
      /// Converts an inherited list from the interface to the suberclass
      /// </summary>
      /// <typeparam name="TSet">The type of the base</typeparam>
      /// <typeparam name="TList">The type of the superclass</typeparam>
      /// <param name="set"></param>
      /// <returns></returns>
      public static List<TList> Convert<TSet, TList>(this IEnumerable<TSet> set)
         where TList : class {
         var results = new List<TList>();
         foreach (var item in set) {
            if (item is TList)
               results.Add(item as TList);
         }

         return results;
      }

      public static bool IsBetween(this DateTime compare, DateTime start, DateTime end) {
         //Recreating the dates eliminates the time component.
         if (compare.Year == 1) return false;
         compare = DateTime.Parse(string.Format("{0}/{1}/{2}", compare.Month, compare.Day, compare.Year));
         start = DateTime.Parse(string.Format("{0}/{1}/{2}", start.Month, start.Day, start.Year));
         end = DateTime.Parse(string.Format("{0}/{1}/{2}", end.Month, end.Day, end.Year));
         return compare >= start && compare <= end;
      }

      public static bool IsGreater(this DateTime compare, DateTime start) {
         return compare > start;
      }

      public static bool IsGreaterEt(this DateTime compare, DateTime start) {
         return compare >= start;
      }

      public static bool IsLess(this DateTime compare, DateTime start) {
         return compare < start;
      }

      public static bool IsLessEt(this DateTime compare, DateTime start) {
         return compare <= start;
      }

      public static bool ContainsType(this PropertyInfo property, Type type) {
         var interfaces = new List<Type>(property.PropertyType.GetInterfaces());
         return interfaces.Exists(m => m.Name == type.Name);
      }

      public static bool IsCollection(this PropertyInfo property) {
         return property.PropertyType.Name.Contains("List`1") ||
                property.PropertyType.Name.Contains("IList`1") ||
                property.PropertyType.Name.Contains("ISet`1");
      }

      public static Type PropertyFQN(this PropertyInfo property) {
         if (!property.PropertyType.IsInterface) return property.PropertyType;

         var fqn = property.PropertyType.AssemblyQualifiedName;
         var newFqn = string.Empty;
         switch (property.PropertyType.Name) {
            case "IList`1":
               newFqn = fqn.Replace("IList`1", "List`1");
               break;
            case "ISet`1":
               newFqn = fqn.Replace("ISet`1", "HashedSet`1");
               break;
         }
         return Type.GetType(newFqn);
      }

   }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iesi.Collections.Generic;

namespace XF {
   public static class XFExtensions {
      public static bool IsEmpty<T>(this IEnumerable<T> collection) {
         return collection.Count() == 0;
      }

      public static bool IsNotEmpty<T>(this IEnumerable<T> collection) {
         return !IsEmpty(collection);
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
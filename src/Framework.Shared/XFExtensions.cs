using System.Collections.Generic;
using System.Linq;
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
   }
}
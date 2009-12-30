using System.Collections.Generic;
using System.Linq;

namespace XF
{
   public static class EnumberableExtensions
   {
      public static bool IsEmpty<T>(this IEnumerable<T> collection)
      {
         return collection.Count() == 0;
      }

      public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
      {
         return !IsEmpty(collection);
      }
   }
}
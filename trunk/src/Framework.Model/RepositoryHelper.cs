using System;

namespace XF.Model {
   public static class RepositoryHelper {
      public static string ToShortDate(object date) {
         return ((DateTime)date).ToShortDateString();
      }
   }
}
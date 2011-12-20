using System;

namespace XF.Model {
   public static class RepositoryHelper {
      public static string ToShortDate(object date) {
         return date != null && date is DateTime ? ((DateTime)date).ToShortDateString() : string.Empty;
      }
   }
}
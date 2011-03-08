using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace XF {
   public sealed class LookupHelper {

      public static List<SimpleLookupMessage> MonthsList() {
         var result = new List<SimpleLookupMessage>();
         for (int i = 1; i < 13; i++) {
            var month = DateAndTime.MonthName(i, false);
            result.Add(new SimpleLookupMessage { ID = i, Name = month });
         }

         return result;
      }

      public static List<SimpleLookupMessage> DaysList(int year, int month) {
         var result = new List<SimpleLookupMessage>();
         var days = DateTime.DaysInMonth(year, month);
         for (int i = 1; i < days+1; i++) {
            result.Add(new SimpleLookupMessage { ID = i, Name = i.ToString() });
         }
         return result;
      }

   }
}
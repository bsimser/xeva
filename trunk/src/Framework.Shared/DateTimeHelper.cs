using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace XF {
   public sealed class DateTimeHelper {
      private static List<string> _months;

      public static List<SimpleLookupMessage> MonthsList() {
         var result = new List<SimpleLookupMessage>();
         if (_months == null) {
            _months = new List<string>();
            for (int i = 1; i < 13; i++) {
               _months.Add(DateAndTime.MonthName(i, false));
            }
         }

         _months.ForEach(month => result.Add(new SimpleLookupMessage {ID = _months.IndexOf(month)+1, Name = month}));
         return result;
      }

      public static List<SimpleLookupMessage> DaysList(int year, int month) {
         var result = new List<SimpleLookupMessage>();
         var days = DateTime.DaysInMonth(year, month);
         for (int i = 1; i < days + 1; i++) {
            result.Add(new SimpleLookupMessage { ID = i, Name = i.ToString() });
         }
         return result;
      }

      public static string MonthName(int month) {
         return DateAndTime.MonthName(month, false);
      }

      public static int MonthNumber(string month) {
         if (_months == null) {
            _months = new List<string>();
            for (int i = 1; i < 13; i++) {
               _months.Add(DateAndTime.MonthName(i, false));
            }
         }

         return _months.IndexOf(month)+1;
      }

   }
}
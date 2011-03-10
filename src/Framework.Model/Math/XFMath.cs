using System;

namespace XF.Model {
   public sealed class XFMath {
      public static void Assign(RoundMethods method) {
         Globals.Data["RoundMethod"] = method;
      }

      public static decimal Round(RoundMethods method, decimal value) {
         switch (method) {
            case RoundMethods.HalfUp:
               return Math.Round(value, 2, MidpointRounding.ToEven);
            case RoundMethods.FromZero:
               return Math.Round(value, 2, MidpointRounding.AwayFromZero);
            default:
               return Math.Round(value, 2, MidpointRounding.AwayFromZero);
         }
      }
   }
}
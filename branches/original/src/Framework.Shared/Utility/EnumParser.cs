using System;

namespace XEVA.Framework.Utility
{
   public class EnumParser
   {
      public static T Parse<T>(string text)
      {
         if (!typeof (T).IsEnum)
            throw new ArgumentException(typeof (T).ToString() + " is not an Enum");

         if (!Enum.IsDefined(typeof(T), text))
            throw new ArgumentException(String.Format("The value passed (\"{0}\") is not a defined within {1}.", text, typeof(T).ToString()));

         T t = (T) Enum.Parse(typeof (T), text, true);
         return t;
      }
   }
}
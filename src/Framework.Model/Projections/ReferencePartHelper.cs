using System;

namespace XF.Model
{
   public static class ReferencePartHelper
   {
      public static bool IsDefaultValue(object value, Type valueType)
      {
         switch (valueType.Name)
         {
            case "String":
               return string.IsNullOrEmpty(value.ToString());
            case "Guid":
               return new Guid(value.ToString()) == Guid.Empty;
            case "Int":
               return int.Parse(value.ToString()) == 0;
            default:
               return false;
         }
      }
   }
}
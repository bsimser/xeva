using System;

namespace XF {
   public class PropertyHelper {
      public static object GetValue(string type, string value) {
         if (string.IsNullOrEmpty(value)) return value;

         switch (type) {
            case "System.String":
               return value;
            case "System.Decimal":
               return decimal.Parse(value);
            case "System.Int32":
               return int.Parse(value);
            case "Int32":
               return int.Parse(value);
            case "System.Int64":
               return int.Parse(value);
            case "System.Float":
               return float.Parse(value);
            case "System.Guid":
               return new Guid(value);
            case "System.DateTime":
               var dtResult = DateTime.Now;
               return DateTime.TryParse(value, out dtResult) ? dtResult : DateTime.MinValue;
            case "System.Nullable`1[System.DateTime]":
               var dtNullResult = DateTime.Now;
               return DateTime.TryParse(value, out dtNullResult) ? (DateTime?)dtNullResult : null;
            case "System.Nullable`1":
               var nullB = DateTime.Parse(value);
               return (DateTime?)nullB;
            case "System.Nullable`1[System.Int32]":
               var nullInt = int.Parse(value);
               return (int?)nullInt;
            case "System.Boolean":
               if (value.Length > 1)
                  return value.ToLower() == "true" ? true : false;
               if (value.Length == 1)
                  return value.ToLower() == "1" ? true : false;
               return false;
            default:
               return value;
         }
      }

      public static object GetValueByType(Type type, string value) {
         if (string.IsNullOrEmpty(value)) return value;

         if (type.BaseType == typeof(Enum))
            return Enum.Parse(type, value);

         switch (type.ToString()) {
            case "System.String":
               return value;
            case "System.Decimal":
               return decimal.Parse(value);
            case "System.Int32":
               return int.Parse(value);
            case "System.Int64":
               return int.Parse(value);
            case "System.Float":
               return float.Parse(value);
            case "System.Guid":
               return new Guid(value);
            case "System.DateTime":
               var dtResult = DateTime.Now;
               return DateTime.TryParse(value, out dtResult) ? dtResult : DateTime.MinValue;
            case "System.Nullable`1[System.DateTime]":
               var dtNullResult = DateTime.Now;
               return DateTime.TryParse(value, out dtNullResult) ? (DateTime?)dtNullResult : null;
            case "System.Nullable`1":
               var nullB = DateTime.Parse(value);
               return (DateTime?)nullB;
            case "System.Nullable`1[System.Int32]":
               var nullInt = int.Parse(value);
               return (int?)nullInt;
            case "System.Boolean":
               if (value.Length > 1)
                  return value.ToLower() == "true" ? true : false;
               if (value.Length == 1)
                  return value.ToLower() == "1" ? true : false;
               return false;
            default:
               return value;
         }
      }

      public static object GetDefaultValue(string type) {
         switch (type) {
            case "System.String":
               return string.Empty;
            case "System.Decimal":
               return 0;
            case "System.Int32":
               return 0;
            case "System.Int64":
               return 0;
            case "System.Float":
               return 0;
            case "System.Guid":
               return Guid.Empty;
            case "System.DateTime":
               return DateTime.Parse("1/1/1900");
            case "System.Nullable`1[System.DateTime]":
               return null;
            case "System.Nullable`1[System.Int32]":
               return null;
            case "System.Boolean":
               return false;
            default:
               return string.Empty;
         }
      }

   }
}
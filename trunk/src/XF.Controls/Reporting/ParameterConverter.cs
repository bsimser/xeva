using System.ComponentModel;

namespace XF.Controls {
   public class ParameterConverter : StringConverter {
      public override bool GetStandardValuesSupported(ITypeDescriptorContext context) {
         return true;
      }

      public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) {
         return new TypeConverter.StandardValuesCollection(new string[] { "Name", "ID" });
      }
   }
}
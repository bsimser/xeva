using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XF.Model {
   public class ProjectionPart : List<ProjectionPart> {
      public string EntityName { get; set; }
      public string EntityProperty { get; set; }
      public List<string> Concatenations { get; set; }
      public PropertyInfo MessageProperty { get; set; }
      public object Value { get; set; }
      public int ParameterIdx { get; set; }
      public bool IsKey { get; set; }
      public object DefaultValue { get; set; }
      public MaskedType MaskType { get; set; }

      public string GetSelectPart() {
         if (DefaultValue != null) return string.Empty;

         var result = new StringBuilder();
         result.Append(AddEntityPropertyToSelect());
         if (Concatenations != null && Concatenations.Count > 0) result.Append(AddConcatenationsToSelect());

         result.Append(",");
         return result.ToString();
      }

      private string AddEntityPropertyToSelect() {
         return !string.IsNullOrEmpty(EntityProperty)
            ? string.Format(" {0}.{1} ", EntityName.ToLower(), EntityProperty)
            : string.Empty;
      }

      private string AddConcatenationsToSelect() {
         var cats = new StringBuilder();
         Concatenations.ForEach(cat => cats.Append(string.Format(" + {0}", cat)));
         return cats.ToString();
      }

      public void SetOutputValue(object output, object[] tuple) {
         var value = default(object);
         switch (MaskType) {
            case MaskedType.EIN:
               var einImpl = MaskFactory.GetMaskImpl(MaskType);
               value = einImpl.GetFormattedValue(tuple[ParameterIdx]);
               break;
            case MaskedType.SSN:
               var ssnImpl = MaskFactory.GetMaskImpl(MaskType);
               value = ssnImpl.GetFormattedValue(tuple[ParameterIdx]);
               break;
            case MaskedType.Phone:
               var phoneImpl = MaskFactory.GetMaskImpl(MaskType);
               value = phoneImpl.GetFormattedValue(tuple[ParameterIdx]);
               break;
            default:
               value = tuple[ParameterIdx];
               break;
         }
         
         if (DefaultValue == null)
            MessageProperty.SetValue(output, value, null);
         else
            MessageProperty.SetValue(output, DefaultValue, null);
      }
   }
}
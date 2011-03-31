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
      public Func<object, object> ValueConversion { get; set; }
      public object OutputValue { get; set; }
      public string Name { get; set; }
      public List<string> NamedArguments { get; set; }
      public Func<object[], object> ComputationTool { get; set; }
      public IArgumentSource ArgumentSource { get; set; }

      public string GetSelectPart() {
         if (DefaultValue != null ||
             ComputationTool != null) return string.Empty;

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
         OutputValue = DefaultValue ?? GetTupleValue(tuple);

         if (ValueConversion != null)
            OutputValue = ValueConversion(OutputValue);
         if (ComputationTool != null)
            OutputValue = ComputationTool(GatherNamedArguments());

         MessageProperty.SetValue(output, OutputValue, null);
      }

      private object[] GatherNamedArguments() {
         if (ArgumentSource.NamedArguments == null ||
            ArgumentSource.NamedArguments.IsEmpty()) return null;

         var results = new List<object>();
         NamedArguments.ForEach(arg => {
            if (ArgumentSource.NamedArguments.ContainsKey(arg))
               results.Add(ArgumentSource.NamedArguments[arg].OutputValue);
         });
         return results.ToArray();
      }

      protected object GetTupleValue(object[] tuple) {
         if (tuple.Length <= ParameterIdx) return null;
         if (MaskType == MaskedType.None) return tuple[ParameterIdx];

         switch (MaskType) {
            case MaskedType.EIN:
               var einImpl = MaskFactory.GetMaskImpl(MaskType);
               return einImpl.GetFormattedValue(tuple[ParameterIdx]);
            case MaskedType.SSN:
               var ssnImpl = MaskFactory.GetMaskImpl(MaskType);
               return ssnImpl.GetFormattedValue(tuple[ParameterIdx]);
            case MaskedType.Phone:
               var phoneImpl = MaskFactory.GetMaskImpl(MaskType);
               return phoneImpl.GetFormattedValue(tuple[ParameterIdx]);
            default:
               return tuple[ParameterIdx];
         }
      }
   }
}
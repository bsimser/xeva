using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NHibernate;

namespace XF.Model
{
   public class ReferenceTypePart : IReferencePart
   {
      public string RootType { get; set; }
      public string ReferencePath { get; set; }
      public string RefEntityType { get; set; }
      public Type MessageType { get; set; }
      public ReferenceJoinType JoinType { get; set; }
      public PropertyInfo SubProjection { get; set; }
      public ProjectionPart Parameters { get; set; }
      public List<IReferencePart> References { get; set; }
      public bool IsKeyed { get; set; }
      public PropertyInfo KeyProperty { get; set; }

      public string GetSelectParts()
      {
         var results = new StringBuilder();
         Parameters.ForEach(param => results.Append(param.GetSelectPart()));
         References.ForEach(reference => results.Append(reference.GetSelectParts()));
         return results.ToString();
      }

      public string GetFromPart()
      {
         var result = new StringBuilder(Environment.NewLine);
         result.Append(string.Format("{0} join ", JoinType != ReferenceJoinType.none
                                                     ? JoinType.ToString()
                                                     : string.Empty));
         result.Append(string.Format("{0}.{1} as {2}", RootType, ReferencePath, RefEntityType.ToLower()));
         References.ForEach(part => result.Append(part.GetFromPart()));
         
         return result.ToString();
      }

      public string GetWhereParts()
      {
         var result = new StringBuilder();
         References.ForEach(reference => result.Append(reference.GetWhereParts()));

         return result.ToString();
      }

      public void SetPartParameters(IQuery query)
      {
         References.ForEach(refpart => refpart.SetPartParameters(query));
      }

      public void GenerateOutputReference(object output, object[] tuple)
      {
         var typePart = Activator.CreateInstance(MessageType);
         Parameters.ForEach(param => param.SetOutputValue(typePart, tuple));
         References.ForEach(reference => reference.GenerateOutputReference(typePart, tuple));

         if (IsKeyed &&
            ReferencePartHelper.IsDefaultValue(KeyProperty.GetValue(typePart, null), KeyProperty.PropertyType)) return;

         SubProjection.SetValue(output, typePart, null);
      }
   }
}
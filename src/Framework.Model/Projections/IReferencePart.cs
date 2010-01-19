using System;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;

namespace XF.Model
{
   public interface IReferencePart
   {
      string RootType { get; set; }
      string ReferencePath { get; set; }
      string RefEntityType { get; set; }
      Type MessageType { get; set; }
      ReferenceJoinType JoinType { get; set; }
      PropertyInfo SubProjection { get; set; }
      ProjectionPart Parameters { get; set; }
      List<IReferencePart> References { get; set; }
      bool IsKeyed { get; set; }
      PropertyInfo KeyProperty { get; set; }
      string GetSelectParts();
      string GetFromPart();
      string GetWhereParts();
      void SetPartParameters(IQuery query);
      void GenerateOutputReference(object output, object[] tuple);
   }
}
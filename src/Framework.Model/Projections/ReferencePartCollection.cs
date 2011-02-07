using System;
using System.Collections;

namespace XF.Model
{
   public class ReferencePartCollection : ReferencePartBase
   {
      public override void GenerateOutputReference(object output, object[] tuple)
      {
         var collection = SubProjection.GetValue(output, null) as IList;
         if (collection == null) return;

         var listPart = Activator.CreateInstance(MessageType);
         Parameters.ForEach(param => param.SetOutputValue(listPart, tuple));
         References.ForEach(reference => reference.GenerateOutputReference(listPart, tuple));

         if(IsKeyed && 
            ReferencePartHelper.IsDefaultValue(KeyProperty.GetValue(listPart, null), KeyProperty.PropertyType)) return;

         collection.Add(listPart);
      }
   }
}
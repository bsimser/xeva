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

         var keyValue = KeyProperty.GetValue(listPart, null);
         if(ReferencePartHelper.CollectionContainsKey(collection, keyValue, KeyProperty)) return;
         if(IsKeyed && ReferencePartHelper.IsDefaultValue(keyValue, KeyProperty.PropertyType)) return;

         collection.Add(listPart);
      }
   }
}
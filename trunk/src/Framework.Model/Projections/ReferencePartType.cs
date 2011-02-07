using System;

namespace XF.Model
{
   public class ReferencePartType : ReferencePartBase
   {
      public override void GenerateOutputReference(object output, object[] tuple)
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
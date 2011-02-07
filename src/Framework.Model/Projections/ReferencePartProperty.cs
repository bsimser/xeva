namespace XF.Model
{
   public class ReferencePartProperty : ReferencePartBase
   {
      public override void GenerateOutputReference(object output, object[] tuple)
      {
         Parameters.ForEach(param => param.SetOutputValue(output, tuple));
         References.ForEach(reference => reference.GenerateOutputReference(output, tuple));
      }
   }
}
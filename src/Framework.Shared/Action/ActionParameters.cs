namespace XF
{
   public class ActionParameters : IActionParameters
   {
      public string PropertyName { get; set; }
      public object OriginalValue { get; set; }
      public object NewValue { get; set; }
   }
}
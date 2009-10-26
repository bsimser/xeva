namespace XF
{
   public interface IActionParameters
   {
      string PropertyName { get; set; }
      object OriginalValue { get; set; }
      object NewValue { get; set; }
   }
}
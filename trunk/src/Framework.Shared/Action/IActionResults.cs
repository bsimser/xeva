namespace XF
{
   public interface IActionResults
   {
      string ErrorCode { get; set; }
      string Message { get; set; }
      string ErrorContent { get; set; }
   }
}
namespace XF
{
   public interface IActionResults
   {
      ActionResultCode ResultCode { get; set; }
      string Message { get; set; }
      string ErrorContent { get; set; }
      object Data { get; set; }
   }
}
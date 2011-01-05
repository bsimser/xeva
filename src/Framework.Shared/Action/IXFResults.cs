namespace XF
{
   public interface IXFResults
   {
      XFResultCode ResultCode { get; set; }
      string Message { get; set; }
      string ErrorContent { get; set; }
      object Data { get; set; }
      bool Success { get; }
      TData GetData<TData>();
   }
}
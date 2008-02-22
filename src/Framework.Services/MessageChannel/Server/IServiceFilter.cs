namespace XF.Services
{
   public interface IServiceFilter
   {
      ServiceRequest ServiceRequest { get; set; }
      ServiceResponse ServiceResponse { get; set; }
      void Process();
   }
}
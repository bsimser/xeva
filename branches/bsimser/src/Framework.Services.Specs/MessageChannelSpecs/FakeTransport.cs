namespace XF.Services
{
   public class FakeTransport
   {
      public virtual byte[] SendChannelRequest(byte[] requestMessage)
      {
         return new byte[0];
      }
   }
}
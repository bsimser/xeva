namespace XF.Services
{
   public interface ITransport
   {
      byte[] SendChannelRequest(byte[] requestMessage);
   }
}
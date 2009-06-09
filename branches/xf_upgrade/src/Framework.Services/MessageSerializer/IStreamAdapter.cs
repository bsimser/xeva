using System.IO;

namespace XF.Services
{
   public interface IStreamAdapter
   {
      bool IsOpen { get; }
      Stream Stream { get; }

      string ReadString();
      byte[] ReadBinary();

      void Close();

      void WriteString(string xmlArgument);
      void WriteBinary(byte[] binaryArgument);

      void Initialize();
   }
}
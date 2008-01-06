using System.IO;

namespace XEVA.Framework.Services
{
   public interface IStreamAdapter
   {
      bool IsOpen { get; }
      Stream Stream { get; }

      string Read();

      void Close();

      void Write(string xmlDocument);

      void Initialize();
   }
}
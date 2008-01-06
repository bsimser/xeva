using System;
using System.IO;
using System.Text;

namespace XEVA.Framework.Services
{
   public class StreamAdapter : IStreamAdapter
   {
      private Stream _stream;

      public Stream Stream
      {
         get
         {
            if (_stream == null) Initialize();

            return _stream;
         }
      }

      public bool IsOpen
      {
         get { return _stream.CanRead; }
      }

      public string Read()
      {
         if (!IsOpen) Initialize();

         _stream.Position = 0;
         byte[] streamContents = new byte[_stream.Length];
         _stream.Read(streamContents, 0, (int)_stream.Length);
         _stream.Flush();

         return Encoding.Default.GetString(streamContents);
      }

      public void Close()
      {
         _stream.Close();
      }

      public void Write(string xmlDocument)
      {
         if (!IsOpen) Initialize();

         ASCIIEncoding encoding = new ASCIIEncoding();
         byte[] documentContents = encoding.GetBytes(xmlDocument);
         _stream.Write(documentContents, 0, documentContents.Length);
         _stream.Position = 0;
      }

      public void Initialize()
      {
         _stream = new MemoryStream();
      }
   }
}
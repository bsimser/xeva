using System;
using System.IO;
using System.Text;

namespace XF.Services
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

      public string ReadString()
      {
         if (!IsOpen) Initialize();

         _stream.Position = 0;
         byte[] streamContents = new byte[_stream.Length];
         _stream.Read(streamContents, 0, (int)_stream.Length);
         _stream.Flush();

         return Encoding.Default.GetString(streamContents);
      }

      public byte[] ReadBinary()
      {
         if (!IsOpen) Initialize();

         _stream.Position = 0;
         byte[] results = new byte[_stream.Length];
         _stream.Read(results, 0, (int)_stream.Length);
         _stream.Flush();

         return results;
      }

      public void Close()
      {
         _stream.Close();
      }

      public void WriteString(string xmlDocument)
      {
         if (!IsOpen) Initialize();

         ASCIIEncoding encoding = new ASCIIEncoding();
         byte[] documentContents = encoding.GetBytes(xmlDocument);
         _stream.Write(documentContents, 0, documentContents.Length);
         _stream.Position = 0;
      }

      public void WriteBinary(byte[] binaryArgument)
      {
         if (!IsOpen) Initialize();

         _stream.Write(binaryArgument, 0, binaryArgument.Length);
         _stream.Position = 0;
      }

      public void Initialize()
      {
         _stream = new MemoryStream();
      }
   }
}
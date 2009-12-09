using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace XF
{
   public static class GZipHelper
   {
      public static byte[] Compress(byte[] bytes) {
         using (var ms = new MemoryStream()) {
            using (var gs = new GZipStream(ms, CompressionMode.Compress, true)) {
               gs.Write(bytes, 0, bytes.Length);
            }
            ms.Position = 0L;
            return ToByteArray(ms);
         }
      }

      public static byte[] Decompress(byte[] bytes) {
         byte[] result;
         using (var ms = new MemoryStream()) {
            ms.Write(bytes, 0, bytes.Length);
            using (var gs = new GZipStream(ms, CompressionMode.Decompress, true)) {
               ms.Position = 0L;
               result = ToByteArray(gs);
            }
         }
         return result;
      }

      public static byte[] IonicCompress(byte[] bytes) {
         using (var ms = new MemoryStream()) {
            using (var gs = new Ionic.Zlib.GZipStream(ms, Ionic.Zlib.CompressionMode.Compress, true)) {
               gs.Write(bytes, 0, bytes.Length);
            }
            ms.Position = 0L;
            return ToByteArray(ms);
         }
      }

      public static byte[] IonicDecompress(byte[] bytes) {
         byte[] result;
         using (var ms = new MemoryStream()) {
            ms.Write(bytes, 0, bytes.Length);
            using (var gs = new Ionic.Zlib.GZipStream(ms, Ionic.Zlib.CompressionMode.Decompress, true)) {
               ms.Position = 0L;
               result = ToByteArray(gs);
            }
         }
         return result;
      }

      // streaming to byte array
      public static byte[] ToByteArray(Stream stream) {
         int count = 0;
         var result = new List<byte>();
         try {
            var buffer = new byte[0x20000];
            int bytes = 0;
            while ((bytes = stream.Read(buffer, 0, 0x20000)) > 0) {
               count += bytes;
               for (int i = 0; i < bytes; i++)
                  result.Add(buffer[i]);
            }
         } catch (Exception ex) {
            return null;
         }
         return result.ToArray();
      }
   }
}
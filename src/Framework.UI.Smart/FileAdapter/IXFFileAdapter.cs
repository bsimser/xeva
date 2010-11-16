using System;
using System.IO;

namespace XF.UI.Smart {
   public interface IXFFileAdapter : IDisposable {
      void InitializeAdapter(string path);

      void InitializeAdapter(string path, int bytesLength);

      void Close();

      void Flush();

      void Write(byte[] fileContent, int offset, int length);

      byte[] Read();

      string CreateFilePath();

      string CreateTempFileName(string extension);

      bool Exists(string path);

      void Delete(string path);

      bool DirectoryExists(string path);

      void CreateDirectory(string path);

      void Move(string source, string destination);

      FileInfo[] GetFilesFromDirectory(string directoryPath, string fileFilter);

      void DeleteAllFiles(string path);
      string CreateTempFilePath(string extension);
   }
}
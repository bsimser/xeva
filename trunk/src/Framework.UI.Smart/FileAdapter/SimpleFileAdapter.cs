using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace XF.UI.Smart {
   public class SimpleFileAdapter : IXFFileAdapter {
      private FileStream _stream;

      public virtual void InitializeAdapter(string path) {
         _stream = new FileStream(path, FileMode.OpenOrCreate);
      }

      public void InitializeAdapter(string path, int bytesLength) {
         _stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, bytesLength);
      }

      public void Close() {
         _stream.Close();
      }

      public void Flush() {
         _stream.Flush();
      }

      public void Write(byte[] fileContent, int offset, int length) {
         _stream.Position = offset;
         _stream.Write(fileContent, 0, length);
      }

      public byte[] Read() {
         var fileContents = new byte[_stream.Length];
         _stream.Read(fileContents, 0, fileContents.Length);
         return fileContents;
      }

      public bool Exists(string path) {
         return File.Exists(path);
      }

      public void Delete(string path) {
         File.Delete(path);
      }

      public FileInfo[] GetFilesFromDirectory(string directoryPath, string fileFilter) {
         var directory = new DirectoryInfo(directoryPath);

         return directory.GetFiles(fileFilter);
      }

      public void DeleteAllFiles(string path) {
         var files = Directory.GetFiles(path);
         foreach (var filename in files) {
            try {
               File.Delete(filename);
            }
            catch (Exception) { }
         }

         var directories = Directory.GetDirectories(path);
         foreach (var directoryName in directories)
            Directory.Delete(directoryName, true);
      }

      public bool DirectoryExists(string path) {
         return Directory.Exists(path);
      }

      public void CreateDirectory(string path) {
         Directory.CreateDirectory(path);
      }

      public void Move(string source, string destination) {
         File.Move(source, destination);
      }

      public string CreateTempFileName(string extension) {
         return "temp" + DateTime.Now.Minute + DateTime.Now.Millisecond + extension;
      }

      public string CreateFilePath() {
         var result = new StringBuilder(ConfigurationManager.AppSettings["WorkingFolder"]);
         result.Append(@"\");

         return result.ToString();
      }

      public string CreateTempFilePath(string extension) {
         var result = new StringBuilder(ConfigurationManager.AppSettings["WorkingFolder"]);
         result.Append(@"\");
         result.Append(string.Format("temp{0}{1}.{2}", DateTime.Now.Minute, DateTime.Now.Millisecond, extension));
         return result.ToString();
      }

      public virtual void Dispose() {
         if (_stream == null) return;

         try {
            _stream.Flush();
         }
         catch { }

         _stream.Close();
         _stream.Dispose();
      }

   }
}
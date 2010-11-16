namespace XF.UI.Smart {
   public sealed class XFFileAdapterFactory {

      public static IXFFileAdapter CreateAdapter() {
         return new SimpleFileAdapter();
      }

   }
}
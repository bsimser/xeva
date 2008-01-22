namespace XF.UI.Smart
{
   public class NullRequest : IRequest
   {
      public bool IsNull
      {
         get { return true; }
      }

      public T GetRequiredItem<T>(string key, T emptyValue)
      {
         return emptyValue;
      }

      public T GetRequiredItem<T>(T emptyValue)
      {
         return emptyValue;
      }

      public T GetOptionalItem<T>(string key, T defaultValue)
      {
         return defaultValue;
      }

      public T GetOptionalItem<T>(T defaultValue)
      {
         return defaultValue;
      }

      public void SetItem(string key, object value)
      {
      }

      public void SetItem(object value)
      {
      }
   }
}
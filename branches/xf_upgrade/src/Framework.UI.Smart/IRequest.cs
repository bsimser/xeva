namespace XF.UI.Smart
{
   public interface IRequest
   {
      bool IsNull { get; }

      T GetOptionalItem<T>(string key, T defaultValue);

      T GetOptionalItem<T>(T defaultValue);
      
      T GetRequiredItem<T>(string key, T emptyValue);

      T GetRequiredItem<T>(T emptyValue);

      void SetItem(string key, object value);

      void SetItem(object value);
   }
}
namespace XF.UI.Smart
{
   public interface IRequest
   {
      bool IsNull { get; }

      T GetItem<T>(string key, T empty);

      T GetItem<T>(string key, T empty, bool required);

      void SetItem<T>(string key, T value);
   }
}
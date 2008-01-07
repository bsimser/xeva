namespace XEVA.Framework.UI.Smart
{
   public interface IRequest
   {
      bool IsNull { get; }

      T GetItem<T>(string key, T empty);

      void SetItem<T>(string key, T value);
   }
}
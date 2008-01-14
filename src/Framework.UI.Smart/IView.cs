
namespace XEVA.Framework.UI.Smart
{
   public interface IView<TCallbacks> 
      where TCallbacks : IViewCallbacks
   {
      object UI { get; }

      void Attach(TCallbacks callback);
      void Show();
      void Hide();
   }
}

namespace XEVA.Framework.UI.Smart
{
   public interface IView<TCallbacks> 
      where TCallbacks : IPresenterCallbacks
   {
      object UI { get; }
      void Attach(TCallbacks callback);
   }
}
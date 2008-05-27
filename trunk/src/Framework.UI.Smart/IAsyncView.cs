namespace XF.UI.Smart
{
   public interface IAsyncView<TCallbacks> : IView<TCallbacks> 
      where TCallbacks : IViewCallbacks
   {
      void ShowWaiting();
      void HideWaiting();
   }
}
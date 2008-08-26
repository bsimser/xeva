namespace XF.UI.Smart
{
   public interface IRefreshes : IPresenter
   {
      void Register(IRefreshable refreshable);
      void RefreshAll();
   }
}
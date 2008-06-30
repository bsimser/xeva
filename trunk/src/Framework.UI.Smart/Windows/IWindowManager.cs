namespace XF.UI.Smart
{
   public interface IWindowManager
   {
      IWindowAdapter CreateWindowFor(IPresenter presenter);
   }
}
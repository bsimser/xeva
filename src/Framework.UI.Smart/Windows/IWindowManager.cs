namespace XF.UI.Smart
{
   public interface IWindowManager
   {
      IWindowAdapter Create();
      IWindowAdapter Create(WindowOptions options);
   }
}
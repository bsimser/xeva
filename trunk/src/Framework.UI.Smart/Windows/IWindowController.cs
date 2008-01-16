namespace XF.UI.Smart
{
   public interface IWindowController
   {
      void Show();
      void ShowModal();
      void ShowModal(int width, int height);
      void Hide();
      void ChangeCaption(string caption);
   }
}
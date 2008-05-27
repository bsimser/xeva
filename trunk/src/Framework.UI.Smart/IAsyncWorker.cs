using System.ComponentModel;

namespace XF.UI.Smart
{
   public interface IAsyncWorker
   {
      event DoWorkEventHandler DoWork;
      event RunWorkerCompletedEventHandler RunWorkerCompleted;
      void RunWorkerAsync(object argument);
      void RunWorkerAsync();
   }
}
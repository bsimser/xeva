using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace XF.UI.Smart
{
   public class BackgroundWorkerDispatcher : IWorkItemDispatcher
   {
      private readonly Queue<WorkItem> _queue;
      private readonly object _queueLock = new object();
      private readonly BackgroundWorker _worker;

      public BackgroundWorkerDispatcher()
      {
         _queue = new Queue<WorkItem>();

         _worker = new BackgroundWorker();
         _worker.DoWork += ProcessWorkItem;
         _worker.RunWorkerCompleted += CompletedWorkItem;
      }

      public void Enqueue(WorkItem item)
      {
         lock (_queueLock)
         {
            Debug.WriteLine("queued");
            _queue.Enqueue(item);
            Debug.WriteLine(_queue.Count.ToString());
         }
         StartOrContinueProcessing();
      }

      private void StartOrContinueProcessing()
      {
         lock (_queueLock)
         {
            if (_queue.Count == 0) return;
            if (!_worker.IsBusy)
               _worker.RunWorkerAsync();
         }
      }

      private void ProcessWorkItem(object sender, DoWorkEventArgs e)
      {
         WorkItem item = null;
         lock (_queueLock)
            item = _queue.Dequeue();
         if (item != null) item.ProcessOperations();
         e.Result = item;
      }

      private void CompletedWorkItem(object sender, RunWorkerCompletedEventArgs e)
      {
         var item = e.Result as WorkItem;
         if (item != null) item.Complete();
         StartOrContinueProcessing();
      }
   }
}
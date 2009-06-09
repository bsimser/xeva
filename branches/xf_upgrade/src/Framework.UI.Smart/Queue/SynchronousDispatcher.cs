namespace XF.UI.Smart
{
   public class SynchronousDispatcher : IWorkItemDispatcher
   {
      public void Enqueue(WorkItem item)
      {
         item.ProcessOperations();
         item.Complete();
      }
   }
}
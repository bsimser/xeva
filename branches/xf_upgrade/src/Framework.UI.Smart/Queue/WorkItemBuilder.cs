using System;
using System.Collections.Generic;

namespace XF.UI.Smart
{
   public class WorkItemBuilder : IWorkItemBuilder, IWorkItemBuilder_AddOperations
   {
      private readonly IWorkItemDispatcher _dispatcher;
      private readonly List<Action> _operations = new List<Action>();
      private Action<Exception> _fault;
      private Action _completed;

      public WorkItemBuilder(IWorkItemDispatcher dispatcher)
      {
         _dispatcher = dispatcher;
      }

      public IWorkItemBuilder_AddOperations Add(Action operation)
      {
         _operations.Add(operation);
         return this;
      }

      public IWorkItemBuilder_Send Fault(Action<Exception> handler)
      {
         return this;
      }

      public void Send(Action completed)
      {
         _completed = completed;
         _dispatcher.Enqueue(this);
      }
      
      public static implicit operator WorkItem(WorkItemBuilder builder)
      {
         return new WorkItem(builder._operations, builder._fault, builder._completed);
      }
   }

   public interface IWorkItemBuilder : IWorkItemBuilder_Start {}

   public interface IWorkItemBuilder_Start
   {
      IWorkItemBuilder_AddOperations Add(Action operation);
   }

   public interface IWorkItemBuilder_Send
   {
      void Send(Action completed);
   }

   public interface IWorkItemBuilder_AddOperations : IWorkItemBuilder_Start, IWorkItemBuilder_Send
   {
      IWorkItemBuilder_Send Fault(Action<Exception> handler);
   }
}
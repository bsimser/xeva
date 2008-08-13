using System;
using System.Collections.Generic;
using System.Threading;

namespace XF.UI.Smart
{
   public class WorkItem
   {
      private readonly List<Action> _operations;
      private readonly Action<Exception> _fault;
      private readonly Action _completed;
      private Exception _storedException;
      private bool _processed = false;

      public WorkItem(List<Action> operations, Action<Exception> fault, Action completed)
      {
         if (operations == null) 
            throw new ArgumentNullException("operations", "operations parameter cannot be null");

         if (operations.Count == 0)
            throw new ArgumentOutOfRangeException("operations", "operations parameter requires at least one operation.");

         if (completed == null)
            throw new ArgumentNullException("completed", "completed parameter cannot be null");
         
         _operations = operations;
         _fault = fault;
         _completed = completed;
      }

      public void ProcessOperations()
      {
         if (_processed) return;

         try
         {
            foreach (var operation in _operations) operation();
         }
         catch (Exception ex)
         {
            _storedException = ex;
         }

         _processed = true;
      }

      public void Complete()
      {
         if (!_processed) return;
         if (_storedException != null)
         {
            if (_fault == null) throw _storedException;
            _fault(_storedException);
         }
         else
            _completed();
      }
   }
}
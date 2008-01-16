using System;
using System.Collections.Generic;

namespace XF.Model.UnitOfWorkImpl
{
   [Serializable]
   public class TransactionElection : ITransaction
   {
      private bool _done = false;
      private bool _commit = false;
      private TransactionElectionResult _result = TransactionElectionResult.InProgress;
      private readonly Stack<Guid> _voteStack = new Stack<Guid>();
      private readonly ITransaction _transaction;

      public TransactionElection(ITransaction transaction)
      {
         if (transaction == null) 
            throw new ArgumentNullException("transaction");

         _transaction = transaction;
      }

      public TransactionElectionResult Result
      {
         get { return _result; }
      }

      public void Rollback()
      {
         VoteNo();
      }

      public void Commit()
      {
         VoteYes();
      }

      public void Dispose()
      {
         if (_result == TransactionElectionResult.InProgress)
            Rollback();
      }

      public void CreateVote()
      {
         Guid result = Guid.NewGuid();

         if (_voteStack.Contains(result)) CreateVote();

         _voteStack.Push(result);
      }

      public void VoteNo()
      {
         _result = TransactionElectionResult.FinishedNo;
         _transaction.Rollback();
         OnTransactionComplete();
      }

      protected virtual void OnTransactionComplete()
      {
         if (this.TransactionComplete != null)
            this.TransactionComplete(this, EventArgs.Empty);
      }

      public void VoteYes()
      {
         if (_voteStack.Count == 0) return;

         Guid id = _voteStack.Pop();

         if (_voteStack.Count > 0) return;

         _result = TransactionElectionResult.FinishedYes;

         try
         {
            _transaction.Commit();
         }
         catch
         {
            _transaction.Rollback();
            throw;
         }
         finally
         {
            OnTransactionComplete();
         }
      }

      public event EventHandler TransactionComplete;
   }
}
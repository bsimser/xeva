using System;
using System.Collections.Generic;
using XF.Model.UnitOfWorkImpl;

namespace XF.Model
{
   public static class UnitOfWork
   {
      public static bool IsReady
      {
         get { return (Store != null); }
      }

      /// <summary>
      /// Typically the store is initialized from the particular Runtime 
      /// you are using.
      /// </summary>
      public static void InitializeWithStore(IStore store)
      {
         Store = store;
      }

      /// <summary>
      /// Generally used to intialize a UnitOfWork with a mock ITransaction.
      /// </summary>
      public static void InitializeWithTransaction(ITransaction transaction)
      {
         ResetTransaction();
         TransactionElection election = new TransactionElection(transaction);
         election.TransactionComplete += OnTransactionComplete;
         Globals.Data[ModelConstants.TRANSACTION_ELECTION_KEY] = election;
      }

      private static void OnTransactionComplete(object sender, EventArgs e)
      {
         Store.Clear();
         ResetTransaction();
      }

      private static void Initialize()
      {
         ITransaction result = Globals.Data[ModelConstants.TRANSACTION_ELECTION_KEY] as ITransaction;

         if (result != null)
            return;

         IStore store = Store;

         if (store == null)
            throw new Exception("Need a store before enlisting work in a transaction.");

         ITransaction transaction = store.CreateTransaction();
         InitializeWithTransaction(transaction);

         return;
      }

      internal static IStore Store
      {
         get { return (IStore)Globals.Data[ModelConstants.STORE_KEY] ?? null; }
         private set { Globals.Data[ModelConstants.STORE_KEY] = value; }
      }

      public static ITransaction Transact()
      {
         Initialize();
         ActiveElection.CreateVote();
         return ActiveElection;
      }

      public static void Rollback()
      {
         ActiveElection.VoteNo();
      }

      public static void Commit()
      {
         ActiveElection.VoteYes();
      }

      private static void ResetTransaction()
      {
         Globals.Data[ModelConstants.TRANSACTION_ELECTION_KEY] = null;
      }

      private static TransactionElection ActiveElection
      {
         get { return Globals.Data[ModelConstants.TRANSACTION_ELECTION_KEY] as TransactionElection; }
      }
   }
}
using System;
using XEVA.Framework.Model;
using NHTransaction = NHibernate.ITransaction;

namespace XEVA.Framework.Store
{
   /// <summary>
   /// An adapter to an underlying NHibernate ITransaction
   /// </summary>
   public class NHibernateTransactionWrapper : ITransaction
   {
      private NHTransaction _transaction;

      public NHibernateTransactionWrapper(NHTransaction transaction)
      {
         _transaction = transaction;
      }

      public void Rollback()
      {
         if (!_transaction.IsActive) return;
         _transaction.Rollback();
      }

      public void Commit()
      {
         if (!_transaction.IsActive) return;
         _transaction.Commit();
      }

      public void Dispose()
      {
         if (_transaction == null) return;
         if (_transaction.IsActive) Rollback();
         _transaction.Dispose();
      }
   }
}
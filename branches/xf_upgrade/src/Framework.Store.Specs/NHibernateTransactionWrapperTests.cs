using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace XF.Store
{
   [TestFixture]
   public class NHibernateTransactionWrapperTests
   {
      private MockRepository _mocks;
      private ITransaction _mockTransaction;

      [SetUp]
      public void Setup()
      {
         this._mocks = new MockRepository();
         this._mockTransaction = _mocks.CreateMock<ITransaction>();
      }

      [Test]
      public void Should_delegate_rollback_to_underlying_NHibernate_transaction()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Rollback();
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transaction1 = new NHibernateTransactionWrapper(this._mockTransaction);
            transaction1.Rollback();
         }
      }

      [Test]
      public void Should_delegate_commit_to_underlying_NHibernate_transaction()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Commit();
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transaction2 = new NHibernateTransactionWrapper(this._mockTransaction);
            transaction2.Commit();
         }
      }

      [Test]
      public void Should_not_commit_if_already_rolled_back()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Rollback();
            Expect.Call(this._mockTransaction.IsActive).Return(false);
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transactionWrapper = new NHibernateTransactionWrapper(this._mockTransaction);
            transactionWrapper.Rollback();
            transactionWrapper.Commit();
         }
      }

      [Test]
      public void Should_automatically_rollback_if_uncommitted_transaction_is_disposed()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Rollback();
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Dispose();
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transactionWrapper = new NHibernateTransactionWrapper(this._mockTransaction);
            transactionWrapper.Dispose();
         }
      }

      [Test]
      public void Cannot_rollback_an_already_committed_transaction()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(true);
            this._mockTransaction.Commit();
            Expect.Call(this._mockTransaction.IsActive).Return(false);
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transactionWrapper = new NHibernateTransactionWrapper(this._mockTransaction);
            transactionWrapper.Commit();
            transactionWrapper.Rollback();
         }
      }

      [Test]
      public void Must_first_begin_a_transaction_before_rolling_back()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(false);
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transactionWrapper = new NHibernateTransactionWrapper(this._mockTransaction);
            // transaction.Begin(); <- We didn't start (or begin) it yet!
            transactionWrapper.Rollback();
         }
      }

      [Test]
      public void Must_first_begin_a_transaction_before_committing()
      {
         using (_mocks.Record())
         {
            Expect.Call(this._mockTransaction.IsActive).Return(false);
         }

         using (_mocks.Playback())
         {
            NHibernateTransactionWrapper transactionWrapper = new NHibernateTransactionWrapper(this._mockTransaction);
            // transaction.Begin(); <- We didn't start (or begin) it yet!
            transactionWrapper.Commit();
         }
      }
   }
}
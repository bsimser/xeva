using System;
using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Specs;

namespace XEVA.Framework.Model
{

   [TestFixture]
   public class When_initializing : Spec
   {
      private MockRepository _mocks;
      private IStore _mockStore;

      protected override void Before_each_spec()
      {
         _mocks = new MockRepository();
         _mockStore = _mocks.DynamicMock<IStore>();
      }

      [Test]
      public void Require_an_underlying_store()
      {
         Globals.Data.Clear();

         Assert.IsFalse(UnitOfWork.IsReady);
         UnitOfWork.InitializeWithStore(_mockStore);
         Assert.IsTrue(UnitOfWork.IsReady);
      }

      [Test]
      public void Puts_the_underlying_Store_in_global_thread_safe_storage()
      {
         Globals.Data.Clear();

         UnitOfWork.InitializeWithStore(_mockStore);

         Assert.IsNotNull(Globals.Data[ModelConstants.STORE_KEY]);
         Assert.IsInstanceOfType(typeof(IStore),
                                 Globals.Data[ModelConstants.STORE_KEY]);
      }
   }

   [TestFixture]
   public class When_marking_a_transaction_start : Spec
   {
      private IStore _mockStore;

      protected override void Before_each_spec()
      {
         _mockStore = Mock<IStore>();
      }

      [Test]
      public void Should_obtain_a_real_transaction_from_the_store()
      {
         ITransaction mockTransaction = Mock<ITransaction>();

         using (Record)
            Expect.Call(_mockStore.CreateTransaction()).Return(mockTransaction);

         using (Playback)
         {
            Globals.Data.Clear();
            UnitOfWork.InitializeWithStore(_mockStore);
            UnitOfWork.Transact();
         }
      }

      [Test, ExpectedException(typeof(Exception))]
      public void Fail_if_there_is_no_underlying_store()
      {
         Globals.Data.Clear();

         UnitOfWork.Transact();
      }

      [Test]
      public void After_the_first_transact_return_voting_transactions()
      {
         ITransaction mockRootTransaction = Mock<ITransaction>();

         using (Record)
         {
            mockRootTransaction.Commit();
         }

         using (Playback)
         {
            Globals.Data.Clear();
            UnitOfWork.InitializeWithStore(_mockStore);
            UnitOfWork.InitializeWithTransaction(mockRootTransaction);

            ITransaction rootTransaction = UnitOfWork.Transact();
            ITransaction nestedTransaction1 = UnitOfWork.Transact();
            ITransaction nestedTransaction2 = UnitOfWork.Transact();
            ITransaction nestedTransaction3 = UnitOfWork.Transact();

            nestedTransaction3.Commit();
            nestedTransaction2.Commit();
            nestedTransaction1.Commit();

            UnitOfWork.Commit();
         }
      }
   }

   [TestFixture]
   public class When_committing_a_transaction : Spec
   {
      private IStore _mockStore;

      protected override void Before_each_spec()
      {
         _mockStore = Mock<IStore>();
      }

      [Test, ExpectedException(typeof(UnitOfWorkTestException))]
      public void Rollback_when_an_exception_occurs()
      {
         ITransaction mockRootTransaction = Mock<ITransaction>();

         using (Record)
         {
            mockRootTransaction.Commit();
            LastCall.Throw(new UnitOfWorkTestException());
            mockRootTransaction.Rollback();
         }

         using (Playback)
         {
            Globals.Data.Clear();
            UnitOfWork.InitializeWithStore(_mockStore);
            UnitOfWork.InitializeWithTransaction(mockRootTransaction);

            ITransaction rootTransaction = UnitOfWork.Transact();
            ITransaction nestedTransaction1 = UnitOfWork.Transact();
            ITransaction nestedTransaction2 = UnitOfWork.Transact();
            ITransaction nestedTransaction3 = UnitOfWork.Transact();

            nestedTransaction3.Commit();
            nestedTransaction2.Commit();
            nestedTransaction1.Commit();

            UnitOfWork.Commit();
         }
      }

      [Test]
      public void Require_a_commit_vote_per_transaction_start()
      {
         ITransaction mockRootTransaction = Mock<ITransaction>();

         using (Record)
         {
            mockRootTransaction.Commit();
         }

         using (Playback)
         {
            Globals.Data.Clear();
            UnitOfWork.InitializeWithStore(_mockStore);
            UnitOfWork.InitializeWithTransaction(mockRootTransaction);

            ITransaction rootTransaction = UnitOfWork.Transact();
            ITransaction nestedTransaction1 = UnitOfWork.Transact();
            ITransaction nestedTransaction2 = UnitOfWork.Transact();
            ITransaction nestedTransaction3 = UnitOfWork.Transact();

            UnitOfWork.Commit();
            UnitOfWork.Commit();
            UnitOfWork.Commit();
            UnitOfWork.Commit();
         }
      }

      [Test]
      public void Clear_the_store_when_the_transaction_completes()
      {
         ITransaction mockRootTransaction = Mock<ITransaction>();

         using (Record)
         {
            mockRootTransaction.Commit();
            _mockStore.Clear();
         }

         using (Playback)
         {
            Globals.Data.Clear();
            UnitOfWork.InitializeWithStore(_mockStore);
            UnitOfWork.InitializeWithTransaction(mockRootTransaction);

            using (UnitOfWork.Transact())
            {
               bool meaninglessStatement = true;
               UnitOfWork.Commit();
            }
         }
      }
   }

   [TestFixture]
   public class When_commit_is_not_called : Spec
   {
      private IStore _mockStore;

      protected override void Before_each_spec()
      {
         _mockStore = Mock<IStore>();
      }

      [Test]
      public void Do_an_automatic_rollback()
      {
         ITransaction mockTransaction = Mock<ITransaction>();
         UnitOfWork.InitializeWithTransaction(mockTransaction);

         using (Record)
         {
            mockTransaction.Rollback();
         }

         using (Playback)
         {
            UnitOfWork.InitializeWithStore(_mockStore);

            using (UnitOfWork.Transact())
            {
               bool meaninglessStatement = true;
            }
         }
      }
   }

   [TestFixture]
   public class When_committing_to_real_transactions_back_to_back : Spec
   {
      private IStore _mockStore;

      protected override void Before_each_spec()
      {
         _mockStore = Mock<IStore>();
      }

      [Test]
      public void Must_initialize_the_store()
      {
         ITransaction mockTransaction1 = Mock<ITransaction>();
         ITransaction mockTransaction2 = Mock<ITransaction>();

         using (Record)
         {
            Expect.Call(_mockStore.CreateTransaction()).Return(mockTransaction1);
            mockTransaction1.Commit();

            Expect.Call(_mockStore.CreateTransaction()).Return(mockTransaction2);
            mockTransaction2.Commit();

            _mockStore.Clear();
            LastCall.Repeat.Twice();
         }

         using (Playback)
         {
            UnitOfWork.InitializeWithStore(_mockStore);

            using (UnitOfWork.Transact())
            {
               bool bogusStatement = true;
               UnitOfWork.Commit();
            }

            Assert.IsNull(Globals.Data[ModelConstants.TRANSACTION_ELECTION_KEY]);

            using (UnitOfWork.Transact())
            {
               bool bogusStatement = true;
               UnitOfWork.Commit();
            }
         }
      }
   }

   public class UnitOfWorkTestException : Exception
   {
   }
}
using System;
using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Model;
using XEVA.Framework.Specs;
using ITransaction=NHibernate.ITransaction;

namespace XEVA.Framework.Store
{
   [TestFixture]
   public class When_saving_an_entity : Spec
   {
      private ISession _session;

      protected override void Before_each_spec()
      {
         _session = Mock<ISession>();
      }

      [Test]
      public void Attempt_to_open_a_closed_session()
      {
         object fakeEntity = new object();

         using (Record)
         {
            _session.FlushMode = FlushMode.Commit;
            Expect.Call(_session.IsOpen).Return(true);
            _session.SaveOrUpdate(fakeEntity);
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter =
               new NHibernateSessionAdapter(_session);
            sessionAdapter.Save(fakeEntity);
         }
      }

      [Test]
      public void Check_to_see_if_session_is_open_before_saving()
      {
         object fakeEntity = new object();

         using (Record)
         {
            _session.FlushMode = FlushMode.Commit;
            Expect.Call(_session.IsOpen).Return(true);
            _session.SaveOrUpdate(fakeEntity);
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter =
               new NHibernateSessionAdapter(_session);
            sessionAdapter.Save(fakeEntity);
         }
      }

      [Test]
      public void Delegate_to_an_NHibernate_session_on_flush()
      {
         using (Record)
         {
            _session.FlushMode = FlushMode.Commit;
            Expect.Call(_session.IsOpen).Return(true);
            _session.Flush();
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_session);
            sessionAdapter.Flush();
         }
      }
   }

   [TestFixture]
   public class When_loading_an_entity : Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Delegate_to_an_NHibernate_session()
      {
         FakeEntity fakeEntity = new FakeEntity();

         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            Expect.Call(_mockSession.Load<FakeEntity>(Guid.NewGuid())).Return(fakeEntity);
            LastCall.IgnoreArguments();
         }

         using (Record)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            sessionAdapter.Load<FakeEntity>(new Guid());
         }
      }

      [Test, ExpectedException(typeof(EntityNotFoundException))]
      public void Fail_if_an_entity_was_not_found()
      {
         Guid entityID = Guid.NewGuid();

         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            Expect.Call(_mockSession.Load<FakeEntity>(entityID)).Throw(
               new UnresolvableObjectException(entityID, typeof(FakeEntity)));
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);

            try
            {
               sessionAdapter.Load<FakeEntity>(entityID);
            }
            catch (EntityNotFoundException ex)
            {
               Assert.AreEqual(entityID, ex.SuppliedEntityID);
               throw;
            }
         }
      }
   }

   [TestFixture]
   public class When_deleting_an_entity : Spec
   {
      private ISession _session;

      protected override void Before_each_spec()
      {
         _session = Mock<ISession>();
      }

      [Test]
      public void Should_delegate_to_the_underlying_NHibernate_session()
      {
         object fakeEntity = new object();

         using (Record)
         {
            _session.FlushMode = FlushMode.Commit;
            Expect.Call(_session.IsOpen).Return(true);
            _session.Delete(fakeEntity);
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter =
               new NHibernateSessionAdapter(_session);
            sessionAdapter.Delete(fakeEntity);
         }
      }
   }

   [TestFixture]
   public class When_opening_with_a_closed_session : Spec
   {
      private ISession _session;

      protected override void Before_each_spec()
      {
         _session = Mock<ISession>();
      }

      [Test, ExpectedException(typeof(StoreClosedException))]
      public void Should_throw_an_exception()
      {
         using (Record)
         {
            _session.FlushMode = FlushMode.Commit;
            Expect.Call(_session.IsOpen).Return(false);
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter =
               new NHibernateSessionAdapter(_session);
            sessionAdapter.Open();
         }
      }
   }

   [TestFixture]
   public class When_clearing_a_session: Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Cause_the_underlying_NHibernate_query_to_clear()
      {
         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            _mockSession.Clear();
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            sessionAdapter.Clear();
         }
      }
   }

   [TestFixture]
   public class When_a_transaction_is_committed: Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Flush_the_NHibernate_session()
      {
         using (Record)
            _mockSession.FlushMode = FlushMode.Commit;

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
         }
      }
   }

   [TestFixture]
   public class When_executing_a_query : Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Map_to_and_execute_a_NHibernate_query()
      {
         IQuery iQuery = Mock<IQuery>();

         IList<FakeEntity> entities = new List<FakeEntity>();
         INamedQuery fakeQuery = new FakeNamedQuery();
         fakeQuery.SetParameter("TestID", Guid.Empty);

         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            Expect.Call(_mockSession.GetNamedQuery(fakeQuery.Name)).Return(iQuery);
            Expect.Call(iQuery.SetParameter("TestID", Guid.Empty)).Return(null);
            Expect.Call(iQuery.List<FakeEntity>()).Return(entities);
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            sessionAdapter.Query<FakeEntity>(fakeQuery);
         }
      }
   }

   [TestFixture]
   public class When_beginning_a_transaction : Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Start_and_wrap_an_NHibernate_transaction()
      {
         ITransaction mockTransaction = Mock<ITransaction>();

         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            Expect.Call(_mockSession.BeginTransaction()).Return(mockTransaction);
         }

         using (Playback)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            Model.ITransaction transaction = sessionAdapter.CreateTransaction();
         }
      }
   }

   [TestFixture]
   public class When_closing : Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Delegate_to_the_NHibernate_session()
      {
         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            Expect.Call(_mockSession.Close()).Return(null);
         }

         using (Record)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            sessionAdapter.Close();
         }
      }

   }

   [TestFixture]
   public class When_refreshing_store_contents : Spec
   {
      private ISession _mockSession;

      protected override void Before_each_spec()
      {
         _mockSession = Mock<ISession>();
      }

      [Test]
      public void Should_delegate_to_the_underlying_NHibernate_session_on_refresh()
      {
         object fakeEntity = new object();

         using (Record)
         {
            _mockSession.FlushMode = FlushMode.Commit;
            Expect.Call(_mockSession.IsOpen).Return(true);
            _mockSession.Refresh(fakeEntity);
            LastCall.IgnoreArguments();
         }

         using (Record)
         {
            NHibernateSessionAdapter sessionAdapter = new NHibernateSessionAdapter(_mockSession);
            sessionAdapter.Refresh(fakeEntity);
         }
      }
   }
}
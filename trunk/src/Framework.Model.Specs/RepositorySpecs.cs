using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using XF.Model;
using XF.Specs;

namespace Specs_for_Repository
{
   [TestFixture]
   public class When_finding_an_individual_entity_by_ID : Spec
   {
      private IStore _store;

      protected override void Before_each_spec()
      {
         _store = Mock<IStore>();
         UnitOfWork.InitializeWithStore(_store);
      }

      [Test]
      public void Should_forward_the_find_request_to_the_store()
      {
         using (Record)
         {
            Expect.Call(_store.Load<FakeEntity>(Guid.Empty)).Return(new FakeEntity());
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            FakeRepository repository = new FakeRepository();
            repository.FindBy(Guid.Empty);
         }
      }

   }

   [TestFixture]
   public class When_finding_a_set_of_entities_by_a_query : Spec
   {
      private FakeRepository _repository;
      private IStore _store;

      protected override void Before_each_spec()
      {
         _repository = new FakeRepository();
         _store = Mock<IStore>();
         UnitOfWork.InitializeWithStore(_store);
      }

      [Test]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Throw_an_exception_if_the_query_is_null()
      {
         _repository.FindByQuery(null);
      }


      [Test]
      public void Forward_the_query_request_to_the_store()
      {
         IList<FakeEntity> stubResult = new List<FakeEntity>();
         INamedQuery stubNamedQuery = Stub<INamedQuery>();

         using (Record)
         {
            Expect.Call(_store.Query<FakeEntity>(null)).Return(stubResult);
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            FakeRepository repository = new FakeRepository();
            repository.FindByQuery(stubNamedQuery);
         }
      }

   }

   [TestFixture]
   public class When_the_store_has_not_been_initialized : Spec
   {
      private FakeRepository _repository;

      protected override void Before_each_spec()
      {
         _repository = new FakeRepository();
         UnitOfWork.InitializeWithStore(null);
      }

      [Test]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_when_finding_an_entity_by_id()
      {
         _repository.FindBy(Guid.Empty);
      }

      [Test]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_when_finding_entities_by_query()
      {
         _repository.FindByQuery(null);
      }

      [Test]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_on_attempts_to_save()
      {
         FakeEntity e = new FakeEntity();
         _repository.Save(e);
      }


      [Test]
      [ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_on_attempts_to_delete()
      {
         FakeEntity e = new FakeEntity();
         _repository.Delete(e);
      }

   }

   [TestFixture]
   public class When_saving_an_entity : Spec
   {
      private FakeRepository _repository;
      private IStore _store;

      protected override void Before_each_spec()
      {
         _repository = new FakeRepository();
         _store = Mock<IStore>();
         UnitOfWork.InitializeWithStore(_store);
      }


      [Test]
      public void Forward_the_save_request_to_the_store()
      {
         FakeEntity fakeEntity = new FakeEntity();
         fakeEntity.ID = Guid.NewGuid(); // pretend it's an existing entity

         using (Record)
         {
            _store.Save(fakeEntity);
         }

         using (Playback)
         {
            _repository.Save(fakeEntity);
         }
      }

   }

   [TestFixture]
   public class When_deleting_an_entity : Spec
   {
      private FakeRepository _repository;
      private IStore _store;

      protected override void Before_each_spec()
      {
         _repository = new FakeRepository();
         _store = Mock<IStore>();
         UnitOfWork.InitializeWithStore(_store);
      }

      [Test]
      public void Forward_the_delete_request_to_the_store()
      {
         FakeEntity fakeEntity = new FakeEntity();
         fakeEntity.ID = Guid.NewGuid(); // pretend it's an existing entity

         using (Record)
         {
            _store.Delete(fakeEntity);
         }

         using (Playback)
         {
            _repository.Delete(fakeEntity);
         }
      }
   }
}
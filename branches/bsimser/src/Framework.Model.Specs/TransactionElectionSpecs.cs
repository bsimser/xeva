using System;
using NUnit.Framework;
using Rhino.Mocks;
using XF.Model.UnitOfWorkImpl;
using XF.Specs;
using XF.Model;

namespace Specs_for_TransactionElection
{
   [TestFixture]
   public class When_an_election_is_started : Spec
   {
      private ITransaction _stubTransaction;
      private TransactionElection _election;

      protected override void Before_each_spec()
      {
         _stubTransaction = Stub<ITransaction>();
         _election = new TransactionElection(_stubTransaction);
      }

      [Test]
      public void The_result_should_be_in_progress()
      {
         
         Assert.AreEqual(_election.Result, TransactionElectionResult.InProgress);
      }
   }

   [TestFixture]
   public class When_votes_are_cast : Spec
   {
      private TransactionElection _election;
      private ITransaction _transaction;

      protected override void Before_each_spec()
      {
         _transaction = Mock<ITransaction>();
         _election = new TransactionElection(_transaction);
      }

      [Test]
      public void A_single_no_vote_should_rollback_the_transaction()
      {
         
         using (Record)
         {
            _transaction.Rollback();
         }

         using (Playback)
         {
            _election.CreateVote();
            _election.VoteNo();
         }
      }

      [Test]
      public void A_no_vote_will_leave_the_election_with_a_finished_and_voted_no_status()
      {
         using (Record)
         {
            _transaction.Rollback();
         }

         using (Playback)
         {
            _election.CreateVote();
            _election.VoteNo();
            Assert.AreEqual(TransactionElectionResult.FinishedNo, _election.Result);
         }
      }

      [Test]
      public void Unanimous_yes_votes_will_commit_the_underlying_transaction()
      {
         using (Record)
         {
            _transaction.Commit();
         }

         using (Playback)
         {
            _election.CreateVote();
            _election.CreateVote();
            _election.VoteYes();

            Assert.AreEqual(TransactionElectionResult.InProgress, _election.Result);

            _election.VoteYes();
         }
      }

      [Test]
      public void A_yes_vote_will_leave_the_election_with_a_finished_and_voted_yes_status()
      {
         using (Record)
         {
            _transaction.Commit();
         }

         using (Playback)
         {
            _election.CreateVote();
            _election.CreateVote();
            _election.VoteYes();

            Assert.AreEqual(TransactionElectionResult.InProgress, _election.Result);

            _election.VoteYes();

            Assert.AreEqual(TransactionElectionResult.FinishedYes, _election.Result);
         }
      }
   }

   [TestFixture]
   public class When_committing_the_underlying_transaction : Spec
   {
      private TransactionElection _election;
      private ITransaction _transaction;

      protected override void Before_each_spec()
      {
         _transaction = Mock<ITransaction>();
         _election = new TransactionElection(_transaction);
      }


      [Test]
      public void Should_notify_that_the_transaction_has_completed()
      {
         using (Record)
         {
            _transaction.Commit();
         }

         using (Playback)
         {
            bool completeEventFired = false;
            _election.TransactionComplete += delegate
                                           {
                                              completeEventFired = true;
                                           };
            _election.CreateVote();
            _election.CreateVote();

            _election.VoteYes();
            _election.VoteYes();

            Assert.IsTrue(completeEventFired);
         }
      }

      [Test, ExpectedException(typeof(Exception), "Random Exception")]
      public void Should_notify_even_when_an_exception_occurs_during_commit()
      {

         using (Record)
         {
            _transaction.Commit();
            LastCall.Throw(new Exception("Random Exception"));
            _transaction.Rollback();
         }

         using (Playback)
         {
            bool completeEventFired = false;
            _election.TransactionComplete += delegate
                                           {
                                              completeEventFired = true;
                                           };
            _election.CreateVote();
            _election.CreateVote();

            _election.VoteYes();

            try
            {
               // this would force a commit.
               // i'm stacking the deck w/ an exception which does a rollback, but...
               // SHOULD still fire the completed event!
               _election.VoteYes();
            }
            catch (Exception ex)
            {
               Assert.IsTrue(completeEventFired);
               throw;
            }
         }
      }
   }

   [TestFixture]
   public class When_rolling_back_the_underling_transaction : Spec
   {
      private TransactionElection _election;
      private ITransaction _transaction;

      protected override void Before_each_spec()
      {
         _transaction = Mock<ITransaction>();
         _election = new TransactionElection(_transaction);
      }

      [Test]
      public void Should_notify_that_the_transaction_has_completed()
      {
         using (Record)
         {
            _transaction.Rollback();
         }

         using (Playback)
         {
            bool completeEventFired = false;
            _election.TransactionComplete += delegate
                                           {
                                              completeEventFired = true;
                                           };
            _election.CreateVote();
            _election.VoteNo();

            Assert.IsTrue(completeEventFired);
         }
      }
   }

}
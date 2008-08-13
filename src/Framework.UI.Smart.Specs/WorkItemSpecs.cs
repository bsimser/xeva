using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_WorkItem
{
   public class When_processing_a_WorkItem : Spec
   {
      [Test]
      public void Execute_operations()
      {
         var executed = false;
         Action operation = () => executed = true;
         Action completed = () => { var one = 1; };
         new WorkItem(new List<Action> {operation}, null, completed).ProcessOperations();
         Assert.That(executed, Is.True);
      }

      [Test]
      public void Execute_multiple_operations_in_the_order_provided()
      {
         var toggle = true;
         Action first = () => toggle = true;
         Action second = () => toggle = false;
         Action completed = () => { var one = 1; };

         new WorkItem(new List<Action> {first, second}, null, completed).ProcessOperations();
         Assert.That(toggle, Is.False);
      }
   }

   public class After_processing_a_WorkItem : Spec
   {
      [Test]
      public void Ignore_subsequent_attempts_to_process()
      {
         var processedCount = 0;
         Action operation = () => { processedCount += 1; };
         Action completed = () => { var one = 1; };

         var item = new WorkItem(new List<Action> { operation }, null, completed);
         item.ProcessOperations();
         item.ProcessOperations();

         Assert.That(processedCount, Is.EqualTo(1));
      }
   }

   public class When_completing_a_WorkItem_that_was_successfully_processed : Spec
   {
      [Test]
      public void Call_the_completed_action()
      {
         var processed = false;
         Action operation = () => { var one = 1; };
         Action completed = () => { processed = true; };

         var item = new WorkItem(new List<Action> { operation }, null, completed);
         item.ProcessOperations();
         item.Complete();
         Assert.That(processed, Is.True);
      }
   }

   public class When_completing_a_WorkItem_that_was_not_processed : Spec
   {
      [Test]
      public void Do_not_call_the_completed_action()
      {
         var processed = false;
         Action operation = () => { var one = 1; };
         Action completed = () => { processed = true; };

         var item = new WorkItem(new List<Action> { operation }, null, completed);

         // user forgot to call this: item.ProcessOperations();
         item.Complete();
         
         Assert.That(processed, Is.False);
      }
   }

   public class When_completing_a_WorkItem_that_encountered_a_processing_error : Spec
   {
      [Test]
      public void Call_the_fault_action_if_supplied()
      {
         var faultCalled = false;
         Action operation = () => { throw new Exception("fun!"); };
         Action<Exception> fault = (e) => faultCalled = true;
         Action completed = () => { var one = 1; };

         var item = new WorkItem(new List<Action> {operation}, fault, completed);
         item.ProcessOperations();
         item.Complete();
         Assert.That(faultCalled, Is.True);
      }

      [Test]
      public void Rethrow_the_exception_if_there_is_no_fault_action()
      {
         Action operation = () => { throw new Exception("fun!"); };
         Action completed = () => { var one = 1; };

         try
         {
            var item = new WorkItem(new List<Action> {operation}, null, completed);
            item.ProcessOperations();
            item.Complete();
         }
         catch (Exception ex)
         {
            Assert.That(ex.Message, Is.EqualTo("fun!"));
            return;
         }

         Assert.Fail();
      }
   }

   public class When_creating_a_new_WorkItem_without_operations : Spec
   {
      [Test]
      public void Throw_an_error()
      {
         var exceptionCount = 0;

         Action completed = () => { var one = 1; };

         try
         {
            new WorkItem(null, null, completed);
         }
         catch
         {
            exceptionCount += 1;
         }
         try
         {
            new WorkItem(new List<Action>(), null, completed);
         }
         catch
         {
            exceptionCount += 1;
         }

         Assert.That(exceptionCount, Is.EqualTo(2));
      }
   }

   public class When_creating_a_new_WorkItem_without_a_completed_action: Spec
   {
      [Test]
      public void Throw_an_error()
      {
         var exceptionCount = 0;
         Action operation = () => { var one = 1; };
         
         try
         {
            new WorkItem(new List<Action>() {operation}, null, null);
         }
         catch
         {
            return;
         }

         Assert.Fail();
      }
   }
}
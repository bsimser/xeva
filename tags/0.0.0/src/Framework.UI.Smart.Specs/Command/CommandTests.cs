using System.ComponentModel;
using NUnit.Framework;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class CommandTests
   {
      [Test]
      public void Can_cancel_an_executing_command()
      {
         Command theUnit = new Command();

         theUnit.Executing += delegate(object sender, CancelEventArgs args)
                              {
                                 args.Cancel = true;
                              };

         bool wasExecuted = false;

         theUnit.Callback = delegate
                            {
                               wasExecuted = true;
                            };

         theUnit.Enabled = true;
         theUnit.Execute();

         Assert.IsFalse(wasExecuted);
      }

      [Test]
      public void Can_specify_the_code_that_gets_executed()
      {
         Command theUnit = new Command();

         bool calledBack = false;

         theUnit.Callback = delegate
                            {
                               calledBack = true;
                            };

         theUnit.Enabled = true;
         theUnit.Execute();

         Assert.IsTrue(calledBack);
      }

      [Test]
      public void Showing_a_command_makes_it_visible()
      {
         Command theUnit = new Command();

         theUnit.Show();

         Assert.IsTrue(theUnit.Visible);
      }

      [Test]
      public void Hiding_a_command_makes_it_invisible()
      {
         Command theUnit = new Command();
         theUnit.Hide();

         Assert.IsFalse(theUnit.Visible);
      }

      [Test]
      public void Sends_a_notification_upon_execution()
      {
         Command theUnit = new Command();

         bool wasExecuted = false;

         theUnit.Executed += delegate
                             {
                                wasExecuted = true;
                             };

         theUnit.Enabled = true;
         theUnit.Execute();

         Assert.IsTrue(wasExecuted);
      }

      [Test]
      public void Disabled_commands_do_not_execute()
      {
         Command theUnit = new Command();

         bool firedExcuting = false;
         bool calledBack = false;
         bool firedExecuted = false;

         theUnit.Executed += delegate
                             {
                                firedExcuting = true;
                             };

         theUnit.Executing += delegate
                              {
                                 firedExecuted = true;
                              };

         theUnit.Callback = delegate
                            {
                               calledBack = true;
                            };

         theUnit.Enabled = false;

         theUnit.Execute();

         Assert.IsFalse(firedExcuting);
         Assert.IsFalse(firedExecuted);
         Assert.IsFalse(calledBack);
      }

      [Test]
      public void Should_be_disabled_by_default()
      {
         Command theUnit = new Command();
         Assert.IsFalse(theUnit.Enabled);
      }

      [Test]
      public void Should_be_invisible_by_default()
      {
         Command theUnit = new Command();
         Assert.IsFalse(theUnit.Visible);
      }

      [Test]
      public void Should_indicate_when_enabled_state_is_changed()
      {
         Command theUnit = new Command();

         int eventFiredCount = 0;

         theUnit.EnabledChanged += delegate
                                   {
                                      eventFiredCount += 1;
                                   };

         theUnit.Enabled = theUnit.Enabled;
         Assert.AreEqual(0, eventFiredCount);

         theUnit.Enabled = !theUnit.Enabled;
         Assert.AreEqual(1, eventFiredCount);

         theUnit.Enabled = !theUnit.Enabled;
         Assert.AreEqual(2, eventFiredCount);
      }

      [Test]
      public void Should_indicate_when_visibility_changes()
      {
         Command theUnit = new Command();

         int eventFiredCount = 0;

         theUnit.VisibleChanged += delegate
                                   {
                                      eventFiredCount += 1;
                                   };

         theUnit.Visible = theUnit.Visible;
         Assert.AreEqual(0, eventFiredCount);

         theUnit.Visible = !theUnit.Visible;
         Assert.AreEqual(1, eventFiredCount);

         theUnit.Visible = !theUnit.Visible;
         Assert.AreEqual(2, eventFiredCount);
      }

      [Test]
      public void Commands_are_hierarchical_and_moveable()
      {
         Command child = new Command();
         Command firstParent = new Command();
         Command secondParent = new Command();

         child.Key = "child";
         firstParent.Key = "firstParent";
         secondParent.Key = "secondParent";

         child.Parent = firstParent;

         Assert.IsTrue(firstParent.Children.ContainsKey("child"));
         Assert.IsFalse(secondParent.Children.ContainsKey("child"));
         Assert.IsTrue(child.Parent.Key == "firstParent");

         child.Parent = secondParent;

         Assert.IsFalse(firstParent.Children.ContainsKey("child"));
         Assert.IsTrue(secondParent.Children.ContainsKey("child"));
         Assert.IsTrue(child.Parent.Key == "secondParent");
      }

      [Test]
      public void A_command_cannot_be_added_as_the_child_of_the_same_parent_twice()
      {
         Command child = new Command();
         Command parent = new Command();

         child.Key = "child";
         parent.Key = "parent";

         child.Parent = parent;
         child.Parent = parent;

         Assert.AreEqual(1, parent.Children.Count);
      }
   }
}
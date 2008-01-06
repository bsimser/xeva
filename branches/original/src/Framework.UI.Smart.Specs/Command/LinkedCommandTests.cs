using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class LinkedCommandTests
   {
      private MockRepository _mocks;

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
      }

      [Test]
      public void When_executed_will_delegate_to_the_target_command()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();
         
         using (_mocks.Record())
         {
            mockCommand.Execute();
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            linkedCommand.Execute();
         }
      }

      [Test]
      public void Inherits_the_visibility_of_the_target_Command()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();

         using (_mocks.Record())
         {
            mockCommand.Visible = true;
            Expect.Call(mockCommand.Visible).Return(true);
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            linkedCommand.Visible = true;
            Assert.IsTrue(linkedCommand.Visible);
         }
      }

      [Test]
      public void Inherits_the_enabled_state_of_the_target_Command()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();

         using (_mocks.Record())
         {
            mockCommand.Enabled = true;
            Expect.Call(mockCommand.Enabled).Return(true);
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            linkedCommand.Enabled = true;
            Assert.IsTrue(linkedCommand.Enabled);
         }
      }

      [Test]
      public void Inherits_the_key_of_the_target_Command()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();

         using (_mocks.Record())
         {
            Expect.Call(mockCommand.Key).Return("TestKey");
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            Assert.AreEqual("TestKey", linkedCommand.Key);
         }
      }

      [Test]
      public void The_label_is_set_to_the_label_of_the_target_Command_by_default()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();

         using (_mocks.Record())
         {
            Expect.Call(mockCommand.Label).Return("OriginalLabel");
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            Assert.AreEqual("OriginalLabel", linkedCommand.Label);
         }
      }

      [Test]
      public void The_label_can_be_customized()
      {
         ICommand mockCommand = _mocks.CreateMock<ICommand>();

         using (_mocks.Record())
         {
            Expect.Call(mockCommand.Label).Return("Original Label");
         }

         using (_mocks.Playback())
         {
            LinkedCommand linkedCommand = new LinkedCommand(mockCommand);
            Assert.AreEqual("Original Label", linkedCommand.Label);

            linkedCommand.Label = "Customized Label";
            Assert.AreEqual("Customized Label", linkedCommand.Label);
         }
      }

   }
}

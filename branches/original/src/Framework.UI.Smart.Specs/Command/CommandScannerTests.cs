using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class CommandScannerTests
   {
      private MockRepository _mocks;
      private ILabelLookupService _stubLabelService;

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
         this._stubLabelService = _mocks.Stub<ILabelLookupService>();
      }

      [Test]
      public void Will_create_a_command_for_each_method_marked_with_a_command_attribute()
      {
         PresenterWithCallbackCommands testPresenter = new PresenterWithCallbackCommands();

         CommandScanner commandScanner = new CommandScanner(this._stubLabelService);

         IList<ICommand> commands = commandScanner.ScanForCommands(testPresenter);

         Assert.AreEqual(2, commands.Count);
      }

      [Test]
      public void Will_give_each_created_command_a_key_of_the_type_name_plus_the_method_name()
      {
         PresenterWithCallbackCommands testPresenter = new PresenterWithCallbackCommands();

         CommandScanner commandScanner = new CommandScanner(this._stubLabelService);

         IList<ICommand> commands = commandScanner.ScanForCommands(testPresenter);

         Assert.AreEqual("XEVA.Framework.UI.Smart.PresenterWithCallbackCommands.SomeMethod1", commands[0].Key);
         Assert.AreEqual("XEVA.Framework.UI.Smart.PresenterWithCallbackCommands.SomeMethod2", commands[1].Key);
      }

      [Test]
      public void Will_lookup_the_command_label_based_on_the_command_key()
      {
         PresenterWithCallbackCommands testPresenter = new PresenterWithCallbackCommands();
         ILabelLookupService mockLookupService = this._mocks.CreateMock<ILabelLookupService>();

         string key1 = "XEVA.Framework.UI.Smart.PresenterWithCallbackCommands.SomeMethod1";
         string key2 = "XEVA.Framework.UI.Smart.PresenterWithCallbackCommands.SomeMethod2";

         using (_mocks.Record())
         {
            Expect.Call(mockLookupService.LabelExists(key1)).Return(true);
            Expect.Call(mockLookupService.LabelExists(key2)).Return(true);
            Expect.Call(mockLookupService.GetLabel(key1)).Return("Buckwheat");
            Expect.Call(mockLookupService.GetLabel(key2)).Return("Spanky");
         }

         using (_mocks.Playback())
         {
            CommandScanner commandScanner = new CommandScanner(mockLookupService);

            IList<ICommand> commands = commandScanner.ScanForCommands(testPresenter);

            Assert.AreEqual(2, commands.Count);

            Assert.AreEqual("Buckwheat", commands[0].Label);
            Assert.AreEqual("Spanky", commands[1].Label);
         }
      }

      [Test]
      public void A_scanned_command_will_execute_the_method_it_is_declared_on()
      {
         PresenterWithCallbackCommands testPresenter = new PresenterWithCallbackCommands();

         CommandScanner theUnit = new CommandScanner(this._stubLabelService);

         IList<ICommand> commands = theUnit.ScanForCommands(testPresenter);

         ICommand command = commands[0];
         command.Enabled = true;

         Assert.IsFalse(testPresenter.MethodOneCalled);

         command.Execute();

         Assert.IsTrue(testPresenter.MethodOneCalled);
      }
   }
}
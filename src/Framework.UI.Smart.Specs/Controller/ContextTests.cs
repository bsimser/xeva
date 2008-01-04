using System;
using NUnit.Framework;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class ContextTests
   {
      private bool _fired = false;

      [Test]
      public void Can_add_and_find_pieces_of_state_to_the_context()
      {
         Context theUnit = new Context();

         Guid data = Guid.NewGuid();

         theUnit["EntityID"] = data;

         Guid returnedData = (Guid)theUnit["EntityID"];

         Assert.AreEqual(data, returnedData);
      }

      [Test, ExpectedException(typeof (ContextStateNotFoundException))]
      public void Will_throw_an_exception_if_a_requested_piece_of_state_is_not_found()
      {
         Context theUnit = new Context();

         Guid id = (Guid)theUnit["EntityID"];
      }

      [Test]
      public void Will_notify_when_a_new_piece_of_state_is_added()
      {
         Context theUnit = new Context();
         
         this._fired = false;

         theUnit.StateChanged += this.StateChangedHandler;

         Guid data = Guid.NewGuid();

         theUnit["EntityID"] = data;

         Assert.IsTrue(this._fired);
      }

      [Test]
      public void Will_notify_when_an_existing_piece_of_state_has_its_value_changed()
      {
         Context theUnit = new Context();
         this._fired = false;

         theUnit.StateChanged += this.StateChangedHandler;

         Guid data1 = Guid.NewGuid();
         theUnit["EntityID"] = data1;

         Assert.IsTrue(this._fired);

         this._fired = false;

         Guid data2 = Guid.NewGuid();
         theUnit["EntityID"] = data2;

         Assert.IsTrue(this._fired);        
      }

      [Test]
      public void Will_not_notify_when_state_changes_if_existing_state_data_has_the_same_value()
      {
         Context theUnit = new Context();

         Guid data = Guid.NewGuid();

         theUnit["EntityID"] = data;

         _fired = false;

         theUnit.StateChanged += StateChangedHandler;

         theUnit["EntityID"] = new Guid(data.ToString());

         Assert.IsFalse(_fired);
      }

      private void StateChangedHandler(object sender, EventArgs e)
      {
         _fired = true;
      }
   }
}
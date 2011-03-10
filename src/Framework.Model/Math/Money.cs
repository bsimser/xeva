using System;

namespace XF.Model {
   public class Money {
      private readonly int _cents;

      public Money() {}

      public Money(decimal amount) {
         Amount = XFMath.Round((RoundMethods)Globals.Data["RoundMethod"], amount);
         _cents = Convert.ToInt32(Amount * 100);
      }

      public decimal Amount { get; private set; }
      public int Cents { get { return _cents; } }

      public virtual bool Equals(Money obj) {
         if (ReferenceEquals(null, obj))
            return false;
         if (ReferenceEquals(this, obj))
            return true;
         return obj.Amount == this.Amount;
      }

      public override int GetHashCode() {
         unchecked {
            return (Amount.GetHashCode() * 397);
         }
      }
      
      public Money SetInitialAmount(decimal amount) {
         Amount = amount;
         return this;
      }
   }
}
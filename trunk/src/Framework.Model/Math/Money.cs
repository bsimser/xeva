using System;

namespace XF.Model {
   public class Money {
      private readonly int _cents;

      public Money() { }

      public Money(decimal amount) {
         Amount = XFMath.Round((RoundMethods)Globals.Data["RoundMethod"], amount);
         _cents = Convert.ToInt32(Amount * 100);
      }

      public decimal Amount { get; private set; }
      public int Cents { get { return _cents; } }
      public string Formatted { get { return string.Format("{0:C}", Amount); } }
      public bool IsZero { get { return Amount == 0; } }
      public bool IsNegative { get { return Amount < 0; } }

      public new bool Equals(object obj) {
         if (ReferenceEquals(null, obj)) return false;
         if (!ReferenceEquals(this, obj)) return false;
         return ((Money)obj).Amount == this.Amount;
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

      public Money Add(Money money) {
         if (money == null) return this;
         return new Money().SetInitialAmount(this.Amount + money.Amount);
      }

      public Money Subtract(Money money) {
         if (money == null) return this;
         return new Money().SetInitialAmount(this.Amount - money.Amount);
      }

      public Money Negate() {
         return new Money().SetInitialAmount(Amount * -1);
      }

      public bool IsGreaterThan(Money compare) {
         return Amount > compare.Amount;
      }

      public bool IsGreaterET(Money compare) {
         return Amount >= compare.Amount;
      }

      public bool IsLessThan(Money compare) {
         return Amount < compare.Amount;
      }

      public bool IsLessET(Money compare) {
         return Amount <= compare.Amount;
      }

      public decimal ToDecimal() {
         return Amount;
      }

      public static Money Empty { get {return new Money(0);}}

      public static string ToFormatted(object arg) {
         if (arg == null || arg.GetType() != typeof(Money)) return null;
         return ((Money)arg).Formatted;
      }

      public static Money ToMoney(object arg) {
         if (arg == null || arg.GetType() != typeof(decimal)) return null;
         return new Money().SetInitialAmount((decimal)arg);
      }

      public static object ToDecimal(object arg) {
         if (arg == null || arg.GetType() != typeof(Money)) return default(decimal);
         return ((Money)arg).Amount;
      }

   }
}
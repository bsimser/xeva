using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace XF.Model {
   public class MoneyUserType : IUserType {
      private static readonly NHibernate.SqlTypes.SqlType[] SQL_TYPES = { NHibernateUtil.Decimal.SqlType };
      public SqlType[] SqlTypes { get { return SQL_TYPES; } }
      public Type ReturnedType { get { return typeof(Money); } }
      public bool IsMutable { get { return false; } } 

      public new bool Equals(object x, object y) {
         if (object.ReferenceEquals(x, y)) return true;
         if (x == null || y == null) return false;
         return x.Equals(y);
      }

      public int GetHashCode(object x) {
         throw new System.NotImplementedException();
      }

      public object DeepCopy(object value) {
         return new Money().SetInitialAmount(((Money)value).Amount);
      }

      public object NullSafeGet(IDataReader dr, string[] names, object owner) {
         var obj = NHibernateUtil.Decimal.NullSafeGet(dr, names[0]);
         if (obj == null) return null;
         var valueInUSD = (decimal)obj;
         return new Money().SetInitialAmount(valueInUSD);
      }

      public void NullSafeSet(IDbCommand cmd, object obj, int index) {
         if (obj == null) {
            ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
         }
         else {
            var internalMoney = obj as Money;
            ((IDataParameter)cmd.Parameters[index]).Value = internalMoney.Amount;
         }
      }

      public object Replace(object original, object target, object owner) {
         throw new System.NotImplementedException();
      }

      public object Assemble(object cached, object owner) {
         return DeepCopy(cached);
      }

      public object Disassemble(object value) {
         return DeepCopy(value);
      }


   }
}
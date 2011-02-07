using System;
using System.Collections.Generic;
using System.Text;

namespace XF.Model {
   public class JoinPart {
      private ReferenceJoinType _joinType = ReferenceJoinType.inner;
      public ReferenceJoinType JoinType {
         get { return _joinType; }
         set { _joinType = value; }
      }

      public IExpressionMapper WithCriteria { get; set; }

      public string WithRestriction() {
         if (WithCriteria == null) return string.Empty;

         var withClause = new StringBuilder(Environment.NewLine + "with ");
         WithCriteria.CriteriaList.ForEach(criteria => withClause.Append(criteria));

         return withClause.ToString();
      }
   }
}
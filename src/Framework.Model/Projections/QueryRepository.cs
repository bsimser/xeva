using System.Collections.Generic;
using System.Text;
using NHibernate;
using XF.Model;
using System;

namespace XF.Model {
   public class QueryRepository {
      private readonly IStore _store;
      private static QueryRepository _instance;
      private readonly IDictionary<string, string> _queries = new Dictionary<string, string>();

      public QueryRepository(IStore store) {
         _store = store;
      }

      public static QueryRepository Instance {
         get {
            if (_instance == null)
               _instance = new QueryRepository(UnitOfWork.Store);
            return _instance;
         }
      }

      public IQuery GetQueryFor(Type message, Type entity, IProjector projector, int rows) {
         if (_queries.ContainsKey((message.ToString()))) return _store.CreateQuery(_queries[message.ToString()]);

         var queryBuilder = new StringBuilder();
         BuildSelectClause(queryBuilder, projector.Parameters, projector.References);
         BuildFromClause(entity, queryBuilder, projector.References);
         BuildWhereClause(queryBuilder, projector.Citerion);
         BuildOrderClause(queryBuilder, projector.Ordering);

         var queryText = queryBuilder.ToString();
         _queries.Add(message.ToString(), queryText);

         var query = _store.CreateQuery(queryBuilder.ToString());
         if (rows != 0)
            query.SetMaxResults(rows);

         return query;
      }

      private void BuildSelectClause(StringBuilder queryBuilder, ProjectionPart parameters, List<IReferencePart> references) {
         var selectBldr = new StringBuilder("select ");
         parameters.ForEach(param => selectBldr.Append(param.GetSelectPart()));
         references.ForEach(reference => selectBldr.Append(reference.GetSelectParts()));
         queryBuilder.Append(selectBldr.ToString().TrimEnd(','));
      }

      private void BuildFromClause(Type entityType, StringBuilder queryBuilder, List<IReferencePart> references) {
         queryBuilder.Append(Environment.NewLine);
         queryBuilder.Append(string.Format("from {0} as {1}_0", entityType.Name, entityType.Name.ToLower()));
         references.ForEach(part => queryBuilder.Append(part.GetFromPart()));
      }

      private void BuildWhereClause(StringBuilder queryBuilder, List<IExpressionMapper> expressions) {
         var whereBldr = new StringBuilder(Environment.NewLine);
         whereBldr.Append("where ");
         foreach (var criterion in expressions) {
            if (criterion.CriteriaList.IsEmpty()) continue;
            if (whereBldr.Length > 8) whereBldr.Append(string.Format(" {0} ", criterion.ConjoinWith));
            whereBldr.Append("(");
            foreach (var criteria in criterion.CriteriaList) {
               whereBldr.Append(criteria);
            }
            whereBldr.Append(")");
         }
         if (whereBldr.Length > 10)
            queryBuilder.Append(whereBldr.ToString());
      }

      private void BuildOrderClause(StringBuilder queryBuilder, List<IOrderingMapper> orderingParts) {
         if (orderingParts.IsEmpty()) return;

         var orderBldr = new StringBuilder(Environment.NewLine + "order by");
         orderingParts.ForEach(part => orderBldr.Append(part.OrderClause));
         queryBuilder.Append(orderBldr.ToString().TrimEnd(','));
      }
   }
}
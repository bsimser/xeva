using System.Collections.Generic;
using System.Text;
using NHibernate;
using XF.Model;
using System;

namespace XF.Model
{
   public class QueryRepository
   {
      private readonly IStore _store;
      private static QueryRepository _instance;
      private readonly IDictionary<string, string> _queries = new Dictionary<string, string>();

      public QueryRepository(IStore store)
      {
         _store = store;
      }

      public static QueryRepository Instance
      {
         get
         {
            if(_instance == null)
               _instance = new QueryRepository(UnitOfWork.Store);
            return _instance;
         }
      }

      public IQuery GetQueryFor(Type message, Type entity, ProjectionPart parameters, List<IReferencePart> references, ReferenceExpression expressions)
      {
         if (_queries.ContainsKey((message.ToString()))) return _store.CreateQuery(_queries[message.ToString()]);

         var queryBuilder = new StringBuilder();
         BuildSelectClause(queryBuilder, parameters, references);
         BuildFromClause(entity, queryBuilder, references);
         BuildWhereClause(queryBuilder, expressions, references);

         var queryText = queryBuilder.ToString();
         _queries.Add(message.ToString(), queryText);

         return _store.CreateQuery(queryBuilder.ToString());
      }

      private void BuildSelectClause(StringBuilder queryBuilder, ProjectionPart parameters, List<IReferencePart> references)
      {
         var selectBldr = new StringBuilder("select ");
         parameters.ForEach(param => selectBldr.Append(param.GetSelectPart()));
         references.ForEach(reference => selectBldr.Append(reference.GetSelectParts()));
         queryBuilder.Append(selectBldr.ToString().TrimEnd(','));
      }

      private void BuildFromClause(Type entityType, StringBuilder queryBuilder, List<IReferencePart> references)
      {
         queryBuilder.Append(Environment.NewLine);
         queryBuilder.Append(string.Format("from {0} as {1}", entityType.Name, entityType.Name.ToLower()));
         references.ForEach(part => queryBuilder.Append(part.GetFromPart()));
      }

      private void BuildWhereClause(StringBuilder queryBuilder, ReferenceExpression expressions, List<IReferencePart> references)
      {
         var whereBldr = new StringBuilder(Environment.NewLine);
         whereBldr.Append("where ");
         expressions.ForEach(exp => whereBldr.Append(exp.GetWherePart()));
         references.ForEach(reference => whereBldr.Append(reference.GetWhereParts()));
         queryBuilder.Append(whereBldr.ToString().Trim('a','n','d'));
      }

   }
}
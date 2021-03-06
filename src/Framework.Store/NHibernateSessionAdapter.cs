using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using XF.Model;
using ITransaction = XF.Model.ITransaction;
using NHQuery = NHibernate.IQuery;
using NHSessionFactory = NHibernate.ISessionFactory;
using NHTransaction = NHibernate.ITransaction;
using NHSession = NHibernate.ISession;
using NHUnresolvableObjectException = NHibernate.UnresolvableObjectException;
using NHFlushMode = NHibernate.FlushMode;

namespace XF.Store {
   public class NHibernateSessionAdapter : IStore {
      private NHSession _session;

      public NHibernateSessionAdapter(NHSession session) {
         _session = session;
         _session.FlushMode = NHFlushMode.Commit;
      }

      public void Open() {
         if (!_session.IsOpen)
            throw new StoreClosedException();
      }

      public void Close() {
         if (_session != null)
            if (_session.IsOpen)
               _session.Close();
      }

      public void Flush() {
         Open();
         _session.Flush();
      }

      public void Evict(object entity) {
         Open();
         _session.Evict(entity);
      }

      public void Clear() {
         Open();
         _session.Clear();
      }

      public TEntity Load<TEntity>(Guid id) where TEntity : IEntity {
         Open();

         TEntity result = default(TEntity);

         try {
            result = _session.Load<TEntity>(id);
         }
         catch (NHUnresolvableObjectException ex) {
            EntityNotFoundException newException = new EntityNotFoundException();
            newException.SuppliedEntityID = id;
            throw newException;
         }

         return result;
      }

      public void Delete(object entity) {
         Open();
         _session.Delete(entity);
      }

      public void Save(object entity) {
         Open();
         _session.SaveOrUpdate(entity);
         return;
      }

      public void Refresh(object entity) {
         Open();
         _session.Refresh(entity);
         return;
      }

      public IList<TEntity> Query<TEntity>(INamedQuery query) where TEntity : IEntity {
         Open();

         NHQuery iQuery = _session.GetNamedQuery(query.Name);
         foreach (KeyValuePair<string, object> pair in query.Parameters) {
            Type type = pair.Value.GetType().GetInterface("IList");

            if (type == typeof(IList))
               iQuery.SetParameterList(pair.Key, (IList)pair.Value);
            else
               iQuery.SetParameter(pair.Key, pair.Value);
         }

         IList<TEntity> result = new List<TEntity>(iQuery.List<TEntity>());

         return result;
      }

      public int GetScalar(INamedQuery query) {
         Open();

         int result = 0;
         NHQuery iQuery = _session.GetNamedQuery(query.Name);

         foreach (var pair in query.Parameters) {
            Type type = pair.Value.GetType().GetInterface("IList");
            if (type == typeof(IList))
               iQuery.SetParameterList(pair.Key, (IList)pair.Value);
            else
               iQuery.SetParameter(pair.Key, pair.Value);
         }


         IEnumerator enumerator = iQuery.Enumerable().GetEnumerator();
         if (enumerator.MoveNext()) {
            if (enumerator.Current != null) {
               result = int.Parse(enumerator.Current.ToString());
            }
         }
         return result;
      }

      public INHibernateQueryable<TEntity> Query<TEntity>() where TEntity : IEntity {
         Open();
         return _session.Linq<TEntity>();
      }

      public IQueryable<T> Queryable<T>() {
         Open();
         return _session.Linq<T>();
      }

      public ICriteria CreateCriteria<TEntity>() {
         Open();
         return _session.CreateCriteria(typeof(TEntity));
      }

      public IQuery GetQueryByName(string queryName) {
         Open();
         return _session.GetNamedQuery(queryName);
      }

      public IQuery CreateQuery(string queryText) {
         Open();
         return _session.CreateQuery(queryText);
      }

      public IQuery CreateSqlQuery(string queryText) {
         Open();
         return _session.CreateSQLQuery(queryText);
      }

      public ITransaction CreateTransaction() {
         Open();
         NHTransaction transaction = _session.BeginTransaction();
         ITransaction result = new NHibernateTransactionWrapper(transaction);
         return result;
      }
   }
}
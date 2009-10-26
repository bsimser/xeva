using System;
using System.Linq.Expressions;
using System.Reflection;
using XF.Model;

namespace XF
{
   public class ActionTransactionMap<TEntity, TRepository, TUpdateMessage> 
      where TRepository : IRepository<TEntity> 
      where TEntity : IEntity
   {
      private readonly TRepository _repository;
      private TEntity _entity;
      private TUpdateMessage _updateMessage;

      public ActionTransactionMap(TRepository repository)
      {
         _repository = repository;
      }

      public event EventHandler ActionComplete;
      public event EventHandler ActionCancled;
      public event EventHandler ActionFailed;

      public Guid EntityID { get; private set; }

      public void InitializeAction(TUpdateMessage message)
      {
         _updateMessage = message;
      }

      public ActionTransactionMap<TEntity, TRepository, TUpdateMessage> ForEntity(Expression<Func<TUpdateMessage, object>> entityIDExpression)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityIDExpression) as PropertyInfo;
         if (entityProperty == null) return this;

         EntityID = (Guid)entityProperty.GetValue(_updateMessage, null);
         return this;
      }

      public ActionTransactionMap<TEntity, TRepository, TUpdateMessage> Map(Expression<Func<TEntity, object>> entityExpression,
                                                                            Expression<Func<TUpdateMessage, object>> updateExpression)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateExpression) as PropertyInfo;

         if (entityProperty == null) return this;
         if (updateExpression == null) return this;

         return this;
      }

      public ActionTransactionMap<TEntity, TRepository, TUpdateMessage> Map(Func<TEntity, object> entityExpression)
      {
         return this;
      }

      public IActionResults Execute()
      {
         _entity = _repository.FindBy(EntityID);

         return default(IActionResults);
      }

   }

   public interface IActionResults
   {
   }

}
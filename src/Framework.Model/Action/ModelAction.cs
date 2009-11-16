using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF.Model;

namespace XF
{
   public class ModelAction<TEntity, TRepository, TUpdateMessage> 
      where TRepository : IRepository<TEntity> 
      where TEntity : IEntity
   {
      private PropertyInfo _entityIDProperty;
      private TEntity _entity;
      private readonly List<ModelActionParameter> _actionParameters = new List<ModelActionParameter>();
      private PropertyInfo _childObjectProperty;
      private object _childObject;

      public ModelAction(TRepository repository)
      {
         Repository = repository;
      }

      protected Guid EntityID { get; private set; }
      protected TRepository Repository { get; private set; }
      protected TUpdateMessage UpdateMessage { get; private set; }
      protected IUserAccount UserAccount { get; set; }
      protected DateTime UpdatedOn { get; set; }

      protected TEntity Root
      {
         get
         {
            if (Equals(_entity, default(TEntity)))
               return Activator.CreateInstance<TEntity>();

            return _entity;
         }
         set { _entity = value; }
      }

      public void InitializeAction(TUpdateMessage message)
      {
         UpdateMessage = message;
         EntityID = (Guid)_entityIDProperty.GetValue(UpdateMessage, null);
      }

      public void InitializeAction(TUpdateMessage message, IUserAccount user, DateTime updatedOn)
      {
         UpdateMessage = message;
         EntityID = (Guid)_entityIDProperty.GetValue(UpdateMessage, null);

         UserAccount = user;
         UpdatedOn = updatedOn;
      }

      public ModelAction<TEntity, TRepository, TUpdateMessage> WithRoot(Expression<Func<TUpdateMessage, Guid>> entityIDExpression)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityIDExpression) as PropertyInfo;
         if (entityProperty == null) return this;

         _entityIDProperty = entityProperty;

         return this;
      }

      public ModelAction<TEntity, TRepository, TUpdateMessage> ForChild(Expression<Func<TEntity, object>> entityExpression)
      {
         var updateObject = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (updateObject == null) return this;
         _childObjectProperty = updateObject;

         return this;
      }

      public ModelAction<TEntity, TRepository, TUpdateMessage> Map(Expression<Func<TEntity, object>> entityExpression, 
                                                                   object targetValue)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (entityProperty == null) return this;

         _actionParameters.Add(new ModelActionParameter { EntityProperty = entityProperty, EntityPropertyValue = targetValue });
         return this;
      }

      public ModelAction<TEntity, TRepository, TUpdateMessage> Map(Expression<Func<TEntity, object>> entityExpression,
                                                                   Expression<Func<TUpdateMessage, object>> updateExpression)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateExpression) as PropertyInfo;

         if (entityProperty == null) return this;
         if (updateProperty == null) return this;

         _actionParameters.Add(new ModelActionParameter{EntityProperty=entityProperty, UpdateProperty=updateProperty});
         return this;
      }

      public ModelAction<TEntity, TRepository, TUpdateMessage> Map<TChild>(Func<TChild, bool> target,
                                                                           object targetValue)
      {
         _actionParameters.Add(new ModelActionParameter {EntityMethod = target.Method, EntityPropertyValue = targetValue});
         return this;
      }

      public void SetMethodTargetArgument<TChild>(Func<TChild, bool> target, TChild arg)
      {
         _actionParameters.ForEach(param => { if (param.EntityMethod.Name == target.Method.Name)
                                                     param.EntityPropertyValue = arg; } );
      }

      public void SetPropertyTargetArgument(Expression<Func<TEntity, object>> entityExpression, object arg)
      {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         if (entityProperty == null) return;

         _actionParameters.ForEach(param =>
         {
            if (param.EntityProperty.Name == entityProperty.Name)
               param.EntityPropertyValue = arg;
         });
      }

      public virtual IActionResults Execute()
      {
         if(EntityID == Guid.Empty) return default(IActionResults);

         Root = Repository.FindBy(EntityID);

         if (_childObjectProperty != null)
         {
            _childObject = _childObjectProperty.GetValue(Root, null);
            UpdateEntityFromParameters(_childObject);
         }
         else
         {
            UpdateEntityFromParameters(Root);
         }

         var results = CommitChangesToRoot();

         return results;
      }

      protected void UpdateEntityFromParameters(object updateEntity)
      {
         foreach (var actionParameter in _actionParameters)
         {
            if (actionParameter.EntityProperty != null &&
                actionParameter.UpdateProperty != null)
               actionParameter.EntityProperty.SetValue(updateEntity, actionParameter.UpdateProperty.GetValue(UpdateMessage, null), null);

            if (actionParameter.EntityProperty != null &&
                actionParameter.EntityPropertyValue != null)
               actionParameter.EntityProperty.SetValue(updateEntity, actionParameter.EntityPropertyValue, null);

            if (actionParameter.EntityMethod != null)
               actionParameter.EntityMethod.Invoke(updateEntity, new[] { actionParameter.EntityPropertyValue });
         }
      }

      protected IActionResults CommitChangesToRoot()
      {
         var results = new ModelActionResults();
         try
         {
            using(UnitOfWork.Transact())
            {
               Repository.Save(Root);

               UnitOfWork.Commit();
            }

            results.ErrorCode = "Success";
            results.Message =
               string.Format("Entity: {0} - ID={1} Was updated successfully.", typeof (TEntity).Name,
                             EntityID);
         }
         catch (Exception exp)
         {
            results.ErrorCode = "Failure";
            results.Message =
               string.Format("Entity: {0} - ID={1} Update Failed.", typeof (TEntity).Name,
                             EntityID);
            results.ErrorContent = exp.Message;
         }
         finally
         {
            //this is where we would record the Action Transaction.
         }

         return results;
      }
   }
}
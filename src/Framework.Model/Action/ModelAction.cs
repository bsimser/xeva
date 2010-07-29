using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using XF.Model;

namespace XF {
   public class ModelAction<TEntity, TRepository, TUpdateMessage>
      where TRepository : IRepository<TEntity>
      where TEntity : IEntity {
      private PropertyInfo _entityIDProperty;
      private TEntity _entity;
      private readonly List<ModelActionParameter> _actionParameters = new List<ModelActionParameter>();
      private PropertyInfo _childObjectProperty;
      private object _childObject;
      private MethodInfo _entityUpdateMethod;
      private MethodInfo _childUpdateMethod;

      public ModelAction(TRepository repository) {
         Repository = repository;
      }

      protected Guid EntityID { get; private set; }
      protected TRepository Repository { get; private set; }
      protected TUpdateMessage UpdateMessage { get; private set; }
      protected IUserAccount UserAccount { get; set; }
      protected DateTime UpdatedOn { get; set; }

      protected TEntity Root {
         get {
            if (Equals(_entity, default(TEntity)))
               return Activator.CreateInstance<TEntity>();

            return _entity;
         }
         set { _entity = value; }
      }

      /// <summary>
      /// Preparing the Action with inputs
      /// </summary>
      /// <param name="message">The message providing updated state</param>
      public void InitializeAction(TUpdateMessage message) {
         UpdateMessage = message;
         EntityID = (Guid)_entityIDProperty.GetValue(UpdateMessage, null);
      }

      /// <summary>
      /// Preparing the Action with inputs
      /// </summary>
      /// <param name="message">The message providing updated state</param>
      /// <param name="user">The user account performing the update</param>
      /// <param name="updatedOn">Date and time update occured</param>
      public void InitializeAction(TUpdateMessage message, IUserAccount user, DateTime updatedOn) {
         UpdateMessage = message;
         EntityID = (Guid)_entityIDProperty.GetValue(UpdateMessage, null);

         UserAccount = user;
         UpdatedOn = updatedOn;
      }

      /// <summary>
      /// Provides the ID value of the Root object
      /// </summary>
      /// <param name="entityIDExpression">Lamda specifing the ID property</param>
      /// <returns>ModelAction</returns>
      public ModelAction<TEntity, TRepository, TUpdateMessage> WithRoot(Expression<Func<TUpdateMessage, Guid>> entityIDExpression) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityIDExpression) as PropertyInfo;
         if (entityProperty == null) return this;

         _entityIDProperty = entityProperty;

         return this;
      }

      /// <summary>
      /// Signifies that the action is against a child of the aggregate root.
      /// </summary>
      /// <param name="entityExpression">Lamda specifing the path of the child</param>
      /// <returns></returns>
      public ModelAction<TEntity, TRepository, TUpdateMessage> ForChild(Expression<Func<TEntity, object>> entityExpression) {
         var updateObject = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (updateObject == null) return this;
         _childObjectProperty = updateObject;

         return this;
      }

      /// <summary>
      /// Maps a property on the model to a static value.
      /// </summary>
      /// <param name="entityExpression">Lamda specifing the updated property</param>
      /// <param name="targetValue">The static value</param>
      /// <returns></returns>
      public ModelAction<TEntity, TRepository, TUpdateMessage> Map(Expression<Func<TEntity, object>> entityExpression,
                                                                   object targetValue) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;

         if (entityProperty == null) return this;

         _actionParameters.Add(new ModelActionParameter { EntityProperty = entityProperty, EntityPropertyValue = targetValue });
         return this;
      }

      /// <summary>
      /// Maps a property on the model to a property on the update message.
      /// </summary>
      /// <param name="entityExpression">Lamda specifing the updated propert</param>
      /// <param name="updateExpression">Lamda specifing the message propert</param>
      /// <returns></returns>
      public ModelAction<TEntity, TRepository, TUpdateMessage> Map(Expression<Func<TEntity, object>> entityExpression,
                                                                   Expression<Func<TUpdateMessage, object>> updateExpression) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         var updateProperty = ExpressionsHelper.GetMemberInfo(updateExpression) as PropertyInfo;

         if (entityProperty == null) return this;
         if (updateProperty == null) return this;

         _actionParameters.Add(new ModelActionParameter { EntityProperty = entityProperty, UpdateProperty = updateProperty });
         return this;
      }

      /// <summary>
      /// Maps a specified type to a method or property on the aggregate root.
      /// </summary>
      /// <typeparam name="TChild">The type accepted by the aggregate root method</typeparam>
      /// <param name="target">The method or property on the aggregate root</param>
      /// <param name="targetValue">generally null; value is set during execution</param>
      /// <returns></returns>
      public ModelAction<TEntity, TRepository, TUpdateMessage> Map<TChild>(Func<TChild, IXFResults> target,
                                                                           object targetValue) {
         _actionParameters.Add(new ModelActionParameter { EntityMethod = target.Method, EntityPropertyValue = targetValue });
         return this;
      }

      /// <summary>
      /// Used to set the value of a mapped type passed to a method
      /// </summary>
      /// <typeparam name="TChild">The type mapped</typeparam>
      /// <param name="target">The method on the aggregate root</param>
      /// <param name="arg">The value of the type</param>
      public void SetMethodTargetArgument<TChild>(Func<TChild, IXFResults> target, TChild arg) {
         var param = _actionParameters.Find(match => match.EntityMethod.Name == target.Method.Name);
         if (param == null) return;

         param.EntityPropertyValue = arg;
      }

      /// <summary>
      /// Used to set the value of a mapped type passed to a property
      /// </summary>
      /// <param name="entityExpression">The property on the aggregate root</param>
      /// <param name="arg">The value of the type</param>
      public void SetPropertyTargetArgument(Expression<Func<TEntity, object>> entityExpression, object arg) {
         var entityProperty = ExpressionsHelper.GetMemberInfo(entityExpression) as PropertyInfo;
         if (entityProperty == null) return;

         var param = _actionParameters.Find(match => match.EntityProperty.Name == entityProperty.Name);
         if (param != null) param.EntityPropertyValue = arg;
      }

      /// <summary>
      /// A method to be called after new state has been mapped.
      /// </summary>
      /// <param name="target"></param>
      public void EntityUpdateMethod(Func<IXFResults> target) {
         _entityUpdateMethod = target.Method;
      }

      /// <summary>
      /// A method to be called after new state has been mapped.
      /// </summary>
      /// <param name="target"></param>
      public void ChildUpdateMethod(Func<IXFResults> target) {
         _entityUpdateMethod = target.Method;
      }

      /// <summary>
      /// The Update method will located the aggregate root and update via mapped elements.
      /// </summary>
      /// <returns>IXFResults</returns>
      public virtual IXFResults Update() {
         if (EntityID == Guid.Empty) return ModelActionResults.InvalidEntityID;

         object updateObj;

         try {
            //Note: must lock the entity 
            Root = Repository.FindBy(EntityID);
            updateObj = _childObjectProperty == null ? Root : _childObjectProperty.GetValue(Root, null);
         }
         catch (Exception) {
            return new ModelActionResults(false,
                                          string.Format("Unable to locate updatable object for {0} id: {1}",
                                                        typeof(TEntity).Name, EntityID));
         }

         IXFResults results;
         try {
            results = UpdateEntity(updateObj);
         }
         catch (ActionFailueException ex) {
            return new ModelActionResults(false, ex.Message);
         }

         return results;
      }

      /// <summary>
      /// The Insert method will generate a new aggergate root object.
      /// </summary>
      /// <returns>IXFResults</returns>
      public virtual IXFResults Insert() {

         Root = Activator.CreateInstance<TEntity>();

         IXFResults results;
         try {
            results = UpdateEntity(Root);
         }
         catch (ActionFailueException ex) {
            return new ModelActionResults(false, ex.Message);
         }

         return results;
      }

      protected IXFResults UpdateEntity(object updateEntity) {
         foreach (var actionParameter in _actionParameters) {
            if (actionParameter.EntityProperty != null &&
                actionParameter.UpdateProperty != null)
               actionParameter.EntityProperty.SetValue(updateEntity, actionParameter.UpdateProperty.GetValue(UpdateMessage, null), null);

            if (actionParameter.EntityProperty != null &&
                actionParameter.EntityPropertyValue != null)
               actionParameter.EntityProperty.SetValue(updateEntity, actionParameter.EntityPropertyValue, null);

            if (actionParameter.EntityMethod != null) {
               var results = actionParameter.EntityMethod.Invoke(updateEntity, new[] { actionParameter.EntityPropertyValue }) as IXFResults;
               if (results.ResultCode != XFResultCode.Success)
                  throw new ActionFailueException(results.Message);
            }
         }

         if (_entityUpdateMethod != null) {
            var results = _entityUpdateMethod.Invoke(Root, null) as IXFResults;
            if (results.ResultCode != XFResultCode.Success)
               throw new ActionFailueException(results.Message);
         }

         if (_childUpdateMethod != null &&
             updateEntity.GetType() != Root.GetType()) {
            var results = _entityUpdateMethod.Invoke(updateEntity, null) as IXFResults;
            if (results.ResultCode != XFResultCode.Success)
               throw new ActionFailueException(results.Message);
         }

         return CommitChangesToRoot();
      }

      protected IXFResults CommitChangesToRoot() {
         var results = new ModelActionResults();
         try {
            using (UnitOfWork.Transact()) {
               //Note: should this be an Update?
               Repository.Save(Root);

               UnitOfWork.Commit();
            }

            results.ResultCode = XFResultCode.Success;
            results.Message =
               string.Format("Entity: {0} - ID={1} Was updated successfully.", typeof(TEntity).Name,
                             EntityID);
         }
         catch (Exception exp) {
            results.ResultCode = XFResultCode.Failure;
            results.Message =
               string.Format("Entity: {0} - ID={1} Update Failed.", typeof(TEntity).Name,
                             EntityID);
            results.ErrorContent = exp.Message;
         }
         finally {
            //this is where we would record the Action Transaction.
         }

         return results;
      }

   }
}
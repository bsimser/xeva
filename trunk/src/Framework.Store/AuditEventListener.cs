using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using XF.Model;
using XF.Services;

namespace XF.Store {
   public class AuditEventListener : IPreUpdateEventListener, IPreInsertEventListener {
      private IDictionary<string, PropertyInfo> _auditProperties;

      public bool OnPreUpdate(PreUpdateEvent @event) {
         GetAuditProperties(@event.Entity);
         if (_auditProperties.IsEmpty()) return false;
         try {
            var sessionID = new Guid(ConfigurationManager.AppSettings["SessionID"]);
            var sessionSerice = Locator.Resolve<ISessionService>();
            var user = sessionSerice.GetUserAccount(sessionID);
            SetAuditProperties(@event, @event.Persister, @event.State, user, DateTime.Now);

            return false;
         }
         catch (Exception) {
            return false;
         }
      }

      public bool OnPreInsert(PreInsertEvent @event) {
         GetAuditProperties(@event.Entity);
         if (_auditProperties.IsEmpty()) return false;
         try {
            var sessionID = new Guid(ConfigurationManager.AppSettings["SessionID"]);
            var sessionSerice = Locator.Resolve<ISessionService>();
            var user = sessionSerice.GetUserAccount(sessionID);
            SetAuditProperties(@event, @event.Persister, @event.State, user, DateTime.Now);

            return false;
         }
         catch (Exception) {
            return false;
         }
      }

      private void GetAuditProperties(object entity) {
         _auditProperties = new Dictionary<string, PropertyInfo>();
         var entityList = new List<PropertyInfo>(entity.GetType().GetProperties());
         entityList.ForEach(prop => {
            if (prop.Name.Equals(ModelConstants.CREATEDBY) ||
                prop.Name.Equals(ModelConstants.CREATEDON) ||
                prop.Name.Equals(ModelConstants.UPDATEDBY) ||
                prop.Name.Equals(ModelConstants.UPDATEDON)) {
               _auditProperties.Add(prop.Name, prop);
            }
         });
      }

      private void SetAuditProperties(AbstractPreDatabaseOperationEvent @event,  IEntityPersister persister, 
                                      object[] state, IUserAccount auditUser, DateTime auditDate) {
         foreach (var propertyPair in _auditProperties) {
            if (propertyPair.Key.Contains(ModelConstants.UPDATEDBY)) {
               propertyPair.Value.SetValue(@event.Entity, auditUser, null);
               Set(persister, state, propertyPair.Key, auditUser);
            }
            if (propertyPair.Key.Contains(ModelConstants.UPDATEDON)) {
               propertyPair.Value.SetValue(@event.Entity, auditDate, null);
               Set(persister, state, propertyPair.Key, auditDate);
            }
            if (propertyPair.Key.Contains(ModelConstants.CREATEDBY)) {
               var currentValue = (IUserAccount)propertyPair.Value.GetValue(@event.Entity, null);
               if(currentValue == null) {
                  propertyPair.Value.SetValue(@event.Entity, auditUser, null);
                  Set(persister, state, propertyPair.Key, auditUser);
               }
               else {
                  propertyPair.Value.SetValue(@event.Entity, currentValue, null);
                  Set(persister, state, propertyPair.Key, currentValue);
               }
            }
            if (propertyPair.Key.Contains(ModelConstants.CREATEDON)) {
               var currentValue = (DateTime?)propertyPair.Value.GetValue(@event.Entity, null);
               if(currentValue == null ||
                  !((DateTime)currentValue).IsBetween(DateTime.Parse("1/1/1900"), DateTime.Parse("1/1/3000"))) {
                  propertyPair.Value.SetValue(@event.Entity, auditDate, null);
                  Set(persister, state, propertyPair.Key, auditDate);
               }
               else {
                  propertyPair.Value.SetValue(@event.Entity, currentValue, null);
                  Set(persister, state, propertyPair.Key, currentValue);
               }
            }
         }
      }

      private void Set(IEntityPersister persister, object[] state, string propertyName, object value) {
         var index = Array.IndexOf(persister.PropertyNames, propertyName);
         if (index == -1)
            return;
         state[index] = value;
      }
   }
}
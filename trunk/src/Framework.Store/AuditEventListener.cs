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
            if (propertyPair.Key.Contains("By")) {
               propertyPair.Value.SetValue(@event.Entity, auditUser, null);
               Set(persister, state, propertyPair.Key, auditUser);
            }
            if (propertyPair.Key.Contains("On")) {
               propertyPair.Value.SetValue(@event.Entity, auditDate, null);
               Set(persister, state, propertyPair.Key, auditDate);
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
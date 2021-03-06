using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
// ayende credit

namespace XF {
   public static class Locator {

      private const string CONTAINER_KEY = "XF.Shared:CONTAINER_KEY";

      public static void Initialize(IWindsorContainer windsorContainer) {
         Container = windsorContainer;
      }

      public static bool HasComponent(string key) {
         GuardContainerInitialized();
         return Container.Kernel.HasComponent(key);
      }

      public static T Resolve<T>() {
         GuardContainerInitialized();
         return Container.Resolve<T>();
      }

      public static T Resolve<T>(string name) {
         GuardContainerInitialized();
         return Container.Resolve<T>(name);
      }

      public static object Resolve(Type service) {
         GuardContainerInitialized();
         return Container.Resolve(service);
      }

      public static object Resolve(string component) {
         GuardContainerInitialized();
         return Container.Resolve(component);
      }

      public static void Release(object toRelease) {
         try {
            Container.Release(toRelease);
         }
         catch (Exception) {}
      }

      public static void Register(IRegistration[] registrations) {
         Container.Register(registrations);
      }

      public static void AddComponent(string componentKey, Type componentType) {
         try {
            Container.AddComponent(componentKey, componentType);
         }
         catch (ComponentRegistrationException) {
         }
      }

      public static void Reset() {
         if (Container == null) return;
         Container.Dispose();
         Container = null;
      }

      public static bool Initialized {
         get { return Container != null; }
      }

      private static IWindsorContainer Container {
         get {
            var result = Globals.Data[CONTAINER_KEY] as IWindsorContainer;
            return result;
         }
         set {
            Globals.Data[CONTAINER_KEY] = value;
         }
      }

      private static void GuardContainerInitialized() {
         if (!Initialized) throw new InvalidOperationException("Container is not initialized.");
      }
   }
}

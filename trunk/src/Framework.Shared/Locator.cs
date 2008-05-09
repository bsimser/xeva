using System;
using Castle.MicroKernel;
using Castle.Windsor;
// ayende credit

namespace XF
{
   public static class Locator
   {

      private const string CONTAINER_KEY = "XF.Shared:CONTAINER_KEY";

      public static void Initialize(IWindsorContainer windsorContainer)
      {
         Container = windsorContainer;
      }

      public static T Resolve<T>()
      {
         return Container.Resolve<T>();
      }

      public static T Resolve<T>(string name)
      {
         return Container.Resolve<T>(name);
      }

      // This method is used to return objects in the RemoteProxy.
      // Types are unknown at the time the objects are created.
      public static object Resolve(string component)
      {
         return Container.Resolve(component);
      }

      private static IWindsorContainer Container
      {
         get
         {
            var result = Globals.Data[CONTAINER_KEY] as IWindsorContainer;
            return result;
         }
         set
         {
            Globals.Data[CONTAINER_KEY] = value;
         }
      }

      public static bool Initialized
      {
         get { return Container != null; }
      }

      public static void Reset()
      {
         if (Container == null) return;
         Container.Dispose();
         Container = null;

      }

      public static void AddComponent(string componentKey, Type componentType)
      {
         try
         {
            Container.AddComponent(componentKey, componentType);
         }
         catch (ComponentRegistrationException)
         {
         }
      }
   }
}

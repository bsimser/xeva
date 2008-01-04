using System;
using Castle.MicroKernel;
using Castle.Windsor;
// ayende credit

namespace XEVA.Framework
{
   public static class IoC
   {
      private static IWindsorContainer container;
      private static object LocalContainerKey = new object();

      public static void Initialize(IWindsorContainer windsorContainer)
      {
         GlobalContainer = windsorContainer;
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

      public static IWindsorContainer Container
      {
         get
         {
            IWindsorContainer result = GlobalContainer;
            if (result == null)
               throw new InvalidOperationException("The container has not been initialized!");
            return result;
         }
      }

      public static bool Initialized
      {
         get { return GlobalContainer != null; }
      }

      public static void Reset()
      {
         IWindsorContainer windsorContainer = GlobalContainer;
         if (windsorContainer != null)
         {
            windsorContainer.Dispose();
            windsorContainer = null;
         }
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

      internal static IWindsorContainer GlobalContainer
      {
         get
         {
            return container;
         }
         set
         {
            container = value;
         }
      }
   }
}

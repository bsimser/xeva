using System;
using Castle.MicroKernel;
using Castle.Windsor;
// ayende credit

namespace XF
{
   public static class Locator
   {

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
            var result = GlobalContainer;
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
         if (GlobalContainer == null) return;
         GlobalContainer.Dispose();
         GlobalContainer = null;
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

      internal static IWindsorContainer GlobalContainer { get; set; }
   }
}
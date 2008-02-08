using Castle.DynamicProxy;

namespace XF.Services
{
   public static class ProxyGeneratorFactory
   {
      private static ProxyGenerator _instance;

      public static ProxyGenerator Instance()
      {
         if (_instance == null)
            _instance = new ProxyGenerator();

         return _instance;
      }
   }
}
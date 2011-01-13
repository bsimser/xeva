using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;

namespace XF.Services {
   public class XFProxyComponentActivator  : DefaultComponentActivator 
   {
      private readonly ComponentModel _model;

      public XFProxyComponentActivator(ComponentModel model, IKernel kernel, ComponentInstanceDelegate onCreation,
                                     ComponentInstanceDelegate onDestruction)
         : base(model, kernel, onCreation, onDestruction) 
      {
         _model = model;
      }

      protected override object CreateInstance(CreationContext context, object[] arguments, Type[] signature) 
      {
         var channel = Locator.Resolve<IMessageChannel>();
         channel.InitializeChannel(_model.Implementation.Name, _model.Implementation);
         return channel.GetChannelInterface();
      }
   }
}
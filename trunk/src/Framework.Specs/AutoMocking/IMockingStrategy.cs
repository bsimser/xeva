using System;
using Castle.MicroKernel;

namespace XEVA.Framework.Specs.AutoMocking
{
    public interface IMockingStrategy
    {
        object Create(CreationContext context, Type type);
    }
}
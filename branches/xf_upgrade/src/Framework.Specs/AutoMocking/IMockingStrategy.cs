using System;
using Castle.MicroKernel;

namespace XF.Specs.AutoMocking
{
    public interface IMockingStrategy
    {
        object Create(CreationContext context, Type type);
    }
}
using System;
using Castle.MicroKernel;

namespace XEVA.Framework.Specs.AutoMocking
{
    public class NonMockedStrategy : AbstractMockingStrategy
    {
        #region NonMockedStrategy()

        public NonMockedStrategy(IAutoMockingRepository autoMock) : base(autoMock)
        {
        }

        #endregion

        public override object Create(CreationContext context, Type type)
        {
            return AutoMock.Kernel[type];
        }
    }
}
using System;
using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Specs.AutoMocking;

namespace XEVA.Framework.Specs
{
   [TestFixture]
   public class Spec
   {
      private MockRepository _mocks;
      private AutoMockingContainer _autoMockingContainer;

      [SetUp]
      public void MainSetup()
      {
         _mocks = new MockRepository();
         _autoMockingContainer = new AutoMockingContainer(_mocks);
         _autoMockingContainer.Initialize();

         Before_each_spec();
      }

      [TearDown]
      protected void MainTeardown()
      {
         After_each_spec();
      }

      public MockRepository Mocks
      {
         get { return _mocks; }
      }

      protected virtual void Before_each_spec()
      {

      }

      protected virtual void After_each_spec()
      {
         
      }

      protected IDisposable Record
      {
         get { return _mocks.Record(); }
      }

      protected IDisposable Playback
      {
         get { return _mocks.Playback(); }
      }

      protected TType Mock<TType>()
      {
         return _mocks.DynamicMock<TType>();
      }

      protected TType Mock<TType>(object[] prams)
      {
         return _mocks.DynamicMock<TType>(prams);
      }

      protected TType Partial<TType>() 
         where TType : class
      {
         return _mocks.PartialMock<TType>();
      }

      protected TType Get<TType>()
         where TType : class
      {
         return _autoMockingContainer.Get<TType>();
      }

      protected TType Create<TType>()
         where TType : class
      {
         return _autoMockingContainer.Create<TType>();
      }

      protected TType Stub<TType>()
      {
         return _mocks.Stub<TType>();
      }

      protected void Verify(object mock)
      {
         _mocks.Verify(mock);
      }

      protected void VerifyAll()
      {
         _mocks.VerifyAll();
      }

      protected void Spec_not_implemented()
      {
         Console.WriteLine("Not implemented");
      }
   }
}
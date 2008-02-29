using System;
using Castle.Windsor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF;
using XF.Services;
using XF.Specs;

namespace Specs_for_CredentialsProvider
{
   [TestFixture]
   public class When_authenticating : Spec
   {
      private CredentialsProvider<FakeAuthentication> _theUnit;
      private IWindsorContainer _mockContainer;

      protected override void Before_each_spec()
      {
         _theUnit = new CredentialsProvider<FakeAuthentication>();
         _mockContainer = Mock<IWindsorContainer>();
         IoC.Initialize(_mockContainer);

         _theUnit.IsAuthenticating = true;
         _theUnit.SessionTicket = Guid.NewGuid();
      }

      [Test]
      public void Return_a_valid_id_for_the_user_credentials()
      {
         using (Record)
         {
            Expect
               .Call(_mockContainer.Resolve<FakeAuthentication>())
               .Return(new FakeAuthentication());
         }

         using (Playback)
         {
            Guid userID = _theUnit.Authenticate(new object[0]);

            Guid ticket = _theUnit.SessionTicket;
            bool authenticating = _theUnit.IsAuthenticating;

            Assert.That(userID, Is.Not.EqualTo(Guid.Empty));
         }
      }
   }
}
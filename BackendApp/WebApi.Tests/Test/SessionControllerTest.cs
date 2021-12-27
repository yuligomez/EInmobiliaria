using System;
using SessionInterface;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Tests.Test

{
    [TestClass]
    public class SessionControllerTest
    {
        private Mock<ISessionLogic> mockSessionLogic;
        private SessionController sessionController ;
        private UserModel userModel;

        [TestInitialize]
        public void InitVariables()
        {
            userModel = new UserModel()
            {
                Email = "yuli@gmail.com",
                Password ="uruguaynomas",
                Name ="Yuliana",
                Role ="admin"
            };
            mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            sessionController = new SessionController(mockSessionLogic.Object);
        }
        [TestMethod]
        public void TestLoginOk()
        {
            SessionModel sessionModel = new SessionModel {Email = "yuli@gmail.com", Password="uruguaynomas"};
            User user = sessionModel.ToEntity();
            Guid newToken = Guid.NewGuid();
            mockSessionLogic.Setup(m => m.Login(user)).Returns(newToken);
            SessionBasicModel sessionModel2 = new SessionBasicModel(newToken);

            var result = sessionController.Post(sessionModel);

            mockSessionLogic.VerifyAll();
            var okResult = result as OkObjectResult;
            var session = okResult.Value as SessionBasicModel;
            Assert.AreEqual(session, sessionModel2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLogInBadRequestArgumentException()
        {
            SessionModel sessionModel = new SessionModel { Email = "yuli@gmail.com",Password ="uruguaynomas"};
            User user = sessionModel.ToEntity();
            mockSessionLogic.Setup(p => p.Login(user)).Throws(new ArgumentException());

            var result = sessionController.Post(sessionModel);

            mockSessionLogic.VerifyAll();
        }
    }
}
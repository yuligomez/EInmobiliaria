using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Logics;
using DataAccessInterface.Repositories;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace BusinessLogic.Tests
{
    [TestClass]
    public class SessionLogicTest
    {
        private SessionLogic sessionUserLogic;
        private Mock<ISessionUserRepository> mockSessionUserRepository;
        private Mock<IUserRepository> mockUserRepository;
        private List<SessionUser> sessions;
        private List<SessionUser> sessionUserEmpty;
        private List<User> userResult;

        [TestInitialize]
        public void Initialize()
        {
            sessions = new List<SessionUser>()
            {
                new SessionUser()
                {
                    Id = 1,
                    Token = Guid.NewGuid(),
                    UserId   = 1,
                },
                new SessionUser()
                {
                    Id = 2,
                    Token = Guid.NewGuid(),
                    UserId   = 2,
                },
            };
            mockSessionUserRepository = new Mock<ISessionUserRepository>(MockBehavior.Strict);
            mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            // mock3 = new Mock<SessionUser>(MockBehavior.Strict);
            sessionUserLogic = new SessionLogic(mockSessionUserRepository.Object, mockUserRepository.Object);
            sessionUserEmpty = new List<SessionUser>();
        }

        [TestMethod]
        public void TestIsCorrectionToken()
        {
            Guid correctToken = sessions.First().Token;
            mockSessionUserRepository.Setup(m => m.IsCorrectToken(correctToken)).Returns(true);
            bool result = sessionUserLogic.IsCorrectToken(correctToken);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotValidToken()
        {
            Guid notExistToken = Guid.NewGuid();
            mockSessionUserRepository.Setup(m => m.IsCorrectToken(notExistToken)).Returns(false);
            bool result = sessionUserLogic.IsCorrectToken(notExistToken);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestLoginNotExistSessionPreviously()
        {
            List<User> userResult = new List<User>()
            {   new User()
                {
                    Id = 5,
                    Email= "yulianagomez@gmail.com",
                    Password = "elbolsonomas",
                    Role = "Admin"
                }
            };
            SessionUser sessionToReturn = new SessionUser()
            {
                Id = 1,
                Token = Guid.NewGuid(),
                UserId = 5
            };
            mockUserRepository.Setup(m => m.GetElements()).Returns(userResult);
            List<SessionUser> sessions = new List<SessionUser>();
            mockSessionUserRepository.Setup(mock => mock.GetElements()).Returns(sessions);
            mockSessionUserRepository.Setup(mock => mock.Add(It.IsAny<SessionUser>())).Returns(sessionToReturn);
            mockSessionUserRepository.Setup(mock => mock.Find(sessionToReturn.Id)).Returns(sessionToReturn);
            mockSessionUserRepository.Setup(mock => mock.Update(sessionToReturn.Id, sessionToReturn));

            Guid guidToReturn = sessionUserLogic.Login(userResult.First());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLoginNotExistser()
        {
            List<User> userResult = new List<User>();
            User user = new User();
            mockUserRepository.Setup(m => m.GetElements()).Returns(userResult);

            Guid guidToReturn = sessionUserLogic.Login(user);

            mockSessionUserRepository.VerifyAll();
        }        
        [TestMethod]
        public void TestLoginExistPreviouslySession ()
        {
            List<User> userResult = new List<User> ()
            {   new User()
                {
                    Id = 1,
                    Email= "soyyuliana@gmail.com",
                    Password = "soyyo"
                }
            };
            List<SessionUser> session2 = new List<SessionUser>()
            {
                new SessionUser()
                {
                    Id = 1,
                    Token = Guid.NewGuid(),
                    UserId   = 1,
                },
            };
            Guid newGuid = Guid.NewGuid();
            mockUserRepository.Setup(m=>m.GetElements()).Returns(userResult);
            mockSessionUserRepository.Setup(mock=>mock.GetElements()).Returns(session2);
            mockSessionUserRepository.Setup(mock=>mock.Find(session2.First().Id)).Returns(session2.First());
            mockSessionUserRepository.Setup(mock=>mock.Update(session2.First().Id,session2.First()));

            Guid tokenAdded = sessionUserLogic.Login(userResult.First()); 
        }
    }
}
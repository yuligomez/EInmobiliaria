using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Tests
{
    [TestClass]
    public class UserLogicTest
    {
        private List<User> usersToReturn;
        private UserLogic userLogic;
        private Mock<IUserRepository> userMock;
        private List<User> emptyUsers;

        [TestInitialize]
        public void InitVariables()
        {
            usersToReturn = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Email = "lorena@gmail.com",
                    Password = "123",
                    Name = "Lorena",
                    Role ="Admin"
                },
                new User()
                {
                    Id = 2,
                    Email = "gonzalo@gmail.com",
                    Password = "123",
                    Name = "Gonzalo",
                    Role ="Admin"
                },
                new User()
                {
                    Id = 3,
                    Email = "jimena@gmail.com",
                    Password = "123",
                    Name = "Jimena",
                    Role ="Chequeador"
                },
                new User()
                {
                    Id = 4,
                    Email = "andres@gmail.com",
                    Password = "123",
                    Name = "Andres",
                    Role ="Chequeador"
                }
            };
            emptyUsers = new List<User>();

            userMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userLogic = new UserLogic(userMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            userMock.Setup(m => m.GetElements()).Returns(usersToReturn);

            var result = userLogic.GetAll();

            Assert.IsTrue(result.SequenceEqual(usersToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.Find(user.Id)).Returns(user);

            var result = userLogic.GetById(user.Id);

            Assert.AreEqual(result, user);
        }

        [TestMethod]
        public void TestAddOk()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.Add(user)).Returns(user);

            User result = userLogic.Add(user);

            Assert.AreEqual(user, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.Add(user)).Throws(new ArgumentException());

            var reuslt = userLogic.Add(user);

            userMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.ExistElement(user)).Returns(true);

            var result = userLogic.Exist(user);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.ExistElement(user)).Returns(false);
            var result = userLogic.Exist(user);

            userMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.Find(user.Id)).Returns(user);
            userMock.Setup(m => m.Update(user.Id, user));

            User newUser = userLogic.Update(user.Id, user);

            Assert.AreEqual(user, newUser);
        }
        [TestMethod]
        public void TestUpdateValidateError()
        {
            User user = usersToReturn.First();
            userMock.Setup(m => m.Find(user.Id)).Returns(user);
            userMock.Setup(m => m.Update(user.Id, user));

            userLogic.Update(user.Id, user);

            userMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            User user = usersToReturn.First();
            ArgumentException exception = new ArgumentException();
            userMock.Setup(m => m.Find(user.Id)).Returns(user);
            userMock.Setup(m => m.Update(user.Id, user)).Throws(exception);

            userLogic.Update(user.Id, user);

            userMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = usersToReturn.First().Id;
            userMock.Setup(m => m.Delete(Id));

            userLogic.Delete(Id);

            userMock.VerifyAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Logics;
using DataAccessInterface.Repositories;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Tests.Test
{
    [TestClass]
    public class CheckLogicTest
    {
        private List<Check> checksToReturn;
        private CheckLogic checkLogic;
        private Mock<ICheckRepository> checkMock;
        private Mock<IApartmentRepository> apartmentMock;
        private Mock<IUserRepository> userMock;
        private List<Check> emptyCheckers;
        [TestInitialize]
        public void InitVariables()
        {
            checksToReturn = new List<Check>()
            {
                new Check()
                {
                    Id = 1,
                    User = new User(){Id=1, Role="ADMIN"},
                    UserId = 1,
                    Apartment = new Apartment(){Id=1},
                    ApartmentId = 1,
                    CheckDate = DateTime.Parse("2020/2/2"),
                    State = "UNDONE"
                },
                new Check()
                {
                    Id = 2,
                    User = new User(){Id=2, Role="ADMIN"},
                    UserId = 2,
                    Apartment = new Apartment(){Id=2},
                    ApartmentId = 2,
                    CheckDate = DateTime.Parse("2021/2/2"),
                    State = "DOING"
                },
                new Check()
                {
                    Id = 3,
                    User = new User(){Id=3, Role="CHECKER"},
                    UserId = 3,
                    Apartment = new Apartment(){Id=3},
                    ApartmentId = 3,
                    CheckDate = DateTime.Parse("2020/3/3"),
                    State = "DONE"
                }
            };
            emptyCheckers = new List<Check>();
            checkMock = new Mock<ICheckRepository>(MockBehavior.Strict);
            apartmentMock = new Mock<IApartmentRepository>(MockBehavior.Strict);
            userMock = new Mock<IUserRepository>(MockBehavior.Strict);
            checkLogic = new CheckLogic(checkMock.Object, apartmentMock.Object, userMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            checkMock.Setup(m => m.GetElements()).Returns(checksToReturn);

            var result = checkLogic.GetAll();

            Assert.IsTrue(result.SequenceEqual(checksToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            Check check = checksToReturn.First();
            checkMock.Setup(m => m.Find(check.Id)).Returns(check);

            var result = checkLogic.GetById(check.Id);

            Assert.AreEqual(result, check);
        }

        [TestMethod]
        public void TestAddOk()
        {
            Check check = checksToReturn.First();
            checkMock.Setup(m => m.Add(check)).Returns(check);
            userMock.Setup(m => m.Find(check.UserId)).Returns(check.User);
            apartmentMock.Setup(m => m.Find(check.ApartmentId)).Returns(check.Apartment);
            apartmentMock.Setup(m => m.Update(check.ApartmentId,check.Apartment));

            Check result = checkLogic.Add(check);

            Assert.AreEqual(check, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            Check check = checksToReturn.First();
            checkMock.Setup(m => m.Add(check)).Throws(new ArgumentException());
            userMock.Setup(m => m.Find(check.UserId)).Returns(check.User);
            apartmentMock.Setup(m => m.Find(check.ApartmentId)).Returns(check.Apartment);
            apartmentMock.Setup(m => m.Update(check.ApartmentId,check.Apartment));

            var reuslt = checkLogic.Add(check);

            checkMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            Check check = checksToReturn.First();
            checkMock.Setup(m => m.ExistElement(check)).Returns(true);

            var result = checkLogic.Exist(check);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            Check check = checksToReturn.First();
            checkMock.Setup(m => m.ExistElement(check)).Returns(false);
            var result = checkLogic.Exist(check);

            checkMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            Check check = checksToReturn.Last();
            checkMock.Setup(m => m.Find(check.Id)).Returns(check);
            checkMock.Setup(m => m.Update(check.Id, check));
            userMock.Setup(m => m.Find(check.UserId)).Returns(check.User);
            apartmentMock.Setup(m => m.Find(check.ApartmentId)).Returns(check.Apartment);
            apartmentMock.Setup(m => m.Update(check.ApartmentId,check.Apartment));

            Check newCheck = checkLogic.Update(check.Id, check);

            checkMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateValidateError()
        {
            Check check = checksToReturn.Last();
            checkMock.Setup(m => m.Find(check.Id)).Returns(check);
            checkMock.Setup(m => m.Update(check.Id, check));
            userMock.Setup(m => m.Find(check.UserId)).Throws(new ArgumentException());
            apartmentMock.Setup(m => m.Find(check.ApartmentId)).Returns(check.Apartment);
            apartmentMock.Setup(m => m.Update(check.ApartmentId,check.Apartment));

            checkLogic.Update(check.Id, check);

            checkMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            Check check = checksToReturn.First();
            ArgumentException exception = new ArgumentException();
            checkMock.Setup(m => m.Find(check.Id)).Returns(check);
            checkMock.Setup(m => m.Update(check.Id, check)).Throws(exception);
            userMock.Setup(m => m.Find(check.UserId)).Returns(check.User);
            apartmentMock.Setup(m => m.Find(check.ApartmentId)).Returns(check.Apartment);
            apartmentMock.Setup(m => m.Update(check.ApartmentId,check.Apartment));

            checkLogic.Update(check.Id, check);

            checkMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = checksToReturn.First().Id;
            checkMock.Setup(m => m.Delete(Id));

            checkLogic.Delete(Id);

            checkMock.VerifyAll();
        }
    
    }
}
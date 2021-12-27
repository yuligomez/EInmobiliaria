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
    public class ElementLogicTest
    {
        private List<Element> elementsToReturn;
        private ElementLogic elementLogic;
        private Mock<IElementRepository> elementMock;
        private Mock<IPhotoRepository> photoMock;
        private Mock<IApartmentRepository> apartmentMock;
        private Mock<IUserRepository> userMock;
        private List<Element> emptyElementers;
        [TestInitialize]
        public void InitVariables()
        {
            elementsToReturn = new List<Element>()
            {
                new Element()
                {
                    Id = 1,
                    Name = "Nombre 1",
                    Amount = 1,
                    IsBroken = false,
                    ApartmentId = 1,
                    Apartment = new Apartment(){Id=1},
                    UserId = 1,
                    User = new User(){Id=1}
                },
                new Element()
                {
                    Id = 2,
                    Name = "Nombre 2",
                    Amount = 2,
                    IsBroken = false,
                    ApartmentId = 2,
                    Apartment = new Apartment(){Id=2},
                    UserId = 2,
                    User = new User(){Id=2}
                },
                new Element()
                {
                    Id = 3,
                    Name = "Nombre 3",
                    Amount = 3,
                    IsBroken = true,
                    ApartmentId = 3,
                    Apartment = new Apartment(){Id=3},
                    UserId = 3,
                    User = new User(){Id=3},
                    PhotoId = 1,
                    Photo = new Photo(){Id=1,Name="NameOfPhoto.png"}
                }
            };
            emptyElementers = new List<Element>();
            elementMock = new Mock<IElementRepository>(MockBehavior.Strict);
            photoMock = new Mock<IPhotoRepository>(MockBehavior.Strict);
            apartmentMock = new Mock<IApartmentRepository>(MockBehavior.Strict);
            userMock = new Mock<IUserRepository>(MockBehavior.Strict);
            elementLogic = new ElementLogic(elementMock.Object,photoMock.Object,apartmentMock.Object,userMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            elementMock.Setup(m => m.GetElements()).Returns(elementsToReturn);

            IEnumerable<Element> result = elementLogic.GetAll();

            Assert.IsTrue(result.SequenceEqual(elementsToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.Find(element.Id)).Returns(element);

            var result = elementLogic.GetById(element.Id);

            Assert.AreEqual(result, element);
        }

        [TestMethod]
        public void TestAddOk()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.Add(element)).Returns(element);
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            Element result = elementLogic.Add(element);

            Assert.AreEqual(element, result);
        }
        [TestMethod]
        public void TestAddIsBrokenFail()
        {
            Element element = elementsToReturn.First();
            element.IsBroken = true;
            element.Photo = new Photo(){Name=""};
            elementMock.Setup(m => m.Add(element)).Returns(element);
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            Element result = elementLogic.Add(element);

            Assert.AreEqual(element, result);
        }
        [TestMethod]
        public void TestAddIsBroken()
        {
            Element element = elementsToReturn.First();
            element.IsBroken = true;
            element.Photo = new Photo(){Name = "foto.png",Image=1};
            elementMock.Setup(m => m.Add(element)).Returns(element);
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);
            photoMock.Setup(m => m.Add(element.Photo)).Returns(element.Photo);

            Element result = elementLogic.Add(element);

            Assert.AreEqual(element, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.Add(element)).Throws(new ArgumentException());
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            var reuslt = elementLogic.Add(element);

            elementMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.ExistElement(element)).Returns(true);

            var result = elementLogic.Exist(element);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.ExistElement(element)).Returns(false);
            var result = elementLogic.Exist(element);

            elementMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.Find(element.Id)).Returns(element);
            elementMock.Setup(m => m.Update(element.Id, element));
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            Element newElement = elementLogic.Update(element.Id, element);

            Assert.AreEqual(element, newElement);
        }
        [TestMethod]
        public void TestUpdateValidateError()
        {
            Element element = elementsToReturn.First();
            elementMock.Setup(m => m.Find(element.Id)).Returns(element);
            elementMock.Setup(m => m.Update(element.Id, element));
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            elementLogic.Update(element.Id, element);

            elementMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            Element element = elementsToReturn.First();
            ArgumentException exception = new ArgumentException();
            elementMock.Setup(m => m.Find(element.Id)).Returns(element);
            elementMock.Setup(m => m.Update(element.Id, element)).Throws(exception);
            apartmentMock.Setup(m => m.Find(element.ApartmentId)).Returns(element.Apartment);
            userMock.Setup(m => m.Find(element.UserId)).Returns(element.User);

            elementLogic.Update(element.Id, element);

            elementMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = elementsToReturn.First().Id;
            elementMock.Setup(m => m.Delete(Id));

            elementLogic.Delete(Id);

            elementMock.VerifyAll();
        }
    }
}
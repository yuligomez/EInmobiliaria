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
    public class ApartmentLogicTest
    {
        private List<Apartment> apartmentsToReturn;
        private ApartmentLogic apartmentLogic;
        private Mock<IApartmentRepository> apartmentMock;
        private List<Apartment> emptyApartmenters;
        [TestInitialize]
        public void InitVariables()
        {
            apartmentsToReturn = new List<Apartment>()
            {
                new Apartment()
                {
                    Id = 1,
                    Name = "Apartamento 1",
                    Description = "Descripcion 1",
                    Latitude = "1213231231",
                    Longitude = "1233242342"
                },
                new Apartment()
                {
                    Id = 2,
                    Name = "Apartamento 2",
                    Description = "Descripcion 2",
                    Latitude = "2223232232",
                    Longitude = "2233242342"
                },
                new Apartment()
                {
                    Id = 3,
                    Name = "Apartamento 3",
                    Description = "Descripcion 3",
                    Latitude = "3233233233",
                    Longitude = "3233242342"
                }
            };
            emptyApartmenters = new List<Apartment>();
            apartmentMock = new Mock<IApartmentRepository>(MockBehavior.Strict);
            apartmentLogic = new ApartmentLogic(apartmentMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            apartmentMock.Setup(m => m.GetElements()).Returns(apartmentsToReturn);

            var result = apartmentLogic.GetAll();

            Assert.IsTrue(result.SequenceEqual(apartmentsToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);

            var result = apartmentLogic.GetById(apartment.Id);

            Assert.AreEqual(result, apartment);
        }

        [TestMethod]
        public void TestAddOk()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.Add(apartment)).Returns(apartment);

            Apartment result = apartmentLogic.Add(apartment);

            Assert.AreEqual(apartment, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.Add(apartment)).Throws(new ArgumentException());

            var reuslt = apartmentLogic.Add(apartment);

            apartmentMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.ExistElement(apartment)).Returns(true);

            var result = apartmentLogic.Exist(apartment);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.ExistElement(apartment)).Returns(false);
            var result = apartmentLogic.Exist(apartment);

            apartmentMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);
            apartmentMock.Setup(m => m.Update(apartment.Id, apartment));

            Apartment newApartment = apartmentLogic.Update(apartment.Id, apartment);

            Assert.AreEqual(apartment, newApartment);
        }
        [TestMethod]
        public void TestUpdateValidateError()
        {
            Apartment apartment = apartmentsToReturn.First();
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);
            apartmentMock.Setup(m => m.Update(apartment.Id, apartment));

            apartmentLogic.Update(apartment.Id, apartment);

            apartmentMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            Apartment apartment = apartmentsToReturn.First();
            ArgumentException exception = new ArgumentException();
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);
            apartmentMock.Setup(m => m.Update(apartment.Id, apartment)).Throws(exception);

            apartmentLogic.Update(apartment.Id, apartment);

            apartmentMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = apartmentsToReturn.First().Id;
            apartmentMock.Setup(m => m.Delete(Id));

            apartmentLogic.Delete(Id);

            apartmentMock.VerifyAll();
        }
    }
}
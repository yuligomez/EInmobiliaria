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
    public class RentalLogicTest
    {
        private List<Rental> rentalsToReturn;
        private RentalLogic rentalLogic;
        private Mock<IRentalRepository> rentalMock;
        private Mock<IApartmentRepository> apartmentMock;
        private List<Rental> emptyRentalers;
        [TestInitialize]
        public void InitVariables()
        {
            rentalsToReturn = new List<Rental>()
            {
                new Rental()
                {
                    Id = 1,
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1},
                    StartDate = DateTime.Parse("2019/1/1"),
                    EndingDate = DateTime.Parse("2019/2/2"),
                },
                new Rental()
                {
                    Id = 2,
                    ApartmentId = 2,
                    Apartment = new Apartment() {Id=2},
                    StartDate = DateTime.Parse("2019/6/6"),
                    EndingDate = DateTime.Parse("2019/7/7"),
                },
                new Rental()
                {
                    Id = 3,
                    ApartmentId = 3,
                    Apartment = new Apartment() {Id=3},
                    StartDate = DateTime.Parse("2019/2/2"),
                    EndingDate = DateTime.Parse("2019/4/3"),
                }
            };
            emptyRentalers = new List<Rental>();
            rentalMock = new Mock<IRentalRepository>(MockBehavior.Strict);
            apartmentMock = new Mock<IApartmentRepository>(MockBehavior.Strict);
            rentalLogic = new RentalLogic(rentalMock.Object,apartmentMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            DateTime date = DateTime.Today;
            rentalMock.Setup(m => m.GetElements()).Returns(rentalsToReturn);

            var result = rentalLogic.GetAll(date);

            Assert.IsTrue(result.SequenceEqual(rentalsToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.Find(rental.Id)).Returns(rental);

            var result = rentalLogic.GetById(rental.Id);

            Assert.AreEqual(result, rental);
        }

        [TestMethod]
        public void TestAddOk()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.Add(rental)).Returns(rental);
            Apartment apartment = rental.Apartment;
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);

            Rental result = rentalLogic.Add(rental);

            Assert.AreEqual(rental, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.Add(rental)).Throws(new ArgumentException());
            Apartment apartment = rental.Apartment;
            apartmentMock.Setup(m => m.Find(apartment.Id)).Returns(apartment);

            var reuslt = rentalLogic.Add(rental);

            rentalMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.ExistElement(rental)).Returns(true);

            var result = rentalLogic.Exist(rental);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.ExistElement(rental)).Returns(false);
            var result = rentalLogic.Exist(rental);

            rentalMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.Find(rental.Id)).Returns(rental);
            rentalMock.Setup(m => m.Update(rental.Id, rental));

            Rental newRental = rentalLogic.Update(rental.Id, rental);

            Assert.AreEqual(rental, newRental);
        }
        [TestMethod]
        public void TestUpdateValidateError()
        {
            Rental rental = rentalsToReturn.First();
            rentalMock.Setup(m => m.Find(rental.Id)).Returns(rental);
            rentalMock.Setup(m => m.Update(rental.Id, rental));

            rentalLogic.Update(rental.Id, rental);

            rentalMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            Rental rental = rentalsToReturn.First();
            ArgumentException exception = new ArgumentException();
            rentalMock.Setup(m => m.Find(rental.Id)).Returns(rental);
            rentalMock.Setup(m => m.Update(rental.Id, rental)).Throws(exception);

            rentalLogic.Update(rental.Id, rental);

            rentalMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = rentalsToReturn.First().Id;
            rentalMock.Setup(m => m.Delete(Id));

            rentalLogic.Delete(Id);

            rentalMock.VerifyAll();
        }
    }
}
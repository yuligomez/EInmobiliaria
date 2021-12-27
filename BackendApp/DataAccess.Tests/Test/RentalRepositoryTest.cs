using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests.Test
{
    [TestClass]
    public class RentalRepositoryTest
    {
        private List<Rental> rentalsToReturn;
        private RentalRepository repositoryRental;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            rentalsToReturn = new List<Rental>()
            {
                new Rental
                {
                    Id =1,
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1},
                    StartDate = new DateTime(2021,1,1),
                    EndingDate = new DateTime(2021,2,2)
                },
                new Rental
                {
                    Id =2,
                    ApartmentId = 2,
                    Apartment = new Apartment() {Id=2},
                    StartDate = new DateTime(2021,3,3),
                    EndingDate = new DateTime(2021,4,4)
                }
            };
            rentalsToReturn.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryRental = new RentalRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestUpdateApartmentId()
        {
            Rental rental = new Rental();
            rental.ApartmentId = 2;
            int newApartmentId = rental.ApartmentId;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreEqual(rentalsToReturn.First().ApartmentId, newApartmentId);
        }
        [TestMethod]
        public void TestUpdateStartDate()
        {
            Rental rental = rentalsToReturn.First();
            rental.StartDate = new DateTime(2021,1,22);
            DateTime newStartDate = rental.StartDate;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreEqual(rentalsToReturn.First().StartDate, newStartDate);
        }
        [TestMethod]
        public void TestUpdateEndDate()
        {
            Rental rental = rentalsToReturn.First();
            rental.EndingDate = new DateTime(2021,2,22);
            DateTime newEndingDate = rental.EndingDate;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreEqual(rentalsToReturn.First().EndingDate, newEndingDate);
        }

        [TestMethod]
        public void TestUpdateApartmentIdNull()
        {
            Rental rental = new Rental();
            rental.ApartmentId = 0;
            int newApartmentId = rental.ApartmentId;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreNotEqual(rentalsToReturn.First().ApartmentId, newApartmentId);
        }
        [TestMethod]
        public void TestUpdateStartDateNull()
        {
            Rental rental = new Rental();
            rental.StartDate = default;
            DateTime newStartDate = rental.StartDate;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreNotEqual(rentalsToReturn.First().StartDate, newStartDate);
        }
        [TestMethod]
        public void TestUpdateEndDateNull()
        {
            Rental rental = new Rental();
            rental.EndingDate = default;
            DateTime newEndingDate = rental.EndingDate;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreNotEqual(rentalsToReturn.First().EndingDate, newEndingDate);
        }
        [TestMethod]
        public void TestUpdateNull()
        {
            Rental rental = new Rental();
            rental.ApartmentId = 0;
            rental.StartDate = default;
            rental.EndingDate = default;

            repositoryRental.Update(rentalsToReturn.First(), rental);

            Assert.AreNotEqual(rentalsToReturn.First(), rental);
        }
        
        [TestMethod]
        public void TestAddValidate()
        {
            Rental rental =  new Rental() { Id = 3 , ApartmentId = 3 , StartDate = new DateTime(2020,1,1) , EndingDate = new DateTime(2020,2,2)};
            int countRentals = repositoryRental.GetElements().Count + 1;

            repositoryRental.Add(rental);

            Assert.AreEqual(repositoryRental.GetElements().Count , countRentals);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateSameStartDate()
        {
            Rental rental = new Rental() { Id = 100 , ApartmentId = 1 , StartDate = rentalsToReturn.First().StartDate };

            repositoryRental.Add(rental);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateSameEndingDate()
        {
            Rental rental = new Rental() { Id = 100 , ApartmentId = 1 , EndingDate = rentalsToReturn.First().EndingDate };

            repositoryRental.Add(rental);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateBetweenRent()
        {
            Rental rental = new Rental() 
            { 
                Id = 100 , 
                ApartmentId = 1 , 
                StartDate = new DateTime(2021,1,20) , 
                EndingDate = new DateTime(2021,1,27) 
            };

            repositoryRental.Add(rental);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateOneDateBetween()
        {
            Rental rental = new Rental() 
            { 
                Id = 100 , 
                ApartmentId = 1 , 
                StartDate = new DateTime(2020,12,20) , 
                EndingDate = new DateTime(2021,2,20) 
            };

            repositoryRental.Add(rental);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateOneDateBetween2()
        {
            Rental rental = new Rental() 
            { 
                Id = 100 , 
                ApartmentId = 1 , 
                StartDate = new DateTime(2021,1,20) , 
                EndingDate = new DateTime(2021,1,26) 
            };

            repositoryRental.Add(rental);
        }
    }
}
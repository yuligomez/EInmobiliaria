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
    public class ApartmentRepositoryTest
    {
        private List<Apartment> apartmentsToReturn;
        private ApartmentRepository repositoryApartment;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            apartmentsToReturn = new List<Apartment>()
            {
                new Apartment
                {
                    Id = 1,
                    Name = "Edil Hugo Prato",
                    Description = "Descripcion 1",
                    Latitude = "234123123",
                    Longitude = "232323424"
                },
                new Apartment
                {
                    Id = 2,
                    Name = "Pablo de Maria",
                    Description = "Descripcion 2",
                    Latitude = "3123123",
                    Longitude = "1231231231"
                }
            };
            apartmentsToReturn.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryApartment= new ApartmentRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestUpdate()
        {
            Apartment apartment = new Apartment();
            apartment.Name = "Gonzalo Ramirez";
            apartment.Latitude = "2065";
            apartment.Description = "Descripcion N";

            repositoryApartment.Update(apartmentsToReturn.First(), apartment);

            Assert.IsTrue(apartmentsToReturn.First().Equals(apartment));
        }
        [TestMethod]
        public void TestUpdateName()
        {
            Apartment apartment = new Apartment();
            apartment.Name = "Gonzalo Ramirez";
            string newName = apartment.Name;

            repositoryApartment.Update(apartmentsToReturn.First(), apartment);

            Assert.AreEqual(apartmentsToReturn.First().Name, newName);
        }
        [TestMethod]
        public void TestUpdateLatitude()
        {
            Apartment apartment = new Apartment();
            apartment.Latitude = "22";
            string newLatitude = apartment.Latitude;

            repositoryApartment.Update(apartmentsToReturn.First(), apartment);

            Assert.AreEqual(apartmentsToReturn.First().Latitude, newLatitude);
        }
        [TestMethod]
        public void TestUpdateLongitude()
        {
            Apartment apartment = new Apartment();
            apartment.Longitude = "22";
            string newLongitude = apartment.Longitude;

            repositoryApartment.Update(apartmentsToReturn.First(), apartment);

            Assert.AreEqual(apartmentsToReturn.First().Longitude, newLongitude);
        }
        [TestMethod]
        public void TestUpdateNull()
        {
            Apartment apartment = new Apartment();
            apartment.Name = "";
            string newName = apartment.Name;

            repositoryApartment.Update(apartmentsToReturn.First(), apartment);

            Assert.AreNotEqual(apartmentsToReturn.First().Name, newName);
        }
        
        [TestMethod]
        public void TestAddValidate()
        {
            Apartment apartment =  new Apartment() { Id = 3, Name="Nombre" };
            int countApartments = repositoryApartment.GetElements().Count + 1;

            repositoryApartment.Add(apartment);

            Assert.AreEqual(repositoryApartment.GetElements().Count , countApartments);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidate()
        {
            Apartment apartment = new Apartment() 
            { 
                Id = 100, 
                Name = "", 
                Latitude = "2249", 
                Longitude ="123123",
                Description = "Descripcion N" };

            repositoryApartment.Add(apartment);
        }
    }
}
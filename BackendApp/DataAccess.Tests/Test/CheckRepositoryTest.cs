using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class CheckRepositoryTest
    {
        private List<Check> checksToReturn;
        private List<Apartment> apartments;
        private CheckRepository repositoryCheck;
        private ApartmentRepository apartmentRepository;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            apartments = new List<Apartment>()
            {
                new Apartment
                {
                    Id = 1
                },
                new Apartment
                {
                    Id = 2
                }
            };
            checksToReturn = new List<Check>()
            {
                new Check
                {
                    Id = 1,
                    State = "UNDONE",
                    ApartmentId  = 1,
                    Apartment =  apartments.First()
                },
                new Check
                {
                    Id = 2,
                    ApartmentId = 2,
                    Apartment = apartments.Last(),
                    State = "DOING"
                }
            };
            checksToReturn.ForEach(m => this.context.Add(m));
            apartments.ForEach(a => this.context.Add(a));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryCheck= new CheckRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestUpdate()
        {
            Check check = new Check();
            check.State = "DOING";
            string newState = check.State;

            repositoryCheck.Update(checksToReturn.First(), check);

            Assert.AreEqual(checksToReturn.First().State, newState);
        }
  
        [TestMethod]
        public void TestAddValidate()
        {
            Check check =  new Check() { Id = 3 , ApartmentId = 3};
            int countChecks = repositoryCheck.GetElements().Count + 1;

            repositoryCheck.Add(check);

            Assert.AreEqual(repositoryCheck.GetElements().Count , countChecks);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidate()
        {
            Check check = new Check() { Id = 100, ApartmentId = 2 };

            repositoryCheck.Add(check);
        }
    }
}
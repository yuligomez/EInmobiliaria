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
    public class ElementRepositoryTest
    {
        private List<Element> elementsToReturn;
        private ElementRepository repositoryElement;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            elementsToReturn = new List<Element>()
            {
                new Element
                {
                    Id = 1,
                    Name = "Nombre 1",
                    Amount = 1,
                    IsBroken = true,
                    PhotoId = 1,
                    Photo = new Photo() {Id=1},
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1}
                },
                new Element
                {
                    Id = 2,
                    Name = "Nombre 2",
                    Amount = 2,
                    IsBroken = false,
                    ApartmentId = 2,
                    Apartment = new Apartment() {Id=2}
                }
            };
            elementsToReturn.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryElement= new ElementRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestUpdateName()
        {
            Element element = new Element();
            element.Name = "Tenedor";

            repositoryElement.Update(elementsToReturn.First(), element);

            Assert.AreEqual(elementsToReturn.First().Name,element.Name);
        }
        [TestMethod]
        public void TestUpdateAmount()
        {
            Element element = new Element();
            element.Amount = 5;

            repositoryElement.Update(elementsToReturn.First(), element);

            Assert.AreEqual(elementsToReturn.First().Amount,element.Amount);
        }
        [TestMethod]
        public void TestUpdatePhotoId()
        {
            Element element = new Element() {Name = "", Amount = 0 , IsBroken = true , ApartmentId = 0};
            element.PhotoId = 2;
            element.Photo = new Photo(){ Id=2, Name="nuevaFoto.png" };

            repositoryElement.Update(elementsToReturn.First(), element);

            Assert.AreEqual(elementsToReturn.First().Photo.Name,element.Photo.Name);
        }
        [TestMethod]
        public void TestUpdateNullName()
        {
            Element element = new Element(){ Name = "", Amount = 1 , IsBroken = false,};
            element.Name = "";
            string newName = element.Name;

            repositoryElement.Update(elementsToReturn.Last(), element);

            Assert.AreNotEqual(elementsToReturn.Last().Name, newName);
        }
        [TestMethod]
        public void TestUpdateNullAmount()
        {
            Element element = elementsToReturn.Last();
            element.Amount = 0;
            int newAmount = element.Amount;

            repositoryElement.Update(elementsToReturn.First(), element);

            Assert.AreNotEqual(elementsToReturn.First().Amount, newAmount);
        }
        [TestMethod]
        public void TestUpdateNull()
        {
            Element element = new Element();
            element.Name = elementsToReturn.First().Name;
            element.Amount = elementsToReturn.First().Amount;
            element.PhotoId = 0;
            int? newPhotoId = element.PhotoId ;

            repositoryElement.Update(elementsToReturn.First(), element);

            Assert.AreNotEqual(elementsToReturn.First().PhotoId, newPhotoId);
        }
        
        [TestMethod]
        public void TestAddValidate()
        {
            Element element =  new Element() 
            { 
                Id = 3 , 
                Name = "Cuchillos" , 
                Amount = 2 , 
                IsBroken = false,
                ApartmentId = 1,
                UserId = 1 
            };
            int countElements = repositoryElement.GetElements().Count + 1;

            repositoryElement.Add(element);

            Assert.AreEqual(repositoryElement.GetElements().Count , countElements);
        }
        [TestMethod]
        public void TestAddValidateBroken()
        {
            Element element =  new Element() 
            { 
                Id = 3 , 
                Name = "Cuchillos" , 
                Amount = 2 , 
                IsBroken = true, 
                PhotoId = 3, 
                Photo = new Photo() {Id=3},
                ApartmentId = 1,
                UserId = 1
            };
            int countElements = repositoryElement.GetElements().Count + 1;

            repositoryElement.Add(element);

            Assert.AreEqual(repositoryElement.GetElements().Count , countElements);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateName()
        {
            Element element = new Element() { Id = 1, Name = "" , Amount = 2 , IsBroken = false };

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateNameBroken()
        {
            Element element = new Element() { Id = 1, Name = "" , Amount = 2 , IsBroken = true, PhotoId = 3, Photo = new Photo() {Id=3}};

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateAmount()
        {
            Element element = new Element() { Id = 1, Name = elementsToReturn.First().Name , Amount = 0, IsBroken = false};

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateAmountBroken()
        {
            Element element = new Element() { Id = 1, Name = elementsToReturn.First().Name , Amount = 0 , IsBroken = true, PhotoId = 3, Photo = new Photo() {Id=3}};

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateBroken()
        {
            Element element = new Element() { Id = 1, Name = elementsToReturn.First().Name , Amount = 10 , IsBroken = true, PhotoId = 0};

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateNoAparmentId()
        {
            Element element = new Element() { Id = 1, Name = elementsToReturn.First().Name , Amount = 10 , IsBroken = false, ApartmentId = 0};

            repositoryElement.Add(element);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateNoUserId()
        {
            Element element = new Element() { Id = 1, Name = elementsToReturn.First().Name , Amount = 10 , IsBroken = false, ApartmentId = 1, UserId = 0};

            repositoryElement.Add(element);
        }
    }
}
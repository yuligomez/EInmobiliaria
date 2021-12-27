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
    public class PhotoRepositoryTest
    {
        private List<Photo> photosToReturn;
        private PhotoRepository repositoryPhoto;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            photosToReturn = new List<Photo>()
            {
                new Photo
                {
                    Id = 1,
                    Name = "Imagen1.png"
                },
                new Photo
                {
                    Id = 2,
                    Name = "Imagen2.png"
                }
            };
            photosToReturn.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryPhoto= new PhotoRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestUpdate()
        {
            Photo photo = new Photo();
            photo.Name = "NewPhoto.png";

            repositoryPhoto.Update(photosToReturn.First(), photo);

            Assert.IsTrue(photosToReturn.First().Equals(photo));
        }
        [TestMethod]
        public void TestUpdateNull()
        {
            Photo photo = new Photo();
            photo.Name = "";
            string newName = photo.Name;

            repositoryPhoto.Update(photosToReturn.First(), photo);

            Assert.AreNotEqual(photosToReturn.First().Name, newName);
        }
        
        [TestMethod]
        public void TestAddValidate()
        {
            Photo photo =  new Photo() { Id = 3 , Name = "OtherPhoto.png" };
            int countPhotos = repositoryPhoto.GetElements().Count + 1;

            repositoryPhoto.Add(photo);

            Assert.AreEqual(repositoryPhoto.GetElements().Count , countPhotos);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidate()
        {
            Photo photo = new Photo() { Id = 1, Name = ""};

            repositoryPhoto.Add(photo);
        }
    }
}
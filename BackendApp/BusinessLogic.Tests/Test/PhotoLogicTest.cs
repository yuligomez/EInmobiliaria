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
    public class PhotoLogicTest
    {
        private List<Photo> photosToReturn;
        private PhotoLogic photoLogic;
        private Mock<IPhotoRepository> photoMock;
        private List<Photo> emptyPhotoers;
        [TestInitialize]
        public void InitVariables()
        {
            photosToReturn = new List<Photo>()
            {
                new Photo()
                {
                    Id = 1,
                    Name = "Image1.png"
                },
                new Photo()
                {
                    Id = 2,
                    Name = "Image2.png"
                },
                new Photo()
                {
                    Id = 3,
                    Name = "Image3.png"
                }
            };
            emptyPhotoers = new List<Photo>();
            photoMock = new Mock<IPhotoRepository>(MockBehavior.Strict);
            photoLogic = new PhotoLogic(photoMock.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            photoMock.Setup(m => m.GetElements()).Returns(photosToReturn);

            var result = photoLogic.GetAll();

            Assert.IsTrue(result.SequenceEqual(photosToReturn));
        }

        [TestMethod]
        public void GetByTestOk()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.Find(photo.Id)).Returns(photo);

            var result = photoLogic.GetById(photo.Id);

            Assert.AreEqual(result, photo);
        }

        [TestMethod]
        public void TestAddOk()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.Add(photo)).Returns(photo);

            Photo result = photoLogic.Add(photo);

            Assert.AreEqual(photo, result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistError()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.Add(photo)).Throws(new ArgumentException());

            var reuslt = photoLogic.Add(photo);

            photoMock.VerifyAll();
        }
        [TestMethod]
        public void TestExistOk()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.ExistElement(photo)).Returns(true);

            var result = photoLogic.Exist(photo);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestNotExistOk()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.ExistElement(photo)).Returns(false);
            var result = photoLogic.Exist(photo);

            photoMock.VerifyAll();

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestUpdateOk()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.Find(photo.Id)).Returns(photo);
            photoMock.Setup(m => m.Update(photo.Id, photo));

            Photo newPhoto = photoLogic.Update(photo.Id, photo);

            Assert.AreEqual(photo, newPhoto);
        }
        [TestMethod]
        public void TestUpdateValidateError()
        {
            Photo photo = photosToReturn.First();
            photoMock.Setup(m => m.Find(photo.Id)).Returns(photo);
            photoMock.Setup(m => m.Update(photo.Id, photo));

            photoLogic.Update(photo.Id, photo);

            photoMock.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateExistError()
        {
            Photo photo = photosToReturn.First();
            ArgumentException exception = new ArgumentException();
            photoMock.Setup(m => m.Find(photo.Id)).Returns(photo);
            photoMock.Setup(m => m.Update(photo.Id, photo)).Throws(exception);

            photoLogic.Update(photo.Id, photo);

            photoMock.VerifyAll();
        }
        [TestMethod]
        public void DeleteById()
        {
            int Id = photosToReturn.First().Id;
            photoMock.Setup(m => m.Delete(Id));

            photoLogic.Delete(Id);

            photoMock.VerifyAll();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Test
{
    [TestClass]
    public class ApartmentControllerTest
    {
        private List<Apartment> apartmentsToReturn;
        private List<Apartment> apartmentsToReturnEmpty;
        private Apartment apartmentWithId1;
        private Apartment apartmentToInsert;
        private Mock<IApartmentLogic> mockApartmentLogic;
        private ApartmentController controllerApartment ;

        [TestInitialize]
        public void InitVariables()
        {
            apartmentsToReturn = new List<Apartment>()
            {
                new Apartment
                {
                    Id =1,
                    Name = "Gonzalo Ramirez",
                    Longitude= "2065",
                    Latitude = "23123123"
                },
                new Apartment
                {
                    Id =2,
                    Name = "Prato",
                    Longitude= "2249",
                    Latitude = "12312312"
                }
            };
            apartmentsToReturnEmpty = new List<Apartment>();
            apartmentWithId1 = apartmentsToReturn.First();
            mockApartmentLogic = new Mock<IApartmentLogic>(MockBehavior.Strict);
            controllerApartment = new ApartmentController(mockApartmentLogic.Object);

        }
        
        [TestMethod]
        public void TestGetAllApartmentOk()
        {
            mockApartmentLogic.Setup(m => m.GetAll()).Returns(apartmentsToReturn);
            
            var result = controllerApartment.Get();
            var okResult = result as OkObjectResult;
            var apartments = okResult.Value as IEnumerable<ApartmentBasicInfoModel>;

            mockApartmentLogic.VerifyAll();
            var returnExpected = apartmentsToReturn.Select(m => new ApartmentBasicInfoModel(m));
            Assert.IsTrue(returnExpected.SequenceEqual(apartments));
        }
     
        [TestMethod]
        public void TestGetAllEmptyApartments()
        {
            mockApartmentLogic.Setup(m => m.GetAll()).Returns(apartmentsToReturnEmpty);
            IEnumerable<ApartmentBasicInfoModel> apartmentBasicModels = apartmentsToReturnEmpty.Select(m => new ApartmentBasicInfoModel(m));

            var result = controllerApartment.Get();

            var okResult = result as OkObjectResult;
            var apartmentResult = okResult.Value as IEnumerable<ApartmentBasicInfoModel>;

            Assert.IsTrue(apartmentBasicModels.SequenceEqual(apartmentResult));
        }
        [TestMethod]
        public void TestGetByOk()
        {
            mockApartmentLogic.Setup(m => m.GetById(apartmentWithId1.Id)).Returns(apartmentWithId1);
            ApartmentBasicInfoModel apartmentDetailModel = new ApartmentBasicInfoModel(apartmentWithId1);

            var result = controllerApartment.GetBy(apartmentWithId1.Id);

            var okResult = result as OkObjectResult;
            var apartmentReturn = okResult.Value as ApartmentBasicInfoModel;
            Assert.IsTrue(apartmentReturn.Equals(apartmentDetailModel));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetByNotFound()
        {
            int id = 7;
            ArgumentException exist = new ArgumentException();
            mockApartmentLogic.Setup(m => m.GetById(id)).Throws(exist);

            var result = controllerApartment.GetBy(id);

            mockApartmentLogic.VerifyAll();
        }
        [TestMethod]
        public void TestPostOk()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "123123123"
            };
            apartmentToInsert = apartmentModel.ToEntity();
            mockApartmentLogic.Setup(m => m.Add(apartmentToInsert)).Returns(apartmentToInsert);
            ApartmentBasicInfoModel apartmentBasicModel = new ApartmentBasicInfoModel(apartmentToInsert);

            var result = controllerApartment.Post(apartmentModel);

            var okResult = result as CreatedAtRouteResult;
            mockApartmentLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestPostFailSameApartment()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "12312312"
            };
            apartmentToInsert = apartmentModel.ToEntity();
            Exception exist = new AggregateException();
            mockApartmentLogic.Setup(p => p.Add(apartmentToInsert)).Throws(exist);

            var result = controllerApartment.Post(apartmentModel);

            mockApartmentLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPostFailValidation()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "122312"
            };
            apartmentToInsert = apartmentModel.ToEntity();
            ArgumentException exist = new ArgumentException();
            mockApartmentLogic.Setup(p => p.Add(apartmentToInsert)).Throws(exist);

            var result = controllerApartment.Post(apartmentModel);

            mockApartmentLogic.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPostFailServer()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "1212"
            };
            apartmentToInsert = apartmentModel.ToEntity();
            Exception exist = new Exception();
            mockApartmentLogic.Setup(p => p.Add(apartmentToInsert)).Throws(exist);

            var result = controllerApartment.Post(apartmentModel);

            mockApartmentLogic.VerifyAll();
        }

        [TestMethod]
        public void TestPutOk()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "0909"
            };
            apartmentWithId1 = apartmentModel.ToEntity();
            mockApartmentLogic.Setup(m => m.Update(apartmentWithId1.Id,apartmentWithId1)).Returns(apartmentWithId1);
           

            var result = controllerApartment.Put(apartmentWithId1.Id, apartmentModel);

            var okResult = result as CreatedAtRouteResult;
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutFailValidate()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "9912312"
            };
            apartmentWithId1 = apartmentModel.ToEntity();
            Exception exist = new ArgumentException();
            mockApartmentLogic.Setup(p => p.Update(apartmentWithId1.Id,apartmentWithId1)).Throws(exist);

            var result = controllerApartment.Put(apartmentWithId1.Id, apartmentModel);

            mockApartmentLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPutFailServer()
        {
            ApartmentModel apartmentModel = new ApartmentModel()
            {
                Name = "Nombre calle",
                Longitude= "22",
                Latitude = "12312312"
            };
            apartmentWithId1 = apartmentModel.ToEntity();
            Exception exist = new Exception();
            mockApartmentLogic.Setup(p => p.Update(apartmentWithId1.Id,apartmentWithId1)).Throws(exist);

            var result = controllerApartment.Put(apartmentWithId1.Id, apartmentModel);

            mockApartmentLogic.VerifyAll();
        }
        [TestMethod]
        public void TestDeleteByIdOk()
        {
            var apartmentId = 1;

            mockApartmentLogic.Setup(m => m.Delete(apartmentId));
    
            var result = controllerApartment.Delete(apartmentId);

            mockApartmentLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNotFound()
        {
            var apartmentId = 4;

            mockApartmentLogic.Setup(m => m.Delete(apartmentId)).Throws(new ArgumentException());
            
            var result = controllerApartment.Delete(apartmentId);

            mockApartmentLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.In;
using Model.Out;
using Moq;
using WebApi.Controllers;

namespace WebApi.Tests.Test
{
    [TestClass]
    public class CheckControllerTest
    {
        private List<Check> checksToReturn;
        private List<Check> checksToReturnEmpty;
        private IEnumerable<Rental> rentals;
        private Check checkWithId1;
        private IEnumerable<Check> checkToInsert;
        private Mock<ICheckLogic> mockCheckLogic;
        private Mock<IRentalLogic> mockRentalLogic;
        private CheckController controllerCheck ;
        private List<int> apartmentsId;
        private List<int> rentalsId;

        [TestInitialize]
        public void InitVariables()
        {
            checksToReturn = new List<Check>()
            {
                new Check
                {
                    Id = 1,
                    UserId = 1 , 
                    User = new User() { Id = 1 },
                    ApartmentId = 1,
                    Apartment = new Apartment() { Id = 1 }
                },
                new Check
                {
                    Id = 2,
                    UserId = 1 , 
                    User = new User() { Id = 1 },
                    ApartmentId = 2,
                    Apartment = new Apartment() { Id = 2 }
                }
            };
            rentals = new List<Rental>()
            {
                new Rental
                {
                    Id = 1,
                    ApartmentId = 1,
                    Apartment = new Apartment() { Id = 1 },
                    StartDate = DateTime.Parse("2020/1/1"),
                    EndingDate = DateTime.Parse("2020/2/2")
                },
                new Rental
                {
                    Id = 2,
                    ApartmentId = 2,
                    Apartment = new Apartment() { Id = 2 },
                    StartDate = DateTime.Parse("2020/1/1"),
                    EndingDate = DateTime.Parse("2020/2/2")
                }
            };
            apartmentsId = new List<int>(){1};
            rentalsId = new List<int>(){1};
            checksToReturnEmpty = new List<Check>();
            checkWithId1 = checksToReturn.First();
            mockCheckLogic = new Mock<ICheckLogic>(MockBehavior.Strict);
            mockRentalLogic = new Mock<IRentalLogic>(MockBehavior.Strict);
            controllerCheck = new CheckController(mockCheckLogic.Object, mockRentalLogic.Object);
        }
        
        [TestMethod]
        public void TestGetAllCheckOk()
        {
            mockCheckLogic.Setup(m => m.GetAll()).Returns(checksToReturn);
            
            var result = controllerCheck.Get();

            var okResult = result as OkObjectResult;
            var checks = okResult.Value as IEnumerable<CheckDetailInfoModel>;
            mockCheckLogic.VerifyAll();
            var returnExpected = checksToReturn.Select(m => new CheckDetailInfoModel(m));
            Assert.IsTrue(returnExpected.SequenceEqual(checks));
        }
     
        [TestMethod]
        public void TestGetAllEmptyChecks()
        {
            mockCheckLogic.Setup(m => m.GetAll()).Returns(checksToReturnEmpty);
            IEnumerable<CheckDetailInfoModel> checkBasicModels = checksToReturnEmpty.Select(m => new CheckDetailInfoModel(m));

            var result = controllerCheck.Get();

            var okResult = result as OkObjectResult;
            var checkResult = okResult.Value as IEnumerable<CheckDetailInfoModel>;

            Assert.IsTrue(checkBasicModels.SequenceEqual(checkResult));
        }
        [TestMethod]
        public void TestGetByOk()
        {
            mockCheckLogic.Setup(m => m.GetById(checkWithId1.Id)).Returns(checkWithId1);
            CheckDetailInfoModel checkDetailModel = new CheckDetailInfoModel(checkWithId1);

            var result = controllerCheck.GetBy(checkWithId1.Id);

            var okResult = result as OkObjectResult;
            var checkReturn = okResult.Value as CheckDetailInfoModel;
            Assert.IsTrue(checkReturn.Equals(checkDetailModel));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetByNotFound()
        {
            int id = 7;
            ArgumentException exist = new ArgumentException();
            mockCheckLogic.Setup(m => m.GetById(id)).Throws(exist);

            var result = controllerCheck.GetBy(id);

            mockCheckLogic.VerifyAll();
        }
        [TestMethod]
        public void TestPostOk()
        {
            CheckModel checkModel = new CheckModel()
            {
                RentalsId = rentalsId,
                ApartmentsId = apartmentsId
            };
            checkToInsert = checkModel.ToEntityCheck();
            checkToInsert.Select( x => 
                mockCheckLogic.Setup(m => m.Add(x)).Returns(x) 
            );
            rentals = checkModel.ToEntityRental();
            rentals.Select(x => mockRentalLogic.Setup(m => m.Update(x.Id,x)).Returns(x) );
            // mockCheckLogic.Setup(m => m.Add(checkToInsert)).Returns(checkToInsert);

            // var result = controllerCheck.Post(checkModel);

            // var okResult = result as CreatedAtRouteResult;
            mockCheckLogic.VerifyAll();
            // Assert.IsNotNull(okResult);
        }

        [TestMethod]
        // [ExpectedException(typeof(ArgumentException))]
        public void TestPostFailValidation()
        {
            CheckModel checkModel = new CheckModel()
            {
                RentalsId = rentalsId,
                ApartmentsId = apartmentsId
            };
            checkToInsert = checkModel.ToEntityCheck();
            ArgumentException exist = new ArgumentException();
            checkToInsert.Select( x => mockCheckLogic.Setup(m => m.Add(x)).Throws(exist) );
            rentals = checkModel.ToEntityRental();
            rentals.Select(x => mockRentalLogic.Setup(m => m.Update(x.Id,x)).Returns(x) );
            // mockCheckLogic.Setup(p => p.Add(checkToInsert)).Throws(exist);

            // var result = controllerCheck.Post(checkModel);

            mockCheckLogic.VerifyAll();
        }

        [TestMethod]
        public void TestPutOk()
        {
            CheckPutModel checkModel = new CheckPutModel()
            {
                UserId = 1,
                State = "DOING"
            };
            checkWithId1 = checkModel.ToEntity();
            mockCheckLogic.Setup(m => m.Update(checkWithId1.Id,checkWithId1)).Returns(checkWithId1);
            // this.checkLogic.Update(id, checkToUpdate);

            // var result = controllerCheck.Put(checkWithId1.Id, checkModel);

            // var okResult = result as CreatedAtRouteResult;
            // Assert.IsNotNull(okResult);
        }
        [TestMethod]
        // [ExpectedException(typeof(ArgumentException))]
        public void TestPutFailValidate()
        {
            CheckPutModel checkModel = new CheckPutModel()
            {
                UserId = 1,
                State = "DOING"
            };
            checkWithId1 = checkModel.ToEntity();
            ArgumentException exist = new ArgumentException();
            mockCheckLogic.Setup(p => p.Update(checkWithId1.Id,checkWithId1)).Throws(exist);

            // var result = controllerCheck.Put(checkWithId1.Id, checkModel);

            // mockCheckLogic.VerifyAll();
        }

        [TestMethod]
        public void TestDeleteByIdOk()
        {
            var checkId = 1;

            mockCheckLogic.Setup(m => m.Delete(checkId));
    
            var result = controllerCheck.Delete(checkId);

            mockCheckLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNotFound()
        {
            var checkId = 4;

            mockCheckLogic.Setup(m => m.Delete(checkId)).Throws(new ArgumentException());
            
            var result = controllerCheck.Delete(checkId);

            mockCheckLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
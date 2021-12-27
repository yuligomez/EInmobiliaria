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
    public class RentalControllerTest
    {
        private List<Rental> rentalsToReturn;
        private List<Rental> rentalsToReturnEmpty;
        private Rental rentalWithId1;
        private Rental rentalToInsert;
        private Mock<IRentalLogic> mockRentalLogic;
        private RentalController controllerRental;

        [TestInitialize]
        public void InitVariables()
        {
            rentalsToReturn = new List<Rental>()
            {
                new Rental
                {
                    Id =1,
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1},
                    StartDate = new System.DateTime(2020,1,1),
                    EndingDate = new System.DateTime(2020,2,2)
                },
                new Rental
                {
                    Id =2,
                    ApartmentId = 2,
                    Apartment = new Apartment() {Id=2},
                    StartDate = new System.DateTime(2020,3,3),
                    EndingDate = new System.DateTime(2020,4,4)
                }
            };
            rentalsToReturnEmpty = new List<Rental>();
            rentalWithId1 = rentalsToReturn.First();
            mockRentalLogic = new Mock<IRentalLogic>(MockBehavior.Strict);
            controllerRental = new RentalController(mockRentalLogic.Object);
        }
        
        [TestMethod]
        public void TestGetAllRentalOk()
        {
            DateTime date = DateTime.Today;
            mockRentalLogic.Setup(m => m.GetAll(date)).Returns(rentalsToReturn);
            
            var result = controllerRental.Get();
            var okResult = result as OkObjectResult;
            var rentals = okResult.Value as IEnumerable<RentalDetailInfoModel>;

            mockRentalLogic.VerifyAll();
            var returnExpected = rentalsToReturn.Select(m => new RentalDetailInfoModel(m));
            Assert.IsTrue(returnExpected.SequenceEqual(rentals));
        }
     
        [TestMethod]
        public void TestGetAllEmptyRentals()
        {
            DateTime date = DateTime.Today;
            mockRentalLogic.Setup(m => m.GetAll(date)).Returns(rentalsToReturnEmpty);
            IEnumerable<RentalDetailInfoModel> rentalBasicModels = rentalsToReturnEmpty.Select(m => new RentalDetailInfoModel(m));

            var result = controllerRental.Get();

            var okResult = result as OkObjectResult;
            var rentalResult = okResult.Value as IEnumerable<RentalDetailInfoModel>;

            Assert.IsTrue(rentalResult.Count()==0);
        }
        [TestMethod]
        public void TestGetByOk()
        {
            mockRentalLogic.Setup(m => m.GetById(rentalWithId1.Id)).Returns(rentalWithId1);
            RentalDetailInfoModel rentalDetailModel = new RentalDetailInfoModel(rentalWithId1);

            var result = controllerRental.GetBy(rentalWithId1.Id);

            var okResult = result as OkObjectResult;
            var rentalReturn = okResult.Value as RentalDetailInfoModel;
            Assert.IsTrue(rentalReturn.Equals(rentalDetailModel));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetByNotFound()
        {
            int id = 7;
            ArgumentException exist = new ArgumentException();
            mockRentalLogic.Setup(m => m.GetById(id)).Throws(exist);

            var result = controllerRental.GetBy(id);

            mockRentalLogic.VerifyAll();
        }
        [TestMethod]
        public void TestPostOk()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 1,
                StartDate = "2021/1/1",
                EndingDate = "2021/2/2"
            };
            rentalToInsert = rentalModel.ToEntity();
            rentalToInsert.Apartment = new Apartment(){Id=1};
            mockRentalLogic.Setup(m => m.Add(rentalToInsert)).Returns(rentalToInsert);
            RentalBasicInfoModel rentalBasicModel = new RentalBasicInfoModel(rentalToInsert);

            var result = controllerRental.Post(rentalModel);

            var okResult = result as CreatedAtRouteResult;
            mockRentalLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestPostFailSameRental()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 1,
                StartDate = "2020/1/1",
                EndingDate = "2021/2/2"
            };
            rentalToInsert = rentalModel.ToEntity();
            Exception exist = new AggregateException();
            mockRentalLogic.Setup(p => p.Add(rentalToInsert)).Throws(exist);

            var result = controllerRental.Post(rentalModel);

            mockRentalLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPostFailValidation()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 1,
                StartDate = "2019/12/6",
                EndingDate = "2020/1/20"
            };
            rentalToInsert = rentalModel.ToEntity();
            ArgumentException exist = new ArgumentException();
            mockRentalLogic.Setup(p => p.Add(rentalToInsert)).Throws(exist);

            var result = controllerRental.Post(rentalModel);

            mockRentalLogic.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPostFailServer()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 1,
                StartDate = "2020/2/1",
                EndingDate = "2020/6/6"
            };
            rentalToInsert = rentalModel.ToEntity();
            Exception exist = new Exception();
            mockRentalLogic.Setup(p => p.Add(rentalToInsert)).Throws(exist);

            var result = controllerRental.Post(rentalModel);

            mockRentalLogic.VerifyAll();
        }

        [TestMethod]
        public void TestPutOk()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 1,
                StartDate = "2021/1/1" ,
                EndingDate = "2021/2/2" 
            };
            rentalWithId1 = rentalModel.ToEntity();
            rentalWithId1.Apartment = new Apartment(){Id=1};
            mockRentalLogic.Setup(m => m.Update(rentalWithId1.Id,rentalWithId1)).Returns(rentalWithId1);
           

            var result = controllerRental.Put(rentalWithId1.Id, rentalModel);

            var okResult = result as CreatedAtRouteResult;
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutFailValidate()
        {
            RentalModel rentalModel = new RentalModel()
            {
                ApartmentId = 100,
                StartDate = "2021/1/1" , 
                EndingDate = ""
            };
            rentalWithId1 = rentalModel.ToEntity();
            Exception exist = new ArgumentException();
            mockRentalLogic.Setup(p => p.Update(rentalWithId1.Id,rentalWithId1)).Throws(exist);

            var result = controllerRental.Put(rentalWithId1.Id, rentalModel);

            mockRentalLogic.VerifyAll();
        }
        [TestMethod]
        public void TestDeleteByIdOk()
        {
            var rentalId = 1;

            mockRentalLogic.Setup(m => m.Delete(rentalId));
    
            var result = controllerRental.Delete(rentalId);

            mockRentalLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNotFound()
        {
            var rentalId = 4;

            mockRentalLogic.Setup(m => m.Delete(rentalId)).Throws(new ArgumentException());
            
            var result = controllerRental.Delete(rentalId);

            mockRentalLogic.VerifyAll();
        }
    }
}
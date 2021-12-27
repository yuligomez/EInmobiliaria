using System;
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
    public class ElementControllerTest
    {
        private List<Element> elementApartmentsToReturn;
        private List<Element> elementApartmentsToReturnEmpty;
        private Element elementApartmentWithId1;
        private Element elementApartmentToInsert;
        private Mock<IElementLogic> mockElementLogic;
        private ElementController controllerElement;

        [TestInitialize]
        public void InitVariables()
        {
            elementApartmentsToReturn = new List<Element>()
            {
                new Element
                {
                    Id =1,
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1},
                    UserId = 1,
                    User = new User() {Id=1}
                },
                new Element
                {
                    Id =2,
                    ApartmentId = 1,
                    Apartment = new Apartment() {Id=1},
                    UserId = 2,
                    User = new User() {Id=2}
                    
                }
            };
            elementApartmentsToReturnEmpty = new List<Element>();
            elementApartmentWithId1 = elementApartmentsToReturn.First();
            mockElementLogic = new Mock<IElementLogic>(MockBehavior.Strict);
            controllerElement = new ElementController(mockElementLogic.Object);
        }
        
        [TestMethod]
        public void TestGetAllElementOk()
        {
            mockElementLogic.Setup(m => m.GetAll()).Returns(elementApartmentsToReturn);
            
            var result = controllerElement.Get();
            var okResult = result as OkObjectResult;

            mockElementLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }
     
        [TestMethod]
        public void TestGetAllEmptyElements()
        {
            mockElementLogic.Setup(m => m.GetAll()).Returns(elementApartmentsToReturnEmpty);
            IEnumerable<ElementBasicInfoModel> elementApartmentBasicModels = elementApartmentsToReturnEmpty.Select(m => new ElementBasicInfoModel(m));

            var result = controllerElement.Get();

            var okResult = result as OkObjectResult;
            var elementApartmentResult = okResult.Value as IEnumerable<ElementBasicInfoModel>;

            Assert.IsTrue(elementApartmentBasicModels.SequenceEqual(elementApartmentResult));
        }
        [TestMethod]
        public void TestGetByOk()
        {
            int id = 1;
            mockElementLogic.Setup(m => m.GetAll()).Returns(elementApartmentsToReturn);

            var result = controllerElement.GetBy(id);

            var okResult = result as OkObjectResult;
            mockElementLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void TestGetByEmpty()
        {
            int id = 1;
            mockElementLogic.Setup(m => m.GetAll()).Returns(elementApartmentsToReturnEmpty);

            var result = controllerElement.GetBy(id);

            mockElementLogic.VerifyAll();
        }
        [TestMethod]
        public void TestPostOk()
        {
            ElementModel elementApartmentModel = new ElementModel()
            {
                ApartmentId = 1,
                UserId = 1,
                Name = "Nombre objeto",
                Amount = 1
            };
            elementApartmentToInsert = elementApartmentModel.ToEntity();
            elementApartmentToInsert.Apartment = new Apartment(){Id=1};
            elementApartmentToInsert.User = new User(){Id=1};
            mockElementLogic.Setup(m => m.Add(elementApartmentToInsert)).Returns(elementApartmentToInsert);
            ElementBasicInfoModel elementApartmentBasicModel = new ElementBasicInfoModel(elementApartmentToInsert);

            var result = controllerElement.Post(elementApartmentModel);

            var okResult = result as CreatedAtRouteResult;
            mockElementLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPostFailValidation()
        {
            ElementModel elementApartmentModel = new ElementModel()
            {
                ApartmentId = 1,
                UserId = 1
            };
            elementApartmentToInsert = elementApartmentModel.ToEntity();
            ArgumentException exist = new ArgumentException();
            mockElementLogic.Setup(p => p.Add(elementApartmentToInsert)).Throws(exist);

            var result = controllerElement.Post(elementApartmentModel);

            mockElementLogic.VerifyAll();
        }

        [TestMethod]
        public void TestPutOk()
        {
            ElementModel elementApartmentModel = new ElementModel()
            {
                ApartmentId = 1,
                UserId = 1
            };
            elementApartmentWithId1 = elementApartmentModel.ToEntity(false);
            elementApartmentWithId1.Apartment = new Apartment(){Id=1};
            elementApartmentWithId1.User = new User(){Id=1};
            mockElementLogic.Setup(m => m.Update(elementApartmentWithId1.Id,elementApartmentWithId1)).Returns(elementApartmentWithId1);

            var result = controllerElement.Put(elementApartmentWithId1.Id, elementApartmentModel);

            var okResult = result as CreatedAtRouteResult;
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutFailValidate()
        {
            ElementModel elementApartmentModel = new ElementModel()
            {
                ApartmentId = 100,
                UserId = 1
            };
            elementApartmentWithId1 = elementApartmentModel.ToEntity();
            Exception exist = new ArgumentException();
            mockElementLogic.Setup(p => p.Update(elementApartmentWithId1.Id,elementApartmentWithId1)).Throws(exist);

            var result = controllerElement.Put(elementApartmentWithId1.Id, elementApartmentModel);

            mockElementLogic.VerifyAll();
        }
        [TestMethod]
        public void TestDeleteByIdOk()
        {
            var elementApartmentId = 1;

            mockElementLogic.Setup(m => m.Delete(elementApartmentId));
    
            var result = controllerElement.Delete(elementApartmentId);

            mockElementLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNotFound()
        {
            var elementApartmentId = 4;

            mockElementLogic.Setup(m => m.Delete(elementApartmentId)).Throws(new ArgumentException());
            
            var result = controllerElement.Delete(elementApartmentId);

            mockElementLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.Out;
using Moq;
using Model.In;
using WebApi.Controllers;
namespace WebApi.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        private List<User> usersToReturn;
        private List<User> usersToReturnEmpty;
        private User userWithId1;
        private User userToInsert;
        private Mock<IUserLogic> mockUserLogic;
        private UserController controllerUser ;

        [TestInitialize]
        public void InitVariables()
        {
            usersToReturn = new List<User>()
            {
                new User
                {
                    Id =1,
                    Name ="Yuliana",
                    Email ="ygomez@gmail.com",
                    Password ="123",
                    Role ="Admin"
                },
                new User
                {
                    Id =2,
                    Name ="Valentina",
                    Email ="vgomez@gmail.com",
                    Password ="123",
                    Role ="Chequer"
                }
            };
            usersToReturnEmpty = new List<User>();
            userWithId1 = usersToReturn.First();
            mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            controllerUser = new UserController(mockUserLogic.Object);

        }
        
        [TestMethod]
        public void TestGetAllUserOk()
        {
            mockUserLogic.Setup(m => m.GetAll()).Returns(usersToReturn);
            
            var result = controllerUser.Get();
            var okResult = result as OkObjectResult;
            var users = okResult.Value as IEnumerable<UserBasicInfoModel>;

            mockUserLogic.VerifyAll();
            Assert.IsTrue(usersToReturn.Select(m => new UserBasicInfoModel(m)).SequenceEqual(users));
        }
     
        [TestMethod]
        public void TestGetAllEmptyUsers()
        {
            mockUserLogic.Setup(m => m.GetAll()).Returns(usersToReturnEmpty);
            IEnumerable<UserBasicInfoModel> userBasicModels = usersToReturnEmpty.Select(m => new UserBasicInfoModel(m));

            var result = controllerUser.Get();

            var okResult = result as OkObjectResult;
            var userResult = okResult.Value as IEnumerable<UserBasicInfoModel>;

            Assert.IsTrue(userBasicModels.SequenceEqual(userResult));
        }
        [TestMethod]
        public void TestGetByOk()
        {
            mockUserLogic.Setup(m => m.GetById(userWithId1.Id)).Returns(userWithId1);
            UserDetailInfoModel userDetailModel = new UserDetailInfoModel(userWithId1);

            var result = controllerUser.GetBy(userWithId1.Id);

            var okResult = result as OkObjectResult;
            var userReturn = okResult.Value as UserDetailInfoModel;
            Assert.IsTrue(userReturn.Equals(userDetailModel));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetByNotFound()
        {
            int id = 7;
            ArgumentException exist = new ArgumentException();
            mockUserLogic.Setup(m => m.GetById(id)).Throws(exist);

            var result = controllerUser.GetBy(id);

            mockUserLogic.VerifyAll();
        }
        [TestMethod]
        public void TestPostOk()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123", 
                Name = "Lorena",
                Role = "admin" 
            };
            userToInsert = userModel.ToEntity();
            mockUserLogic.Setup(m => m.Add(userToInsert)).Returns(userToInsert);
            UserBasicInfoModel userBasicModel = new UserBasicInfoModel(userToInsert);

            var result = controllerUser.Post(userModel);

            var okResult = result as CreatedAtRouteResult;
            mockUserLogic.VerifyAll();
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestPostFailSameUser()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123", 
                Name = "Lorena",
                Role = "admin" 
            };
            userToInsert = userModel.ToEntity();
            Exception exist = new AggregateException();
            mockUserLogic.Setup(p => p.Add(userToInsert)).Throws(exist);

            var result = controllerUser.Post(userModel);

            mockUserLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPostFailValidation()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123"
            };
            userToInsert = userModel.ToEntity();
            ArgumentException exist = new ArgumentException();
            mockUserLogic.Setup(p => p.Add(userToInsert)).Throws(exist);

            var result = controllerUser.Post(userModel);

            mockUserLogic.VerifyAll();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPostFailServer()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123", 
                Name = "Lorena",
                Role= "admin"
                
            };
            userToInsert = userModel.ToEntity();
            Exception exist = new Exception();
            mockUserLogic.Setup(p => p.Add(userToInsert)).Throws(exist);

            var result = controllerUser.Post(userModel);

            mockUserLogic.VerifyAll();
        }

        [TestMethod]
        public void TestPutOk()
        {
            UserModel userModel = new UserModel()
            {
                
                Name ="Yuliana",
                Email ="yuliana@gmail.com",
                Password ="123",
                Role ="Admin"
            };
            userWithId1 = userModel.ToEntity();
            userWithId1.Email = "yuliana@gmail.com";
            mockUserLogic.Setup(m => m.Update(userWithId1.Id,userWithId1)).Returns(userWithId1);
           

            var result = controllerUser.Put(userWithId1.Id, userModel);

            var okResult = result as CreatedAtRouteResult;
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutFailValidate()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123"
            };
            userWithId1 = userModel.ToEntity();
            Exception exist = new ArgumentException();
            mockUserLogic.Setup(p => p.Update(userWithId1.Id,userWithId1)).Throws(exist);

            var result = controllerUser.Put(userWithId1.Id, userModel);

            mockUserLogic.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestPutFailServer()
        {
            UserModel userModel = new UserModel()
            {
                Email = "lorena@gmail.com",
                Password = "123", 
                Name = "Lorena",
                Role ="Admin"
                
            };
            userWithId1 = userModel.ToEntity();
            Exception exist = new Exception();
            mockUserLogic.Setup(p => p.Update(userWithId1.Id,userWithId1)).Throws(exist);

            var result = controllerUser.Put(userWithId1.Id, userModel);

            mockUserLogic.VerifyAll();
        }
        [TestMethod]
        public void TestDeleteByIdOk()
        {
            var userId = 1;

            mockUserLogic.Setup(m => m.Delete(userId));
    
            var result = controllerUser.Delete(userId);

            mockUserLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteNotFound()
        {
            var userId = 4;

            mockUserLogic.Setup(m => m.Delete(userId)).Throws(new ArgumentException());
            
            var result = controllerUser.Delete(userId);

            mockUserLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


    }
}

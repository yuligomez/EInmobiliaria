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
    public class UserRepositoryTest
    {
        private List<User> usersToReturn;
        private UserRepository repositoryUser;
        private DbContext context;
        private DbContextOptions options;
        private RepositoryMaster repositoryMaster;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
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

            usersToReturn.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositoryUser = new UserRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void TestGetAllUsersOk()
        {
            var result = repositoryUser.GetElements();

            Assert.IsTrue(usersToReturn.SequenceEqual(result));
        }

        [TestMethod]
        public void TestExistElement()
        {
            User user = usersToReturn.First();

            bool result = repositoryUser.ExistElement(user);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestExistElementFail()
        {
            User user = new User()
            {
                Id = 7,
                Name = "Yuliana",
                Email = "ygomez@gmail.com",
                Password = "123",
                Role = "Admin"
            };

            bool result = repositoryUser.ExistElement(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExistWithIdFail()
        {
            int userId = 56;

            bool result = repositoryUser.ExistElement(userId);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestExistById()
        {
            User user = usersToReturn.First();

            bool result = repositoryUser.ExistElement(user.Id);

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestExistByIdFail()
        {
            User user = new User() { Id = 123423 };

            bool result = repositoryUser.ExistElement(user.Id);

            Assert.IsFalse(result);
        }
        [TestMethod]
        public void TestFindOk()
        {
            User user = usersToReturn.First();

            User result = repositoryUser.Find(user.Id);

            Assert.AreEqual(result, user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFindFail()
        {
            User user = new User() { Id = 90 };

            User result = repositoryUser.Find(user.Id);
        }


        [TestMethod]
        public void TestAddOk()
        {
            User user = new User()
            {
                Name = "Vanessa Guiterrez",
                Email = "vgutierrez@gmail.com",
                Password = "123",
                Role = "Admin"
            };
            UserRepository repo = new UserRepository(this.repositoryMaster);
            int cantRepo = this.repositoryUser.GetElements().Count();

            repo.Add(user);

            Assert.AreEqual(repo.GetElements().Count(), cantRepo + 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateName()
        {
            User user = new User() { Id = 1, Name = "", Email = "mail@gmail.com" , Role = "ADMIN"};

            repositoryUser.Add(user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidatePassword()
        {
            User user = new User() { Id = 1, Name = "Nombre", Password = "" , Role = "ADMIN"};

            repositoryUser.Add(user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailValidateRole()
        {
            User user = new User() { Id = 1, Name = "Nombre", Email = "mail@gmail.com" , Role = ""};

            repositoryUser.Add(user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddFailExist()
        {
            User user = usersToReturn.First();
            ArgumentException exception = new ArgumentException();

            repositoryUser.Add(user);
        }

        [TestMethod]
        public void TestUpdateInRepo()
        {
            User user = usersToReturn.First();
            user.Email = "Newmail@gmail.com";
            user.Name = "Nuevo Nombre";
            user.Password = "New password";

            repositoryUser.Update(usersToReturn.First().Id, user);

            Assert.AreEqual(usersToReturn.First(), user);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateNoInRepo()
        {
            User user = new User();
            user.Id = 100;
            user.Email = "Newmail@gmail.com";
            user.Name = "Nuevo Nombre";
            user.Password = "New password";

            repositoryUser.Update(user.Id, usersToReturn.First());
        }

        [TestMethod]
        public void TestUpdateAll()
        {
            User user = usersToReturn.First();
            user.Email = "Newmail@gmail.com";
            user.Name = "Nuevo Nombre";
            user.Password = "New password";
            string newEmail = user.Email;

            repositoryUser.Update(usersToReturn.First(), user);

            Assert.AreEqual(user.Email, newEmail);
        }
        
        [TestMethod]
        public void TestUpdateAllButNoName()
        {
            User user = new User();
            user.Name = "";
            user.Email = "Newmail@gmail.com";
            user.Password = "New password";
            string newName = user.Name;

            repositoryUser.Update(usersToReturn.First(), user);

            Assert.AreNotEqual(usersToReturn.First().Name, newName);
        }
        [TestMethod]
        public void TestUpdateAllButNoEmail()
        {
            User user = new User();
            user.Name = "New Name";
            user.Email = "";
            user.Password = "New password";
            string newEmail = user.Email;

            repositoryUser.Update(usersToReturn.First(), user);

            Assert.AreNotEqual(usersToReturn.First().Email, newEmail);
        }
        
        [TestMethod]
        public void TestUpdateAllButNoPassword()
        {
            User user = new User();
            user.Name = "New Name";
            user.Email = "newemail@gmail.com";
            user.Password = "";
            string newPassword = user.Password;

            repositoryUser.Update(usersToReturn.First(), user);

            Assert.AreNotEqual(usersToReturn.First().Password, newPassword);
        }
        
        [TestMethod]
        public void TestNoUpdate()
        {
            User user = new User();
            user.Id = 1;
            user.Name = "";
            user.Email = "";
            user.Password = "";

            repositoryUser.Update(usersToReturn.First(), user);

            Assert.AreNotEqual(usersToReturn.First(), user);
        }


        [TestMethod]
        public void TestDelete()
        {
            User user = usersToReturn.First();
            int repoCount = this.repositoryUser.GetElements().Count();

            repositoryUser.Delete(user);

            Assert.AreEqual(repoCount - 1, repositoryUser.GetElements().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteFailExist()
        {
            User user = new User() { Id = 2342342 };

            repositoryUser.Delete(user);
        }

        [TestMethod]
        public void TestDeleteById()
        {
            User user = usersToReturn.First();
            int repoCount = this.repositoryUser.GetElements().Count();

            repositoryUser.Delete(user.Id);

            Assert.AreEqual(repoCount - 1, repositoryUser.GetElements().Count());
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteByIdFailExist()
        {
            int id = 44;

            repositoryUser.Delete(id);
        }

    }
}

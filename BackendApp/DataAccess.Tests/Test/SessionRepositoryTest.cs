using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests.Test
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private List<SessionUser> sessions;
        private RepositoryMaster repositoryMaster;
        private DbContext context;
        private DbContextOptions options;
        private SessionUserRepository repositorySession;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<SgiContext>().UseInMemoryDatabase(databaseName: "SgiDb").Options;
            this.context = new SgiContext(this.options);
            sessions = new List<SessionUser>()
            {
                new SessionUser()
                {
                    Id = 1,
                    Token = Guid.NewGuid(),
                },
                new SessionUser()
                {
                    Id = 2,
                    Token = Guid.NewGuid(),
                }
            };

            sessions.ForEach(m => this.context.Add(m));
            this.context.SaveChanges();
            repositoryMaster = new RepositoryMaster(context);
            repositorySession = new SessionUserRepository(repositoryMaster);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void TestIsCorrectTokenOk ()
        {
            Guid correctToken = sessions.First().Token;

            bool result = repositorySession.IsCorrectToken(correctToken);

            Assert.IsTrue(result);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotValidToken()
        {
            Guid notExistToken = Guid.NewGuid();

            bool result = repositorySession.IsCorrectToken(notExistToken);
        }
    }
}
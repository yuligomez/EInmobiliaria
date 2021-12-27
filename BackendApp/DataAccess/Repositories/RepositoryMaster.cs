using System;
using System.Diagnostics.CodeAnalysis;
using DataAccessInterface.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class RepositoryMaster : IDisposable
    {
        private DbContext context;
        private IRepository<User> users;
        private IRepository<Apartment> aparments;
        private IRepository<Check> cheks;
        private IRepository<Element> elements;
        private IRepository<Photo> photos;
        private IRepository<Rental> rentals;
        private IRepository<SessionUser> sessionUsers;
        public RepositoryMaster(DbContext masterContext)
        {
            this.context = masterContext;
        }
        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            this.context.Dispose();
        }
        public IRepository<User> Users
        {
            get
            {
                if (users == null)
                {
                    this.users = new Repository<User>(context);
                }
                return this.users;
            }
        }
        public IRepository<Apartment> Apartments
        {
            get
            {
                if (aparments == null)
                {
                    this.aparments = new Repository<Apartment>(context);
                }
                return this.aparments;
            }
        }
        public IRepository<Check> Checks
        {
            get
            {
                if (cheks == null)
                {
                    this.cheks = new Repository<Check>(context);
                }
                return this.cheks;
            }
        }
        public IRepository<Element> Elements
        {
            get
            {
                if (elements == null)
                {
                    this.elements = new Repository<Element>(context);
                }
                return this.elements;
            }
        }
        public IRepository<Rental> Rentals
        {
            get
            {
                if (rentals == null)
                {
                    this.rentals = new Repository<Rental>(context);
                }
                return this.rentals;
            }
        }
        public  IRepository<SessionUser> SessionUsers
        {
            get
            {
                if (sessionUsers == null)
                {
                    this.sessionUsers = new Repository<SessionUser>(context);
                }
                return this.sessionUsers;
            }
        }
        public IRepository<Photo> Photos
        {
            get
            {
                if (photos == null)
                {
                    this.photos = new Repository<Photo>(context);
                }
                return this.photos;
            }
        }
    }
}
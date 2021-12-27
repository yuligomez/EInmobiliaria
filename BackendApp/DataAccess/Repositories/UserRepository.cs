using System;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class UserRepository : AccessData<User>,IUserRepository 
    {

        public UserRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Users;
        }

        public User FindInRepository(string email)
        {
            User findByEmail = null;
            foreach(var p in this.repository.GetElementsInContext())
            {
                if (p.Email == email)
                {
                   findByEmail = p;
                }
            }
            return findByEmail;;
        }

        public override void Update(User elementToUpdate, User element)
        {
           elementToUpdate.Update(element);
        }

        public override void Validate(User element)
        {
            User emailUniq = this.FindInRepository(element.Email);
            if (emailUniq!=null)
            {
                throw new ArgumentException("The email already exist");
            }
            bool nameNull = element.Name=="";
            if (nameNull)
            {
                throw new ArgumentException("Can't create a user without name");
            }
            bool passwordNull = element.Password=="";
            if (passwordNull)
            {
                throw new ArgumentException("Can't create a user without password");
            }
            bool roleNull = element.Role=="";
            if (roleNull)
            {
                throw new ArgumentException("Can't create a user without role");
            }
        }
    }
}
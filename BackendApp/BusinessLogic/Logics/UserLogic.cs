using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User Add(User user)
        {
            User userAdded = this.userRepository.Add(user);
            return userAdded;
        }

        public void Delete(int id)
        {
            this.userRepository.Delete(id);
        }
        public IEnumerable<User> GetAll()
        {
            return this.userRepository.GetElements();
        }

        public User GetById(int id)
        {
            return this.userRepository.Find(id);
        }

        public User Update(int id, User user)
        {
            User userBd = this.userRepository.Find(id);
            userBd.Update(user);
            this.userRepository.Update(id, userBd);
            return userBd;
        }

        public bool Exist(User user)
        {
            return this.userRepository.ExistElement(user);
        }
    }
}
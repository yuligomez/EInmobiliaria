using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User user);
        User Update(int id, User user);
        void Delete(int id);
        bool Exist(User user);
    }
}
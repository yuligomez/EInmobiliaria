using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface ICheckLogic
    {
        IEnumerable<Check> GetAll();
        Check GetById(int id);
        Check Add(Check check);
        Check Update(int id, Check check);
        void Delete(int id);
        bool Exist(Check check);
    }
}
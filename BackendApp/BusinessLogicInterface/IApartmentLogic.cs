using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface IApartmentLogic
    {
        IEnumerable<Apartment> GetAll();
        Apartment GetById(int id);
        Apartment Add(Apartment apartment);
        Apartment Update(int id, Apartment apartment);
        void Delete(int id);
        bool Exist(Apartment apartment);
    }
}
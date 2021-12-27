using System;
using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface IRentalLogic
    {
        IEnumerable<Rental> GetAll(DateTime date);
        Rental GetById(int id);
        Rental Add(Rental rental);
        Rental Update(int id, Rental rental);
        void Delete(int id);
        bool Exist(Rental rental);
    }
}
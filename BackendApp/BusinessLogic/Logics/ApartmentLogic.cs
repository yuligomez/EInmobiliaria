using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class ApartmentLogic : IApartmentLogic
    {
        private readonly IApartmentRepository apartmentRepository;
        public ApartmentLogic(IApartmentRepository apartmentRepository)
        {
            this.apartmentRepository = apartmentRepository;
        }
        public Apartment Add(Apartment apartment)
        {
            Apartment apartmentAdded = this.apartmentRepository.Add(apartment);
            return apartmentAdded;
        }

        public void Delete(int id)
        {
            this.apartmentRepository.Delete(id);
        }

        public IEnumerable<Apartment> GetAll()
        {
            return this.apartmentRepository.GetElements();
        }

        public Apartment GetById(int id)
        {
            return this.apartmentRepository.Find(id);
        }

        public Apartment Update(int id, Apartment apartment)
        {
            Apartment apartmentBd = this.apartmentRepository.Find(id);
            apartmentBd.Update(apartment);
            this.apartmentRepository.Update(id, apartmentBd);
            return apartmentBd;
        }

        public bool Exist(Apartment apartment)
        {
            return this.apartmentRepository.ExistElement(apartment);
        }
    }
}
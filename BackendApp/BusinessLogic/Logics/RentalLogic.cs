using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class RentalLogic : IRentalLogic
    {
        private readonly IRentalRepository rentalRepository;
        private readonly IApartmentRepository apartmentRepository;
        public RentalLogic(IRentalRepository rentalRepository, IApartmentRepository apartmentRepository)
        {
            this.rentalRepository = rentalRepository;
            this.apartmentRepository = apartmentRepository;
        }
        public Rental Add(Rental rental)
        {
            Apartment apartment = ValidateApartment(rental.ApartmentId);
            rental.Apartment  = apartment;
            Rental rentalAdded = this.rentalRepository.Add(rental);
            return rentalAdded;
        }

        public void Delete(int id)
        {
            this.rentalRepository.Delete(id);
        }

        public IEnumerable<Rental> GetAll(DateTime date)
        {
            List<Rental> rentals =  this.rentalRepository.GetElements();
            rentals = rentals.FindAll(x => x.EndingDate < date && !x.HasCheck);
            return rentals;
        }

        public Rental GetById(int id)
        {
            return this.rentalRepository.Find(id);
        }

        public Rental Update(int id, Rental rental)
        {
            Rental rentalBd = this.rentalRepository.Find(id);
            rentalBd.Update(rental);
            this.rentalRepository.Update(id, rentalBd);
            return rentalBd;
        }

        public bool Exist(Rental rental)
        {
            return this.rentalRepository.ExistElement(rental);
        }

        public Apartment ValidateApartment(int apartmentId)
        {
            Apartment findApartment = this.apartmentRepository.Find(apartmentId);
            return findApartment;
        }
    }
}
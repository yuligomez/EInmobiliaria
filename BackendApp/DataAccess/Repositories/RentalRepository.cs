using System;
using System.Collections.Generic;
using DataAccess.Repositories;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class RentalRepository : AccessData<Rental>, IRentalRepository
    {
        public RentalRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Rentals;
        }
        public override void Update(Rental elementToUpdate, Rental element)
        {
            elementToUpdate.Update(element);
        }

        public override void Validate(Rental element)
        {
            List<Rental> rentsWithApartment = this.repository.GetElementsInContext().FindAll(x => x.ApartmentId == element.ApartmentId);
            bool sameDate = rentsWithApartment.FindAll(x => x.StartDate == element.StartDate || x.EndingDate == element.EndingDate).Count  > 0;
            bool betweenRent = rentsWithApartment.FindAll(x => x.StartDate < element.StartDate && x.EndingDate > element.EndingDate).Count  > 0;
            bool oneDateBetween = rentsWithApartment.FindAll(x => x.StartDate > element.StartDate && x.EndingDate < element.EndingDate || x.StartDate < element.StartDate && x.EndingDate > element.EndingDate ).Count  > 0;
            if (sameDate || betweenRent || oneDateBetween)
            {
                throw new ArgumentException("Hay un alquiler para esas fechas, ingrese otra");
            }
        }
    }
}
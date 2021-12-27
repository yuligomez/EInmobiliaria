using System;
using System.Collections.Generic;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class CheckRepository : AccessData<Check>, ICheckRepository
    {
        IRepository<Apartment> apartmentRepository;
        public CheckRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Checks;
            this.apartmentRepository = repositoryMaster.Apartments;
        }
        public override void Update(Check elementToUpdate, Check element)
        {
            elementToUpdate.Update(element);
        }

        public override void Validate(Check element)
        {
            if (FindApartmentInChecks(element.ApartmentId))
            {
                throw new ArgumentException("The apartment with id "+element.ApartmentId+" is already been checked");
            }
        }

        private bool FindApartmentInChecks(int idApartment)
        {
            bool find = false;
            List<Check> listElements = this.repository.GetElementsInContext();
            find = listElements.Find(x => x.ApartmentId == idApartment && x.State!="Done") != null;
            return find;
        }
    }
}
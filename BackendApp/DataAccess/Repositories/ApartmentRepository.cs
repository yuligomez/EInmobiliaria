using System;
using DataAccess.Repositories;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class ApartmentRepository : AccessData<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Apartments;
        }
        public override void Update(Apartment elementToUpdate, Apartment element)
        {
            elementToUpdate.Update(element);
        }

        public override void Validate(Apartment element)
        {
            if(element.Name=="") 
            {
                throw new ArgumentException("El apartamento necesita al menos un nombre");
            }
        }
    }
}
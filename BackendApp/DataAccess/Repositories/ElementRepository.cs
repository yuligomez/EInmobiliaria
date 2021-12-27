using System;
using DataAccess.Repositories;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class ElementRepository : AccessData<Element>, IElementRepository
    {
        public ElementRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Elements;
        }
        public override void Update(Element elementToUpdate, Element element)
        {
            elementToUpdate.Update(element);
        }

        public override void Validate(Element element)
        {
            if(element.Name=="") throw new ArgumentException("El elemento debe tener un nombre");
            if(element.Amount<1) throw new ArgumentException("El elemento debe tener minimo 1 como unidad");
            if(element.IsBroken && element.PhotoId == 0)
            {
                throw new ArgumentException("Si el elemento esta roto, tiene que tener una foto asociada");
            }
            if(element.ApartmentId<1) throw new ArgumentException("Tiene que tener asociado un apartamento");
            if(element.UserId<1)  throw new ArgumentException("Tine que estar asociado a un usuario");
        }
    }
}
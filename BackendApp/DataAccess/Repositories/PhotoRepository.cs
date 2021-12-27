using System;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class PhotoRepository : AccessData<Photo>, IPhotoRepository
    {
        public PhotoRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.Photos;
        }
        public override void Update(Photo elementToUpdate, Photo element)
        {
            elementToUpdate.Update(element);
        }

        public override void Validate(Photo element)
        {
            if(element.Name == "")
            {
                throw new ArgumentException("Tiene que haber un nombre para guardar la foto");
            }
        }
    }
}
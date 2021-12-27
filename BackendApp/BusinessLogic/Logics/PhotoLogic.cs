using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class PhotoLogic : IPhotoLogic
    {
        private readonly IPhotoRepository photoRepository;
        public PhotoLogic(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }
        public Photo Add(Photo photo)
        {
            Photo photoAdded = this.photoRepository.Add(photo);
            return photoAdded;
        }

        public void Delete(int id)
        {
            this.photoRepository.Delete(id);
        }

        public IEnumerable<Photo> GetAll()
        {
            return this.photoRepository.GetElements();
        }

        public Photo GetById(int id)
        {
            return this.photoRepository.Find(id);
        }

        public Photo Update(int id, Photo photo)
        {
            Photo photoBd = this.photoRepository.Find(id);
            photoBd.Update(photo);
            this.photoRepository.Update(id, photoBd);
            return photoBd;
        }

        public bool Exist(Photo photo)
        {
            return this.photoRepository.ExistElement(photo);
        }
    }
}
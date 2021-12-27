using System.Collections.Generic;
using Domain.Entities;

namespace BusinessLogicInterface
{
    public interface IPhotoLogic
    {
        IEnumerable<Photo> GetAll();
        Photo GetById(int id);
        Photo Add(Photo photo);
        Photo Update(int id, Photo photo);
        void Delete(int id);
        bool Exist(Photo photo);
    }
}
using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class ElementLogic : IElementLogic
    {
        private readonly IElementRepository elementRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly IApartmentRepository apartmentRepository;
        private readonly IUserRepository userRepository;
        public ElementLogic(
            IElementRepository elementRepository,
            IPhotoRepository photoRepository,
            IApartmentRepository apartmentRepository,
            IUserRepository userRepository)
        {
            this.elementRepository = elementRepository;
            this.photoRepository = photoRepository;
            this.apartmentRepository = apartmentRepository;
            this.userRepository = userRepository;
        }
        public Element Add(Element element)
        {
            Apartment  apartment  = ValidateApartment(element.ApartmentId);
            User user= ValidateUser(element.UserId);
            if(element.IsBroken && element.Photo.Name != "")
            {
                Photo photo  = new Photo()
                {
                    Name = element.Photo.Name,
                    Image = element.Photo.Image
                };
                photo = this.photoRepository.Add(photo);
                element.Photo = photo;
            }
            element.Apartment = apartment;
            element.User = user;
            Element elementAdded = this.elementRepository.Add(element);
            return elementAdded;
        }

        public void Delete(int id)
        {
            this.elementRepository.Delete(id);
        }
        public IEnumerable<Element> GetAll()
        {
            return this.elementRepository.GetElements();
        }

        public Element GetById(int id)
        {
            return this.elementRepository.Find(id);
        }

        public Element Update(int id, Element element)
        {
            Element elementBd = this.elementRepository.Find(id);
            elementBd.Update(element);
            if(element.ApartmentId>0)
            {
                elementBd.Apartment = ValidateApartment(elementBd.ApartmentId);
            }
            if(element.UserId>0)
            {
                elementBd.User = ValidateUser(elementBd.UserId);
            }
            this.elementRepository.Update(id, elementBd);
            return elementBd;
        }

        public bool Exist(Element element)
        {
            return this.elementRepository.ExistElement(element);
        }

        public Apartment ValidateApartment(int apartmentId)
        {
            Apartment apartment = this.apartmentRepository.Find(apartmentId);
            return apartment;
        }

        public User ValidateUser(int userId)
        {
            User user  = this.userRepository.Find(userId);
            return user;
        }
    }
}
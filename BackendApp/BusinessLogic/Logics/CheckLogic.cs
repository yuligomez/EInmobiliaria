using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class CheckLogic : ICheckLogic
    {
        private readonly ICheckRepository checkRepository;
        private readonly IApartmentRepository apartmentRepository;
        private readonly IUserRepository userRepository;
        public CheckLogic(ICheckRepository checkRepository, IApartmentRepository apartmentRepository, IUserRepository userRepository)
        {
            this.checkRepository = checkRepository;
            this.apartmentRepository = apartmentRepository;
            this.userRepository = userRepository;
        }
        public Check Add(Check check)
        {
            Apartment apartment  =  ValidateApartment(check.ApartmentId);
            apartment.State = check.State;
            this.apartmentRepository.Update(apartment.Id, apartment);
            check.Apartment = apartment;
            User user  =  ValidateUser(check.UserId);
            check.User = user;
            Check checkAdded = this.checkRepository.Add(check);
            return checkAdded;
        }

        public void Delete(int id)
        {
            this.checkRepository.Delete(id);
        }
        public IEnumerable<Check> GetAll()
        {
            return this.checkRepository.GetElements();
        }

        public Check GetById(int id)
        {
            return this.checkRepository.Find(id);
        }

        public Check Update(int id, Check check)
        {
            Check checkBd = this.checkRepository.Find(id);
            if( check.UserId > 0)
            {
                User user = ValidateUser(check.UserId);
                check.User = user;
            }
            checkBd.Update(check);
            Apartment apartmentBD = this.apartmentRepository.Find(checkBd.ApartmentId);
            apartmentBD.State = checkBd.State;
            this.apartmentRepository.Update(checkBd.ApartmentId,apartmentBD);
            checkBd.Apartment = apartmentBD;
            this.checkRepository.Update(id, checkBd);
            return checkBd;
        }

        public bool Exist(Check check)
        {
            return this.checkRepository.ExistElement(check);
        }

        public Apartment ValidateApartment(int apartmentId)
        {
            Apartment findApartment = this.apartmentRepository.Find(apartmentId);
            return findApartment;
            
        }

        public User ValidateUser(int userId)
        {
            User findUser = this.userRepository.Find(userId);
            return findUser;
        }
    }
}
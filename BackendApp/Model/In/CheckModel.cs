using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Model.Out;

namespace Model
{
    public class CheckModel
    {
        private string CONST_STATE = "UNDONE";
        public List<int> RentalsId { get; set; }
        public List<int> ApartmentsId { get; set;}
        public int UserId { get; set; }
        public IEnumerable<Check> ToEntityCheck()
        {
            if (this.AnyValueEmpty())
            {
                throw new ArgumentException("Se necesita al menos una renta");
            }
            IEnumerable<Check> returnCheck = new List<Check>();
            var checks = this.ApartmentsId.Select( x => 
            {
                Check check = new Check()
                {
                    ApartmentId = x,
                    CheckDate = DateTime.Today,
                    State = CONST_STATE,
                    UserId = UserId
                };  
                return check;
            });
            return checks;
        }
        public IEnumerable<Rental> ToEntityRental()
        {
            if (this.AnyValueEmpty())
            {
                throw new ArgumentException("Se necesita al menos una renta");
            }
            IEnumerable<Rental> returnRental = new List<Rental>();
            var rentals = this.RentalsId.Select( x => 
            {
                Rental rental = new Rental()
                {
                    Id = x,
                    HasCheck = true,
                };  
                return rental;
            });
            return rentals;
        }
        public bool AnyValueEmpty()
        {
            bool rentalEmpty = this.RentalsId.Count() < 1;
            return rentalEmpty;
        }
    }
}
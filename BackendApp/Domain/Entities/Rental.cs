using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Rental
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HasCheck { get; set; }

        public void Update(Rental rental)
        {
            if(rental.StartDate != default) this.StartDate = rental.StartDate;
            if(rental.EndingDate != default && rental.StartDate < rental.EndingDate) this.EndingDate = rental.EndingDate;
            if(rental.ApartmentId > 0) this.ApartmentId = rental.ApartmentId;
            if(rental.HasCheck) this.HasCheck = rental.HasCheck;            
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is Rental rental)
            {
                result =  (this.ApartmentId == rental.ApartmentId) &&  (this.StartDate== rental.StartDate)  && (this.EndingDate == rental.EndingDate);
            }
            return result;
        }
    }
}
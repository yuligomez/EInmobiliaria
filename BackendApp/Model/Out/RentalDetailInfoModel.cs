using System;
using Domain.Entities;

namespace Model.Out
{
    public class RentalDetailInfoModel
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public virtual ApartmentBasicInfoModel Apartment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HasCheck { get; set; }

        public RentalDetailInfoModel(Rental rental)
        {
            this.Id = rental.Id;
            this.Apartment = new ApartmentBasicInfoModel(rental.Apartment);
            this.ApartmentId = rental.ApartmentId;
            this.StartDate = rental.StartDate;
            this.EndingDate = rental.EndingDate;
            this.HasCheck = rental.HasCheck;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is RentalDetailInfoModel rental)
            {
                result = this.ApartmentId == rental.ApartmentId &&
                    this.StartDate == rental.StartDate &&
                    this.EndingDate == rental.EndingDate;
            }

            return result;
        } 
    }
}
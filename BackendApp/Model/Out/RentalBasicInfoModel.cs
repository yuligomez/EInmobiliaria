using System;
using Domain.Entities;

namespace Model.Out
{
    public class RentalBasicInfoModel
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public bool HasCheck { get; set; }

        public RentalBasicInfoModel(Rental rental)
        {
            this.Id = rental.Id;
            this.ApartmentId = rental.ApartmentId;
            this.StartDate = rental.StartDate;
            this.EndingDate = rental.EndingDate;
            this.HasCheck = rental.HasCheck;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is RentalBasicInfoModel rental)
            {
                result = this.ApartmentId == rental.ApartmentId &&
                    this.StartDate == rental.StartDate &&
                    this.EndingDate == rental.EndingDate;
            }

            return result;
        } 

    }
}
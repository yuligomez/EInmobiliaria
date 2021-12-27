using System;
using Domain.Entities;
using Model.Out;

namespace Model.In
{
    public class RentalModel
    {
        public int ApartmentId { get; set; }
        public string StartDate { get; set; }
        public string EndingDate { get; set; }
        public Rental ToEntity(bool post = true)
        {
            Rental rental = new Rental()
            {
                ApartmentId = this.ApartmentId,
                StartDate = ConvertStringToDate(this.StartDate),
                EndingDate = ConvertStringToDate(this.EndingDate),  
            };
            if(post) rental.HasCheck = !post;
            return rental;
        }

        private DateTime ConvertStringToDate(string date)
        {
            try
            {
                DateTime valueDate = DateTime.Parse(date);
                return valueDate;
            }
            catch(Exception)
            {
                throw new ArgumentException("Date is not in the correct format");
            }
        }
    }
}
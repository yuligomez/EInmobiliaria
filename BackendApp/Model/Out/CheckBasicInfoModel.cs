using System;
using Domain.Entities;

namespace Model
{
    public class CheckBasicInfoModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime CheckDate { get; set; }
        public string State { get; set; }

        public CheckBasicInfoModel(Check check)
        {
            this.Id = check.Id;
            this.UserId = check.UserId;
            this.ApartmentId = check.ApartmentId;
            this.CheckDate = check.CheckDate;
            this.State = check.State;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is CheckBasicInfoModel check)
            {
                result = this.UserId == check.UserId && this.ApartmentId == check.ApartmentId
                    && this.CheckDate == check.CheckDate && this.State == check.State;
            }

            return result;
        }  
    }
}
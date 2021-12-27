using System;
using Domain.Entities;
using Model.Out;

namespace Model
{
    public class CheckDetailInfoModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual UserBasicInfoModel User { get; set; }
        public int ApartmentId { get; set; }
        public virtual ApartmentBasicInfoModel Apartment { get; set; }
        public DateTime CheckDate { get; set; }
        public string State { get; set; }
        public CheckDetailInfoModel(Check check)
        {
            this.Id = check.Id;
            this.UserId = check.UserId;
            this.User = new UserBasicInfoModel(check.User);
            this.ApartmentId = check.ApartmentId;
            this.Apartment = new ApartmentBasicInfoModel(check.Apartment);
            this.CheckDate = check.CheckDate;
            this.State = check.State;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is CheckDetailInfoModel check)
            {
                result = this.Id == check.Id ;
            }

            return result;
        }
    }
}
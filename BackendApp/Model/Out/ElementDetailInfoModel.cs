using Domain.Entities;
using Model.Out;

namespace Model
{
    public class ElementDetailInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool IsBroken { get; set; }
        public int? PhotoId { get; set; }
        public virtual PhotoBasicInfoModel Photo { get; set; }   
        public int ApartmentId { get; set; }
        public virtual ApartmentBasicInfoModel Apartment {get;set;}
        public int UserId { get; set; } 
        public virtual  UserBasicInfoModel User {get; set;}
        public ElementDetailInfoModel(Element element)
        {
            this.Id = element.Id;
            this.Name = element.Name;
            this.Amount = element.Amount;
            this.IsBroken = element.IsBroken;
            if(this.IsBroken)
            {
                this.PhotoId = element.PhotoId;
                this.Photo = new PhotoBasicInfoModel(element.Photo);
            }
            this.ApartmentId = element.ApartmentId;
            this.Apartment = new ApartmentBasicInfoModel(element.Apartment);
            this.UserId = element.UserId;
            this.User = new UserBasicInfoModel(element.User);
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is ElementDetailInfoModel element)
            {
                result = this.Name == element.Name
                    && this.IsBroken == element.IsBroken 
                    && this.ApartmentId == element.ApartmentId
                    && this.UserId == element.UserId;
            }

            return result;
        }         
    }
}
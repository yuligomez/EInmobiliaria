using System;
using Domain.Entities;

namespace Model.In
{
    public class ElementModel
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool IsBroken { get; set; }
        public string PhotoName { get; set; }   
        public int Image {get; set;} 
        public int ApartmentId { get; set; }
        public int UserId { get; set; } 
        public Element ToEntity(bool post = true)
        {
            if (post && this.AnyValueEmpty())
            {
                throw new ArgumentException("Se necesita nombre y cantidad");
            }
            Element elementModel =  new Element()
            {
    
                Name = this.Name,
                Amount = this.Amount,
                IsBroken = this.IsBroken,
                ApartmentId = this.ApartmentId,
                UserId = this.UserId
            };
            if (elementModel.IsBroken)
            {
                elementModel.Photo = new Photo();
                elementModel.Photo.Name = this.PhotoName;
                elementModel.Photo.Image = this.Image;
            }
            return elementModel;
        }
        public bool AnyValueEmpty()
        {
            bool nameEmpty = this.Name == "";
            bool amountEmpty = this.Amount < 1;
            bool isBroken = this.IsBroken && this.PhotoName == "";
            return nameEmpty || amountEmpty  || isBroken;
        }   
    }
}
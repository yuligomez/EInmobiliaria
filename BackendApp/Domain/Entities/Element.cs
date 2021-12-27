using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Element
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool IsBroken { get; set; }
        public int? PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public void Update(Element element)
        {
            if(element.Name != "") this.Name = element.Name;
            if(element.Amount > 0) this.Amount = element.Amount;
            if(this.PhotoId > 0 && element.PhotoId > 0 && element.Photo.Name != "") 
            {
                this.Photo.Name = element.Photo.Name;
            }
            if(element.ApartmentId > 0) this.ApartmentId = element.ApartmentId;
            if(element.UserId > 0) this.UserId = element.UserId;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is Element element)
            {
                result = this.Name == element.Name 
                    && this.Amount == element.Amount
                    && this.IsBroken == element.IsBroken 
                    && this.ApartmentId == element.ApartmentId 
                    && this.UserId == element.UserId ;
            }
            return result;
        }
    }
}
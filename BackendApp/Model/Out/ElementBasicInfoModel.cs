using Domain.Entities;

namespace Model.Out
{
    public class ElementBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public bool IsBroken { get; set; }
        public int? PhotoId { get; set; }
        public virtual PhotoBasicInfoModel Photo { get; set; }   
        public int ApartmentId { get; set; }
        public int UserId { get; set; } 
        public ElementBasicInfoModel(Element element)
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
            this.UserId = element.UserId;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is ElementBasicInfoModel element)
            {
                result = this.Name == element.Name
                    && this.IsBroken == element.IsBroken 
                    && (this.IsBroken && this.PhotoId==element.PhotoId)
                    && this.ApartmentId == element.ApartmentId
                    && this.UserId == element.UserId;
            }

            return result;
        }          
    }
}
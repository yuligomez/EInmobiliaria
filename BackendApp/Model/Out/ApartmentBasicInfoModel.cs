using Domain.Entities;

namespace Model.Out
{
    public class ApartmentBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }

        public ApartmentBasicInfoModel(Apartment apartment)
        {
            this.Id = apartment.Id;
            this.Name = apartment.Name;
            this.Latitude = apartment.Latitude;
            this.Longitude = apartment.Longitude;
            this.Description = apartment.Description;
            this.State = apartment.State;
        }

        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is ApartmentBasicInfoModel apartment)
            {
                result = this.Name.Equals(apartment.Name) && 
                this.Latitude.Equals(apartment.Latitude) && 
                this.Longitude.Equals(apartment.Longitude);
            }

            return result;
        }        
    }
}
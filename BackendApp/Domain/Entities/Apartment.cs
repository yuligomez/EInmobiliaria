using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is Apartment apartment)
            {
                result = (this.Name == apartment.Name)  
                    && (this.Latitude == apartment.Latitude)
                    && (this.Longitude == apartment.Longitude);
            }
            return result;
        }
        public void Update(Apartment apartment)
        {
            if(apartment.Name != "") this.Name = apartment.Name;
            if(apartment.Latitude != "") this.Latitude = apartment.Latitude;
            if(apartment.Longitude != "") this.Longitude = apartment.Longitude;      
            if(apartment.Description != "") this.Description = apartment.Description;   
            if(apartment.State != "") this.State = apartment.State;           
        }

    }
}
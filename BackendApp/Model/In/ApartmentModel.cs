using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Model.In
{
    public class ApartmentModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }
        public Apartment ToEntity(bool post = true)
        {
            if (post && this.AnyValueEmpty())
            {
                throw new ArgumentException("You need to put all values: name, latitute and longitude");
            }
            return new Apartment()
            {
                Name = this.Name,
                Latitude = this.Latitude, 
                Longitude = this.Longitude, 
                Description = this.Description, 
                State = "REGISTERED"      
            }; 
        }
        public bool AnyValueEmpty()
        {
            bool nameEmpty = this.Name == "";
            bool latitudeEmpty = this.Latitude == "";
            bool longitudeEmpty = this.Longitude == "";
            return longitudeEmpty || latitudeEmpty || nameEmpty ;
        }
    }
}
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Street_location { get; set; }
        public string Number_location { get; set; }
        public virtual List<Content> Contents { get; set; }
        public virtual List<Rental> Rentals { get; set; }
        public virtual List<Photo> Photos { get; set; }


    }
}
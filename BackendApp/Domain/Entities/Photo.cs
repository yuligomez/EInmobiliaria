using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public int Image { get; set; }
        public void Update(Photo photo)
        {
            if(photo.Name != "") this.Name = photo.Name;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is Photo photo)
            {
                result = this.Name == photo.Name  ;
            }
            return result;
        }
    }
}
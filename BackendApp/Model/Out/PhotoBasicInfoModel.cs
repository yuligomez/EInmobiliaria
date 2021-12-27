using Domain.Entities;

namespace Model.Out
{
    public class PhotoBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public int Image { get; set; }  
        public PhotoBasicInfoModel(Photo element)
        {
            this.Id = element.Id;
            this.Name = element.Name;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is PhotoBasicInfoModel element)
            {
                result = this.Name == element.Name;
            }

            return result;
        }    
    }
}
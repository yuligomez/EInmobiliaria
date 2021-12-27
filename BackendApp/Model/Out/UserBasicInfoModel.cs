using Domain.Entities;

namespace Model.Out
{
    public class UserBasicInfoModel
    {
        public int Id { get; private set; }
        public string Name {get; set;}
        public string Email {get; set;}
        public string Role {get; set;}

        public UserBasicInfoModel(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Role = user.Role;
        }
        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is UserBasicInfoModel user)
            {
                result = this.Id == user.Id;
            }

            return result;
        }        
    }
}
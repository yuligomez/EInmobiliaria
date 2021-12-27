using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Model.Out
{
    public class UserDetailInfoModel
    {
        public int Id { get; private set; }
        public string Name {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}

        public UserDetailInfoModel(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Role = user.Role; 
        }

        public override bool Equals(object obj)
        {
            var result = false;
            
            if(obj is UserDetailInfoModel user)
            {
                result = this.Id == user.Id && this.Name.Equals(user.Name);
            }

            return result;
        }
    }
}
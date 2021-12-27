using System;
using Domain.Entities;

namespace Model.In
{
    public class UserModel
    {
        public string Name {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public string Role {get; set;}
        public User ToEntity(bool post = true)
        {
            if (post && this.AnyValueEmpty())
            {
                throw new ArgumentException("You need to put all values: email, password, name, role");
            }
            return new User()
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password,
                Role = this.Role 
            };
        }
        public bool AnyValueEmpty()
        {
            bool emailEmpty = this.Email == "";
            bool passwordEmpty = this.Password == "";
            bool nameEmpty = this.Name == "";
            bool roleEmpty = this.Role == "";
            return emailEmpty || passwordEmpty || nameEmpty || roleEmpty;
        }
    }

        
}
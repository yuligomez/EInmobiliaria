using System;
using Domain.Entities;

namespace Model.In
{
    public class SessionModel
    {
        public string Email {get; set;}
        public string Password {get; set;}
        public User ToEntity()
        {
            if (this.AnyValueEmpty())
            {
                throw new ArgumentException("You need to put all values: email and password");
            }
            return new User()
            {
                Email = this.Email,
                Password = this.Password,
            };
        }
        public bool AnyValueEmpty()
        {
            bool emailEmpty = this.Email == "";
            bool passwordEmpty = this.Password == "";
            return emailEmpty || passwordEmpty;
        }

    }
}
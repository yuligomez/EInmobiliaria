using System;
using Domain.Entities;

namespace Model.In
{
    public class CheckPutModel
    {
        public int UserId { get; set; }
        public string State { get; set; }
        public Check ToEntity()
        {
            if (this.AnyValueEmpty())
            {
                throw new ArgumentException("Se necesita usario y estado");
            }
            return new Check()
            {
                UserId = this.UserId,
                State = this.State
            };  
        }
        public bool AnyValueEmpty()
        {
            bool userIdEmpty = this.UserId < 1;
            bool stateEmpty = this.State == "";
            return userIdEmpty || stateEmpty;
        }
    }
}
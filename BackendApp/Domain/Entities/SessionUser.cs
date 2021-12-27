using System;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class SessionUser
    {
        public int Id {get; set;}
        public Guid Token   {get; set;}
        public int UserId  {get; set;}
        public string Role  {get; set;}
        public virtual User User {get; set;}
       
        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is SessionUser sessionUser)
            {
                result = this.Id == sessionUser.Id ;
            }
            return result;
        }

         public void Update(Guid token)
        {
            this.Token = token;
        }

    }
}
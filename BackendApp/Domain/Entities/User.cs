using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public override bool Equals(object obj)
        {
            var result = false;
            if(obj is User user)
            {
                result =  (this.Id == user.Id) &&  (this.Name== user.Name)  && (this.Password == user.Password) && (this.Role == user.Role) ;
            }
            return result;
        }

        public void Update(User user)
        {
            if(user.Password != "") this.Password = user.Password;
            if(user.Email != "") this.Email = user.Email;
            if(user.Name != "") this.Name = user.Name;
        }
    }
}
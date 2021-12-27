using Domain.Entities;

namespace DataAccessInterface.Repositories
{
    public interface IUserRepository : IAccessData<User>
    {
        User FindInRepository(string email);
    }
    
}
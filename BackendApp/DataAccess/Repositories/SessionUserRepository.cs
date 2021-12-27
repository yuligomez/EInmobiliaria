using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccessInterface.Repositories;
using Domain.Entities;

namespace DataAccess.Repositories
{
    public class SessionUserRepository : AccessData<SessionUser>, ISessionUserRepository
    {
        public SessionUserRepository(RepositoryMaster repositoryMaster)
        {
            this.repository = repositoryMaster.SessionUsers;
        }
        public bool IsCorrectToken(Guid token)
        {
            var result = this.repository.GetElementsInContext();
            var resultToReturn = result.Where(kz=>kz.Token==token);
            if (resultToReturn.Count() == 0)
            {
                throw new ArgumentException("No user with that id ");
            }
            return true;
        }
        [ExcludeFromCodeCoverage]
        public override void Update(SessionUser elementToUpdate, SessionUser element)
        {
        }
        [ExcludeFromCodeCoverage]
        public override void Validate(SessionUser element)
        {
        }
    }
}
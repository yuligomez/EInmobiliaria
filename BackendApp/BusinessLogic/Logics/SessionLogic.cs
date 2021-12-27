using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterface.Repositories;
using SessionInterface;
using Domain.Entities;

namespace BusinessLogic.Logics
{
    public class SessionLogic : ISessionLogic
    {
        private readonly ISessionUserRepository sessionUserRepository;
        private readonly IUserRepository userRepository;
        public SessionLogic(ISessionUserRepository sessionUserRepository, IUserRepository userRepository)
        {
            this.sessionUserRepository = sessionUserRepository;
            this.userRepository = userRepository;
        }

        public bool IsCorrectToken(Guid token)
        {
            return this.sessionUserRepository.IsCorrectToken(token);

        }
        public Guid Login(User user)
        {
            IEnumerable<User> userResult = this.userRepository.GetElements().Where(p => p.Email == user.Email && p.Password == user.Password);
            if (userResult.Count() == 0)
            {
                throw new ArgumentException("Email or password was not valid ");
            }
            user = userResult.First();
            Guid guid = Guid.NewGuid();
            IEnumerable<SessionUser> sessions = this.sessionUserRepository.GetElements().Where(m => m.UserId == user.Id);
            if (sessions.Count() == 0)
            {
                SessionUser newSession = new SessionUser()
                {
                    Token = guid,
                    UserId = user.Id
                };
                this.sessionUserRepository.Add(newSession);
            }
            else
            {
                SessionUser session = sessions.First();
                session.Token = guid;
                this.sessionUserRepository.Update(session.Id , session);
            }
            return guid;
        }
    }
}
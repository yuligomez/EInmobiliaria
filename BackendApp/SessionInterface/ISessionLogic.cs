using System;
using Domain.Entities;

namespace SessionInterface
{
    public interface ISessionLogic
    {
        bool IsCorrectToken(Guid token);

        Guid Login(User user) ;
    }
}

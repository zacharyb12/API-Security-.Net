using Models.Auth;
using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Auth
{
    public interface IAuthRepository
    {
        Task<Guid> Register(AddUser user);

        Task<Guid> Login(LoginForm login);
    }
}

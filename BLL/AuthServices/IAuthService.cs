using Models.Auth;
using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AuthServices
{
    public interface IAuthService
    {
        Task<string> Register(AddUser user);

        Task<string> Login(LoginForm login);
    }
}

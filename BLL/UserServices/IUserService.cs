using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UserServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserFull>> GetAll();

        Task<UserFull> GetById(Guid id);

        Task<bool> Update(UpdateUser user);

        Task<bool> Delete(Guid id);
    }
}

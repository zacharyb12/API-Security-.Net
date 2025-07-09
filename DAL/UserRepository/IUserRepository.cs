using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserFull>> GetAll();

        Task<UserFull> GetById(Guid id);

        Task<bool> Update(UpdateUser user);

        Task<bool> Delete(Guid id);
    }
}

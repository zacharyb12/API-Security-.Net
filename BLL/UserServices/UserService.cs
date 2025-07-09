using DAL.UserRepository;
using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async  Task<IEnumerable<UserFull>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserFull> GetById(Guid id)
        {
             return await _repository.GetById(id);
        }

        public async Task<bool> Update(UpdateUser user)
        {
            return await _repository.Update(user);
        }
    }
}

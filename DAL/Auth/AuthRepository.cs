using Microsoft.Data.SqlClient;
using Models.Auth;
using Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SqlConnection _connection;
        public AuthRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        #region Register
        public async Task<Guid> Register(AddUser user)
        {
            using SqlCommand command = new SqlCommand("SP_Register", _connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Role", user.Role);
            command.Parameters.AddWithValue("@Lastname", user.Lastname);
            command.Parameters.AddWithValue("@Firstname", user.Firstname);
            command.Parameters.AddWithValue("@Address", user.Address);

            await _connection.OpenAsync(); 
            
            Guid response = Guid.Empty; 

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync())
                {
                    response = reader.GetGuid(reader.GetOrdinal("NewUserId"));
                }
            }

            await _connection.CloseAsync();

            return response;
        }
        #endregion

        #region LOGIN
        public async Task<Guid> Login(LoginForm login)
        {
            using SqlCommand command = new SqlCommand("Login", _connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", login.Email);
            command.Parameters.AddWithValue("@Password", login.Password);

            await _connection.OpenAsync();

            Guid response = Guid.Empty;

            await using (var reader  = await command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync())
                {
                    response = reader.GetGuid(reader.GetOrdinal("Id"));
                }
            }

            await _connection.CloseAsync();

            return response;
        }
        #endregion

    }
}

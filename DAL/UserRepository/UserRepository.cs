using Microsoft.Data.SqlClient;
using Models.UserModel;
using System.Data;


namespace DAL.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;
        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }


        #region GET ALL
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserFull>> GetAll()
        {
            List<UserFull> response = new List<UserFull>();

            await _connection.OpenAsync();
            SqlCommand command = new SqlCommand("SELECT Id,Username,Email,Role,Lastname,Firstname,Address FROM Users", _connection);


            SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                UserFull user = new()
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Role = reader.GetString(reader.GetOrdinal("Role")),
                    Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                    Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                };
                response.Add(user);

               // await _connection.CloseAsync();

            }
                return response.AsEnumerable();
        }
        #endregion

        #region GET BY ID
        public async Task<UserFull> GetById(Guid id)
        {
            UserFull response = new();

            using SqlCommand command = _connection.CreateCommand();

            command.CommandText = "SELECT Id,Username,Email,Role,Lastname,Firstname,Address FROM Users WHERE Id = @Id";
            
            command.Parameters.AddWithValue("@Id", id);

            await _connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if(await reader.ReadAsync())
            {
                response = new UserFull()
                {
                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Role = reader.GetString(reader.GetOrdinal("Role")),
                    Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                    Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                    Address = reader.GetString(reader.GetOrdinal("Address"))
                };
            }
            await _connection.CloseAsync();
            return response;

        }
        #endregion

        #region UPDATE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Update(UpdateUser user)
        {
            bool response = false;

            using SqlCommand command = new SqlCommand("SP_User_Update", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };


            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Lastname", user.Lastname);
            command.Parameters.AddWithValue("@Firstname", user.Firstname);
            command.Parameters.AddWithValue("@Address", user.Address);

            await _connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();

            if(rowsAffected == 1)
            {
                response = true;
            }

            await _connection.CloseAsync();

            return response;
        }
        #endregion

        #region DELETE
        public async Task<bool> Delete(Guid id)
        {
            bool response = false;

            using SqlCommand command = new SqlCommand("DELETE FROM [dbo.[Users] WHERE Id = @Id",_connection)
            {
                CommandType = CommandType.Text
            };

            command.Parameters.AddWithValue("@Id", id);

            await _connection.OpenAsync();

            int rows = await command.ExecuteNonQueryAsync();

            if(rows == 1)
            {
                response = true;
            }
            await _connection.CloseAsync();

            return response;
        }
        #endregion
    }
}

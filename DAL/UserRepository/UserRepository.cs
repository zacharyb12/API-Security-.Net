using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UserRepository
{
    public class UserRepository
    {
        private readonly SqlConnection _connection;
        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }



        #region GET ALL

        #endregion

        #region GET BY ID

        #endregion

        #region UPDATE

        #endregion

        #region DELETE

        #endregion
    }
}

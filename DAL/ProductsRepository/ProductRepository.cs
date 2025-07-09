using Azure;
using Microsoft.Data.SqlClient;
using Models.ProductModel;
using Models.Response;
using System.Data;

namespace DAL.ProductsRepository
{
    public class ProductRepository : IProductRepository
    {

        private readonly SqlConnection _connection;

        public ProductRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        #region GetAll
        /// <summary>
        /// Return : Task<ApiListResponse<ProductMin>>
        /// </summary>
        /// <returns>
        /// {
        ///     bool : success,
        ///     IEnumerable<ProductMin> : data,
        ///     string : errorMessage
        /// }
        /// </returns>
        public async Task<IEnumerable<ProductMin>> GetAllProducts()
        {

            List<ProductMin> products = new();

            using SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Price FROM Product";

            await _connection.OpenAsync();

            await using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                products.Add(new ProductMin
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                });
            }
                
            await _connection.CloseAsync();

            return products.AsEnumerable();
        }
        #endregion

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Guid> AddProduct(AddProduct product)
        {

            using var command = new SqlCommand("SP_Product_Add", _connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Stock", product.Stock);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Category", product.Category);

            Guid newId = Guid.Empty;

            await _connection.OpenAsync();

            await using (var reader = await command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync() && reader[0] != DBNull.Value)
                {
                    newId = reader.GetGuid(0);
                }
            }

            await _connection.CloseAsync();

            return newId;

        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> Update(UpdateProduct product)
        {
            bool response = false;
            using var command = new SqlCommand("SP_Product_Update", _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Category", product.Category);

                await _connection.OpenAsync();

                int rows = await command.ExecuteNonQueryAsync();

                if(rows == 1 )
                {
                    response = true;

                }

                await _connection.CloseAsync();
                return response;

        }
        #endregion

        #region GetById
        public async Task<ProductFull> GetById(Guid id)
        {


            ProductFull response = null;

                using SqlCommand command = _connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, Price, Stock, Description, Category FROM Product WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                await _connection.OpenAsync();

                await using SqlDataReader reader = await command.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    response = new ProductFull
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Stock = reader.GetInt32(3),
                        Description = reader.GetString(4),
                    };
                }

                await _connection.CloseAsync();
                return response;

        }
        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            bool response = false;

            using SqlCommand command = _connection.CreateCommand();
            command.CommandText = "DELETE FROM Product WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            await _connection.OpenAsync();

            int rows = await command.ExecuteNonQueryAsync();

            if(rows == 1)
            {
                response = true;
            }

            return response;
        }
        #endregion
    }
}

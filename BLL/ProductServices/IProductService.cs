using Models.ProductModel;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductMin>> GetAllProducts();

        Task<Guid> AddProduct(AddProduct product);

        Task<bool> Update(UpdateProduct product);

        Task<ProductFull> GetById(Guid id);

        Task<bool> Delete(Guid id);
    }
}

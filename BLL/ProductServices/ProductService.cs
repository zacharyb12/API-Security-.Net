using DAL.ProductsRepository;
using Models.ProductModel;

namespace BLL.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        #region AddProduct
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Guid> AddProduct(AddProduct product)
        {
            Guid response = await _repository.AddProduct(product);

            return response;
        }
        #endregion

        #region GetAllProducts
        /// <summary>
        ///  Gestion des message d'erreurs
        /// </summary>
        /// <returns>
        ///  ApiListResponse<ProductMin> 
        /// </returns>
        public async Task<IEnumerable<ProductMin>> GetAllProducts()
        {
            IEnumerable<ProductMin> response = await _repository.GetAllProducts();

            return response;
        }

        #endregion

        #region GetById
        public Task<ProductFull> GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        #endregion

        #region Update
        public async Task<bool> Update(UpdateProduct product)
        {

            return await _repository.Update(product);

        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }
        #endregion
    }
}

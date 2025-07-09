using BLL.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Models.ProductModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        #region GetAll
        /// <summary>
        /// Controllet GetAll Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(_service == null)
            {
                return NotFound("Le service est indisponible");
            }

            IEnumerable<ProductMin> response = await _service.GetAllProducts();


            return Ok(response);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProduct product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid response = await _service.AddProduct(product);

            return Ok(response);
        }
        #endregion

        #region Update
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        ///
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProduct product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("le model est invalide");
            }

            bool response = await _service.Update(product);

            return Ok(response);
        }
        #endregion

        #region GetBYId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("le model est invalide");
            }

            ProductFull response = await _service.GetById(id);

            return Ok(response);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool response = await _service.Delete(id);

            return Ok(response);
        }
        #endregion
    }
}

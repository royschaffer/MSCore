using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSCore.Models;
using MSCore.Repository;
using System.Transactions;

namespace MSCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController: ControllerBase
	{

		private readonly IProductRepository _productRepository;
		private readonly ILogger _logger;

		public ProductsController(IProductRepository productRepository, ILoggerFactory loggerFactory)
		{
			_productRepository = productRepository;
			_logger = loggerFactory.CreateLogger<ProductsController>();
		}

		[HttpGet]
		public IActionResult Get()
		{

			var products = _productRepository.GetProducts();
			return new OkObjectResult(products);
		}

		[HttpGet("{id}", Name = "Get")]
		public IActionResult Get(int id)
		{
			_logger.LogInformation("Get" + id.ToString());
			var product = _productRepository.GetProductByID(id);
			return new OkObjectResult(product);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Product product)
		{
			using (var scope = new TransactionScope())
			{
				_productRepository.InsertProduct(product);
				scope.Complete();
				return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
			}
		}

		[HttpPut]
		public IActionResult Put([FromBody] Product product)
		{
			if (product != null)
			{
				using (var scope = new TransactionScope())
				{
					_productRepository.UpdateProduct(product);
					scope.Complete();
					return new OkResult();
				}
			}
			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_productRepository.DeleteProduct(id);
			return new OkResult();
		}
	}
}
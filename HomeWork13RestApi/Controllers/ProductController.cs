using Microsoft.AspNetCore.Mvc;
using RestApiHW13.Contract.Requests;
using RestApiHW13.Contract.Responses;
using RestApiHW13.Service;
using RestApiHW13.Service.Commands;

namespace HomeWork13RestApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        {
            return Ok(await getProductsQuery.Handle());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(int productId, [FromServices] IRequestHandler<int, ProductResponse> getProductByIdQuery)
        {
            return Ok(await getProductByIdQuery.Handle(productId));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertProductAsync([FromServices] IRequestHandler<UpsertProductCommand, ProductResponse> upsertProductCommand, [FromBody] UpsertProductRequest request)
        {
            var product = await upsertProductCommand.Handle(new UpsertProductCommand
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate
            });

            return Ok(product);
        }

        [HttpDelete("{deleteId}")]
        public async Task<IActionResult> DeleteProductById(int productId, [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductByIdCommand)
        {
            var result = await deleteProductByIdCommand.Handle(new DeleteProductCommand { ProductId = productId });

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}

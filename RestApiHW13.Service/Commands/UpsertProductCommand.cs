using RestApiHW13.Contract.Responses;
using RestApiHW13.Data.Context;
using RestApiHW13.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace RestApiHW13.Service.Commands
{
    public class UpsertProductCommand
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Product UpsertProduct()
        {
            var product = new Product()
            {
                ProductId = ProductId,
                ProductName = ProductName,
                Description = Description,
                ReleaseDate = ReleaseDate
            };

            return product;
        }

        public class UpsertProductCommandHandler : IRequestHandler<UpsertProductCommand, ProductResponse>
        {
            private readonly AppDbContext _context;

            public UpsertProductCommandHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ProductResponse> Handle(UpsertProductCommand request, CancellationToken cancellationToken = default)
            {
                var product = await GetProductAsync(request.ProductId, cancellationToken);

                if (product == null)
                {
                    product = request.UpsertProduct();
                    await _context.AddAsync(product, cancellationToken);
                }

                product.ProductName = request.ProductName;
                product.Description = request.Description;
                product.ReleaseDate = request.ReleaseDate;

                await _context.SaveChangesAsync(cancellationToken);

                return new ProductResponse
                {
                    ProductId = product.ProductId,
                    ProductName = request.ProductName,
                    Description = request.Description,
                    ReleaseDate = request.ReleaseDate
                };
            }

            public async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
            {
                return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
            }
        }
    }
}

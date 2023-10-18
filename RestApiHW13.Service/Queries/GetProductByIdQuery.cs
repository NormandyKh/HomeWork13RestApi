using Microsoft.EntityFrameworkCore;
using RestApiHW13.Contract.Responses;
using RestApiHW13.Data.Context;


namespace RestApiHW13.Service.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<int, ProductResponse>
    {
        private readonly AppDbContext _context;

        public GetProductByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}

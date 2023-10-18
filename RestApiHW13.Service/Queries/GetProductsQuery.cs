using Microsoft.EntityFrameworkCore;
using RestApiHW13.Contract.Responses;
using RestApiHW13.Data.Context;

namespace RestApiHW13.Service.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<IList<ProductResponse>>
    {
        private readonly AppDbContext _context;

        public GetProductsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate
                })
                .OrderByDescending(x => x.ProductId)
                .ToListAsync(cancellationToken);
        }
    }
}

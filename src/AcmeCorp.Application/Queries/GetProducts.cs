using MediatR;
using System;
using System.Collections.Generic;
using AcmeCorp.Infrastructure;
using System.Threading.Tasks;
using System.Threading;
using AcmeCorp.Infrastructure.Interfaces;
using System.Linq;

namespace AcmeCorp.Application.Queries
{
    public class GetProducts : IRequest<List<Product>>
    {
    }
    public class GetProductsHandler : IRequestHandler<GetProducts, List<Product>>
    {
        private readonly IProductService _productService;

        public GetProductsHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<List<Product>> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            var list = await _productService.GetProducts();
            return list.ToList();
        }
    }
}

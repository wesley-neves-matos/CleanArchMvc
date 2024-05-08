﻿using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<Product> Handle(ProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image);

            if(product == null)
            {
                throw new ApplicationException("Error creating entity!");
            }
            else
            {
                product.CategoryId = request.CategoryId;
                return await _productRepository.CreateAsync(product);
            }
        }
    }
}

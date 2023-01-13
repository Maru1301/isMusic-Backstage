using iSMusic.Models.DTOs;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductDTO> Search(int? categoryId, string productName)
            => _repository.Search(categoryId, productName, true);

        public ProductDTO Load(int productId)
            => _repository.Load(productId, true);
    }
}
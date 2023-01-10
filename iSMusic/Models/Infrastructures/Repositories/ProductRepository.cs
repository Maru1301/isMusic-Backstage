using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.Infrastructures.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductDTO> Search(int? categoryId, string productName, bool? status)
        {
            IEnumerable<Product> query = _db.Products;
            if (categoryId.HasValue) query = query.Where(x => x.productCategoryId == categoryId);
            if (!string.IsNullOrEmpty(productName)) query = query.Where(x => x.productName.Contains(productName));
            if (status.HasValue) query = query.Where(x => x.status == status);
            query = query.OrderBy(x => x.productName);

            return query.Select(x => x.ToEntity());
        }

        public ProductDTO Load(int productId, bool? status)
        {
            IEnumerable<Product> query = _db.Products.Where(x => x.id == productId);
            if (status.HasValue) query = query.Where(x => x.status == status);

            var product = query.FirstOrDefault();

            return product == null ? null : product.ToEntity();
        }
    }
}
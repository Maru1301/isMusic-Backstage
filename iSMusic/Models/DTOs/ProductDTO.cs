using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string productName { get; set; }
        public CategoryDTO Category { get; set; }
        public int productCategoryId { get; set; }
        public decimal productPrice { get; set; }
        public decimal stock { get; set; }
        public bool status { get; set; }

    }

    public static partial class ProductExts
    {
        public static ProductDTO ToEntity(this Product source)
            => new ProductDTO
            {
                id = source.id,
                productName = source.productName,
                Category = source.ProductCategory.ToEntity(),
                productCategoryId = source.productCategoryId,
                productPrice = source.productPrice,
                stock = source.stock,
                status = source.status,

            };
    }
}
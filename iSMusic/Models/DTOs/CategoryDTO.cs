using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string categoryName { get; set; }


    }
    public static class CategoryExts
    {
        public static CategoryDTO ToEntity(this ProductCategory source)
            => new CategoryDTO
            {
                id = source.id,
                categoryName = source.categoryName,

            };
    }
}
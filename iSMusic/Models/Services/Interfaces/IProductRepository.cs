using iSMusic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSMusic.Models.Services.Interfaces
{
    public interface IProductRepository
    {
        /// <summary>
		/// 篩選商品
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="productName"></param>
		/// <param name="status"></param>
		/// <returns></returns>
		IEnumerable<ProductDTO> Search(int? categoryId, string productName, bool? status);

        /// <summary>
        /// 傳回一筆商品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ProductDTO Load(int productId, bool? status);


    }
}

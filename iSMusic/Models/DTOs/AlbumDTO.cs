using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iSMusic.Models.DTOs
{
	public class AlbumDTO
	{
		public int id { get; set; }

		public string albumName { get; set; }

		public string albumCoverPath { get; set; }

		public HttpPostedFileBase CoverFile { get; set; }

		public int typeId { get; set; }

		public int GenreId { get; set; }

		public DateTime released { get; set; }

		public string description { get; set; }

		public int mainArtistId { get; set; }

        public string albumProducer { get; set; }

        public string albumCompany { get; set; }

        public List<int> songIdList { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    
}
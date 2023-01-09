namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Library_Artist_Metadata
    {
        public int id { get; set; }

        public int libraryId { get; set; }

        public int artistId { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Library Library { get; set; }
    }
}

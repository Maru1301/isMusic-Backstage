namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Library_Album_Metadata
    {
        public int id { get; set; }

        public int libraryId { get; set; }

        public int albumId { get; set; }

        public virtual Album Album { get; set; }

        public virtual Library Library { get; set; }
    }
}

namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Library_PlayList_Metadata
    {
        public int id { get; set; }

        public int libraryId { get; set; }

        public int playListId { get; set; }

        public virtual Library Library { get; set; }

        public virtual Playlist Playlist { get; set; }
    }
}

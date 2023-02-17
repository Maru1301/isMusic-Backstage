namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity_Tag_Metadata
    {
        public int id { get; set; }

        public int activityId { get; set; }

        public int tagId { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual ActivityTag ActivityTag { get; set; }
    }
}

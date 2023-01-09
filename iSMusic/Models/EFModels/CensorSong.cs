namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CensorSong
    {
        public int id { get; set; }

        public int songId { get; set; }

        public bool status { get; set; }

        public int memberId { get; set; }

        public int adminId { get; set; }

        public DateTime censored { get; set; }

        public int censorReasonId { get; set; }

        public bool censorResult { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual CensorReason CensorReason { get; set; }

        public virtual Member Member { get; set; }

        public virtual Song Song { get; set; }
    }
}

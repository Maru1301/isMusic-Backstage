namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin_Role_Metadata
    {
        public int id { get; set; }

        public int adminId { get; set; }

        public int roleId { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Role Role { get; set; }
    }
}

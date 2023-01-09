namespace iSMusic.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubscriptionRecord
    {
        public int id { get; set; }

        public int memberId { get; set; }

        public int subscriptionPlanId { get; set; }

        public DateTime subscribedTime { get; set; }

        public virtual Member Member { get; set; }

        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    }
}

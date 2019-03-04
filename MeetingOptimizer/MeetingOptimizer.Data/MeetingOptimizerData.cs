namespace MeetingOptimizer.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MeetingOptimizerData : DbContext
    {
        public MeetingOptimizerData()
            : base("name=MeetingOptimizerData")
        {
        }

        public virtual DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

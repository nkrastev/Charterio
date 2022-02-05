namespace Charterio.Data.Models
{
    using System;

    using Charterio.Data.Common.Models;

    public class UptimeRobot : IAuditInfo
    {
        public int Id { get; init; }

        public string Ratio { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

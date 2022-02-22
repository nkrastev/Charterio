namespace Charterio.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Common.Models;

    public class UserQuestion : IAuditInfo
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPhone { get; set; }

        public bool IsAnswered { get; set; }

        public bool IsArchived { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

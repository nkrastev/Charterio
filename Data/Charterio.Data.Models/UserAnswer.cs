namespace Charterio.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Common.Models;

    public class UserAnswer : IAuditInfo
    {
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public UserQuestion Question { get; set; }

        public string AnswerContent { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

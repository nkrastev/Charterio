namespace Charterio.Web.ViewModels.Administration.Question
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class QuestionAnswerViewModel
    {
        public int QuestionId { get; set; }

        public string UserEmail { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}

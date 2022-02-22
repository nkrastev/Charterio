namespace Charterio.Web.ViewModels.Administration.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public string CreatedOn { get; set; }

        public string IsAnswered { get; set; }
    }
}

namespace Charterio.Services.Data.Question
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Administration.Question;

    public interface IQuestionService
    {
        List<QuestionViewModel> GetAll();

        QuestionViewModel GetById(int id);

        Task Answer(QuestionAnswerViewModel model, string username);

        void Archive(int id);
    }
}

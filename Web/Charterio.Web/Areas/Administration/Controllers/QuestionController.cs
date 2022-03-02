namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Global;
    using Charterio.Services.Data.Question;
    using Charterio.Web.ViewModels.Administration.Question;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public QuestionController(IQuestionService questionService, IHtmlSanitizer htmlSanitizer)
        {
            this.questionService = questionService;
            this.htmlSanitizer = htmlSanitizer;
        }

        public IActionResult Index()
        {
            var model = this.questionService.GetAll();
            return this.View(model);
        }

        public IActionResult Archive(int id)
        {
            this.questionService.Archive(id);
            return this.RedirectToAction("Index");
        }

        public IActionResult Answer(int id)
        {
            var model = this.questionService.GetById(id);
            var answerModel = new QuestionAnswerViewModel
            {
                QuestionId = model.Id,
                UserEmail = model.UserEmail,
                Question = this.htmlSanitizer.Sanitize(model.Question),
            };
            return this.View(answerModel);
        }

        [HttpPost]
        public IActionResult Answer(QuestionAnswerViewModel model)
        {
            this.questionService.Answer(model, this.User.Identity.Name);
            return this.RedirectToAction("Index");
        }
    }
}

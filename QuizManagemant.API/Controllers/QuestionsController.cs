using Microsoft.AspNetCore.Mvc;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence.Repositories;

namespace QuizManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionRepository _repository = new QuestionRepository();

        // 1. GET ALL
        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAllQuestions());

        // 2. GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var question = _repository.GetQuestionById(id);
            if (question == null) return NotFound("Question not found!");
            return Ok(question);
        }

        // 3. POST (Create)

        [HttpPost]
        public IActionResult Create(Question question)
        {
            _repository.AddQuestion(question);
            return Ok(question);
        }
        // 4. PUT (Update)
        [HttpPut]
        public IActionResult Update(Question question)
        {
            _repository.UpdateQuestion(question);
            return Ok(question);
        }

        // 5. DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteQuestion(id);
            return Ok(new { message = "Question successfully deleted!" });
        }
    }
}
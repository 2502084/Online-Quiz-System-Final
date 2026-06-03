using Microsoft.AspNetCore.Mvc;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence.Repositories;

namespace QuizManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizRepository _repository = new QuizRepository();

        // 1. GET ALL
        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAllQuizzes());

        // 2. GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quiz = _repository.GetQuizById(id);
            if (quiz == null) return NotFound("Quiz not found!");
            return Ok(quiz);
        }

        // 3. POST (Create)
        [HttpPost]
        public IActionResult Create(Quiz quiz)
        {            _repository.AddQuiz(quiz);
            return Ok(quiz); // string ki jagah object
        }

        // 4. PUT (Update)
        [HttpPut]
        public IActionResult Update(Quiz quiz)
        {
            _repository.UpdateQuiz(quiz);
            return Ok(quiz); // string ki jagah object
        }

        // 5. DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteQuiz(id);
            return Ok(new { message = "Quiz successfully deleted!" });
        }
    }
}
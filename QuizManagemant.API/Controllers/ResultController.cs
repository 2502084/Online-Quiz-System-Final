using Microsoft.AspNetCore.Mvc;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence.Repositories;

namespace QuizManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly ResultRepository _repository = new ResultRepository();

        // 1. GET ALL
        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAllResults());

        // 2. GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _repository.GetResultById(id);
            if (result == null) return NotFound("Result not found!");
            return Ok(result);
        }

        // 3. POST (Create)
        [HttpPost]
        public IActionResult Create(Result result)
        {
            _repository.AddResult(result);
            return Ok(new { message = "Result successfully added!" });
        }

        // 4. PUT (Update)
        [HttpPut]
        public IActionResult Update(Result result)
        {
            _repository.UpdateResult(result);
            return Ok(new { message = "Result successfully updated!" });
        }

        // 5. DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteResult(id);
            return Ok(new { message = "Result successfully deleted!" });
        }
    }
}
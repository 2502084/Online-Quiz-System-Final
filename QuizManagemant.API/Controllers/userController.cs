using Microsoft.AspNetCore.Mvc;
using QuizManagement.API.Domain.Entities;
using QuizManagement.API.Infrastructure.Persistence.Repositories;

namespace QuizManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository = new UserRepository();

        // 1. GET ALL
        [HttpGet]
        public IActionResult GetAll() => Ok(_repository.GetAllUsers());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound("User not found!");
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _repository.AddUser(user);
            return Ok(user); // string ki jagah object
        }


        // 4. PUT (Update)
        [HttpPut]
        public IActionResult Update(User user)
        {
            _repository.UpdateUser(user);
            return Ok(user);
        }


        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteUser(id);
            return Ok(new { message = "Deleted!" }); // yeh fix karo
        }
    }
}
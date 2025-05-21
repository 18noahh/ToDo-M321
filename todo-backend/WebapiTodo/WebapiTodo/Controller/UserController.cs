using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebapiTodo.Models;

namespace WebapiTodo.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetTodo(int? todoId)
        {
            Expression<Func<TodoModel, bool>> condition = i => (todoId != null ? i.Id.Equals(todoId) : true);
            List<TodoModel> todoListe = await _repository.FindByConditionAsync(condition);

            if (todoListe == null)
            {
                return NotFound();
            }
            todoListe = todoListe.OrderBy(i => i.Id).ToList();

            return Ok(todoListe);
        }

        [HttpPost]
        public async Task
    }
}

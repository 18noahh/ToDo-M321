using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using WebapiTodo.Models;

namespace WebapiTodo.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly string _connectionString;
        public TodoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MariaDb")!;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] int? todoId)
        {
            if (!await UserIsValidAsync())
                return Unauthorized("Invalid credentials");

            var todos = new List<TodoModel>();

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = todoId.HasValue
                ? "SELECT id, title, description FROM todos WHERE id = @id"
                : "SELECT id, title, description FROM todos";

            using var cmd = new MySqlCommand(query, connection);
            if (todoId.HasValue)
                cmd.Parameters.AddWithValue("@id", todoId.Value);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                todos.Add(new TodoModel
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description")
                });
            }

            if (!todos.Any())
                return NotFound();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoModel todo)
        {
            if (!await UserIsValidAsync())
                return Unauthorized("Invalid credentials");

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "INSERT INTO todos (title, description) VALUES (@title, @description)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@title", todo.Title);
            cmd.Parameters.AddWithValue("@description", todo.Description);

            var result = await cmd.ExecuteNonQueryAsync();
            return result > 0 ? Ok(new { message = "Todo created" }) : StatusCode(500);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo([FromQuery] int todoId)
        {
            if (!await UserIsValidAsync())
                return Unauthorized("Invalid credentials");

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "DELETE FROM todos WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", todoId);

            var result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
                return NotFound($"Todo with id {todoId} not found.");

            return Ok(new { message = $"Todo with id {todoId} deleted." });
        }


        private async Task<bool> UserIsValidAsync()
        {
            var email = Request.Headers["email"].FirstOrDefault();
            var password = Request.Headers["password"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return false;

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @Password";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }
    }
}


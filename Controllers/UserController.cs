using Dapper;
using DapperLesson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperLesson.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");

        [HttpGet]

        public IActionResult GetAllUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = "Select * from Test";

                IEnumerable<User> users = connection.Query<User>(query);

                return Ok(users);
            }
        }


        [HttpPost]
        public IActionResult GetUser(User model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"Insert into Test Values({model.Id},'{model.Name}', {model.Age})";

                connection.Execute(query);

                return Ok("Created");
            }
        }


        [HttpGet]
        public async ValueTask<IActionResult> GetALlMultipleQueryUsers()
        {
            string? connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"select * from userTable; select * from persons;";

                var users = await connection.QueryMultipleAsync(query);

                var firstTable = users.ReadAsync<User>().Result;

                var secondTable = users.ReadAsync<Person>().Result;

                return Ok(new
                {
                    Users = firstTable,
                    Persons = secondTable
                });
            }
        }

        [HttpPut]
        public IActionResult UpdateUser(int userId, string firstName, string lastName)
        {
            string? connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"update userTable set firstName = '{firstName}',lastName = '{lastName}' where userId = {userId}";

                connection.Query(query);

                return Ok("Updated");
            }
        }
    } 
}

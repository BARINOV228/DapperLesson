﻿using DapperLesson.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace DapperLesson.Service
{
    public class UserService
    {
        public static List<User> GetAll(string connectionString)
        {

            using (var connection = new SqlConnection(connectionString))
            {
                List<User> user = connection.Query<User>("getAllUsers", CommandType.StoredProcedure).ToList();

                return user;
            }
        }

        public static void Create(User user)
        {
            string? connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"insert into UserTable VALUES ({user.Id},'{user.Name}','{user.lastName}')";

                connection.Query(query);
            }
        }

        public static void Delete(int UserId)
        {
            string connectionString = WebApplication.CreateBuilder().Configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string query = $"delete from userTable where userId = {UserId}";

                connection.Query(query);
            }
        }
    }
}

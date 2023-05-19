using Microsoft.EntityFrameworkCore;
using System.Data;

namespace RecipeAPI.Data
{
    public static class DbContextExtensions
    {
        public static bool TableExists(this DbContext context, string tableName)
        {
            var sql = $"SELECT OBJECT_ID(N'dbo.{tableName}', N'U')";

            var connection = context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            var result = command.ExecuteScalar();

            return result != null && result != DBNull.Value;
        }
    }
}

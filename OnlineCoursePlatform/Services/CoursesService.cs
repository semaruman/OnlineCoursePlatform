using MySql.Data.MySqlClient;

namespace OnlineCoursePlatform.Services
{
    public class CoursesService
    {
        /// <summary>
        /// Получение общего количества курсов
        /// </summary>
        public static int GetTotalCount()
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = @"SELECT COUNT(id) FROM courses";

            using var command = new MySqlCommand(sqlQuery, connection);

            object res = command.ExecuteScalar();

            return Convert.ToInt32(res);
        }
    }
}

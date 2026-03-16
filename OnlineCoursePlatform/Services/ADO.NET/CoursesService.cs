using MySql.Data.MySqlClient;
using OnlineCoursePlatform.Models;

namespace OnlineCoursePlatform.Services.ADO.NET
{
    public class CoursesService
    {
        /// <summary>
        /// Получение общего количества курсов
        /// </summary>
        public static List<Course> Get(string userFullName)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = @"
        SELECT 
            c.title,
            c.summary,
            c.photo
        FROM users as u
        JOIN user_courses as uc ON uc.user_id = u.id
        JOIN courses as c ON uc.course_id = c.id
        WHERE u.is_active = 1 AND u.full_name = @fullName   
        ORDER BY uc.last_viewed DESC;";

            using var command = new MySqlCommand(sqlQuery, connection);
            command.Parameters.Add(new MySqlParameter("@fullName", userFullName));

            using var reader = command.ExecuteReader();
            List<Course> coursesList = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Title = reader.GetString(0),
                    Summary = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Photo = reader.IsDBNull(2) ? null : reader.GetString(2)
                };
                coursesList.Add(course);
            }

            return coursesList;
        }

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

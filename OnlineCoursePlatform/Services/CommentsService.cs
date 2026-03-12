using MySql.Data.MySqlClient;
using OnlineCoursePlatform.Models;

namespace OnlineCoursePlatform.Services
{
    public class CommentsService
    {
        public static List<Comment> Get(int course_idP)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = @"SELECT c.id, c.text, c.time
FROM comments AS c
JOIN steps AS s ON c.step_id = s.id
JOIN unit_lessons AS ul ON s.lesson_id = ul.lesson_id
JOIN units AS u ON ul.unit_id = u.id
JOIN courses AS cr ON u.course_id = cr.id
WHERE reply_comment_id IS NULL AND cr.id = @course_idP
ORDER BY c.time DESC;";

            using var command = new MySqlCommand(sqlQuery, connection);

            var courseIdParam = new MySqlParameter("@course_idP", course_idP);

            command.Parameters.Add(courseIdParam);

            using var reader = command.ExecuteReader();

            List<Comment> resList = new List<Comment>();
            while (reader.Read())
            {
                Comment comment = new Comment
                {
                    Id = reader.GetInt32("id"),
                    Text = reader.GetString("text"),
                    Time = reader.GetDateTime("time")
                };
                resList.Add(comment);
            }

            return resList;
        }

        /// <summary>
        /// Удаление комментария пользователя
        /// </summary>
        /// <param name="id">id комментария</param>
        /// <returns>Удалось ли удалить комментарий</returns>
        public static bool Delete(int id)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string sqlQuery = @"DELETE FROM course_reviews
                                WHERE comment_id = @id;";

                using MySqlCommand command = new MySqlCommand(sqlQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                command.CommandText = $@"DELETE FROM comments
                                     WHERE reply_comment_id = @id;";

                command.ExecuteNonQuery();

                command.CommandText = $@"DELETE FROM comments
                                     WHERE id = @id;";

                command.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
using System.Data;
using MySql.Data.MySqlClient;
using OnlineCoursePlatform.Models;

namespace OnlineCoursePlatform.Services
{
    public class UsersService
    {
        /// <summary>
        /// Добавление нового пользователя в таблицу users
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Удалось ли добавить пользователя</returns>
        public static bool Add(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(Constant.ConnectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("", connection))
                    {
                        command.CommandText = @"INSERT INTO users (full_name, details, join_date, avatar, is_active) VALUES " +
                            "(@FullName, @Details, @JoinDate, @Avatar, @IsActive)";

                        command.Parameters.AddWithValue("@FullName", user.FullName);
                        command.Parameters.AddWithValue("@Details", user.Details);
                        command.Parameters.AddWithValue("@JoinDate", user.JoinDate);
                        command.Parameters.AddWithValue("@Avatar", user.Avatar);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);

                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Получение пользователя из таблицы users
        /// </summary>
        /// <param name="fullName">Полное имя пользователя</param>
        /// <returns>User</returns>
        public static User Get(string fullName)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = @"SELECT *
FROM users
WHERE full_name = @fullName AND is_active";

            using var command = new MySqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@fullName", fullName);

            using var reader = command.ExecuteReader();

            return reader.Read()
         ? new User
         {
             FullName = reader.GetString("full_name"),
             Details = reader.IsDBNull("details") ? null : reader.GetString("details"),
             JoinDate = reader.GetDateTime("join_date"),
             Avatar = reader.IsDBNull("avatar") ? null : reader.GetString("avatar"),
             IsActive = reader.GetBoolean("is_active"),
             Knowledge = reader.GetInt32("knowledge"),
             Reputation = reader.GetInt32("reputation"),
             FollowersCount = reader.GetInt32("followers_count")
         }
         : null;
        }

        /// <summary>
        /// Получение общего количества пользователей
        /// </summary>
        public static int GetTotalCount()
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            string sqlQuery = @"SELECT COUNT(id) FROM users";

            using var command = new MySqlCommand(sqlQuery, connection);

            object res = command.ExecuteScalar();

            return Convert.ToInt32(res);
        }
    }
}

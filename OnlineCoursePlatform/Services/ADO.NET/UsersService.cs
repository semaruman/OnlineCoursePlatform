using System.Data;
using MySql.Data.MySqlClient;
using OnlineCoursePlatform.Models;

namespace OnlineCoursePlatform.Services.ADO.NET
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

                        command.Parameters.AddWithValue("@FullName", user.full_name);
                        command.Parameters.AddWithValue("@Details", user.details);
                        command.Parameters.AddWithValue("@JoinDate", user.join_date);
                        command.Parameters.AddWithValue("@Avatar", user.avatar);
                        command.Parameters.AddWithValue("@IsActive", user.is_active);

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
             full_name = reader.GetString("full_name"),
             details = reader.IsDBNull("details") ? null : reader.GetString("details"),
             join_date = reader.GetDateTime("join_date"),
             avatar = reader.IsDBNull("avatar") ? null : reader.GetString("avatar"),
             is_active = reader.GetBoolean("is_active"),
             knowledge = reader.GetInt32("knowledge"),
             reputation = reader.GetInt32("reputation"),
             followers_count = reader.GetInt32("followers_count")
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

        /// <summary>
        /// Форматирование показателей пользователя
        /// </summary>
        /// <param name="number">Число для форматирования</param>
        /// <returns>Отформатированное число</returns>
        public static string FormatUserMetrics(int number)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);

            connection.Open();

            string sqlFunctionName = "format_number";

            using var command = new MySqlCommand(sqlFunctionName, connection);
            command.CommandType = CommandType.StoredProcedure;

            var numberParameter = new MySqlParameter("number", number)
            {
                Direction = ParameterDirection.Input
            };

            var returnValueParameter = new MySqlParameter()
            {
                Direction = ParameterDirection.ReturnValue
            };

            command.Parameters.Add(numberParameter);
            command.Parameters.Add(returnValueParameter);

            command.ExecuteNonQuery();

            return returnValueParameter.Value.ToString();
        }
    }
}

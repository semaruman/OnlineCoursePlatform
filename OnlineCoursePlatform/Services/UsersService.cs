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
    }
}

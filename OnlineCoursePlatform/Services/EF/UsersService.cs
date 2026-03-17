using System.Globalization;
using OnlineCoursePlatform.Data;
using OnlineCoursePlatform.Models;

namespace OnlineCoursePlatform.Services.EF
{
    public class UsersService 
    {
        /// <summary>
        /// Получение пользователя из таблицы users
        /// </summary>
        /// <param name="fullName">Полное имя пользователя</param>
        /// <returns>User</returns> 
        public User? Get(string fullName)
        {
            using var dbContext = new ApplicationDbContext();
            var user = dbContext.Users.FirstOrDefault(u => u.full_name == fullName && u.is_active);

            return user;
        }

        /// <summary>
        /// Получение общего количества пользователей
        /// </summary>
        public int GetTotalCount()
        {
            using var dbContext = new ApplicationDbContext();

            if (dbContext.Users == null || dbContext.Users.Count() == 0 )
            {
                return 0;
            }
            else
            {
                return dbContext.Users.Count();
            }        
        }

        /// <summary>
        /// Добавление нового пользователя в таблицу users
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <returns>Удалось ли добавить пользователя</returns>
        public bool Add(User user)
        {
            try
            {
                using var dbContext = new ApplicationDbContext();
                dbContext.Users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Форматирование показателей пользователя
        /// </summary>
        /// <param name="number">Число для форматирования</param>
        /// <returns>Отформатированное число</returns>
        public string? FormatUserMetrics(int number)
        {
            if (number < 1000)
            {
                return number.ToString();
            }
            else if (number % 1000 == 0)
            {
                return (number/1000).ToString() + "K";
            }
            else
            {
                double resNumber = number / 1000.0;
                string res = resNumber.ToString("0.0") + "K";
                res = res.Replace(".0K", "K");
                return res;
            }
        }
    }
}

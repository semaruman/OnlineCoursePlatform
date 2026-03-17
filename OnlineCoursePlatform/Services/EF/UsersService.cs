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
    }
}

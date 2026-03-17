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
    }
}

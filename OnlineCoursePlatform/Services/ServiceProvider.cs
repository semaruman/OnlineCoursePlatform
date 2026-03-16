using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCoursePlatform.Services
{
    public static class ServiceProvider
    {
        public static IUsersService usersService = new OnlineCoursePlatform.Services.ADO.NET.UsersService();
        public static ICoursesService coursesService = new OnlineCoursePlatform.Services.ADO.NET.CoursesService();
        public static ICommentsService commentsService = new OnlineCoursePlatform.Services.ADO.NET.CommentsService();
    }
}

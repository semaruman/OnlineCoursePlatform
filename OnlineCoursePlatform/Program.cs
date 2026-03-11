using OnlineCoursePlatform.Models;
using OnlineCoursePlatform.Services;

public class Program
{
    public static void RegisterUser()
    {
        Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");
        string fullName = Console.ReadLine();

        User user = new User
        {
            FullName = fullName
        };

        if (UsersService.Add(user))
        {
            Console.WriteLine($"Пользователь '{fullName}' успешно добавлен.\n");
        }
        else
        {
            Console.WriteLine("Произошла ошибка, произведен выход на главную страницу\n");
        }
    }
}
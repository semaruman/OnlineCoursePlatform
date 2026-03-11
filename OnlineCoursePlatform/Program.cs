using OnlineCoursePlatform.Models;
using OnlineCoursePlatform.Services;

public class Program
{
    public static void Main(string[] args)
    {
        string menuValue;
        while (true)
        {
            Console.WriteLine(@"
************************************************
* Добро пожаловать на онлайн платформу Stepik! *
************************************************

Выберите действие (введите число и нажмите Enter):

1. Зарегистрироваться
2. Закрыть приложение

************************************************
");
            menuValue = Console.ReadLine();
            if (menuValue == "1")
            {
                RegisterUser();
            }
            else if (menuValue == "2")
            {
                Console.WriteLine("До свидания!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
            }
        }
    }

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
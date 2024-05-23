using DbHandler.Entities;
using DbHandler.Helpers;
using DbHandler.Services.ConcrateServices;
using DbHandler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace DbHandler
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Пожалуйста, выберете режим выполнения (1-6).");

                return;
            }

            var configuration = new ConfigurationBuilder()
             .AddJsonFile("D:\\Microsoft VS\\Projects\\DbHandler\\DbHandler\\appsettings.json", optional: false, reloadOnChange: true)
             .Build();

            var services = new ServiceCollection();
      
            string dbConnection = configuration.ConfigureApp();

            var serviceProvider = services.InitializeApp(dbConnection);

            var employeeService = serviceProvider.GetService<IEmployeeService>();

            int mode = int.Parse(args[0]);
            
            switch (mode)
            {
                case 1:

                    var result = await employeeService.CreateEmployeeTable();

                    if (result)
                    {
                        Console.WriteLine("Таблица создана.");
                    }
                    else
                    {
                        Console.WriteLine("Таблица уже существует.");
                    }

                    break;

                case 2:

                    if (args.Length != 4)
                    {
                        Console.WriteLine("Используйте данный шаблон: App.ехе 2 \"FullName\" BirthDate Gender");

                        return;
                    }

                    var employee = new Employee
                    {
                        FullName = args[1],

                        DateOfBirth = DateTime.Parse(args[2]),

                        Gender = args[3]
                    };

                    await employeeService.CreateEmployee(employee);

                    break;

                case 3:

                    var employees = employeeService.GetAllEmployees();

                    await foreach (var emp in employees)
                    {
                        Console.WriteLine($"{emp.FullName}, {emp.DateOfBirth.ToShortDateString()}, {emp.Gender}, {emp.GetFullAge()}");
                    }

                    break;

                case 4:

                    var randEmployees = await Generator.GenerateRandomEmployeesAsync(1000000);

                    await employeeService.CreateEmployees(randEmployees);

                    var specEmployees = await Generator.GenerateSpecialEmployeesAsync(100);

                    await employeeService.CreateEmployees(specEmployees);

                    break;

                case 5:

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    var filteredEmployees = employeeService.GetFilteredEmployees();

                    stopwatch.Stop();

                    await foreach (var emp in filteredEmployees)
                    {
                        Console.WriteLine($"{emp.FullName}, {emp.DateOfBirth.ToShortDateString()}, {emp.Gender}, {emp.GetFullAge()}");
                    }

                    Console.WriteLine($"Запрос выполнялся {stopwatch.ElapsedMilliseconds} мс");

                    break;

                case 6:

                    employeeService.OptimizeDatabase();

                    break;

                default:

                    Console.WriteLine("Неизвестный режим.");

                    break;
            
            }

        }
    }
}

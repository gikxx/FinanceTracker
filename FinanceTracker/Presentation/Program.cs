using FinanceTracker.Application.Services;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Services;
using FinanceTracker.Infrastructure.Caching;
using FinanceTracker.Infrastructure.Import;
using FinanceTracker.Presentation.ConsoleUI;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FinanceTracker.Presentation
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                var serviceProvider = ConfigureServices();
                var mainMenu = serviceProvider.GetRequiredService<MainMenu>();
                mainMenu.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
                Console.ReadKey();
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Регистрация сервисов с прокси-кэшированием
            services.AddSingleton<IBankAccountService>(provider =>
                new BankAccountProxy(new BankAccountService()));

            services.AddSingleton<ICategoryService>(provider =>
                new CategoryProxy(new CategoryService()));

            services.AddSingleton<IOperationService>(provider =>
                new OperationProxy(new OperationService(provider.GetService<IBankAccountService>())));
            services.AddSingleton<AnalyticsService>();

            // Регистрация фасадов
            services.AddSingleton<BankAccountFacade>();
            services.AddSingleton<CategoryFacade>();
            services.AddSingleton<OperationFacade>();
            services.AddSingleton<AnalyticsFacade>();


            services.AddSingleton<CsvImporter>();
            services.AddSingleton<JsonImporter>();
            services.AddSingleton<YamlImporter>();

            // Главное меню
            services.AddSingleton<MainMenu>();
            services.AddSingleton<ShareMenu>();



            return services.BuildServiceProvider();
        }
    }
}
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using SF_Module11_TelegramBot.Controllers;
using SF_Module11_TelegramBot.Services;
using SF_Module11_TelegramBot.Configuration;

namespace SF_Module11_TelegramBot
{
    public class Program
    {
        /// <summary>
        /// Точка входа программы
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, обеспечивающий постоянную работу приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        // Контейнер зависимостей
        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<IStorage, MemoryStorage>();
            services.AddSingleton<ICounter, Counter>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        // Настройки бота
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "BOT_TOKEN"
            };
        }
    }
}
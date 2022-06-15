using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SF_Module11_TelegramBot.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        // Метод обработки запроса
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, 
                $"Сообщение неподдерживаемого формата", cancellationToken: ct);
        }
    }
}

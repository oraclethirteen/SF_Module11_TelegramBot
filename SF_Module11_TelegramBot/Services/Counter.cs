using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SF_Module11_TelegramBot.Services
{
    /// <summary>
    /// Основной класс, реализующий функцию подсчёта
    /// </summary>
    public class Counter : ICounter
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IStorage _memoryStorage;

        public Counter(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        // Метод, отвечающий за подсчёт символов и сложение чисел в сообщении
        public async Task Count(Message message, string userCountMode, CancellationToken ct)
        {
            switch (userCountMode)
            {
                case "Symbols":
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b>Длина сообщения:</b> {message.Text.Length}", cancellationToken: ct, parseMode: ParseMode.Html);
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                            $"Нажмите /start для выбора режима", cancellationToken: ct);

                    _memoryStorage.GetSession(message.Chat.Id).CountMode = null;
                    break;
                case "Numbers":
                    long check = 0;
                    long result = 0;
                    string sNumbers = null;

                    long[] iNumbers = Array.ConvertAll(message.Text.Split(), s => long.TryParse(s, out var x) ? x : check += 1);

                    if (check != 0 || iNumbers.Length == 1)
                        result = 0;
                    else
                    {
                        try
                        {
                            foreach (var num in iNumbers)
                            {
                                result = checked(result + num);
                            }
                        }
                        catch (OverflowException)
                        {
                            result = 0;
                        }
                    }

                    sNumbers = result.ToString();

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                            $"<b>Результат:</b> {sNumbers}", cancellationToken: ct, parseMode: ParseMode.Html);
                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
                            $"Нажмите /start для выбора режима", cancellationToken: ct);

                    _memoryStorage.GetSession(message.Chat.Id).CountMode = null;
                    break;
                default:
                    _memoryStorage.GetSession(message.Chat.Id).CountMode = null;
                    break;
            }
        }
    }
}

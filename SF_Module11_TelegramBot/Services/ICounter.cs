using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SF_Module11_TelegramBot.Services
{
    /// <summary>
    /// Интерфейс функции подсчёта, содержащий метод, отвечающий за вычисления
    /// </summary>
    public interface ICounter
    {
        Task Count(Message message, string userCountMode, CancellationToken ct);
    }
}

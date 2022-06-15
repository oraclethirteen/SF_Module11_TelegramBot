using System;
using System.Text;
using SF_Module11_TelegramBot.Models;

namespace SF_Module11_TelegramBot.Services
{
    /// <summary>
    /// Интерфейс пользовательской сессии, содержащий метод получения сессии пользователя по идентификатору
    /// </summary>
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
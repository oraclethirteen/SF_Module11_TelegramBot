using System;
using System.Text;
using System.Collections.Concurrent;
using SF_Module11_TelegramBot.Models;

namespace SF_Module11_TelegramBot.Services
{
    /// <summary>
    /// Класс хранения пользовательских сессий
    /// </summary>
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        // Метод, возвращащий сессию по ключу и возвращающий новую, в случае её отсутствия
        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { CountMode = null };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}

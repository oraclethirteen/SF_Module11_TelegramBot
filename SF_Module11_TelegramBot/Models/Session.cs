using System;
using System.Text;

namespace SF_Module11_TelegramBot.Models
{
    /// <summary>
    /// Основной класс пользовательской сессии с полем, отвечающим за выбранный режим подсчёта
    /// </summary>
    public class Session
    {
        public string CountMode { get; set; }
    }
}
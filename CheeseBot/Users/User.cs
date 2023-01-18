using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CheeseBot
{
    public class User
    {
        protected string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        protected long _chatID;
        public long ChatID
        {
            get { return _chatID; }
            set { _chatID = value; }
        }

        protected string _lastCommand;
        public string LastCommand
        {
            get { return _lastCommand; }
            set { _lastCommand = value; }
        }

        protected Message _lastHandleMessage;
        public Message LastHandleMessage
        {
            get { return _lastHandleMessage; }
            set { _lastHandleMessage = value; }
        }

        protected string _penultimateCommand;
        public string PenultimateCommand
        {
            get { return _penultimateCommand; }
            set { _penultimateCommand = value; }
        }
        public User(Message message)
        {
            _username = message.From.Username;
            _chatID = message.Chat.Id;
        }

        /// <summary>
        /// Крайне не рекомендуется!!! Использовать только при создонии обобщенных типов
        /// </summary>
        public User()
        {

        }
    }
}

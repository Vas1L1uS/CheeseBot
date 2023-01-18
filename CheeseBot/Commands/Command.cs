using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheeseBot
{
    abstract class Command
    {
        protected List<string> _namesList;
        public List<string> NamesList
        {
            get { return _namesList; }
            protected set { _namesList = value; }
        }

        protected List<string> _previousCommandList;
        public List<string> PreviousCommandList
        {
            get { return _previousCommandList; }
            protected set { _previousCommandList = value; }
        }

        public Command PreviousCommandBack { get; protected set; }

        protected Command()
        {
            this._namesList = new List<string>();
            this._previousCommandList = new List<string>();
        }

        public void AddOtherCommands(List<Command> othercommands)
        {
            foreach (var othercommand in othercommands)
            {
                foreach (var item in othercommand._namesList)
                {
                    if (CheckDublicate(item) == false)
                    {
                        this._namesList.Add(item);
                    }
                }
            }
        }

        public void AddPreviousCommands(List<Command> previousCommands)
        {
            foreach (var previousCommand in previousCommands)
            {
                foreach (var item in previousCommand._namesList)
                {
                    if (CheckDublicate(item) == false)
                    {
                        this._previousCommandList.Add(item);
                    }
                }
            }
        }


        private bool CheckDublicate(string element)
        {
            foreach (var item in _namesList)
            {
                if (item == element)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckNameCommand(string text)
        {
            foreach (string item in _namesList)
            {
                if (text == item)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPreviousCommand(string text)
        {
            if (_previousCommandList.Count > 0)
            {
                foreach (string item in _previousCommandList)
                {
                    if (text == item)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual IReplyMarkup GetButtons()
        {
            return null;
        }

        public virtual IReplyMarkup GetInlineButtons()
        {
            return null;
        }

        public virtual Task<Message>[] SendMessageToClient(TelegramBotClient botClient,Message message, Client client)
        {
            return null;
        }

        public virtual Task<Message>[] SendMessageToClientWhenBackCommand(TelegramBotClient botClient, Message message, Client client)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Вы вернулись на предыдущий шаг.", replyMarkup: GetButtons())
            };
            client.LastCommand = this._namesList.First();
            return messages;
        }

        public virtual Task<Message>[] SendMessageToAdmin(TelegramBotClient botClient, long chatID)
        {
            return null;
        }
    }
}

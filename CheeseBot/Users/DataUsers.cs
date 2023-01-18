using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CheeseBot
{
    internal class DataUsers<T>
        where T : User , new()
    {
        public List<T> UsersList { get; set; }

        public DataUsers()
        {
            UsersList = new List<T>();
        }

        public void SetLastCommandToCurrentUser(Message message, DataUsers<T> dataUsers, DataCommands dataCommands)
        {
            foreach (User currentUser in dataUsers.UsersList)
            {
                if (message.Chat.Id == currentUser.ChatID)
                {
                    Command currentCommand = dataCommands.FindAndGetCommand(message.Text);

                    if (currentCommand != null)
                    {
                        currentUser.LastCommand = currentCommand.NamesList.First();
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public string GetLastCommandToCurrentUser(Message message, DataUsers<T> dataUsers)
        {
            foreach (T currentUser in dataUsers.UsersList)
            {
                if (message.Chat.Id == currentUser.ChatID)
                {
                    return currentUser.LastCommand;
                }
            }
            return null;
        }

        public T GetCurrentUser(Message message)
        {
            foreach (T currentUser in UsersList)
            {
                
                if (message.Chat.Id == currentUser.ChatID)
                {
                    return currentUser;
                }
            }
            User newUser = new User(message);
            T newUserT = new T();
            newUserT.UserName = newUser.UserName;
            newUserT.ChatID = newUser.ChatID;
            UsersList.Add(newUserT);
            return newUserT;
        }
    }
}

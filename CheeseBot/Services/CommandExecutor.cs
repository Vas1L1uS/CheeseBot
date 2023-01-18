using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CheeseBot.Services
{
    public class CommandExecutor : ICommandExecutor
    {
        static private MainData _mainData;

        static private TelegramBotClient _cheeseBot;

        static private DataCommands _dataCommands;
        static private DataProducts _dataProducts;

        static private DataUsers<Client> _dataClients;
        static private DataUsers<Admin> _dataAdmins;

        static private DataOrders _dataOrders;

        static private ExcelWriter _myExcelWriter;

        public CommandExecutor()
        {
            _dataCommands = new DataCommands();
            _dataProducts = new DataProducts();

            _dataClients = new DataUsers<Client>();
            _dataAdmins = new DataUsers<Admin>();

            _dataOrders = new DataOrders();

            _mainData = new MainData();

            _cheeseBot = new TelegramBotClient(_mainData.TokenCheeseBot);

            _myExcelWriter = new ExcelWriter();
        }

        public async Task Execute(Update update)
        {
            if(update?.Message?.Chat == null && update?.CallbackQuery == null)
                return;

            var message = update.Message;

            if (message != null)
            {
                if (message.Text == "/start-AdminPas12Gltp7lhIgfk575hgf")
                {
                    await _cheeseBot.SendTextMessageAsync(message.Chat.Id, $"Здравствуйте, {message.From.FirstName}!");

                    _dataAdmins.GetCurrentUser(message);
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                Client currentClient = _dataClients.GetCurrentUser(update.CallbackQuery.Message);

                UpdateHandleMessage(update, currentClient);
            }

            if (message != null)
            {
                Client currentClient = _dataClients.GetCurrentUser(message);

                if (currentClient.TypeDelivery == Client.TypesDelivery.Pickup)
                {

                }

                if (DataCommands.MyAdressEntry.CheckPreviousCommand(currentClient.LastCommand) && CheckingForCommonCommands(message) == false)
                {
                    if (currentClient.TypeDelivery == Client.TypesDelivery.Delivery)
                    {
                        if (message.Location != null)
                        {
                            await DataCommands.MyAdressEntry.SendMessageToClientWhenLocation(_cheeseBot, message, currentClient);
                            currentClient.LastCommand = DataCommands.MyAdressEntry.NamesList.First();
                            return;
                        }
                        else
                        {
                            await DataCommands.MyAdressEntry.SendMessageToClientWhenAdress(_cheeseBot, message, currentClient);
                            currentClient.LastCommand = DataCommands.MyAdressEntry.NamesList.First();
                            return;
                        }
                    }
                }

                if (DataCommands.MyNameEntry.CheckPreviousCommand(currentClient.LastCommand) && CheckingForCommonCommands(message) == false)
                {
                    await DataCommands.MyNameEntry.SendMessageToClientWhenName(_cheeseBot, message, currentClient);
                    currentClient.LastCommand = DataCommands.MyNameEntry.NamesList.First();
                    return;
                }

                if (DataCommands.MyPhoneEntry.CheckPreviousCommand(currentClient.LastCommand) && CheckingForCommonCommands(message) == false)
                {
                    await DataCommands.MyPhoneEntry.SendMessageToClientWhenPhone(_cheeseBot, message, currentClient);
                    currentClient.LastCommand = DataCommands.MyPhoneEntry.NamesList.First();
                    return;
                }

                Command currentCommand = _dataCommands.FindAndGetCommand(message.Text);

                if (currentCommand != null)
                {
                    try
                    {
                        await DataCommands.MySelectProduct.DeleteHandleMessage(_cheeseBot, message, currentClient);
                        if (currentCommand.CheckPreviousCommand(currentClient.LastCommand))
                        {
                            Task<Message>[] botMessagesToClient = currentCommand.SendMessageToClient(_cheeseBot, message, currentClient);

                            if (botMessagesToClient != null)
                            {
                                foreach (var item in botMessagesToClient)
                                {
                                    if (item != null)
                                    {
                                        await item;
                                    }
                                }

                                if (currentCommand.NamesList.First() == DataCommands.MySelectProduct.NamesList.First())
                                {
                                    await DataCommands.MySelectProduct.SendHandleMessage(_cheeseBot, message, currentClient);
                                }

                                foreach (var admin in _dataAdmins.UsersList)
                                {
                                    Task<Message>[] botMessagesToAdmins = currentCommand.SendMessageToAdmin(_cheeseBot, admin.ChatID);

                                    if (botMessagesToAdmins != null)
                                    {
                                        foreach (var item in botMessagesToAdmins)
                                        {
                                            if (item != null)
                                            {
                                                await item;
                                            }
                                        }
                                    }
                                }
                            }
                            if (currentCommand == DataCommands.MyFinishOrder)
                            {
                                Order newOrder = new Order(currentClient);
                                _dataOrders.Orders_List.Add(new Order(currentClient));
                                _myExcelWriter.WriteNewOrder(newOrder);
                                currentClient.ClearInformation();
                                return;
                            }
                            if (CheckingForCommonCommands(message))
                            {
                            }
                            else
                            {
                                currentClient.LastCommand = message.Text;
                            }
                        }
                        else
                        {
                            await _cheeseBot.SendTextMessageAsync(message.Chat.Id, "Данную команду вызвать здесь нельзя. \nВыберите команду из предложенного списка внизу. \nЕсли вам нужна помощь, то введите /help или \"Помощь\"");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Что пошло не так!!!");
                    }
                }
                else
                {
                    await _cheeseBot.SendTextMessageAsync(message.Chat.Id, "Данная команда не существует. \nВыберите команду из предложенного списка внизу. \nЕсли вам нужна помощь, то введите /help или \"Помощь\"");
                }
                Console.WriteLine($"Пользователь @{message.From.Username} написал {message.Text} id чата {message.Chat.Id}");

            }
        }

        static private void UpdateHandleMessage(Update update, Client client)
        {
            if (update.CallbackQuery.Data.EndsWith("Subtract100"))
            {
                client.SelectedProduct.Weight -= 100;
            }
            else if (update.CallbackQuery.Data.EndsWith("Add100"))
            {
                client.SelectedProduct.Weight += 100;
            }
            else if (update.CallbackQuery.Data.EndsWith("Subtract500"))
            {
                client.SelectedProduct.Weight -= 500;
            }
            else if (update.CallbackQuery.Data.EndsWith("Add500"))
            {
                client.SelectedProduct.Weight += 500;
            }

            else if (update.CallbackQuery.Data.EndsWith("Subtract1/2"))
            {
                client.SelectedProduct.Weight -= 0.5f;
            }
            else if (update.CallbackQuery.Data.EndsWith("Add1/2"))
            {
                client.SelectedProduct.Weight += 0.5f;
            }

            else if (update.CallbackQuery.Data.EndsWith("Subtract1"))
            {
                client.SelectedProduct.Weight -= 1;
            }
            else if (update.CallbackQuery.Data.EndsWith("Add1"))
            {
                client.SelectedProduct.Weight += 1;
            }
            DataCommands.MySelectProduct.SendHandleMessage(_cheeseBot, update.CallbackQuery.Message, client);
        }

        static private bool CheckingForCommonCommands(Message message)
        {
            List<Command> commonCommandsList = new List<Command>() { DataCommands.MyBackCommand, DataCommands.MyHelp, DataCommands.MyGoToNextStep };
            foreach (var command in commonCommandsList)
            {
                foreach (var item in command.NamesList)
                {
                    if (item == message.Text)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
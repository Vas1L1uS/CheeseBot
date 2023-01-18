using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheeseBot
{
    internal class DataCommands
    {
        static public GoToNextStep          MyGoToNextStep          { get; private set; }
        static public ViewListProducts      MyViewListProducts      { get; private set; }
        static public FAQ                   MyFAQ                   { get; private set; }
        //static public MoreAboutUs           MyMoreAboutUs           { get; private set; }
        static public StartCommand          MyStartCommand          { get; private set; }
        static public SelectProduct         MySelectProduct         { get; private set; }
        static public CancelCheckout        MyCancelCheckout        { get; private set; }
        static public AddToBasket           MyAddToBasket           { get; private set; }
        static public ViewMyBasket          MyViewMyBasket          { get; private set; }
        static public BackCommand           MyBackCommand           { get; private set; }
        static public Help                  MyHelp                  { get; private set; }
        static public DeleteProduct         MyDeleteProduct         { get; private set; }
        static public ContinueСheckout      MyContinueCheckout      { get; private set; }
        static public Pickup                MyPickup                { get; private set; }
        static public EditProductInMyBasket MyEditProductInMyBasket { get; private set; }
        static public Delivery              MyDelivery              { get; private set; }
        static public SelectAdress          MySelectAdress          { get; private set; }
        static public AdressEntry           MyAdressEntry           { get; private set; }
        static public NameEntry             MyNameEntry             { get; private set; }
        static public PhoneEntry            MyPhoneEntry            { get; private set; }
        static public FinishOrder           MyFinishOrder           { get; private set; }

        static public List<Command> Commands { get; private set; }

        static DataCommands()
        {
            MyGoToNextStep = new GoToNextStep();
            MyViewListProducts = new ViewListProducts();
            MyFAQ = new FAQ();
            //MyMoreAboutUs = new MoreAboutUs();
            MyStartCommand = new StartCommand();
            MySelectProduct = new SelectProduct();
            MyCancelCheckout = new CancelCheckout();
            MyAddToBasket = new AddToBasket();
            MyViewMyBasket = new ViewMyBasket();
            MyDeleteProduct = new DeleteProduct();
            MyBackCommand = new BackCommand();
            MyHelp = new Help();
            MyContinueCheckout = new ContinueСheckout();
            MyPickup = new Pickup();
            MyEditProductInMyBasket = new EditProductInMyBasket();
            MyDelivery = new Delivery();
            MySelectAdress = new SelectAdress();
            MyAdressEntry = new AdressEntry();
            MyNameEntry = new NameEntry();
            MyPhoneEntry = new PhoneEntry();
            MyFinishOrder = new FinishOrder();

            Commands = new List<Command>() { MyViewListProducts, MyFAQ, /*MyMoreAboutUs,*/ MyStartCommand, MySelectProduct, MyCancelCheckout, MyAddToBasket, MyViewMyBasket, MyBackCommand, MyHelp, MyGoToNextStep, MyDeleteProduct, MyContinueCheckout, MyPickup, MyEditProductInMyBasket, MyDelivery, MySelectAdress, MyAdressEntry, MyNameEntry, MyPhoneEntry, MyFinishOrder };

            MyViewMyBasket.AddPreviousCommands      (new List<Command>() { DataCommands.MyAddToBasket } );
            MySelectProduct.AddPreviousCommands     (new List<Command>() { DataCommands.MyViewListProducts });
            MyPickup.AddPreviousCommands            (new List<Command>() { DataCommands.MyContinueCheckout });
            MyContinueCheckout.AddPreviousCommands  (new List<Command>() { DataCommands.MyAddToBasket });
            MyDeleteProduct.AddPreviousCommands     (new List<Command>() { DataCommands.MyEditProductInMyBasket });
            MyDelivery.AddPreviousCommands          (new List<Command>() { DataCommands.MyContinueCheckout });
            MySelectAdress.AddPreviousCommands      (new List<Command>() { DataCommands.MyDelivery });
            MyAdressEntry.AddPreviousCommands       (new List<Command>() { DataCommands.MySelectAdress });
            MyNameEntry.AddPreviousCommands         (new List<Command>() { DataCommands.MyAdressEntry });
            MyPhoneEntry.AddPreviousCommands        (new List<Command>() { DataCommands.MyNameEntry });
            MyNameEntry.AddPreviousCommands         (new List<Command>() { DataCommands.MyPickup });
            MyFinishOrder.AddPreviousCommands       (new List<Command>() { DataCommands.MyPhoneEntry });
            MyContinueCheckout.AddPreviousCommands  (new List<Command>() { DataCommands.MyPhoneEntry });

            MyViewMyBasket.AddPreviousCommands      (new List<Command>() { DataCommands.MyAddToBasket });
            MySelectProduct.AddPreviousCommands     (new List<Command>() { DataCommands.MyViewListProducts });
            MyPickup.AddPreviousCommands            (new List<Command>() { DataCommands.MyContinueCheckout });
            MyContinueCheckout.AddPreviousCommands  (new List<Command>() { DataCommands.MyAddToBasket });
            MyDeleteProduct.AddPreviousCommands     (new List<Command>() { DataCommands.MyEditProductInMyBasket });
            MyDelivery.AddPreviousCommands          (new List<Command>() { DataCommands.MyContinueCheckout });
            MySelectAdress.AddPreviousCommands      (new List<Command>() { DataCommands.MyDelivery });
            MyAdressEntry.AddPreviousCommands       (new List<Command>() { DataCommands.MySelectAdress });
            MyNameEntry.AddPreviousCommands         (new List<Command>() { DataCommands.MyAdressEntry });
            MyPhoneEntry.AddPreviousCommands        (new List<Command>() { DataCommands.MyNameEntry });
            MyNameEntry.AddPreviousCommands         (new List<Command>() { DataCommands.MyPickup });
            MyFinishOrder.AddPreviousCommands       (new List<Command>() { DataCommands.MyPhoneEntry });
            MyContinueCheckout.AddPreviousCommands(new List<Command>() { DataCommands.MyPhoneEntry });
        }

        public Command FindAndGetCommand(string text)
        {
            foreach (Command command in Commands)
            {
                if (command.CheckNameCommand(text))
                {
                    return command;
                }
            }
            return null;
        }

        public IReplyMarkup GetButtons(string text)
        {
            foreach (Command command in Commands)
            {
                if (command.CheckNameCommand(text))
                {
                    return command.GetButtons();
                }
            }
            return null;
        }


    }
}

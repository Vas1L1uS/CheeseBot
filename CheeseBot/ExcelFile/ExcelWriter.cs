using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types;

namespace CheeseBot
{
    class ExcelWriter
    {
        private string _path = @".\orders.csv";

        public void WriteNewOrder(Order order)
        {
            using (var writer = new StreamWriter(_path, true, Encoding.UTF8))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                {
                    Delimiter = ";"
                };

                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecord(order);
                    csv.NextRecord();
                }
            }
        }

        private string GetHeader(Order order)
        {
            string text = "";
            text += $"Ник пользователя";
            text += $"Имя";
            text += $"Номер телефона";
            text += $"Способ получения";
            text += $"Адрес";
            text += $"Геопозиция";
            text += $"Продуктовая корзина";
            text += $"Итоговая цена с учетом доставки";
            text += $"Дата оформления\n";

            return text;
        }
    }
}

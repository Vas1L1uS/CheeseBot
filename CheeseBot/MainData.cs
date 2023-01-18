using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CheeseBot
{
    public class MainData
    {
        public string TokenCheeseBot { get; private set; }
        public string TokenAdminBot { get; private set; }

        public MainData()
        {
            TokenCheeseBot = "Введите токен";
        }
    }
}

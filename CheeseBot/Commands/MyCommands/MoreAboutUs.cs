using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class MoreAboutUs : Command
    {
        public MoreAboutUs()
        {
            base._namesList = new List<string>() { "Подробнее о нас" };
        }
    }
}

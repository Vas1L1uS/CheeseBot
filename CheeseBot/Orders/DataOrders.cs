using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class DataOrders
    {
        public List<Order> Orders_List;

        public DataOrders()
        {
            Orders_List = new List<Order>();
        }

        public DataOrders(List<Order> orders_list)
        {
            Orders_List = orders_list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class Smetana : Product
    {
        public Smetana()
        {
            this._name = $"Сметана";
            this._description = "Сметана";
            this._price = 19;
            this._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MinWeight = 500;
            base.MyTypeProductAmount = TypeProductAmount.Weight;
        }

        public override object Clone()
        {
            return new Smetana();
        }
    }
}

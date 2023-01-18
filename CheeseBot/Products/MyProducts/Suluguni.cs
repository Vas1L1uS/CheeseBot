using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class Suluguni : Product
    {
        public Suluguni()
        {
            this._name = $"Сыр \"Сулугуни\"";
            this._description = "Сыр Сулугуни";
            this._price = 30;
            this._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MyTypeProductAmount = TypeProductAmount.Float;
        }

        public override object Clone()
        {
            return new Suluguni();
        }
    }
}

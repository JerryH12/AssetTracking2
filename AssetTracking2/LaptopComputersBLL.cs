using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    internal class LaptopComputersBLL : AssetsBLL
    {
        public LaptopComputersBLL(int id, string type, string brand, string model, string office, DateTime purchaseDate, double price) : base(office, purchaseDate, price)
        {
            Type = type;
            Brand = brand;
            Model = model;
            Id = id;
        }
    }
}

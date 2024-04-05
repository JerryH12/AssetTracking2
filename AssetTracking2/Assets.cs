using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    internal class Assets
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Office { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PriceInUSD { get; set; }
        public List<MobilePhones> MobilePhoneList { get; set; }
        public List<LaptopComputers> LaptopComputerList { get; set; }
    }
}

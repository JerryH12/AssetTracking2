using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    internal class MobilePhones
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public int AssetsId {  get; set; } // foreign key

        public Assets Assets { get; set; }
    }
}

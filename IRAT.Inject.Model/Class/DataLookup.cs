using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRAT.Inject.Model.Class
{
    public class DataLookup
    {
        public string ID { get; set; }
        public int? ProductNumber { get; set; }
        public string Name { get; set; }
        public string OrderType { get; set; }
        public string PickupZone { get; set; }
        public string Supplier { get; set; }
        public decimal AdultCost { get; set; }
        public decimal ChildCost { get; set; }
        public string Country { get; set; }
    }
}

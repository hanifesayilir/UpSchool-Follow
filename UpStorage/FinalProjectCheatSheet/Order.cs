using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectCheatSheet
{
    public class Order
    {
        public Guid Id { get; set; }

        public int RequestedAmount { get; set; } // 100 itendi 656 bulduk

        public int TotalFoundAmount { get; set; }

        public ProductCrawlType ProductCrawlType { get; set; }

        public ICollection<OrderEvents> OrderEvents { get; set; }

        public ICollection<Product> 

        public DateTimeOffset CreatedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public enum StoreType
    {
        Phisical = 1,
        Online = 2,
        Both = 3
    }

    public class Store
    {
        [Key]
        public string StoreId { get; set; }

        public Chain Chain { get; set; }

        public Chain SubChainId { get; set; }

        public string ReportedChainId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public StoreType StoreType { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

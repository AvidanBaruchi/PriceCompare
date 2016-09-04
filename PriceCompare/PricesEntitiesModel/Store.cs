using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(Order = 1), Key]
        public string StoreId { get; set; }
        [Column(Order = 2), Key]
        public Chain Chain { get; set; }

        public Chain SubChain { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public StoreType StoreType { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

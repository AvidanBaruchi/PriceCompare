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
        Physical = 1,
        Online = 2,
        Both = 3
    }

    public class Store
    {
        [Key, Column(Order = 1)]
        public string StoreId { get; set; }
        [Key, Column(Order = 2), ForeignKey("Chain")]
        public string ChainId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public StoreType StoreType { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual Chain Chain { get; set; }
    }
}

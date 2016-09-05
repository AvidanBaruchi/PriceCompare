using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }
        //[ForeignKey("Item")]
        //public int ItemId { get; set; }
        //[ForeignKey("Store")]
        //public string StoreId { get; set; }

        public double PriceValue { get; set; }

        public double UnitOfMeasurePrice { get; set; }

        public string UnitOfMeasure { get; set; }

        public string Quantity { get; set; }

        public virtual Item Item { get; set; }

        public virtual Store Store { get; set; }
    }
}

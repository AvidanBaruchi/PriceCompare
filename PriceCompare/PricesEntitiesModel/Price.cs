using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }

        public Item Item { get; set; }

        public Store Store { get; set; }

        public double PriceValue { get; set; }

        public double UnitOfMeasurePrice { get; set; }

        public string UnitOfMeasure { get; set; }

        public string Quantity { get; set; }
    }
}

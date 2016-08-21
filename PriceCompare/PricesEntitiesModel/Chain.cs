using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public class Chain
    {
        [Key]
        public string ChainId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}

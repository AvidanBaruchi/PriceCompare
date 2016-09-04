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
        public Chain()
        {
            
        }

        public Chain(string id, string name)
        {
            ChainId = id;
            Name = name;
        }

        [Key]
        public string ChainId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}

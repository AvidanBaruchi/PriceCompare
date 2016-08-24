using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricesEntitiesModel
{
    public enum ItemType
    {
        Internal,
        Barcode
    }

    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        //public Chain Chain { get; set; }

        public string ItemCode { get; set; }

        public ItemType ItemType { get; set; }

        public string Name { get; set; }

        public string ManufacturerName { get; set; }

        public string ManufacturerItemDescription { get; set; }

        public string UnitQuantity { get; set; }

        public bool IsWeighted { get; set; }

        public string QuantityInPackage { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
    }
}

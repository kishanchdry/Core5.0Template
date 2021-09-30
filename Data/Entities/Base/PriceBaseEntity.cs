using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Base
{
    public abstract class PriceBaseEntity : QuantityBaseEntity
    {
        [Column(TypeName = "decimal(18,9)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal Discount { get; set; }
        public DateTime? PlacedDate { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal CancelationFee { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal MaxDiscountAmount { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal MaxDiscountPercentage { get; set; }
        public DateTime? DiscountEndDateTime { get; set; }
        public DateTime? DiscountStartDateTime { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal DeliveryCharges { get; set; }

    }

    public abstract class QuantityBaseEntity : BaseEntity
    {
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }
    }
}

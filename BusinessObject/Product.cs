using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "EMPTY_NAME")]
        [StringLength(40, ErrorMessage = "NAME_IN_INTERVAL_0_40", MinimumLength = 0)]
        public string ProductName { get; set; } = null!;

        [StringLength(20, ErrorMessage = "WEIGHT_CANNOT_EXCEED_20")]
        public string Weight { get; set; } = null!;

        [DataType(DataType.Currency, ErrorMessage = "INVALID_DATA_UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "INVALID_INT_NUMBER")]
        public int UnitsInStock { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int MemberId { get; set; }

        [Required(AllowEmptyStrings = false ,ErrorMessage = "ORDER_DATE_EMPTY")]
        [DataType(DataType.DateTime, ErrorMessage = "ERROR_FORMAT_ORDER_DATE")]
        public DateTime OrderDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "REQUIRE_DATE_EMPTY")]
        [DataType(DataType.DateTime, ErrorMessage = "ERROR_FORMAT_REQUIRE_DATE")]
        public DateTime RequireDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "ERROR_FORMAT_SHIP_DATE")]
        public DateTime ShippedDate { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "ERROR_FORMAT_FREIGHT")]
        public decimal Freight { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order(int orderId, int memberId, DateTime orderDate, DateTime requireDate, DateTime shippedDate, decimal freight, Member member, ICollection<OrderDetail> orderDetails)
        {
            OrderId = orderId;
            MemberId = memberId;
            OrderDate = orderDate;
            RequireDate = requireDate;
            ShippedDate = shippedDate;
            Freight = freight;
            Member = member;
            OrderDetails = orderDetails;
        }

        public Order(int orderId, int memberId, DateTime orderDate, DateTime requireDate, DateTime shippedDate, decimal freight)
        {
            OrderId = orderId;
            MemberId = memberId;
            OrderDate = orderDate;
            RequireDate = requireDate;
            ShippedDate = shippedDate;
            Freight = freight;
        }

        public Order(int memberId, DateTime orderDate, DateTime requireDate, DateTime shippedDate, decimal freight)
        {
            MemberId = memberId;
            OrderDate = orderDate;
            RequireDate = requireDate;
            ShippedDate = shippedDate;
            Freight = freight;
        }
    }
}

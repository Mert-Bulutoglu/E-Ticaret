using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order : BaseEntity
    {
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderTotal { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool IsDelivered { get; set; } = false;

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

    }
}
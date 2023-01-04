using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderTotal { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool IsDelivered { get; set; } = false;

        public string Customer { get; set; }
    }
}
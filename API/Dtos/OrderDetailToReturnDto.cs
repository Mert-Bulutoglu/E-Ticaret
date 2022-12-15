using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDetailToReturnDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Subtotal { get; set; }
        public string Order { get; set; }
        public string Product { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Subtotal { get; set; } 

        //    Related Entities  

       public Product Product { get; set; }
       public int ProductId { get; set; }  
       public Order Order { get; set; }
       public int OrderId { get; set; }
        
    }
}
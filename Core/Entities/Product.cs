using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
       public string ProductName { get; set; }
       public string Description { get; set; }
       public string Img1 { get; set; }
       public string Img2 { get; set; }
       public string Img3 { get; set; }
       public decimal Price { get; set; }
       public int Stock { get; set; }

       //    Related Entities  

       public virtual Category Category { get; set; }
       public int? CategoryId { get; set; }
      
    }
}
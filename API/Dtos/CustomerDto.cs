using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }  
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string Address { get; set; }  
        public string PostCode { get; set; }  
        public string City { get; set; }  
        public string Phone { get; set; }  
    }
}
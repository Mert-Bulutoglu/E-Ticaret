using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; }  
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string Password { get; set; }  
        public string Address { get; set; }  
        public string PostCode { get; set; }  
        public string City { get; set; }  
        public string Phone { get; set; }  
    }
}
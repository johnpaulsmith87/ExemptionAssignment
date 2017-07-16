using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// Represents the contact information of any person, i.e name, tel, etc...
    /// </summary>
    public class Contact
    {
        public Contact()
        {
            //default constructor for XML, maybe not be necessary in this case
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //For simplicity's sake, I will allow a max of 2 phone numbers from the GUI
        public List<string> PhoneNumbers { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        
    }
}

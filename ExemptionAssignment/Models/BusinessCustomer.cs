using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ExemptionAssignment.Models
{
    /// <summary>
    /// Represents a business customer. Like ordinary customers, it has an ID and contact info. Additionally, it contains properties
    /// for trading and registered names.
    /// </summary>
    public class BusinessCustomer : Customer
    {
        public BusinessCustomer()
        {
            //default constructor for json
        }
        /// <summary>
        /// Create a new Business customer with contact, trading name and registered name
        /// </summary>
        /// <param name="contactInfo"> Contact details </param>
        /// <param name="registeredName"></param>
        /// <param name="tradingName"></param>
        public BusinessCustomer(Contact contactInfo, string registeredName, string tradingName)
        {
            ContactInformation = contactInfo;
            RegisteredName = registeredName;
            TradingName = tradingName;
        }
        [Display(Name = "Registered Name")]
        public string RegisteredName { get; set; }
        [Display(Name = "Trading Name")]
        public string TradingName { get; set; }
    }
}

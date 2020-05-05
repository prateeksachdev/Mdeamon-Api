using System;

namespace AltnCrossAPI.Database.Models
{
    public class ShopifyOrderAddressModel
    {
        #region Class Property Declarations

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string CountryCode { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateUpdated { get; set; }

        public long OrderId { get; set; }

        public enum AddressesType
        {
            Billing = 0,
            Shipping = 1,
            Home = 2,
            Office = 3
        }
        public AddressesType AddressType { get; set; }

        #endregion
    }
}
using System;

namespace AltnCrossAPI.Database.Models
{
    public class UsersModel
    {
        public string UserID { get; set; }

        public string UserEmail { get; set; }

        public int UserType { get; set; }

        public long ShopifyCustomerID { get; set; }

        public bool IsPopulated { get; set; }

    }
}
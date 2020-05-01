using System;

namespace AltnCrossAPI.Models
{
    public class ShopifyDataModel
    {
        public int Id { get; set; }

        public string JSON { get; set; }

        public string EventType { get; set; }

        public DateTime DateAdded { get; set; }

        public string Entity { get; set; }

    }
}
using System;
namespace EtsyService.Controllers.RequestObjects
{
    public class CreateEtsyListingRequest
    {
        public int quantity { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string[] materials { get; set; }
        public long shipping_template_id { get; set; }
        public string who_made { get; set; }
        public bool is_supply { get; set; }
        public string when_made { get; set; }
        public string state { get; set; }
        public long taxonomy_id { get; set; }
    }
}

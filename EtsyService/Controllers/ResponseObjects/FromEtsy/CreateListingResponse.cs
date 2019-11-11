using System;
using System.Collections.Generic;

namespace EtsyService.Controllers.ResponseObjects.FromEtsy
{
    public class ShippingInfo
    {
        public long shipping_info_id { get; set; }
        public int origin_country_id { get; set; }
        public int destination_country_id { get; set; }
        public string currency_code { get; set; }
        public string primary_cost { get; set; }
        public string secondary_cost { get; set; }
        public int listing_id { get; set; }
        public object region_id { get; set; }
        public string origin_country_name { get; set; }
        public string destination_country_name { get; set; }
    }

    public class Result
    {
        public int listing_id { get; set; }
        public string state { get; set; }
        public int user_id { get; set; }
        public int category_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int creation_tsz { get; set; }
        public int ending_tsz { get; set; }
        public int original_creation_tsz { get; set; }
        public int last_modified_tsz { get; set; }
        public string price { get; set; }
        public string currency_code { get; set; }
        public int quantity { get; set; }
        public List<object> sku { get; set; }
        public List<object> tags { get; set; }
        public List<string> category_path { get; set; }
        public List<int> category_path_ids { get; set; }
        public List<string> materials { get; set; }
        public object shop_section_id { get; set; }
        public object featured_rank { get; set; }
        public int state_tsz { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int num_favorers { get; set; }
        public long shipping_template_id { get; set; }
        public int processing_min { get; set; }
        public int processing_max { get; set; }
        public string who_made { get; set; }
        public string is_supply { get; set; }
        public string when_made { get; set; }
        public object item_weight { get; set; }
        public object item_weight_unit { get; set; }
        public object item_length { get; set; }
        public object item_width { get; set; }
        public object item_height { get; set; }
        public object item_dimensions_unit { get; set; }
        public bool is_private { get; set; }
        public object recipient { get; set; }
        public object occasion { get; set; }
        public object style { get; set; }
        public bool non_taxable { get; set; }
        public bool is_customizable { get; set; }
        public bool is_digital { get; set; }
        public string file_data { get; set; }
        public bool can_write_inventory { get; set; }
        public bool should_auto_renew { get; set; }
        public string language { get; set; }
        public bool has_variations { get; set; }
        public int taxonomy_id { get; set; }
        public List<string> taxonomy_path { get; set; }
        public bool used_manufacturer { get; set; }
        public bool is_vintage { get; set; }
        public List<ShippingInfo> ShippingInfo { get; set; }
    }

    public class Params
    {
        public int quantity { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public List<string> materials { get; set; }
        public long shipping_template_id { get; set; }
        public object shop_section_id { get; set; }
        public object image_ids { get; set; }
        public object is_customizable { get; set; }
        public object non_taxable { get; set; }
        public object image { get; set; }
        public string state { get; set; }
        public object shipping_profile_id { get; set; }
        public object primary_shipping_cost { get; set; }
        public object origin_country_id { get; set; }
        public object processing_min { get; set; }
        public object processing_max { get; set; }
        public object category_id { get; set; }
        public int taxonomy_id { get; set; }
        public object tags { get; set; }
        public object should_auto_renew { get; set; }
        public string who_made { get; set; }
        public bool is_supply { get; set; }
        public string when_made { get; set; }
        public object recipient { get; set; }
        public object occasion { get; set; }
        public object style { get; set; }
    }

    public class Pagination
    {
    }

    public class CreateListingResponse
    {
        public int count { get; set; }
        public List<Result> results { get; set; }
        public Params @params { get; set; }
        public string type { get; set; }
        public Pagination pagination { get; set; }
    }
}

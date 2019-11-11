using System;
namespace EtsyService.Controllers.RequestObjects
{
    public class PostListingRequest
    {
        public CreateEtsyListingRequest createListingRequest { get; set; }
        public AddEtsyInventoryAttributesRequest addInventoryAttributesRequest { get; set; }

        public PostListingRequest()
        {
        }
    }
}

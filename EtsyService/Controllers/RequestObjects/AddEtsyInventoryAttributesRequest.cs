using System.Collections.Generic;

namespace EtsyService.Controllers.RequestObjects
{
    public class AddEtsyInventoryAttributesRequest
    {
        public List<EtsyAttribute> etsyAttributes { get; set; }
    }
    public class EtsyAttribute
    {
        public long attributeId { get; set; }
        public long? valueId { get; set; }
    }
}
using System.Collections.Generic;

namespace AlexaSkill.Models.Commerce
{
    public partial class Line
    {
        public string ExternalCartLineId { get; set; }
        public string Image { get; set; }
        public string DisplayName { get; set; }
        public string ProductUrl { get; set; }
        public object ColorInformation { get; set; }
        public object SizeInformation { get; set; }
        public object StyleInformation { get; set; }
        public object GiftCardAmountInformation { get; set; }
        public string Quantity { get; set; }
        public string LinePrice { get; set; }
        public string LineTotal { get; set; }
        public object ShippingOptions { get; set; }
        public List<object> DiscountOfferNames { get; set; }
        public string LineDiscount { get; set; }
        public string ProductId { get; set; }
        public List<object> Errors { get; set; }
        public List<object> Info { get; set; }
        public List<object> Warnings { get; set; }
        public bool HasErrors { get; set; }
        public bool HasInfo { get; set; }
        public bool HasWarnings { get; set; }
        public bool Success { get; set; }
        public object Url { get; set; }
        public object ContentEncoding { get; set; }
        public object ContentType { get; set; }
        public object Data { get; set; }
        public int JsonRequestBehavior { get; set; }
        public object MaxJsonLength { get; set; }
        public object RecursionLimit { get; set; }
    }
}
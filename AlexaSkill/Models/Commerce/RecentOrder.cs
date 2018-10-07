using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlexaSkill.Models.Commerce
{
    public class RecentOrder
    {
        [JsonProperty("Order")] public Order Order { get; set; }

        [JsonProperty("Properties")] public object[] Properties { get; set; }

        [JsonProperty("SystemMessages")] public object[] SystemMessages { get; set; }

        [JsonProperty("Success")] public bool Success { get; set; }
    }

    public class OrderForm
    {
        public object BillingAddressId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public object CartLines { get; set; }
        public object ModifiedBy { get; set; }
        public object Name { get; set; }
        public string OrderFormId { get; set; }
        public object PromoCodeRecords { get; set; }
        public List<object> PromoCodes { get; set; }
        public object PromoUserIdentity { get; set; }
        public object Status { get; set; }
        public object Total { get; set; }
        public List<object> Shipping { get; set; }
        public List<object> Payment { get; set; }
    }

    public class TaxTotal
    {
        public int Amount { get; set; }
        public object Description { get; set; }
        public object Id { get; set; }
        public List<object> TaxSubtotals { get; set; }
    }

    public class Total
    {
        public double Subtotal { get; set; }
        public int HandlingTotal { get; set; }
        public int ShippingTotal { get; set; }
        public int LineItemDiscountAmount { get; set; }
        public int OrderLevelDiscountAmount { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public object Description { get; set; }
        public TaxTotal TaxTotal { get; set; }
    }

    public class Price
    {
        public double ListPrice { get; set; }
        public object LowestPricedVariant { get; set; }
        public object LowestPricedVariantListPrice { get; set; }
        public object HighestPricedVariant { get; set; }
        public double Amount { get; set; }
        public List<object> Conditions { get; set; }
        public string CurrencyCode { get; set; }
        public object Description { get; set; }
        public object PriceType { get; set; }
    }

    public class Product
    {
        public string ProductCatalog { get; set; }
        public object ProductCategory { get; set; }
        public string ProductVariantId { get; set; }
        public string DisplayName { get; set; }
        public object Description { get; set; }
        public List<object> Adjustments { get; set; }
        public int LineNumber { get; set; }
        public List<object> Options { get; set; }
        public string ProductId { get; set; }
        public Price Price { get; set; }
        public string SitecoreProductItemId { get; set; }
        public object StockStatus { get; set; }
        public object InStockDate { get; set; }
        public object ShippingDate { get; set; }
        public string ProductName { get; set; }
    }

    public partial class Line
    {
        public bool AllowBackordersAndPreorders { get; set; }
        public int BackorderQuantity { get; set; }
        public DateTime Created { get; set; }
        public int InStockQuantity { get; set; }
        public object InventoryCondition { get; set; }
        public DateTime LastModified { get; set; }
        public object ModifiedBy { get; set; }
        public string OrderFormId { get; set; }
        public int PreorderQuantity { get; set; }
        public int Index { get; set; }
        public object ShippingAddressId { get; set; }
        public string ShippingMethodId { get; set; }
        public object ShippingMethodName { get; set; }
        public object Status { get; set; }
        public List<object> Adjustments { get; set; }
        public List<object> SubLines { get; set; }
        public Total Total { get; set; }
        public Product Product { get; set; }
        public int LineNumber { get; set; }
    }

    public class TaxSubtotal
    {
        public object BaseUnitMeasure { get; set; }
        public object Description { get; set; }
        public int Percent { get; set; }
        public double PerUnitAmount { get; set; }
        public object TaxSubtotalType { get; set; }
    }

    public class TaxTotal2
    {
        public double Amount { get; set; }
        public object Description { get; set; }
        public object Id { get; set; }
        public List<TaxSubtotal> TaxSubtotals { get; set; }
    }

    public class Total2
    {
        public double Subtotal { get; set; }
        public int HandlingTotal { get; set; }
        public int ShippingTotal { get; set; }
        public int LineItemDiscountAmount { get; set; }
        public int OrderLevelDiscountAmount { get; set; }
        public double Amount { get; set; }
        public string CurrencyCode { get; set; }
        public object Description { get; set; }
        public TaxTotal2 TaxTotal { get; set; }
    }

    public class Party
    {
        public string UserProfileAddressId { get; set; }
        public string CountryCode { get; set; }
        public object EveningPhoneNumber { get; set; }
        public object FaxNumber { get; set; }
        public string Name { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string PartyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public object Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
        public string ExternalId { get; set; }
    }

    public class ShippingOptionType
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public class Shipping
    {
        public ShippingOptionType ShippingOptionType { get; set; }
        public string ShippingMethodName { get; set; }
        public string ElectronicDeliveryEmail { get; set; }
        public string ElectronicDeliveryEmailContent { get; set; }
        public List<string> LineIDs { get; set; }
        public string ShippingMethodID { get; set; }
        public object ShippingProviderID { get; set; }
        public string PartyID { get; set; }
        public string ExternalId { get; set; }
    }

    public class Payment
    {
        public double Amount { get; set; }
        public string AuthorizationResult { get; set; }
        public string CardToken { get; set; }
        public List<object> LineIDs { get; set; }
        public string PaymentMethodID { get; set; }
        public object PaymentProviderID { get; set; }
        public string PartyID { get; set; }
        public string ExternalId { get; set; }
    }

    public class Order
    {
        public DateTime Created { get; set; }
        public bool IsDirty { get; set; }
        public bool IsEmpty { get; set; }
        public DateTime LastModified { get; set; }
        public int LineItemCount { get; set; }
        public object ModifiedBy { get; set; }
        public List<OrderForm> OrderForms { get; set; }
        public object SoldToAddressId { get; set; }
        public object SoldToName { get; set; }
        public object StatusCode { get; set; }
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string TrackingNumber { get; set; }
        public bool IsOfflineOrder { get; set; }
        public List<object> Adjustments { get; set; }
        public List<Line> Lines { get; set; }
        public Total2 Total { get; set; }
        public List<Party> Parties { get; set; }
        public List<Shipping> Shipping { get; set; }
        public List<Payment> Payment { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string CurrencyCode { get; set; }
        public object IsLocked { get; set; }
        public string Status { get; set; }
        public object CartType { get; set; }
        public object BuyerCustomerParty { get; set; }
        public object AccountingCustomerParty { get; set; }
        public object LoyaltyCardID { get; set; }
        public string Email { get; set; }
        public string ExternalId { get; set; }
    }
}
using System.Collections.Generic;

namespace AlexaSkill.Models.Commerce
{
    public class TopProduct
    {
        public string CatalogName { get; set; }
        public string DisplayName { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public bool IsCategory { get; set; }
        public object ParentCategoryId { get; set; }
        public object ParentCategoryName { get; set; }
        public string SummaryImageUrl { get; set; }
        public string Link { get; set; }
        public string ProductId { get; set; }
        public string CurrencySymbol { get; set; }
        public int CustomerAverageRating { get; set; }
        public bool IsOnSale { get; set; }
        public double ListPrice { get; set; }
        public double AdjustedPrice { get; set; }
        public string AdjustedPriceWithCurrency { get; set; }
        public string ListPriceWithCurrency { get; set; }
        public object LowestPricedVariantAdjustedPrice { get; set; }
        public string LowestPricedVariantAdjustedPriceWithCurrency { get; set; }
        public object LowestPricedVariantListPrice { get; set; }
        public string LowestPricedVariantListPriceWithCurrency { get; set; }
        public object HighestPricedVariantAdjustedPrice { get; set; }
        public int VariantSavingsPercentage { get; set; }
        public int SavingsPercentage { get; set; }
        public object Quantity { get; set; }
        public object StockStatus { get; set; }
        public object StockAvailabilityDate { get; set; }
        public object StockStatusName { get; set; }
        public bool DisplayStartingFrom { get; set; }
        public string PriceStartingFromText { get; set; }
        public string ProductPageLinkText { get; set; }
        public string SavePercentLead { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public bool IsVariant { get; set; }
        public object Variants { get; set; }
        public List<object> VariantDefinitions { get; set; }
        public object GiftCardAmountOptions { get; set; }
    }
}
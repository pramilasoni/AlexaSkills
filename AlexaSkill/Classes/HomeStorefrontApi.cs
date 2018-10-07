using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AlexaSkill.Models.Commerce;
using Newtonsoft.Json;

#pragma warning disable 1587
/// <summary>
/// Use postman
//  1.	For recent order of the user : http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot/Cartbot/GetRecentOrders ( Post Request ) 
//  2.	Get Top Product : http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot/Cartbot/GetPromotedProductsJsonResult ( Post Request)
//              currentCatalogItemId : {803E72C3-5ADC-EBE3-7AEE-5956712C95C8}
//              relationshipFieldId: {A96D901B-BF4B-4960-9F55-65485F2C2FE7}
//  3.	Get Mini cart : http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot/Cartbot/GetMinicart ( Get Request )
//  4.	Add cart : http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot/Cartbot/AddCartLine ( post )
//              addToCart_CatalogName : Habitat_Master
//              addToCart_ProductId : anything(example Mira laptop ) 6042185
///              quantity: 20
/// </summary>
#pragma warning restore 1587
namespace AlexaSkill.Classes
{
    public class HomeStorefrontApi
    {
        private readonly string _baseUrl = "http://homestorefront.northeurope.cloudapp.azure.com/api/BabyBot";

        public async Task<MiniCart> MiniCart()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(_baseUrl + "/Cartbot/GetMinicart");
                var contentResp = await response.Result.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MiniCart>(contentResp);
                return result;
            }
        }

        public async Task<IList<TopProduct>> TopProduct()
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"relationshipFieldId", "{A96D901B-BF4B-4960-9F55-65485F2C2FE7}"},
                    {"currentCatalogItemId", "{803E72C3-5ADC-EBE3-7AEE-5956712C95C8}"}
                };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync(_baseUrl + "/Cartbot/GetPromotedProductsJsonResult", content);
                var contentResp = await response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IList<TopProduct>>(contentResp);
                return result;
            }
        }

        public async Task<RecentOrder> GetRecentOrders()
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>());
                var response = client.PostAsync(_baseUrl + "/Cartbot/GetRecentOrders", content);
                var contentResp = await response.Result.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<RecentOrder>(contentResp);
                return result;
            }
        }

        public async Task<bool> AddCartLine(string catalog, string productId)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"addToCart_CatalogName", catalog},
                    {"addToCart_ProductId", productId},
                    {"quantity", "1"}
                };
                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync(_baseUrl + "/Cartbot/AddCartLine", content);
                if (response.Result.StatusCode == HttpStatusCode.OK) return true;

                return false;
            }
        }
    }
}
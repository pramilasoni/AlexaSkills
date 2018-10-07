using System;
using System.Linq;
using System.Text;
using AlexaSkill.Adapters.Twitter;
using AlexaSkill.Classes;
using AlexaSkill.Data;
using AlexaSkill.Models;
using AlexaSkill.Models.Alexa.Response;
using Microsoft.Ajax.Utilities;

namespace AlexaSkill.Adapters
{
    public class IntentHandlers
    {
        public static bool IsDemo { get; set; }

        public static AlexaResponse LaunchRequestIntentHandler(Request request)
        {
            var response =
                new AlexaResponse(
                    "Welcome to sitecore storefront. What would you like to hear, the Top Products or New products?", 
                    new SimpleCard()
                    {
                        Title = "sitecore storefront",
                        Content = "Hello\ncruel world!, What would you like to hear about, the Top Products or New products?"

                    })
                {
                    Session = {MemberId = request.MemberId}
                }; 
            response.Response.Reprompt.OutputSpeech.Text = "Please pick one, Top products or New products?";
            response.Response.ShouldEndSession = false;
            return response;
        }

        internal static AlexaResponse ShowMyBasketintentHandler(Request request)
        {
            var text = new StringBuilder("Your Current Basket is ");
            if (IsDemo)
            {
            }
            else
            {
                var homeStorefrontApi = new HomeStorefrontApi();
                var miniCart = homeStorefrontApi.MiniCart().Result;
                miniCart.Lines.ForEach(c =>
                    text.AppendFormat("{0} and price {1}. ", c.DisplayName, c.LinePrice));
            }

            return new AlexaResponse(text.ToString(),
                new SimpleCard()
                {
                    Title = "sitecore storefront.",
                    Content = text.ToString()

                }, true, text.ToString());
        }

        internal static AlexaResponse ShowBioIntentHandler(Request request)
        {
            var text =
                "Sumith Damodaran ! working as Director of engineering at <emphasis level=\"strong\">THINK</emphasis>, <break time=\"1s\"/> Previously he was working with <prosody pitch=\"medium\">Sitecore UK</prosody><amazon:effect name=\"whispered\"></amazon:effect> Leading the Services team. He has nearly 20 years of industry experience. Would you like to know more about his experience.";
            var response = new AlexaResponse(text,  
                new SimpleCard()
                {
                    Title = "sitecore storefront",
                    Content = text

                }, true,text);
            return response;
        }

        internal static AlexaResponse WhereIsTheOrderIntentHandler(Request request)
        {
            var output = new StringBuilder();
            var homeStorefrontApi = new HomeStorefrontApi();
            var recentOrder = homeStorefrontApi.GetRecentOrders().Result;
          
                output.AppendFormat("The order placed on {0} in route with delivery. " +
                                    "Tracking  number <say-as interpret-as=\"spell-out\">{1}</say-as>. Total Amount {2} {3}", 
                    recentOrder.Order.OrderDate.ToShortDateString(),recentOrder.Order.TrackingNumber.Substring(0,5), recentOrder.Order.Total.Amount, recentOrder.Order.Total.CurrencyCode);

            var response = new AlexaResponse(output.ToString(),
                new SimpleCard()
                {
                    Title = "Sitecore storefront",
                    Content = output.ToString()
                }, true, output.ToString());
            return response;
        }

        internal static AlexaResponse PickAWinnerIntentHandler(Request request)
        {
            TwitterAdapter.ConsumerKey = TwitterConfiguration.ConsumerKeyApiKey;
            TwitterAdapter.ConsumerSecret = TwitterConfiguration.ConsumerSecretApiSecret;
            var query = TwitterConfiguration.DefaultQuery;
            var results = TwitterAdapter.SearchAsync(query);

            var model = new TwitterSearch {Query = query, TwitterResult = results};
            var result = model.TwitterResult[new Random().Next(model.TwitterResult.Count)];

            using (var db = new alexaskilldemoEntities())
            {
                var competitionWinner = db.CompetitionWinners.FirstOrDefault();

                if (competitionWinner == null)
                {
                    db.CompetitionWinners.Add(new CompetitionWinner
                    {
                        Name = result.ScreenNameResponse,
                        Tweet = result.Text,
                        CreatedDate = result.CreatedAt,
                        ProfileImageUrl = result.ProfileImageUrl,
                        UpdatedDate = DateTime.UtcNow
                    });
                }
                else
                {
                    competitionWinner.Name = result.ScreenNameResponse;
                    competitionWinner.Tweet = result.Text;
                    competitionWinner.CreatedDate = result.CreatedAt;
                    competitionWinner.ProfileImageUrl = result.ProfileImageUrl;
                    competitionWinner.UpdatedDate = DateTime.UtcNow;
                }

                db.SaveChanges();
            }

            var text = "I have selected user <prosody volume=\"x-loud\">" + result.ScreenNameResponse + "</prosody><emphasis level=\"moderate\"> Tweet <break time=\"1s\"/>" + result.Text + "</emphasis>" +
                "<audio src='https://s3.amazonaws.com/ask-soundlibrary/human/amzn_sfx_crowd_cheer_med_01.mp3'/> " +
                       "<audio src='https://s3.amazonaws.com/ask-soundlibrary/human/amzn_sfx_crowd_applause_05.mp3'/>";
            var response = new AlexaResponse(text, new SimpleCard()
            {
                Title = "Sitecore storefront",
                Content = text

            }, true, text );
            return response;
        }

        internal static AlexaResponse AddProductToCartIntentHandler(Request request)
        {
            var homeStorefrontApi = new HomeStorefrontApi();
            var cartadded = homeStorefrontApi.AddCartLine("Habitat_Master", "6042185").Result;
            var text = new StringBuilder();
            text.AppendFormat("Product Mira laptop added to your existing cart.");
            var response = new AlexaResponse(text.ToString(), new SimpleCard()
            {
                Title = "Sitecore storefront",
                Content = text.ToString()
            });
            return response;
        }

        internal static AlexaResponse HelpIntent(Request request)
        {
            string text =
                "To use the Sitecore storefront skill, you can say, Alexa, ask Sitecore storefront for top products, to retrieve the top products or say, Alexa, ask Sitecore storefront for the new products, to retrieve the latest new products. You can also say, Alexa, stop or Alexa, cancel, at any time to exit the Sitecore storefront skill. For now, do you want to hear the Top products or New products?";
            var response = new AlexaResponse(text,
                 new SimpleCard()
                {
                    Title = "Sitecore storefront",
                    Content = text
                },false);
            response.Response.Reprompt.OutputSpeech.Text = "Please select one, top products or new products?";
            return response;
        }

        internal static AlexaResponse CancelOrStopIntentHandler(Request request)
        {
            string text = "Thanks for listening, let's talk again soon.";
            return new AlexaResponse(text,
                new SimpleCard()
                {
                    Title = "Sitecore storefront",
                    Content = text
                }, true);
        }

        internal static AlexaResponse NewProductsIntentHandler(Request request)
        {
            var output = new StringBuilder("Here are the latest products. ");

            using (var db = new alexaskilldemoEntities())
            {
                db.Products.Take(10).OrderByDescending(c => c.CreatedDate).ToList()
                    .ForEach(c => output.AppendFormat("{0} cost {1}. ", c.ProductName, c.Price));
            }

            return new AlexaResponse(output.ToString(),
                new StandardCard()
                {
                    Title = "Sitecore storefront",
                    Content = output.ToString()
                });
        }

        internal static AlexaResponse TopProductsIntentHandler(Request request)
        {
            var limit = 10;
            var criteria = string.Empty;

            if (request.SlotsList.Any())
            {
                var maxLimit = 10;
                var limitValue = request.SlotsList.FirstOrDefault(s => s.Key == "Limit").Value;

                if (!string.IsNullOrWhiteSpace(limitValue) && int.TryParse(limitValue, out limit) &&
                    !(limit >= 1 && limit <= maxLimit)) limit = maxLimit;

                criteria = request.SlotsList.FirstOrDefault(s => s.Key == "Criteria").Value;
            }

            var output = new StringBuilder();
            output.AppendFormat("Here are the top {0} {1}. ", limit,
                string.IsNullOrWhiteSpace(criteria) ? "products" : criteria);
            if (IsDemo)
            {
                using (var db = new alexaskilldemoEntities())
                {
                    if (criteria == "make")
                        db.Products.Take(limit).OrderByDescending(c => c.Votes).ToList()
                            .ForEach(c => output.AppendFormat("{0}. ", c.Description));
                    else
                        db.Products.Take(limit).OrderByDescending(c => c.Votes).ToList()
                            .ForEach(c => output.AppendFormat("{0} and price {1}. ", c.ProductName, c.Price));
                }
            }
            else
            {
                var homeStorefrontApi = new HomeStorefrontApi();
                var topProducts = homeStorefrontApi.TopProduct().Result;
                topProducts.ForEach(c =>
                    output.AppendFormat("{0} and price {1}. ", c.DisplayName, c.ListPriceWithCurrency));
            }

            return new AlexaResponse(output.ToString(),
                new SimpleCard()
                {
                    Title = "Sitecore storefront",
                    Content = output.ToString()
                });
        }
    }
}
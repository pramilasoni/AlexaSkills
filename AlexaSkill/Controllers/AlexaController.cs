using System;
using System.Web.Http;
using AlexaSkill.Adapters;
using AlexaSkill.Classes;
using AlexaSkill.Data;
using AlexaSkill.Models;

namespace AlexaSkill.Controllers
{
    public class AlexaController : ApiController
    {
        [HttpPost]
        [Route("api/alexa/HabitatHome")]
        public dynamic HabitatHome(AlexaRequest alexaRequest)
        {
            var request = new Requests().Create(new Request
            {
                MemberId = alexaRequest.Session.Attributes?.MemberId ?? 0,
                TimeStamp = alexaRequest.Request.Timestamp,
                Intent = alexaRequest.Request.Intent == null ? "" : alexaRequest.Request.Intent.Name,
                AppId = alexaRequest.Session.Application.ApplicationId,
                RequestId = alexaRequest.Request.RequestId,
                SessionId = alexaRequest.Session.SessionId,
                UserId = alexaRequest.Session.User.UserId,
                IsNew = alexaRequest.Session.New,
                Version = alexaRequest.Version,
                Type = alexaRequest.Request.Type,
                Reason = alexaRequest.Request.Reason,
                SlotsList = alexaRequest.Request.Intent?.GetSlots(),
                DateCreated = DateTime.UtcNow
            });

            AlexaResponse response = null;


            switch (request.Type)
            {
                case "LaunchRequest":
                    response = LaunchRequestHandler(request);
                    break;
                case "IntentRequest":
                    response = IntentRequestHandler(request);
                    break;
                case "SessionEndedRequest":
                    response = SessionEndedRequestHandler(request);
                    break;
            }

            return response;
        }

        private AlexaResponse LaunchRequestHandler(Request request)
        {
            var response = IntentHandlers.LaunchRequestIntentHandler(request);

            return response;
        }


        private AlexaResponse IntentRequestHandler(Request request)
        {
            AlexaResponse response = null;

            switch (request.Intent)
            {
                case AlexaIntents.Showbiointent:
                    response = IntentHandlers.ShowBioIntentHandler(request);
                    break;
                case AlexaIntents.Newproductsintent:
                    response = IntentHandlers.NewProductsIntentHandler(request);
                    break;
                case AlexaIntents.Topproductsintent:
                    response = IntentHandlers.TopProductsIntentHandler(request);
                    break;
                case AlexaIntents.AmazonCancelintent:
                case AlexaIntents.AmazonStopintent:
                    response = IntentHandlers.CancelOrStopIntentHandler(request);
                    break;
                case AlexaIntents.AmazonHelpintent:
                    response = IntentHandlers.HelpIntent(request);
                    break;
                case AlexaIntents.ShowMyBasketintent:
                    response = IntentHandlers.ShowMyBasketintentHandler(request);
                    break;
                case AlexaIntents.Whereistheorderintent:
                    response = IntentHandlers.WhereIsTheOrderIntentHandler(request);
                    break;
                case AlexaIntents.AddProductToCartintent:
                    response = IntentHandlers.AddProductToCartIntentHandler(request);
                    break;
                case AlexaIntents.PickAWinnerintent:

                    response = IntentHandlers.PickAWinnerIntentHandler(request);
                    break;
            }

            return response;
        }


        private AlexaResponse SessionEndedRequestHandler(Request request)
        {
            return null;
        }
    }
}
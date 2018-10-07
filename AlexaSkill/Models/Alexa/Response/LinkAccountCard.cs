using Newtonsoft.Json;

namespace AlexaSkill.Models.Alexa.Response
{
    public class LinkAccountCard : ICard
    {
        [JsonProperty("type")]
        public string Type
        {
            get { return "LinkAccount"; }
        }
    }
}
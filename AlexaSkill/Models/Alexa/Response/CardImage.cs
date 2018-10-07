using Newtonsoft.Json;

namespace AlexaSkill.Models.Alexa.Response
{
    public class CardImage
    {
        [JsonProperty("smallImageUrl")]
        [JsonRequired]
        public string SmallImageUrl { get; set; }

        [JsonProperty("largeImageUrl")]
        [JsonRequired]
        public string LargeImageUrl { get; set; }
    }
}

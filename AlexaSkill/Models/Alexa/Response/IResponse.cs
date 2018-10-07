using Newtonsoft.Json;

namespace AlexaSkill.Models.Alexa.Response
{
    public interface IResponse
    {
        [JsonRequired]
        string Type { get; }
    }
}

using AlexaSkill.Models.Alexa.Response;
using Newtonsoft.Json;

namespace AlexaSkill.Models
{
    [JsonObject]
    public class AlexaResponse
    {
        public AlexaResponse()
        {
            Version = "1.0";
            Session = new SessionAttributes();
            Response = new ResponseAttributes();
        }
        public AlexaResponse(string outputSpeechText, ICard card)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card = card;
        }

        public AlexaResponse(string outputSpeechText, ICard card, bool shouldEndSession)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.ShouldEndSession = shouldEndSession;

            Response.Card = shouldEndSession ? card : null;
        }

        public AlexaResponse(string outputSpeechText, ICard card, bool isSsml, string ssmlVersion)
            : this()
        {
            Response.OutputSpeech.Text = outputSpeechText;
            Response.Card = card;
            Response.OutputSpeech.Type = "SSML";
            Response.OutputSpeech.Ssml = "<speak>" + ssmlVersion + "</speak>";
        }

       

        [JsonProperty("version")] public string Version { get; set; }

        [JsonProperty("sessionAttributes")] public SessionAttributes Session { get; set; }

        [JsonProperty("response")] public ResponseAttributes Response { get; set; }

        [JsonObject("sessionAttributes")]
        public class SessionAttributes
        {
            [JsonProperty("memberId")] public int MemberId { get; set; }
        }

        [JsonObject("response")]
        public class ResponseAttributes
        {
            public ResponseAttributes()
            {
                ShouldEndSession = true;
                OutputSpeech = new OutputSpeechAttributes();
                Card = new SimpleCard();
                Reprompt = new RepromptAttributes();
            }

            [JsonProperty("shouldEndSession")] public bool ShouldEndSession { get; set; }

            [JsonProperty("outputSpeech")] public OutputSpeechAttributes OutputSpeech { get; set; }
             
            [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
            public ICard Card { get; set; }
            [JsonProperty("reprompt")] public RepromptAttributes Reprompt { get; set; }

            [JsonObject("outputSpeech")]
            public class OutputSpeechAttributes
            {
                public OutputSpeechAttributes()
                {
                    Type = "PlainText";
                }

                [JsonProperty("type")] public string Type { get; set; }

                [JsonProperty("text")] public string Text { get; set; }

                [JsonProperty("ssml")] public string Ssml { get; set; }
            }

       
            [JsonObject("reprompt")]
            public class RepromptAttributes
            {
                public RepromptAttributes()
                {
                    OutputSpeech = new OutputSpeechAttributes();
                }

                [JsonProperty("outputSpeech")] public OutputSpeechAttributes OutputSpeech { get; set; }
            }
        }
    }
}
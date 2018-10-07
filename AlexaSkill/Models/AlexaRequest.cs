using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlexaSkill.Models
{
    [JsonObject]
    public class AlexaRequest
    {
        [JsonProperty("version")] public string Version { get; set; }

        [JsonProperty("session")] public Session Session { get; set; }

        [JsonProperty("context")] public Context Context { get; set; }

        [JsonProperty("request")] public RequestAttributes Request { get; set; }
    }

    public class Context
    {
        [JsonProperty("System")] public SystemClass System { get; set; }
    }

    public class SystemClass
    {
        [JsonProperty("application")] public Application Application { get; set; }

        [JsonProperty("user")] public UserAttributes User { get; set; }

        [JsonProperty("device")] public Device Device { get; set; }

        [JsonProperty("apiEndpoint")] public string ApiEndpoint { get; set; }

        [JsonProperty("apiAccessToken")] public string ApiAccessToken { get; set; }
    }

    public class Application
    {
        [JsonProperty("applicationId")] public string ApplicationId { get; set; }
    }

    public class Device
    {
        [JsonProperty("deviceId")] public string DeviceId { get; set; }

        [JsonProperty("supportedInterfaces")] public SupportedInterfaces SupportedInterfaces { get; set; }
    }

    public class SupportedInterfaces
    {
    }

    [JsonObject("user")]
    public class UserAttributes
    {
        [JsonProperty("userId")] public string UserId { get; set; }

        [JsonProperty("accessToken")] public string AccessToken { get; set; }
    }

    [JsonObject("request")]
    public class RequestAttributes
    {
        private double _timestamp;
        private string _timestampEpoch;

        public RequestAttributes()
        {
            Intent = new IntentAttributes();
        }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("requestId")] public string RequestId { get; set; }

        [JsonProperty("timestamp")]
        public string TimestampEpoch
        {
            get => _timestampEpoch;
            set
            {
                _timestampEpoch = value;

                if (double.TryParse(value, out _timestamp) && _timestamp > 0)
                {
                    Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(_timestamp);
                }
                else
                {
                    var timeStamp = DateTime.MinValue;
                    if (DateTime.TryParse(_timestampEpoch, out timeStamp))
                        Timestamp = timeStamp.ToUniversalTime();
                }
            }
        }

        [JsonIgnore] public DateTime Timestamp { get; set; }

        [JsonProperty("intent")] public IntentAttributes Intent { get; set; }

        [JsonProperty("reason")] public string Reason { get; set; }

        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("shouldLinkResultBeReturned")]
        public bool ShouldLinkResultBeReturned { get; set; }

        [JsonObject("intent")]
        public class IntentAttributes
        {
            [JsonProperty("name")] public string Name { get; set; }

            [JsonProperty("slots")] public dynamic Slots { get; set; }

            public List<KeyValuePair<string, string>> GetSlots()
            {
                var output = new List<KeyValuePair<string, string>>();
                if (Slots == null) return output;

                foreach (var slot in Slots.Children())
                    if (slot.First.value != null)
                        output.Add(new KeyValuePair<string, string>(slot.First.name.ToString(),
                            slot.First.value.ToString()));

                return output;
            }
        }
    }

    public class Session
    {
        [JsonProperty("new")] public bool New { get; set; }

        [JsonProperty("sessionId")] public string SessionId { get; set; }

        [JsonProperty("application")] public Application Application { get; set; }

        [JsonProperty("user")] public UserAttributes User { get; set; }

        [JsonProperty("attributes")] public SessionCustomAttributes Attributes { get; set; }
    }

    [JsonObject("attributes")]
    public class SessionCustomAttributes
    {
        [JsonProperty("memberId")] public int MemberId { get; set; }
    }
}
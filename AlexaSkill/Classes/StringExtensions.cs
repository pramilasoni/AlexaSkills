using System;
using System.Collections.Generic;
using System.Text;

namespace AlexaSkill.Classes
{
    public static class StringExtensions
    {
        public static string ToWebString(this Dictionary<string, string> source)
        {
            var body = new StringBuilder();

            foreach (var requestParameter in source)
            {
                body.Append(requestParameter.Key);
                body.Append("=");
                body.Append(Uri.EscapeDataString(requestParameter.Value));
                body.Append("&");
            }

            //remove trailing '&'
            body.Remove(body.Length - 1, 1);

            return body.ToString();
        }
    }
}
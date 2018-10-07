using System.Collections.Generic;
using AlexaSkill.Adapters.Twitter;

namespace AlexaSkill.Models
{
    public class TwitterSearch
    {
        public string Query { get; set; }

        public List<TwitterResult> TwitterResult { get; set; }
    }
}
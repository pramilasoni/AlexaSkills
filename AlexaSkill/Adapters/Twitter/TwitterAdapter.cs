using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace AlexaSkill.Adapters.Twitter
{
    public static class TwitterAdapter
    {
        public static string ConsumerKey;

        public static string ConsumerSecret;

        public static List<TwitterResult> SearchAsync(string query)
        {
            var auth = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret
                }
                //CredentialStore =
                //    new SessionStateCredentialStore()
                //    {
                //        ConsumerKey = ConsumerKey,
                //        ConsumerSecret = ConsumerSecret,
                //    }
            };

            var task = auth.AuthorizeAsync();
            task.Wait();
            var twitterCtx = new TwitterContext(auth);

            var searchResults =
                (from search in twitterCtx.Search
                    where search.Type == SearchType.Search &&
                          search.Query == query
                    select search.Statuses)
                .SingleOrDefault();

            var twitterResult = new List<TwitterResult>();

            if (searchResults != null && searchResults.Count > 0)
                foreach (var result in searchResults)
                    twitterResult.Add(new TwitterResult
                    {
                        Text = result.Text,
                        Url = result.User.Url,
                        ProfileImageUrl = result.User.ProfileImageUrl,
                        ScreenNameResponse = result.User.ScreenNameResponse,
                        CreatedAt = result.CreatedAt
                    });
            return twitterResult;
        }
    }

    public class TwitterResult
    {
        public string Html =>
            $"<blockquote class=\"twitter-tweet\"><p>{Text}</p><p><a href=\"{Url}\"><img src=\"{ProfileImageUrl}\"/>{ScreenNameResponse}</a> - {CreatedAt}</p></blockquote>";

        public string Text { get; set; }
        public string Url { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ScreenNameResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
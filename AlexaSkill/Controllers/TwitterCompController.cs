using System.Threading.Tasks;
using System.Web.Mvc;
using AlexaSkill.Adapters.Twitter;
using AlexaSkill.Classes;
using AlexaSkill.Models;
using AlexaSkill.Twitter;

namespace AlexaSkill.Controllers
{
    public class TwitterCompController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            var twitter = new TwitterApi(TwitterConfiguration.ConsumerKeyApiKey,
                TwitterConfiguration.ConsumerSecretApiSecret, TwitterConfiguration.AccessToken,
                TwitterConfiguration.AccessTokenSecret);
            var result = Task.Run(async () => { return await twitter.SearchTweets("sitecore", 100); }).Result;

            return View(result);
        }

        [ActionName("TwitterSearch")]
        public async Task<ActionResult> TwitterSearchAsync(string query)
        {
            TwitterAdapter.ConsumerKey = TwitterConfiguration.ConsumerKeyApiKey;
            TwitterAdapter.ConsumerSecret = TwitterConfiguration.ConsumerSecretApiSecret;
            var results = TwitterAdapter.SearchAsync(query);

            var model = new TwitterSearch {Query = query, TwitterResult = results};

            return View(model);
        }
    }
}
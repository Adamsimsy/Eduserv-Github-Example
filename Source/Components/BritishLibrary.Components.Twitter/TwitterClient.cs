using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace BritishLibrary.Components.Twitter
{
    public class TwitterClient
    {
        private string _consumerKey = "22NBcg6hb2tYay78f3vFqQ";
        private string _consumerSecret = "R4I9N1KUHUXgKpfgmowUVXJmDZLgCr77gatwTkcD4o";
        private string _accessToken = "107821248-JK0flGmKv7MrNVuNilqAzkYizIXya4S0ODdEQ8no";
        private string _accessTokenSecret = "Yv7BlpBehXitCW3fgcFE4dBKFMoQRWjvkcZnyWNjU0";
        private TwitterService service;

        public TwitterClient()
        {
            service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);
        }

        public List<KeyValuePair<string, string>> GetTimelineTweets()
        {
            var tweets = new List<KeyValuePair<string,string>>();

            var timelineTweets = service.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = "adamsimsy" });

            timelineTweets.ToList().ForEach(x => tweets.Add(new KeyValuePair<string,string>(x.Author.ScreenName, x.Text)));

            return tweets;
        }

        public void Tweet(string status)
        {
            service.SendTweet(new SendTweetOptions { Status = status });
        }
    }
}

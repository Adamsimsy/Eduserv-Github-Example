using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BritishLibrary.Components.Twitter;

namespace BritishLibrary.Components.Twitter.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterClient client = new TwitterClient();

            Console.WriteLine(client.GetTimelineTweets().First().Value);
            var status = Console.ReadLine();

            client.Tweet(status);
        }
    }
}

using System;
using System.Net;
using Quixote;

namespace Sitecore.Glimpse.Smoke.Test
{
    internal class TestRunner
    {
        public static void Execute(string url)
        {
            var siteToTest = new Uri(url);

            Runner.SiteRoot = siteToTest.ToString();

            TheFollowing.Describes("Home Page");

            It.Should("Have 'Welcome to Sitecore' in the title", () =>
                {
                    return Runner.Get("/").Title.ShouldContain("Welcome to Sitecore");
                });

            It.ShouldNot("Not have Glimpse script tag in the body", () =>
                {
                    return Runner.Get("/").Body.ShouldNotContain("/Glimpse.axd?n=glimpse_client");
                });

            TheFollowing.Describes("Glimpse Configuration Page");

            It.Should("Have 'Configuration Page' in the title", () =>
                {
                    return Runner.Get("/glimpse.axd").Title.ShouldContain("Configuration Page");
                });

            TheFollowing.Describes("Home Page with Glimpse Enabled");

            It.Should("Have Glimpse script tag in the body", () =>
                {
                    var cookie = new Cookie("glimpsePolicy", "On") { Domain = siteToTest.Host };
                    return Runner.Get("/", cookie).Body.ShouldContain("/Glimpse.axd?n=glimpse_client");
                });

        }
    }
}
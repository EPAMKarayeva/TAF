using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;

namespace TAF.Tests.TestClasses
{
    internal class BaseTestClass
    {
        protected static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient()
        {
            _client = new RestClient("http://localhost:8080");
        }

        protected RestRequest RequestWithAuth(string url)
        {
            return RequestWithoutAuth(url)
              .AddHeader("Authorization", $"Bearer {DashboardUrl.ApiKey}")
              .AddUrlSegment("user", DashboardUrl.AdminUserName);
        }

        protected RestRequest RequestWithoutAuth(string url)
        {
            return new RestRequest(url);
        }

        [OneTimeTearDown]
        public static void TearDownRestClient()
        {
            _client?.Dispose();
        }
    }
}

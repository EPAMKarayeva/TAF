using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;
using TAF.Tests.TestClasses;

namespace TAF.Tests.API_tests.GET
{
  internal class GetTests : BaseTestClass
  {
    [Test]
    public void CheckGetAllDashboardsTest()
    {
      var request = RequestWithAuth(DashboardEnpoints.GetAllDashboardsUrl);

      //var client = new RestClient("http://localhost:8080");
      //var request = new RestRequest("/api/v1/superadmin_personal/dashboard")
      //.AddHeader("Authorization", $"Bearer {DashboardUrl.ApiKey}");
      var response = _client.Get(request);

      Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
    }

    [Test]
    public void CheckGetDashboarsTest()
    {
      var request = RequestWithAuth(DashboardEnpoints.GetDashboardUrl)
          .AddQueryParameter("field", "id,name")
          .AddUrlSegment("id", DashboardUrl.ExistingDashboardId);

      var response = _client.Get(request);
      var responseContent = JToken.Parse(response.Content);

      Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
      Assert.That("DEMO DASHBOARD", Is.EqualTo(JToken.Parse(response.Content).SelectToken("name").ToString()));
    }
  }
}
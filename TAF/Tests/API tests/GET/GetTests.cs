using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;

namespace TAF.Tests.API_tests.GET
{
  internal class GetTests : BaseTestClass
  {

    [Test]
    public void CheckGetAllDashboardsTest()
    {
      var request = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      var response = _client.Execute(request);
      var responseContent = JToken.Parse(response.Content);

      bool dashboardsExists = responseContent["content"]
                           .Children<JObject>()
                           .Any();

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      Assert.That(dashboardsExists, Is.True);

    }

    [Test]
    public void CheckGetDashboardTest()
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Get)
          .AddQueryParameter("fields", "id,name")
          .AddUrlSegment("id", DashboardUrl.ExistingDashboardId);

      RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
      Assert.That(response.Data.Name, Is.EqualTo("DEMO DASHBOARD"));
    }
  }
}
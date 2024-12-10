using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;

namespace TAF.Tests.API_tests.PUT
{
  public class PutTests : BaseTestClass
  {
    [Test]
    public void CheckUpdateDashboard()
    {
      var newName = "TEST DASHBOARD " + DateTime.Now;

      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

      var getRequest = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);
      var getResponse = _client.Execute(getRequest);
      var responseContent = JToken.Parse(getResponse.Content);

      bool dashboardExists = responseContent["content"]
                            .Children<JObject>()
                            .Any(item => item["name"].ToString() == newName);

      Assert.That(dashboardExists, Is.True);
    }
  }
}
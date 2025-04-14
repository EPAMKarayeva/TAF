using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;

namespace TAF.Tests.APITests.PutDashboard
{
  public class PutDashboardPositiveScenarios : BaseAPITest
  {
    [Test]
    public void CheckUpdateDashboard()
    {
      //Arrange
      var newName = "TEST DASHBOARD " + DateTime.Now;

      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

      //Arrange
      var getRequest = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      //Act
      var getResponse = _client.Execute(getRequest);
      var responseContent = JToken.Parse(getResponse.Content);

      bool dashboardExists = responseContent["content"]
                            .Children<JObject>()
                            .Any(item => item["name"].ToString() == newName);

      //Assert
      Assert.That(dashboardExists, Is.True);
    }
  }
}
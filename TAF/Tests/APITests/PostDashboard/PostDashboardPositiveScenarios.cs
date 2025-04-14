using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;

namespace TAF.Tests.APITests.PostDashboard
{
  public class PostDashboardPositiveScenarios : BaseAPITest
  {
    private int dashboardId;

    [Test]
    public void CheckPostDashboardTest()
    {
      //Arrange
      var dashboardName = "New Dashboard " + DateTime.Now;
      var description = "Description " + DateTime.Now;

      var request = RequestWithAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

      //Act
      RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

      dashboardId = response.Data.Id;

      //Assert
      Assert.That(HttpStatusCode.Created, Is.EqualTo(response.StatusCode));


      //Arrange
      var getRequest = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      //Act
      var getResponse = _client.Execute(getRequest);
      var responseContent = JToken.Parse(getResponse.Content);

      bool dashboardExists = responseContent["content"]
                            .Children<JObject>()
                            .Any(item => item["name"].ToString() == dashboardName &&
                            int.Parse(item["id"].ToString()) == dashboardId);

      //Assert
      Assert.That(dashboardExists, Is.True);
    }

    [TearDown]
    public void DeleteCreatedBoard()
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", dashboardId);

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
  }
}
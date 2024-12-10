using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;

namespace TAF.Tests.API_tests.DELETE
{
  public class DeleteTests : BaseTestClass
  {
    int dashboardId;

    [SetUp]
    public void CreateDashboard()
    {
      var dashboardName = "New Dashboard " + DateTime.Now;
      var description = "Description " + DateTime.Now;

      var request = RequestWithAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

      RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

      dashboardId = response.Data.Id;
    }

    [Test]
    public void CheckDeleteDashboard()
    {
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
                    .AddUrlSegment("id", dashboardId);

      var response = _client.Execute(request);

      Assert.That(HttpStatusCode.OK, Is.EqualTo( response.StatusCode));
      Assert.That(response.Content,  Does.Contain($"Dashboard with ID = '{dashboardId}' successfully deleted."));

      var getRequest = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);
      var getResponse = _client.Execute(getRequest);
      var responseContent = JToken.Parse(getResponse.Content);

      bool dashboardExists = responseContent["content"]
                            .Children<JObject>()
                            .Any(item => int.Parse(item["id"].ToString()) == dashboardId);

      Assert.That(dashboardExists, Is.False);

    }
  }
}

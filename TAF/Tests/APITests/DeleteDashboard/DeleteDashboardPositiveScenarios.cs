using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;

namespace TAF.Tests.APITests.DeleteDashboard
{
  public class DeleteDashboardPositiveScenarios : BaseAPITest
  {
    private int dashboardId;
    private static Logger logger = LogManager.GetCurrentClassLogger();

    [SetUp]
    public void CreateDashboard()
    {
      var dashboardName = "New Dashboard " + DateTime.Now;
      var description = "Description " + DateTime.Now;

      try
      {
        var request = RequestWithAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                      .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

        logger.Info("Sending a POST request to {url}", DashboardEndpoints.CreateDashboardUrl);

        RestResponse<Dashboard> response = _client.Execute<Dashboard>(request);

        if (response.IsSuccessful)
        {
          dashboardId = response.Data.Id;

          logger.Info("Dashboard created successfully. Response: {@response}", response);
        }
        else
        {
          logger.Error("Failed to create dashboard. StatusCode: {statusCode}, Response: {response}", response.StatusCode, response.Content);

          throw new ApplicationException($"Failed to create dashboard. Status code: {response.StatusCode}");
        }
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Exception occurred while creating dashboard");

        throw;
      }
    }

    [Test]
    public void CheckDeleteDashboard()
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
                    .AddUrlSegment("id", dashboardId);

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(HttpStatusCode.OK, Is.EqualTo(response.StatusCode));
      Assert.That(response.Content, Does.Contain($"Dashboard with ID = '{dashboardId}' successfully deleted."));

      //Arrange
      var getRequest = RequestWithAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      //Act
      var getResponse = _client.Execute(getRequest);
      var responseContent = JToken.Parse(getResponse.Content);

      bool dashboardExists = responseContent["content"]
                            .Children<JObject>()
                            .Any(item => int.Parse(item["id"].ToString()) == dashboardId);

      //Assert
      Assert.That(dashboardExists, Is.False);
    }
  }
}
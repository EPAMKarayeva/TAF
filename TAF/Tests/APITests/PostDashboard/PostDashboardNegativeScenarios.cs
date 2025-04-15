using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataManager;

namespace TAF.Tests.APITests.PostDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class PostDashboardNegativeScenarios : BaseAPITest
  {
    private string dashboardName = "New Dashboard ";
    private string description = "Description ";

    [Test]
    public void CheckCreateDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.ErrorDescription, Is.EqualTo("Full authentication is required to access this resource"));
        Assert.That(jsonResponse.Error, Is.Not.Null.Or.Empty, "Field 'error' is missing or empty.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      });
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "invalidApiKey" })]
    public void CheckCreateDashboardWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Post)
                    .AddHeader("Authorization", apiKey)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<Business.Models.ErrorResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.Error, Is.Not.Null.Or.Empty, "Field 'error' is missing or empty.");
        Assert.That(jsonResponse.ErrorDescription, Is.EqualTo("Full authentication is required to access this resource"));
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      });
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "invalidNameValue" })]
    public void CheckCreateDashboardWithInvalidName(string key, string name)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<InvalidValueResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.ErrorCode, Is.EqualTo(4001), "Expected status code 4001.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.ErrorException.Message, Is.EqualTo("Request failed with status code BadRequest"));
      });
    }
  }
}
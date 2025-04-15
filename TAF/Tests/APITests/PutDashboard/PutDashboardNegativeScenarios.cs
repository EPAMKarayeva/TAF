using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataManager;

namespace TAF.Tests.APITests.PutDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class PutDashboardNegativeScenarios : BaseAPITest
  {
    private string newName = "NEW NAME " + DateTime.Now;
    private string description = "Description ";

    [Test]
    public void CheckUpdateDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    //.AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

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
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "invalidApiKey" })]
    public void CheckUpdateDashboardWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Put)
                    .AddHeader("Authorization", apiKey)
                    //.AddQueryParameter("fields", "id,name")
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

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
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "invalidIds" })]
    public void CheckUpdateDashboardWithInvalidId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
        .AddUrlSegment("id", id)
        .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<ErrorStatusResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.Error, Is.Not.Null.Or.Empty, "Field 'error' is missing or empty.");
        Assert.That(jsonResponse.Status, Is.EqualTo(400), "Expected status code 400.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.ErrorException.Message, Is.EqualTo("Request failed with status code BadRequest"));
      });
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "invalidNameValue" })]
    public void CheckUpdateDashboardWithInvalidName(string key, string name)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<InvalidValueResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.Message, Is.Not.Null.Or.Empty, "Field 'error' is missing or empty.");
        Assert.That(jsonResponse.ErrorCode, Is.EqualTo(4001), "Expected status code 4001.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.ErrorException.Message, Is.EqualTo("Request failed with status code BadRequest"));
      });
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "nonExistingIds" })]
    public void CheckUpdateDashboardWithNonExistingId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Put)
                    .AddUrlSegment("id", id)
                    .AddJsonBody(new Dictionary<string, string> { { "name", newName } });

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<InvalidValueResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.Message, Is.EqualTo($"Dashboard with ID '{id}' not found on project 'superadmin_personal'. Did you use correct Dashboard ID?"));
        Assert.That(jsonResponse.ErrorCode, Is.EqualTo(40422), "Expected status code 40422.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
      });
    }
  }
}
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataManager;

[assembly: LevelOfParallelism(5)]

namespace TAF.Tests.APITests.GetDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  internal class GetDashboardNegativeScenarios : BaseAPITest
  {
    [Test]
    public void CheckGetAllDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get);

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

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
    public void CheckGetAllDashboardsWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Get)
        .AddHeader("Authorization", apiKey);

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

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
    public void CheckGetDashboardWithInvalidId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

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
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "nonExistingIds" })]
    public void CheckGetDashboardWithNonExistingId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Get)
        .AddUrlSegment("id", id);

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
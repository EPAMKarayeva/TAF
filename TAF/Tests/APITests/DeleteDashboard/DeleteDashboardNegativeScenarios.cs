using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Business.Models;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataManager;

namespace TAF.Tests.APITests.DeleteDashboard
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class DeleteDashboardNegativeScenarios : BaseAPITest
  {
    [Test]
    public void CheckDeleteDashboardsWithOutAuth()
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Delete)
                    .AddUrlSegment("id", DashboardUrl.TestDashBoard);

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
    public void CheckDeleteWithWrongApiKey(string key, string apiKey)
    {
      //Arrange
      var request = RequestWithoutAuth(DashboardEndpoints.GetAllDashboardsUrl, Method.Delete)
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
    public void CheckDeleteDashboardWithInvalidId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
        .AddUrlSegment("id", id);

      //Act
      var response = _client.Execute(request);
      var jsonResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

      //Assert
      Assert.Multiple(() =>
      {
        Assert.That(jsonResponse.Error, Is.Not.Null.Or.Empty, "Field 'error' is missing or empty.");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.ErrorException.Message, Is.EqualTo("Request failed with status code BadRequest"));
      });
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataByKey), new object[] { "TestDataValues.json", "nonExistingIds" })]
    public void CheckDeleteDashboardWithNonExistingId(string key, string id)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.DashboardUrl, Method.Delete)
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
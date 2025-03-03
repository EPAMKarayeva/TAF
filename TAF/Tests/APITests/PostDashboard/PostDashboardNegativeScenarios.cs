using RestSharp;
using System.Net;
using TAF.Business.Constants;
using TAF.Core.BaseClasses;
using TAF.Tests.TestData.TestDataParsers;

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

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataProvider), nameof(TestCaseDataProvider.GetTestDataFromJson), new object[] { "TestDataInvalidValues.json" })]
    public void CheckCreateDashboardWithWrongParams(string description, string name)
    {
      //Arrange
      var request = RequestWithAuth(DashboardEndpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      //Act
      var response = _client.Execute(request);

      //Assert
      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }
  }
}
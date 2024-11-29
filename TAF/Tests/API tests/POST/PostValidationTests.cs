using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TAF.Core.Utilities.Contants;
using TAF.Core.Utilities.TestData.TestDataProviders;
using TAF.Tests.TestClasses;

namespace TAF.Tests.API_tests.POST
{
  [TestFixture]
  [Parallelizable(ParallelScope.Children)]
  public class PostValidationTests : BaseTestClass
  {
    private string dashboardName = "New Dashboard ";
    private string description = "Description ";

    [Test]
    public void CheckCreateDashboardsWithOutAuth()
    {
      var request = RequestWithoutAuth(DashboardEnpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", dashboardName } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
      Assert.That(response.Content, Does.Contain("Full authentication is required to access this resource"));
    }

    [Test]
    [Parallelizable(ParallelScope.Self)]
    [TestCaseSource(typeof(TestCaseDataParamsProvider), nameof(TestCaseDataParamsProvider.PostWithWrongParams))]
    public void CheckCreateDashboardWithWrongParams(string description, string name)
    {
      var request = RequestWithAuth(DashboardEnpoints.CreateDashboardUrl, Method.Post)
                    .AddJsonBody(new Dictionary<string, string> { { "description", description }, { "name", name } });

      var response = _client.Execute(request);

      Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
      Assert.That(response.ErrorException.Message, Does.Contain("Request failed with status code BadRequest"));
    }
  }
}
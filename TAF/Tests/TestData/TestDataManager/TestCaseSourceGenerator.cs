using Newtonsoft.Json.Linq;

namespace TAF.Tests.TestData.TestDataManager
{
  public class TestCaseSourceGenerator
  {
    public static IEnumerable<object[]> GenerateCase(JArray jsonArray)
    {
      foreach (JObject jObj in jsonArray)
      {
        foreach (var property in jObj.Properties())
        {
          var valueAsString = property.Value.ToString();
          yield return new object[] { property.Name, valueAsString };
        }
      }
    }
  }
}
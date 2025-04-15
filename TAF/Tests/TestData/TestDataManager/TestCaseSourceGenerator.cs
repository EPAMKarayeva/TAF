using Newtonsoft.Json.Linq;
using NLog;

namespace TAF.Tests.TestData.TestDataManager
{
  public static class TestCaseSourceGenerator
  {
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

    public static IEnumerable<object[]> GenerateCasesByKey(JObject jsonObject, string key)
    {
      if (jsonObject[key] is not JArray array)
      {
        logger.Error($"Key '{key}' not found in JSON object or is not an array.");
        throw new ArgumentException($"Key '{key}' not found in JSON object or is not an array.");
      }

      return array.Select(item => new object[] { key, item.ToString() });
    }
  }
}
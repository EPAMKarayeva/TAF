namespace TAF.Business.Constants
{
  internal class DashboardEndpoints
  {
    public const string DashboardUrl = "/api/v1/{user}/dashboard/{id}";
    public const string GetAllDashboardsUrl = "/api/v1/{user}/dashboard";
    public const string CreateDashboardUrl = "/api/v1/{user}/dashboard";
    public const string UpdateDashboardWithWidget = "/api/v1/{user}/dashboard/{id}/add";
    public const string GetAllWidgets = "/api/v1/{user}/widget/all";
  }
}

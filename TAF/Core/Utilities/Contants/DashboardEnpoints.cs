﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF.Core.Utilities.Contants
{
  internal class DashboardEnpoints
  {
    public const string GetDashboardUrl = "/api/v1/{user}/dashboard/{id}";
    public const string GetAllDashboardsUrl = "/api/v1/{user}/dashboard";
    public const string CreateDashboardUrl = "/api/v1/{user}/dashboard/{id}";
  }
}
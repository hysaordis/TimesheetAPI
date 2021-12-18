using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetAPI.api.ConfigurationModels
{
    public class JwtConfiguration
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireDays { get; set; }
        public bool JwtAuthEnabled { get; set; }
    }
}

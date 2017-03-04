using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.ApiEndpoint
{
    public class RecurringJobSettings
    {
        public string CronExpression { get; set; }

        public string TimeZone { get; set; }

        public string Queue { get; set; } = "default";
    }
}

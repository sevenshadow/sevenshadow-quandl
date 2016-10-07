using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenShadow.Quandl
{
    public enum PeriodType
    {
        [Description("_A")]
        Annual = 1,

        [Description("_Q")]
        Quarterly = 2,

        [Description("_LTG")]
        LongTermGrowthEstimate = 3,

    }
}

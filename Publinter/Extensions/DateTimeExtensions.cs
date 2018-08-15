using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Publinter.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek, int plus)
        {

            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays((-1 * diff) + plus).Date;
        }


    }
}
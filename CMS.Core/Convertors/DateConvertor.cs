using System;
using System.Globalization;

namespace CMS.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {

            PersianCalendar pc = new PersianCalendar();

            return pc.GetHour(value).ToString("00") + ":" + pc.GetMinute(value).ToString("00") + " - " + pc.GetYear(value).ToString("0000") + "/" + pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString("00");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DaliFood.AdminPanel.Helpers
{
    public class DateTimeConvertor
    {
        public DateTimeConvertor() { }
        PersianCalendar pc = new PersianCalendar();

        ///<summary>
        ///Convert DateTime Now to Persian
        /// </summary>
        /// <returns>DateTime Now to Persian</returns>
        public DateTime PersianDateTime()
        {
            DateTime timeNow = DateTime.UtcNow;
            string Converted = string.Format("{0}/{1}/{2}-{3}:{4}:{5}", pc.GetYear(timeNow), pc.GetMonth(timeNow), pc.GetDayOfMonth(timeNow), pc.GetHour(timeNow), pc.GetMinute(timeNow), pc.GetSecond(timeNow));
            var result = Convert.ToDateTime(Converted);
            return result;
        }

        ///<summary>
        ///Returns Persian Date
        /// </summary>
        ///<param name="dt"></param>
        ///<returns>YYYY/MM/DD</returns>
        public string PersianDate(DateTime dt)
        {
            var year = pc.GetDayOfYear(dt);
            var month = pc.GetMonth(dt);
            var day = pc.GetDayOfMonth(dt);
            var persianDate = string.Format("{0}/{1}/{2}", year, month, day);
            return persianDate;
        }

        ///<summary>
        ///Returns Persain Time
        ///<param name="dt"></param>
        ///<returns>HH:MM:SS</returns>
        /// </summary>
        public string PersianTime(DateTime dt)
        {
            var hour = pc.GetHour(dt);
            var minute = pc.GetMinute(dt);
            var seconed = pc.GetSecond(dt);
            var PersianTime = string.Format("{0}:{1}:{2}", hour, minute, seconed);
            return PersianTime;
        }
    }
}

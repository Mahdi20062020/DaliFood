using System;
using System.Globalization;

namespace DaliFood.AdminPanel.Helpers
{
    public static class DateHelper
    {
        public static string ToPersian(this DateTime dateTime)
        {
            if (dateTime.Year == 001)
            {
                return "";
            }
            else
            {
                PersianCalendar pc = new PersianCalendar();
                return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)}";
            }
        }
        public static string ToPersianTrackingCode(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            string year = $"{pc.GetYear(dateTime).ToString()[2]}{pc.GetYear(dateTime).ToString()[3]}";
            string month = pc.GetMonth(dateTime).ToString().Length == 1 ? $"0{pc.GetMonth(dateTime).ToString()}" : pc.GetMonth(dateTime).ToString();
            string day = pc.GetDayOfMonth(dateTime).ToString().Length == 1 ? $"0{pc.GetDayOfMonth(dateTime).ToString()}" : pc.GetDayOfMonth(dateTime).ToString();


            return $"{year}{month}{day}";
        }

        public static string Time(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetHour(dateTime)}:{pc.GetMinute(dateTime)}";
        }
        public static string ToPersianFull(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetHour(dateTime)}:{pc.GetMinute(dateTime)} {pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)}";
        }


        public static DateTime ToGeoDate(this string date)
        {
            PersianCalendar pc = new PersianCalendar();
            return new DateTime(
                int.Parse(date.Split('/')[0]),
                int.Parse(date.Split('/')[1]),
                int.Parse(date.Split('/')[2]),
                pc
                );
        }

        public static string toEnglishNumber(string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
            {
                input = input.Replace(persian[j], j.ToString());
            }

            return input;
        }

        public static DateTime ConvertJalaliToAd(string date)
        {
            DateTime adDate = new DateTime(int.Parse(toEnglishNumber(date.Split('/')[0])), int.Parse(toEnglishNumber(date.Split('/')[1])), int.Parse(toEnglishNumber(date.Split('/')[2])), new PersianCalendar());
            return adDate;
            //return string.Format("{0}/{1}/{2}", jalali.GetYear(ja), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));

        }
        public static string ConvertAdToJalali(DateTime ad)
        {
            //DateTime miladi = DateTime.Now;
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
            string today = string.Format("{0}/{1}/{2}", shamsi.GetYear(ad), shamsi.GetMonth(ad).ToString("00"), shamsi.GetDayOfMonth(ad).ToString("00"));
            return today;
            //var jalali = new DateTime(ad.Year, ad.Month, ad.Day, new PersianCalendar());
            //return string.Format("{0}/{1}/{2}", jalali.Year, jalali.Month, jalali.Day);
        }
    }
}
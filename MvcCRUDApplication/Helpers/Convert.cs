using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MvcCRUDApplication.Helpers
{
    public static class Convert
    {


        public static int ToInt(this object arg)
        {
            int ret = 0;

            Int32.TryParse(arg.ToStr(), out ret);

            return ret;
        }


        public static int ToIntFirst(this object arg, bool allowCommasInside)
        {
            int ret = 0;

            Regex rx = allowCommasInside ? new Regex(@"(\d|,)+") : new Regex(@"(\d)+");

            var match = rx.Match(arg.ToStr());
            string val = allowCommasInside ? match.Value.Replace(",", "") : match.Value;



            Int32.TryParse(val, out ret);

            return ret;
        }


        public static long ToLong(this object arg)
        {
            long ret = 0;

            Int64.TryParse(arg.ToStr(), out ret);

            return ret;
        }
        public static float ToFloat(this object arg)
        {
            float ret = 0;


            Single.TryParse(arg.ToStr(), out ret);

            return ret;
        }

        public static double ToDouble(this object arg)
        {
            double ret = 0;


            Double.TryParse(arg.ToStr(), out ret);

            return ret;
        }


        public static string ToStr(this object arg)
        {
            string ret = String.Empty;
            if (arg != null)
            {
                ret = arg.ToString();
            }
            return ret;
        }


        public static string ToStr(this object arg, int length)
        {
            string ret = String.Empty;
            if (arg != null)
            {
                ret = arg.ToString();
            }
            if (ret.Length > length)
            {
                return ret.Substring(0, length);
            }
            else
            {
                return ret;
            }
        }

        public static string ToStr(this string text, int minLen, int maxLen)
        {
            string s = text != null ? text : "";
            if (s.Length > maxLen) s = s.Substring(0, maxLen).Trim();

            int ix = 0;
            ix = s.LastIndexOf(".");
            if (ix > minLen)
            {
                s = s.Substring(0, ix + 1).Trim();
            }
            else if ((ix = s.LastIndexOf(",")) > minLen)
            {
                s = s.Substring(0, ix).Trim();

            }
            else if ((ix = s.LastIndexOf(" ")) > minLen)
            {
                s = s.Substring(0, ix).Trim();
            }

            return s;
        }

        public static bool HasValue(this object arg)
        {
            string ret = String.Empty;
            if (arg != null)
            {
                ret = arg.ToString();
            }
            return !String.IsNullOrEmpty(ret);
        }

 
 

        public static bool ToBool(this object arg, bool defaultValue = false)
        {
            bool ret = defaultValue;

            if (!Boolean.TryParse(arg.ToStr(), out ret))
            {
                if (arg.ToStr().ToLower().Contains((!defaultValue).ToString().ToLower()))
                {
                    ret = !defaultValue;
                }

            }



            return ret;
        }
    }
}
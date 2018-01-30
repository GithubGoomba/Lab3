using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    public static class ExtensionMethods
    {
        public static bool ApproximatelyEquals(this decimal v1, decimal v2, decimal precision = 0.0000000001M)
        {
            bool flag = false;
            if (Math.Abs(v1 - v2) < precision)
            {
                flag = true;
            }
            
            return flag;
        }
        public static int Constrain(this int value, int min, int max)
        {
            if(value < min)
            {
                value = min;
            }

            if(value > max)
            {
                value = max;
            }
            return value;
        }
        public static string ToSymbol(this Angle.AngleUnits units)
        {
            string result = string.Empty;
            if (units == Angle.AngleUnits.Degrees)
            {
                result = "°";
            }
            if (units == Angle.AngleUnits.Radians)
            {
                result = "rad";
            }
            if (units == Angle.AngleUnits.Gradians)
            {
                result = "g";
            }
            if (units == Angle.AngleUnits.Turns)
            {
                result = "tr";
            }
            return result;
        }
    }
}

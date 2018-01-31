using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class AngleFormatter : IFormatProvider, ICustomFormatter
    {
      public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType))
            {
                return this;
            }
            return null;
        }
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Angle test = new Angle();
            string result = string.Empty;
                       
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
            if (arg.GetType() == typeof(Angle) || arg.GetType() == typeof(IEnumerable))
            {

            }
            if ((string.IsNullOrEmpty(format)) || (format == "c") || (format =="C") ) 
            {
                format = "c";
            }
            if (arg is Angle)
            {

                //c = 2 decimal places default
                //c0..c9 = current units with number of decimals indicated
                //d = degrees with degree symbol and 2 decimal places
                //d0.. d9 = above with variable number of decimal places
                //g = gradians
                //g0..g9
                //p = formats angle using pi radian units. π numerical value is number of radians divided by pi with the pi symbol displayed before rad. assume 5 decimal places
                //p0.. p9
                //r = radians assume 5 decimals
                //r0 .. r9
                //t = turns 2 decimal places
                //t0 .. t9 
                test = arg as Angle;
                char code = char.ToLower(format.First());
                switch (code)
                {
                    case 'c':
                        if (format.Length == 1)
                        {
                            if (test.Units == Angle.AngleUnits.Radians)
                            {
                                result = $"{test.Value.ToString("f5")}{test.Units.ToSymbol()}";
                            }
                            else
                            {
                                result = $"{test.Value.ToString("f2")}{test.Units.ToSymbol()}";
                            }
                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            fmt = "f" + digs;
                            result = test.Value.ToString(fmt) + test.Units.ToSymbol();
                        }
                        break;

                    case 'd':
                        if (format.Length ==1)
                        {
                            test = test.ConvertAngle(Angle.AngleUnits.Degrees);
                            result = $"{test.Value.ToString("f2")}{test.Units.ToSymbol()}";

                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            test = test.ConvertAngle(Angle.AngleUnits.Degrees);
                            result = test.Value.ToString(fmt) + test.Units.ToSymbol();
                        }
                        break;
                    case 'g':
                        if (format.Length == 1)
                        {
                            test = test.ConvertAngle(Angle.AngleUnits.Gradians);
                            result = $"{test.Value.ToString("f2")}{test.Units.ToSymbol()}";

                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            test = test.ConvertAngle(Angle.AngleUnits.Gradians);
                            result = test.Value.ToString(fmt) + test.Units.ToSymbol();
                        }
                        break;
                    case 'p':
                        if (format.Length == 1)
                        {
                            test = test.ConvertAngle(Angle.AngleUnits.Radians);
                            test.Value = test.Value / Angle.pi;
                            result = $"{test.Value.ToString("f5")}πrad";

                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            test = test.ConvertAngle(Angle.AngleUnits.Radians);
                            test.Value = test.Value / Angle.pi; //conversion to pirads
                            result = test.Value.ToString(fmt) + "πrad";
                        }
                        break;
                    case 'r':
                        if (format.Length == 1)
                        {
                            test = test.ConvertAngle(Angle.AngleUnits.Radians);
                            result = $"{test.Value.ToString("f5")}{test.Units.ToSymbol()}";

                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            test = test.ConvertAngle(Angle.AngleUnits.Radians);
                            result = test.Value.ToString(fmt) + test.Units.ToSymbol();
                        }
                        break;
                    case 't':
                        if (format.Length == 1)
                        {
                            test = test.ConvertAngle(Angle.AngleUnits.Turns);
                            result = $"{test.Value.ToString("f2")}{test.Units.ToSymbol()}";

                        }
                        else if (char.IsDigit(format[1]))
                        {
                            string fmt = format.Substring(1);
                            int digs = Int32.Parse(fmt);
                            digs = digs.Constrain(0, 9);
                            test = test.ConvertAngle(Angle.AngleUnits.Turns);
                            result = test.Value.ToString(fmt) + test.Units.ToSymbol();
                        }
                        break;
                }
            }
           else if (arg is IFormattable)
           {
               result = ((IFormattable)arg).ToString(format, formatProvider);
            }
            else
            {
                result = arg.ToString();
            }

            return result;
        }

    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Angle : IFormattable
    {
        public enum AngleUnits { Degrees, Gradians, Radians, Turns };
        
        public const decimal pi = 3.1415926535897932384626434M;
        public const decimal twoPi = 2M * pi;
        private decimal _Value = 0M;
        private AngleUnits _Units = AngleUnits.Degrees; 

        private static decimal[,] _ConversionFactors =
        {
            {1M, 9M/10M , 180M/pi, 360M },
            { 10M/9M, 1M, 200M/pi, 400M },
            {pi/180M, pi/200M, 1M, twoPi },
            {1M/360M, 1M/400M, 1M/twoPi,1M}

        };

        public decimal Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = Normalize(value, Units);
            }
        }
        public AngleUnits Units
        {
            get
            {
                return _Units;
            }
            set
            {
                _Value = ConvertAngleValue(Value, Units, value);
                _Units = value;
            }
        }

        public static decimal Normalize(decimal value, AngleUnits units)
        {
            if (units == AngleUnits.Degrees)
            {
                if (value < 0)
                {
                    while (value < 0)
                    {
                        value = value + 360;
                    }
                }
                if (value > 360)
                {
                    while (value > 360)
                    {
                        value = value - 360;
                    }
                }
            }
            if (units == AngleUnits.Gradians)
            {
                if (value < 0)
                {
                    while (value < 0)
                    {
                        value = value + 400;
                    }
                }
                if (value > 400)
                {
                    while (value > 400)
                    {
                        value = value - 400;
                    }
                }
            }
            if (units == AngleUnits.Radians)
            {
                if (value < 0)
                {
                    while (value < 0)
                    {
                        value = value + twoPi;
                    }
                }
                if (value > twoPi)
                {
                    while (value > twoPi)
                    {
                        value = value - twoPi;
                    }
                }
            }
            if (units == AngleUnits.Turns)
            {
                if (value < 0)
                {
                    while (value < 0)
                    {
                        value = value + 1;
                    }
                }
                if (value > 1)
                {
                    while (value > 1)
                    {
                        value = value - 1;
                    }
                }
            }
            return value;
        }
        public static decimal ConvertAngleValue(decimal angle, AngleUnits fromUnits, AngleUnits toUnits)
        {
            decimal result = 0M;
            decimal factor = _ConversionFactors[(int)toUnits, (int)fromUnits];
            angle = angle * factor;
            result = Normalize(angle, toUnits);
            return result;
        }

        public Angle ToDegrees()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Degrees), AngleUnits.Degrees);
        }
        public Angle ToRadians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Radians), AngleUnits.Radians);
        }
        public Angle ToGradians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Gradians), AngleUnits.Gradians);
        }
        public Angle ToTurns()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Turns), AngleUnits.Turns);
        }

        public Angle ConvertAngle(AngleUnits targetUnits)
        {
            Angle newuom = new Angle();
            switch (targetUnits)
            {
                
                case AngleUnits.Degrees:
                    newuom = ToDegrees();
                    break;

                case AngleUnits.Radians:
                    newuom = ToRadians();
                    break;

                case AngleUnits.Gradians:
                    newuom = ToGradians();
                    break;

                case AngleUnits.Turns:
                    newuom = ToTurns();
                    break;
               
            }
            return newuom;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new AngleFormatter().Format(format,this,formatProvider);
        }
        public string ToString(string format)
        {
            AngleFormatter fmt = new AngleFormatter();
            return fmt.Format(format, this, fmt);
        }

        public override string ToString()
        {
            return ToString(string.Empty);
        }
        #region operators
        public static Angle operator +(Angle a1, Angle a2)
        {
            Angle test = a2.ConvertAngle(a1.Units);
            
            //decimal calc = 0;
            //calc = test.Value + a1.Value;
            decimal calc = 0;
            calc = ConvertAngleValue(a2.Value, a2.Units, a1.Units);
            return new Lab3.Angle(a1.Value + calc, a1.Units);
            //Angle result = new Angle(calc,a1.Units);
            //return result; 
        }
        public static Angle operator -(Angle a1, Angle a2)
        {
            decimal calc = 0;
            calc = ConvertAngleValue(a2.Value, a2.Units, a1.Units);
            return new Lab3.Angle(a1.Value - calc, a1.Units);
        }
        public static Angle operator +(Angle a, decimal scalar)
        {
            return new Lab3.Angle(a.Value + scalar, a.Units);
        }
        public static Angle operator -(Angle a, decimal scalar)
        {
            return new Lab3.Angle(a.Value - scalar, a.Units);
        }
        public static Angle operator *(Angle a, decimal scalar)
        {
            return new Lab3.Angle(a.Value * scalar,a.Units);
        }
        public static Angle operator /(Angle a, decimal scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Don't divide by zero!");

            }
            else
            {
                return new Lab3.Angle(a.Value / scalar, a.Units);
            }
        }
        public static bool operator ==(Angle a, Angle b)
        {
            bool flag = false;
            object o1 = a;
            object o2 = b;
            decimal calc = 0;

            if (o1 == null && o2 == null)
            {
                flag = true;
                return flag;
            }
            if(o1 == null ^ o2 == null)
            {
                flag = false;
                return flag;
            }
           
            calc = ConvertAngleValue(b.Value, b.Units, a.Units);
            flag = a.Value.ApproximatelyEquals(calc);
            return flag;
        }
        public static bool operator !=(Angle a, Angle b)
        {
            return !(a == b);
        }

        public static bool operator < (Angle a, Angle b)
        {
            decimal calc = 0;
            if (a == null && b == null) return false;
            if (a == null) return true;
            if (b == null) return false;
            calc = ConvertAngleValue(b.Value, b.Units, a.Units);
            return !(a.Value.ApproximatelyEquals(calc)) && (a.Value < calc);            
        }

        public static bool operator > (Angle a, Angle b)
        {
            return !(a == b || a < b);
        }

        public static bool operator <= (Angle a, Angle b)
        {
            return (a < b || a == b);
        }
        public static bool operator >= (Angle a, Angle b)
        {
            return (a > b || a == b);
        }

        public override bool Equals(object obj)
        {
            return this == obj as Angle;
        }
        public override int GetHashCode()
        {
            return ConvertAngleValue(Value, Units, AngleUnits.Degrees).GetHashCode();
        }

        public static explicit operator decimal(Angle a) //need to add nullref ex here?
        {
            return a.Value;
        }
        public static explicit operator double(Angle a) 
        {
            return (double)a.Value;
        }
        #endregion


        #region constructors

        public Angle() : this(0, AngleUnits.Degrees) { } 

        public Angle(Angle other) : this(other.Value, other.Units) { }

        public Angle(decimal value, AngleUnits units = AngleUnits.Degrees)
        {
            _Units = units;
            Value = value;
        }

        #endregion
    }

}

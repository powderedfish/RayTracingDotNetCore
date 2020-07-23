using System;

namespace RayTracing
{
    public abstract class Vec3Base
    {
        protected double[] _e;

        public Vec3Base()
        {
            _e = new double[3] { 0, 0, 0 };
        }

        public Vec3Base(double x, double y, double z)
        {
            _e = new double[3] { x, y, z };
        }
    }

    public class Vec3: Vec3Base
    {
        public double X { get { return _e[0]; } set { _e[0] = value; } }
        public double Y { get { return _e[1]; } set { _e[1] = value; } }
        public double Z { get { return _e[2]; } set { _e[2] = value; } }

        public double Length
        {
            get { return Math.Sqrt(LengthSquare()); }
        }

        public Vec3():base()
        {
        }

        public Vec3(double x, double y, double z): base(x,y,z)
        {
        }

        public static Vec3 operator -(Vec3 a)
        {
            return new Vec3(-a.X, -a.Y, -a.Z);
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator *(Vec3 a, double b)
        {
            return new Vec3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vec3 operator *(double a, Vec3 b)
        {
            return b * a;
        }

        public static Vec3 operator /(Vec3 a, double b)
        {
            return a * (1 / b);
        }

        public double this[int key]
        {
            get { return _e[key]; }
            set { _e[key] = value; }
        }

        public double LengthSquare()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Dot(Vec3 v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public Vec3 Cross(Vec3 v)
        {
            return new Vec3(Y * v.Z - Z * v.Y
                           , Z * v.X - X * v.Z,
                             X * v.Y - Y * v.X);
        }

        public Vec3 UnitVector()
        {
            return this / Length;
        }
    }

    public class Colour3 : Vec3Base
    {
        public double R { get { return _e[0]; } set { _e[0] = value; } }
        public double G { get { return _e[1]; } set { _e[1] = value; } }
        public double B { get { return _e[2]; } set { _e[2] = value; } }

        public int SamplePerPixel { get; }

        public Colour3() : base() 
        {
            SamplePerPixel = 100;
        }

        public Colour3(double r, double g, double b):base(r, g, b) 
        {
            SamplePerPixel = 100;
        }

        public override string ToString()
        {
            double r = R;
            double g = G;
            double b = B;

            double scale = 1d / SamplePerPixel;

            r *= scale;
            g *= scale;
            b *= scale;

            r = Math.Clamp(r, 0.0d, 0.999d);
            g = Math.Clamp(g, 0.0d, 0.999d);
            b = Math.Clamp(b, 0.0d, 0.999d);

            return $"{(int)(255.999 * r)} {(int)(255.999 * g)} {(int)(255.999 * b)} ";
        }

        public static Colour3 operator*(Colour3 c, double t)
        {
            return new Colour3(c.R * t, c.G * t, c.B * t);
        }

        public static Colour3 operator*(double t, Colour3 c)
        {
            return c * t;
        }

        public static Colour3 operator+(Colour3 c1, Colour3 c2)
        {
            return new Colour3(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
        }
    }
    public class Point3 : Vec3Base
    {
        public double X { get { return _e[0]; } set { _e[0] = value; } }
        public double Y { get { return _e[1]; } set { _e[1] = value; } }
        public double Z { get { return _e[2]; } set { _e[2] = value; } }

        public Point3():base(){ }

        public Point3(double x, double y, double z) : base(x, y, z){}

        public static Point3 operator+(Point3 p, Vec3 u)
        {
            return new Point3(p.X + u.X, p.Y + u.Y, p.Z + u.Z);
        }

        public static Point3 operator-(Point3 p, Vec3 u)
        {
            return p + (-u);
        }

        public static Vec3 operator-(Point3 p1, Point3 p2)
        {
            return new Vec3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

    }
}

﻿using System;
using System.Data;
using System.IO;
using System.Text;

namespace RayTracing
{
    class Program
    {
        const double _aspactRatio = 16.0d / 9.0d;

        const int _imageWidth = 1024;
        const int _imageHeight = (int)(_imageWidth / _aspactRatio);

        static double HitSphere(Point3 centre, double radius, Ray r)
        {
            Vec3 oc = r.Origin - centre;
            double a = r.Direction.Dot(r.Direction);
            double halfB = r.Direction.Dot(oc);
            double c = oc.Dot(oc) - radius * radius;

            double discriminant = halfB * halfB - a * c;
            
            if(discriminant < 0)
            {
                return -1;
            }

            return (-halfB - Math.Sqrt(discriminant)) / a;
        }

        static Colour3 RayColour(Ray r, IHitable world)
        {

            HitRecord rec;
            if (world.Hit(r, 0, double.MaxValue, out rec))
            {
                return new Colour3(rec.Normal.X + 1, rec.Normal.Y + 1, rec.Normal.Z + 1) * 0.5;
            }
            //double d = HitSphere(new Point3(0, 0, -1), 0.5, r);
            //if (d > 0 )
            //{
            //    Vec3 normal = (r.At(d) - new Point3(0, 0, -1)).UnitVector();
            //    return new Colour3(normal.X + 1, normal.Y + 1, normal.Z + 1) * 0.5;
            //}


            Vec3 unitDirection = r.Direction.UnitVector();
            double t = 0.5 * (unitDirection.Y + 1.0);
            return (1.0 - t) * new Colour3(1.0, 1.0, 1.0) + t * new Colour3(0.5, 0.7, 1.0);
        }

        static int Main(string[] args)
        {
            double viewportHeight = 2.0d;
            double viewportWidth = viewportHeight * _aspactRatio;
            double focalLength = 1.0d;

            Point3 origin = new Point3(0, 0, 0);

            Vec3 horizontal = new Vec3(viewportWidth, 0, 0);
            Vec3 virtical = new Vec3(0, viewportHeight, 0);

            Point3 lowerLeftCorner = origin - horizontal / 2 - virtical / 2 - new Vec3(0, 0, focalLength);

            FileStream fs = new FileStream("./output.ppm", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);

            HitableList world = new HitableList();
            world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
            world.Add(new Sphere(new Point3(0, -100.5d, -1), 100));

            sw.WriteLine("P3");
            sw.WriteLine(_imageWidth + " " + _imageHeight);
            sw.WriteLine("256");

            for(int i = _imageHeight; i >= 0 ; i--)
            {
                Console.WriteLine("Scanlines remaining: {0}", i);
                for(int j = 0; j < _imageWidth; j++)
                {
                    double u = (double)j / (_imageWidth - 1);
                    double v = (double)i / (_imageHeight - 1);

                    Colour3 colour = RayColour(new Ray(origin, lowerLeftCorner + u * horizontal + v * virtical - origin), world);

                    sw.Write(colour.ToString());
                }
                sw.WriteLine();
                sw.Flush();
            }

            sw.Dispose();
            fs.Dispose();

            return 0;
        }
    }
}

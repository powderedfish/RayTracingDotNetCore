using System;
using System.Collections.Generic;
using System.Text;
using RayTracing.Materials;

namespace RayTracing
{
    public class Sphere : IHitable
    {
        public Point3 Centre { get; set; }
        public double Radius { get; set; }

        public Material Mat { get; set; }

        public Sphere()
        {

        }

        public Sphere(Point3 cen, double r, Material mat)
        {
            Centre = cen;
            Radius = r;
            Mat = mat;
        }

        public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec)
        {
            rec = null;

            Vec3 oc = r.Origin - Centre;
            double a = r.Direction.Dot(r.Direction);
            double halfB = r.Direction.Dot(oc);
            double c = oc.LengthSquare() - Radius * Radius;

            double discriminant = halfB * halfB - a * c;

            if (discriminant > 0)
            {
                double root = Math.Sqrt(discriminant);
                double temp = (-halfB - root) / a;
                if (temp < tMax && temp > tMin)
                {
                    rec = new HitRecord();
                    rec.T = temp;
                    rec.Point = r.At(rec.T);
                    Vec3 outwardNormal = (rec.Point - Centre).UnitVector();
                    rec.SetFaceNormal(r, outwardNormal);
                    rec.Mat = Mat;
                    return true;
                }

                temp = c / (a * temp);

                if (temp < tMax && temp > tMin)
                {
                    rec = new HitRecord();
                    rec.T = temp;
                    rec.Point = r.At(rec.T);
                    Vec3 outwardNormal = (rec.Point - Centre).UnitVector();
                    rec.SetFaceNormal(r, outwardNormal);
                    rec.Mat = Mat;
                    return true;
                }
            }


            return false;
        }

    }
}

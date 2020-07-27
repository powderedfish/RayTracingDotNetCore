using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracing.Materials
{
    public class Lambertian : Material
    {
        public Colour3 Albedo { get; set; }

        public Lambertian(Colour3 a)
        {
            Albedo = a;
        }

        public override bool Scatter(Ray rIn, HitRecord rec, out Colour3 attenuation, out Ray scattered)
        {
            Vec3 scatterDirection = rec.Normal + Vec3.RandomUnitVector();
            scattered = new Ray(rec.Point, scatterDirection);
            attenuation = Albedo;
            return true;
        }
    }
}

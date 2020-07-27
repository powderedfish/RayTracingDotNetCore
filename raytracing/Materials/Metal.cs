using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing.Materials
{
    public class Metal : Material
    {
        public Colour3 Albedo { get; set; }

        public Metal(Colour3 a)
        {
            Albedo = a;
        }

        public override bool Scatter(Ray rIn, HitRecord rec, out Colour3 attenuation, out Ray scattered)
        {
            Vec3 reflected = rIn.Direction.UnitVector().Reflect(rec.Normal);
            scattered = new Ray(rec.Point, reflected);
            attenuation = Albedo;

            return scattered.Direction.Dot(rec.Normal) > 0;
        }
    }
}

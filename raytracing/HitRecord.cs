using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing
{
    public class HitRecord
    {
        public Point3 Point { get; set; }
        public Vec3 Normal { get; set; }
        public double T { get; set; }
        public bool FrontFace {get; set; }

        public void SetFaceNormal(Ray r, Vec3 outwardNormal)
        {
            FrontFace = r.Direction.UnitVector().Dot(outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }

    }
}

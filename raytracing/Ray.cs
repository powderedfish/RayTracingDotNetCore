using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;

namespace RayTracing
{
    public class Ray
    {
        public Point3 Origin { get; set; }
        public Vec3 Direction { get; set; }

        public Ray()
        {
        }

        public Ray(Point3 org, Vec3 dir)
        {
            Origin = org;
            Direction = dir;
        }

        public Point3 At(double t)
        {
            return Origin + t * Direction;
        }

    }
}

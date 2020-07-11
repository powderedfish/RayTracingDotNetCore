using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing
{
    public interface IHitable
    {
        public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec);
    }
}

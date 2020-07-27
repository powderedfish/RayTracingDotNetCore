using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing.Materials
{
    public abstract class Material
    {
        public abstract bool Scatter(Ray rIn, HitRecord rec, out Colour3 attenuation, out Ray scattered);
    }
}

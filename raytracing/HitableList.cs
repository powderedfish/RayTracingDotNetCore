using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RayTracing
{
    public class HitableList : IHitable
    {
        List<IHitable> Objects { get; set; }

        public HitableList()
        {
            Objects = new List<IHitable>();
        }

        public HitableList(IHitable item)
        {
            Objects = new List<IHitable>();
            Objects.Add(item);
        }

        public void Add(IHitable item)
        {
            Objects.Add(item);
        }

        public void Clear()
        {
            Objects.Clear();
        }

        public bool Hit(Ray r, double tMin, double tMax, out HitRecord rec)
        {
            rec = null;
            bool hitAnything = false;
            double closestSoFar = tMax;
            HitRecord tempRec;

            foreach(IHitable item in Objects)
            {
                if(item.Hit(r, tMin, closestSoFar, out tempRec))
                {
                    hitAnything = true;
                    rec = tempRec;
                    closestSoFar = rec.T;
                }
            }

            return hitAnything;
        }
    }
}

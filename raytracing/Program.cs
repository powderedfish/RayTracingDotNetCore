using System;
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
        const int _maxDepth = 50;

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

        static Colour3 RayColour(Ray r, IHitable world, int depth)
        {

            if(depth <= 0)
            {
                return new Colour3(0, 0, 0);
            }

            HitRecord rec;
            if (world.Hit(r, 0, double.MaxValue, out rec))
            {
                Point3 target = rec.Point + rec.Normal;
                Point3 p = Point3.RandomInUnitSphere();
                target.X += p.X;
                target.Y += p.Y;
                target.Z += p.Z;
                return 0.5 * RayColour(new Ray(rec.Point, target - rec.Point), world, --depth);
            }

            Vec3 unitDirection = r.Direction.UnitVector();
            double t = 0.5 * (unitDirection.Y + 1.0);
            return (1.0 - t) * new Colour3(1.0, 1.0, 1.0) + t * new Colour3(0.5, 0.7, 1.0);
        }

        static int Main(string[] args)
        {
            

            FileStream fs = new FileStream("./output.ppm", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);

            HitableList world = new HitableList();
            world.Add(new Sphere(new Point3(0, 0, -1), 0.5));
            world.Add(new Sphere(new Point3(0, -100.5d, -1), 100));

            Camera cam = new Camera();
            Random rand = new Random();

            sw.WriteLine("P3");
            sw.WriteLine(_imageWidth + " " + _imageHeight);
            sw.WriteLine("256");

            for(int i = _imageHeight; i >= 0 ; i--)
            {
                Console.WriteLine("Scanlines remaining: {0}", i);
                for (int j = 0; j < _imageWidth; j++)
                {
                    Colour3 colour = new Colour3(0 ,0 ,0);
                    for (int k = 0; k < colour.SamplePerPixel; k++)
                    {

                        double u = (j + rand.NextDouble()) / (_imageWidth - 1);
                        double v = (i + rand.NextDouble()) / (_imageHeight - 1);
                        Ray r = cam.GetRay(u, v);
                        colour += RayColour(r, world, _maxDepth);

                    }
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

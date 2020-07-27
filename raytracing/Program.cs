using System;
using System.Data;
using System.IO;
using System.Text;
using RayTracing.Materials;

namespace RayTracing
{
    class Program
    {
        const double _aspactRatio = 16.0d / 9.0d;
        const int _imageWidth = 1024;
        const int _imageHeight = (int)(_imageWidth / _aspactRatio);
        const int _maxDepth = 50;

        static Colour3 RayColour(Ray r, IHitable world, int depth)
        {

            if(depth <= 0)
            {
                return new Colour3(0, 0, 0);
            }

            HitRecord rec;

            if (world.Hit(r, 0.001d, double.MaxValue, out rec))
            {
                Ray scattered;
                Colour3 attenuation;
                if(rec.Mat.Scatter(r, rec, out attenuation, out scattered))
                {
                    return attenuation * RayColour(scattered, world, --depth);
                }

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

            Lambertian ground = new Lambertian(new Colour3(0.8, 0.8, 0.0));
            Lambertian centre = new Lambertian(new Colour3(0.7, 0.3, 0.3));
            Metal left = new Metal(new Colour3(0.8, 0.8, 0.8));
            Metal right = new Metal(new Colour3(0.8, 0.6, 0.2));
            
            world.Add(new Sphere(new Point3(0, 0, -1), 0.5, centre));//centre sphere
            world.Add(new Sphere(new Point3(0, -100.5d, -1), 100, ground));//ground
            world.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, left));//left sphere 
            world.Add(new Sphere(new Point3(1, 0, -1), 0.5, right));//right sphere

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

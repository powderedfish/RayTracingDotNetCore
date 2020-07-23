using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracing
{
    public class Camera
    {
        const double _aspactRatio = 16.0d / 9.0d;
        private Point3 _origin;
        private Point3 _lowerLeftCorner;
        private Vec3 _horizontal;
        private Vec3 _vertical;

        public Camera()
        {
            double viewportHeight = 2.0d;
            double viewportWidth = viewportHeight * _aspactRatio;
            double focalLength = 1.0d;

            _origin = new Point3(0, 0, 0);

            _horizontal = new Vec3(viewportWidth, 0, 0);
            _vertical = new Vec3(0, viewportHeight, 0);
            _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - new Vec3(0, 0, focalLength);

        }

        public Ray GetRay(double x, double y)
        {
            return new Ray(_origin, _lowerLeftCorner + x * _horizontal + y * _vertical - _origin);
        }
    }
}

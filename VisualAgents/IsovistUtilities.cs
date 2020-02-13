using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAgents
{
    public static class IsovistUtilities
    {
        public static Point3d IsovistCast(Point3d P, Vector3d D, List<Curve> C)
        {
            var intersections = C.Select(x => RayCurveIntersection(P, D, x)).ToList();
            return ClosestPoint(intersections, P);
        }

        public static Point3d RayCurveIntersection(Point3d P, Vector3d D, Curve curve)
        {
            var points = LineCurveIntersection(new Line(P, D, 99999), curve);
            return ClosestPoint(points, P);
        }

        public static Point3d ClosestPoint(List<Point3d> C, Point3d P)
        {
            var pc = new PointCloud(C);
            var index = pc.ClosestPoint(P);

            if (index != -1)
                return C[index];
            else
                return Point3d.Unset;
        }

        public static List<Point3d> LineCurveIntersection(Line l, Curve curve)
        {
            var events = Rhino.Geometry.Intersect.Intersection.CurveCurve(curve, l.ToNurbsCurve(), 0.1, 0.1);
            List<Point3d> points = new List<Point3d>();

            if (events != null)
            {
                for (int i = 0; i < events.Count; i++)
                {
                    var ccx_event = events[i];
                    points.Add(ccx_event.PointA);
                }
            }
            return points;
        }

        public static List<Vector3d> Isovist(int num, double angle, Vector3d direction)
        {
            var a = Vector3d.VectorAngle(Vector3d.YAxis, direction);
            if (direction.X > 0)
                a = 2 * Math.PI - a;

            var vectors = new List<Vector3d>();
            double phase = (Math.PI - angle) / 2;
            for (int i = 0; i < num; i++)
            {
                var x = Math.Cos(i * (1.0 / (num - 1)) * angle + phase + a);
                var y = Math.Sin(i * (1.0 / (num - 1)) * angle + phase + a);
                vectors.Add(new Vector3d(x, y, 0));
            }
            return vectors;
        }
    }
}

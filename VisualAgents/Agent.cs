using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualAgents
{
    public class IsoAgent
    {
        int detail;
        double range;
        public Point3d pos;
        public Vector3d dir;
        Vector3d iso;

        public IsoAgent(int detail, double field, Point3d pos, Vector3d dir)
        {
            this.detail = detail;
            this.range = field;
            this.pos = pos;
            this.dir = dir;
            dir.Unitize();
        }

        public void Update()
        {
            dir += iso * 0.5f;
            pos += dir;
        }

        public void Observe(List<Curve> context)
        {
            // Compute Isovist from your point of view
            var isovist = IsovistUtilities.Isovist(detail, (range / 180) * Math.PI, dir);
            var hits = isovist.Select(x => IsovistUtilities.IsovistCast(pos, x, context)).ToList();
            var distances = hits.Select(x => x.DistanceTo(pos)).ToList();

            //Get Longest Ray
            double max = double.MinValue;
            Point3d longest = Point3d.Unset;
            for (int i = 0; i < distances.Count(); i++)
            {
                if (distances[i] > max)
                {
                    max = distances[i];
                    longest = hits[i];
                }
            }

            Vector3d direction = longest - pos;
            direction.Unitize();
            iso = direction - dir;
        }

    }
}

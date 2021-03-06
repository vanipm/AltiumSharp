using System;
using System.Drawing;
using AltiumSharp.BasicTypes;

namespace AltiumSharp.Records
{
    public class EllipticalArcRecord : ArcRecord
    {
        public Coord SecondaryRadius { get; internal set; }

        public override CoordRect CalculateBounds() =>
            new CoordRect(Location.X - Radius, Location.Y - SecondaryRadius, Radius * 2, SecondaryRadius * 2);

        public override void ImportFromParameters(ParameterCollection p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            base.ImportFromParameters(p);
            SecondaryRadius = Utils.DxpFracToCoord(p["SECONDARYRADIUS"].AsIntOrDefault(), p["SECONDARYRADIUS_FRAC"].AsIntOrDefault());
          }

        public override void ExportToParameters(ParameterCollection p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));

            base.ExportToParameters(p);
            {
                var (n, f) = Utils.CoordToDxpFrac(SecondaryRadius);
                if (n != 0 || f != 0) p.Add("SECONDARYRADIUS", n);
                if (f != 0) p.Add("SECONDARYRADIUS"+"_FRAC", f);
            }
        }
    }
}

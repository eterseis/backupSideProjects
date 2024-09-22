using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sloth.Core
{
    public class Entity
    {
        public IntPtr Address { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Health { get; set; }
        public byte Team { get; set; }
        public Vector3 HeadCoords = new Vector3();
        public Vector3 Position = new Vector3();
        public Vector3 RelativeXYZ = new Vector3();
        public float DistanceFromMe { get; set; }

        public bool IsAlive() => Health > 0;
    }
}

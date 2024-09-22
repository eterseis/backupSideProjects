using System.Numerics;

namespace ACCheetos
{
    public class Entity
    {
        public IntPtr BaseAddress;
        public Vector3 Head, Feet;
        public Vector2 ViewAngles;
        public float Mag, ViewOffset;
        public int Health, Team, Ammo, Dead;
        public string Name = String.Empty;
    }

    public class ViewMatrix
    {
        public float m11, m12, m13, m14;
        public float m21, m22, m23, m24;
        public float m31, m32, m33, m34;
        public float m41, m42, m43, m44;
    }
}

using Swed32;
using System.Numerics;
using System.Text;

namespace ACCheetos
{
    public class Methods
    {
        private Swed mem;
        private IntPtr moduleBase;

        public Methods()
        {
            mem = new Swed("ac_client");
            moduleBase = mem.GetModuleBase(".exe");

        }

        public Entity ReadEntity(IntPtr entBase)
        {
            var ent = new Entity();

            ent.BaseAddress = entBase;
            ent.Ammo = mem.ReadInt(ent.BaseAddress, (int)Offsets.iAmmo);
            ent.Team = mem.ReadInt(ent.BaseAddress, (int)Offsets.iTeam);

            ent.Feet = mem.ReadVec(ent.BaseAddress, (int)Offsets.vFeet);
            ent.Head = mem.ReadVec(ent.BaseAddress, (int)Offsets.vHead);

            ent.Name = ASCIIEncoding.UTF8.GetString(mem.ReadBytes(ent.BaseAddress, (int)Offsets.sName, 16));
            ent.Health = mem.ReadInt(ent.BaseAddress, (int)Offsets.iHealth);
            return ent;
        }

        public Entity ReadLocalPlayer()
        {
            var localPlayer = ReadEntity(mem.ReadPointer(moduleBase, (int)Offsets.LocalPlayer));
            localPlayer.ViewAngles.X = mem.ReadFloat(localPlayer.BaseAddress, (int)Offsets.vAngles);
            localPlayer.ViewAngles.Y = mem.ReadFloat(localPlayer.BaseAddress, (int)Offsets.vAngles + 0x4);

            return localPlayer;
        }

        public List<Entity> ReadEntities(Entity localPlayer)
        {
            var entities = new List<Entity>();
            var entityList = mem.ReadPointer(moduleBase, (int)Offsets.EntityList);

            for (int i = 0; i < mem.ReadInt(moduleBase, (int)Offsets.iPlayers); i++)
            {
                var currentEntBase = mem.ReadPointer(entityList, i * 0x4);
                var ent = ReadEntity(currentEntBase);
                ent.Mag = CalcMag(localPlayer, ent);

                if (ent.Health > 0)
                    entities.Add(ent);
            }
            return entities;
        }

        public float CalcMag(Entity localPlayer, Entity destEnt)
        {
            return (float)
                Math.Sqrt(Math.Pow(destEnt.Feet.X - localPlayer.Feet.X, 2)
                + Math.Pow(destEnt.Feet.Y - localPlayer.Feet.Y, 2)
                + Math.Pow(destEnt.Feet.Z - localPlayer.Feet.Z, 2));
        }

        public Vector2 CalcAngles(Entity localPlayer, Entity destEnt)
        {
            float x, y;
            var deltaX = destEnt.Head.X - localPlayer.Head.X;
            var deltaY = destEnt.Head.Y - localPlayer.Head.Y;

            x = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI) + 90;

            float deltaZ = destEnt.Head.Z - localPlayer.Head.Z;
            float dist = CalcDist(localPlayer, destEnt);

            y = (float)(Math.Atan2(deltaZ, dist) * 180 / Math.PI);
            return new Vector2(x, y);
        }

        public float CalcDist(Entity localPlayer, Entity destEnt)
        {
            return (float)
                Math.Sqrt(Math.Pow(destEnt.Feet.X - localPlayer.Feet.X, 2)
                + Math.Pow(destEnt.Feet.Y - localPlayer.Feet.Y, 2));
        }

        public void Aim(Entity ent, float x, float y)
        {
            mem.WriteFloat(ent.BaseAddress, (int)Offsets.vAngles, x);
            mem.WriteFloat(ent.BaseAddress, (int)Offsets.vAngles + 0x4, y);
        }

        public ViewMatrix ReadMatrix()
        {
            var viewMatrix = new ViewMatrix();
            var mtx = mem.ReadMatrix(moduleBase + (int)Offsets.ViewMatrix);

            viewMatrix.m11 = mtx[0];
            viewMatrix.m12 = mtx[1];
            viewMatrix.m13 = mtx[2];
            viewMatrix.m14 = mtx[3];

            viewMatrix.m21 = mtx[4];
            viewMatrix.m22 = mtx[5];
            viewMatrix.m23 = mtx[6];
            viewMatrix.m24 = mtx[7];

            viewMatrix.m31 = mtx[8];
            viewMatrix.m32 = mtx[9];
            viewMatrix.m33 = mtx[10];
            viewMatrix.m34 = mtx[11];

            viewMatrix.m41 = mtx[12];
            viewMatrix.m42 = mtx[13];
            viewMatrix.m43 = mtx[14];
            viewMatrix.m44 = mtx[15];
            return viewMatrix;
        }

        public Point WorldToScreen(ViewMatrix mtx, Vector3 pos, int width, int height)
        {
            var twoD = new Point();

            float screenW = (mtx.m14 * pos.X) + (mtx.m24 * pos.Y) + (mtx.m34 * pos.Z) + mtx.m44;

            if (screenW > 0.001f)
            {
                float screenX = (mtx.m11 * pos.X) + (mtx.m21 * pos.Y) + (mtx.m31 * pos.Z) + mtx.m41;
                float screenY = (mtx.m12 * pos.X) + (mtx.m22 * pos.Y) + (mtx.m32 * pos.Z) + mtx.m42;

                float camX = width / 2f;
                float camY = height / 2f;

                float x = camX + (camX * screenX / screenW);
                float y = camY - (camY * screenY / screenW);

                twoD.X = (int)x;
                twoD.Y = (int)y;
                return twoD;
            }
            else
            {
                return new Point(-99, -99);
            }
        }

        public Rectangle CalcRect(Point feet, Point head)
        {
            var rect = new Rectangle();
            rect.X = head.X - (feet.Y - head.Y) / 4;
            rect.Y = head.Y;

            rect.Width = (feet.Y - head.Y) / 2;
            rect.Height = feet.Y - head.Y;

            return rect;
        }
    }
}

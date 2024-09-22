using Sloth.Core.Enums;
using Swed32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloth.Core
{
    public class EntityServices
    {
        private Swed _mem;
        private IntPtr _moduleBase;

        public EntityServices(Swed swed, IntPtr moduleBase)
        {
            _mem = swed;
            _moduleBase = moduleBase;
        }

        public HashSet<Entity> GetEntities(Entity myself)
        {
            var entities = new HashSet<Entity>();

            for (var i = 0; i < _mem.ReadInt(_moduleBase, (int)Offsets.NumOfEntities) - 1; i++)
            {
                var entity = new Entity
                {
                    Address = _mem.ReadPointer(_moduleBase, (int)Offsets.EntityList, i * 0x4)
                };

                UpdateEntityInfo(entity, myself);

                entities.Add(entity);
            }

            return entities.OrderBy(x => x.DistanceFromMe).ToHashSet();
        }

        public void UpdateEntityInfo(Entity entity, Entity myself = null!)
        {
            entity.Health = _mem.ReadInt(entity.Address, (int)Offsets.Health);
            if (!entity.IsAlive())
                return;
            entity.Name = ASCIIEncoding.UTF8.GetString(_mem.ReadBytes(entity.Address, (int)Offsets.Name, 16));
            entity.Team = (byte)_mem.ReadInt(entity.Address, (int)Offsets.bTeam);

            entity.HeadCoords.X = _mem.ReadFloat(entity.Address, (int)Offsets.vHead);
            entity.HeadCoords.Y = _mem.ReadFloat(entity.Address, (int)Offsets.vHead + 0x4);
            entity.HeadCoords.Z = _mem.ReadFloat(entity.Address, (int)Offsets.vHead + 0x8);

            entity.Position.X = _mem.ReadFloat(entity.Address, (int)Offsets.vPos);
            entity.Position.Y = _mem.ReadFloat(entity.Address, (int)Offsets.vPos + 0x4);
            entity.Position.Z = _mem.ReadFloat(entity.Address, (int)Offsets.vPos + 0x8);


            if (myself != null)
            {
                entity.RelativeXYZ.X = (entity.HeadCoords.X - myself.HeadCoords.X);
                entity.RelativeXYZ.Y = (entity.HeadCoords.Y - myself.HeadCoords.Y);
                entity.RelativeXYZ.Z = (entity.HeadCoords.Z - myself.HeadCoords.Z);
                entity.DistanceFromMe = (float)Math.Sqrt(
                    (entity.RelativeXYZ.X * entity.RelativeXYZ.X) + (entity.RelativeXYZ.Y * entity.RelativeXYZ.Y));
            }

        }

        public Entity GetLocalPlayer()
        {
            var localPlayer = new Entity()
            {
                Address = _mem.ReadPointer(_moduleBase, (IntPtr)Offsets.LocalPlayer)
            };
            UpdateEntityInfo(localPlayer);
            return localPlayer;
        }

        public void Aimbot(Entity target, Entity myself)
        {
            if (target is null)
                return;

            float yaw = (float)Math.Atan2(target.RelativeXYZ.Y, target.RelativeXYZ.X);
            yaw = (float)(yaw * 180 / Math.PI) + 90;
            float pitch = (float)Math.Atan2(target.RelativeXYZ.Z, target.DistanceFromMe);
            pitch *= (float)(180 / Math.PI);

            _mem.WriteFloat(myself.Address, (int)Offsets.vAngles, yaw);
            _mem.WriteFloat(myself.Address, (int)Offsets.vAngles + 0x4, pitch);
        }

        public Entity ClosestEnemy(HashSet<Entity> entities, Entity myself)
        => entities.Where(x => x.Team != myself.Team && x.IsAlive()).FirstOrDefault()!;
    }
}

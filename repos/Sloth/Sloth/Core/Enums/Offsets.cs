using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sloth.Core.Enums
{
    public enum Offsets
    {
        vHead = 0x4,
        vPos = 0x28,
        vAngles = 0x34,
        Health = 0xEC,
        AssaultAmmo = 0x140,
        AssaultFireWait = 0x164,
        Name = 0x205,
        bTeam = 0x30C,

        LocalPlayer = 0x17E0A8,
        EntityList = 0x191FCC, // 0x4++ per entity
        NumOfEntities = 0x18AC0C
    }
}

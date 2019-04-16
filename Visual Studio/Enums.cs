using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomItemStats
{
    public enum ItemModType
    {
        WEAPON,
        ARMOR
    }

    public enum ArmourModType
    {
        SPEED,
        HEALTH_BONUS,
        POUCH_BONUS,
        HEAT_PROTECTION,
        COLD_PROTECTION,
        IMPACT_PROTECTION,
        CORRUPTION_PROTECTION,
        WATER_PROOF,
        MANA_USE_MODIFIER,
        COUNT
    }

    public enum WeaponModType
    {
        DAMAGE,
        SPEED,
        REACH,
        COUNT
    }

    public enum WeaponModDamageType
    {
        Physical,
        Ethereal,
        Decay,
        Electric,
        Frost,
        Fire,
        COUNT
    }
}

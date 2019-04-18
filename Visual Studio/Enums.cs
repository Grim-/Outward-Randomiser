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
        DAMAGE_PROTECTION,
        DAMAGE_RESISTANCE,
        STAMINA_USE_MODIFIER,
        WATER_PROOF,
        DURABILITY,
        WEIGHT,
        MANA_USE_MODIFIER,
        COUNT
    }

    public enum WeaponModType
    {
        DAMAGE,
        IMPACT,
        DURABILITY,
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


    public enum RollQuality
    {
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY,
        CURSED,
        COUNT
    }
}
